<Window x:Class="YourNamespace.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="Главное окно" Height="350" Width="525">
    <Grid>
        <StackPanel HorizontalAlignment="Center" VerticalAlignment="Center">
            <Button Content="Войти" Width="200" Margin="5" Click="LoginButton_Click"/>
            <Button Content="Зарегистрироваться" Width="200" Margin="5" Click="RegisterButton_Click"/>
        </StackPanel>
    </Grid>
</Window>

using System.Windows;

namespace YourNamespace
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Обработчик нажатия кнопки "Войти"
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.ShowDialog();
        }

        // Обработчик нажатия кнопки "Зарегистрироваться"
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.ShowDialog();
        }
    }
}

<!-- LoginWindow.xaml -->
<Window x:Class="YourNamespace.LoginWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="Авторизация" Height="300" Width="400">
  <Grid Margin="10">
    <StackPanel>
      <TextBlock Text="Имя пользователя:" Margin="0,0,0,5"/>
      <TextBox x:Name="UsernameTextBox" Margin="0,0,0,10" />
      
      <TextBlock Text="Пароль:" Margin="0,0,0,5"/>
      <PasswordBox x:Name="PasswordBox" Margin="0,0,0,10"/>
      
      <Button Content="Войти" Click="LoginButton_Click" Width="100" HorizontalAlignment="Center"/>
    </StackPanel>
  </Grid>
</Window>

// User.cs
using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Username { get; set; }
    
    // Хранится в виде Base64 строки
    [Required]
    public string PasswordHash { get; set; }
    
    // Соль также в формате Base64
    [Required]
    public string Salt { get; set; }
}

// AppDbContext.cs (для EF6)
using System.Data.Entity;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    // Название строки подключения должно соответствовать тому, что прописано в App.config/Web.config
    public AppDbContext() : base("name=DefaultConnection")
    {
    }
}

// PasswordHelper.cs
using System;
using System.Security.Cryptography;

public static class PasswordHelper
{
    /// <summary>
    /// Создает хеш пароля и генерирует соль.
    /// </summary>
    /// <param name="password">Исходный пароль.</param>
    /// <param name="passwordHash">Результирующий хеш в формате Base64.</param>
    /// <param name="salt">Соль в формате Base64.</param>
    public static void CreatePasswordHash(string password, out string passwordHash, out string salt)
    {
        // Генерируем 16-байтовую соль
        byte[] saltBytes = new byte[16];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(saltBytes);
        }
        salt = Convert.ToBase64String(saltBytes);

        // Используем Rfc2898DeriveBytes для генерации хеша с 10 000 итерациями
        using (var deriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 10000))
        {
            byte[] hashBytes = deriveBytes.GetBytes(20); // 20 байт = 160 бит
            passwordHash = Convert.ToBase64String(hashBytes);
        }
    }

    /// <summary>
    /// Проверяет введенный пароль, сравнивая его хеш с сохраненным.
    /// </summary>
    /// <param name="enteredPassword">Пароль, введенный пользователем.</param>
    /// <param name="storedHash">Сохраненный хеш пароля.</param>
    /// <param name="storedSalt">Сохраненная соль.</param>
    /// <returns>True, если пароли совпадают; иначе false.</returns>
    public static bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
    {
        byte[] saltBytes = Convert.FromBase64String(storedSalt);
        using (var deriveBytes = new Rfc2898DeriveBytes(enteredPassword, saltBytes, 10000))
        {
            byte[] enteredHashBytes = deriveBytes.GetBytes(20);
            string enteredHash = Convert.ToBase64String(enteredHashBytes);
            return enteredHash == storedHash;
        }
    }
}

<!-- LoginWindow.xaml -->
<Window x:Class="YourNamespace.LoginWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="Авторизация" Height="300" Width="400">
  <Grid Margin="10">
    <StackPanel>
      <TextBlock Text="Имя пользователя:" Margin="0,0,0,5"/>
      <TextBox x:Name="UsernameTextBox" Margin="0,0,0,10" />
      
      <TextBlock Text="Пароль:" Margin="0,0,0,5"/>
      <PasswordBox x:Name="PasswordBox" Margin="0,0,0,10"/>
      
      <Button Content="Войти" Click="LoginButton_Click" Width="100" HorizontalAlignment="Center"/>
    </StackPanel>
  </Grid>
</Window>

// LoginWindow.xaml.cs
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Data.Entity; // Для EF6

namespace YourNamespace
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text.Trim();
            string password = PasswordBox.Password;

            // Проверка заполнения обязательных полей
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите имя пользователя и пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (var context = new AppDbContext())
                {
                    // Поиск пользователя в базе данных
                    var user = await context.Users.FirstOrDefaultAsync(u => u.Username == username);
                    if (user == null)
                    {
                        MessageBox.Show("Пользователь не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    // Проверка корректности пароля
                    bool isPasswordValid = PasswordHelper.VerifyPassword(password, user.PasswordHash, user.Salt);
                    if (!isPasswordValid)
                    {
                        MessageBox.Show("Неверный пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    // Авторизация успешна
                    MessageBox.Show("Вход выполнен успешно.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    // Здесь можно открыть основное окно приложения и закрыть окно авторизации
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при авторизации: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

<!-- RegistrationWindow.xaml -->
<Window x:Class="YourNamespace.RegistrationWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="Регистрация" Height="350" Width="400">
  <Grid Margin="10">
    <StackPanel>
      <TextBlock Text="Имя пользователя:" Margin="0,0,0,5"/>
      <TextBox x:Name="UsernameTextBox" Margin="0,0,0,10" />
      
      <TextBlock Text="Пароль:" Margin="0,0,0,5"/>
      <PasswordBox x:Name="PasswordBox" Margin="0,0,0,10"/>
      
      <TextBlock Text="Подтвердите пароль:" Margin="0,0,0,5"/>
      <PasswordBox x:Name="ConfirmPasswordBox" Margin="0,0,0,10"/>
      
      <Button Content="Зарегистрироваться" Click="RegisterButton_Click" Width="150" HorizontalAlignment="Center"/>
    </StackPanel>
  </Grid>
</Window>

// RegistrationWindow.xaml.cs
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Data.Entity; // Для EF6

namespace YourNamespace
{
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text.Trim();
            string password = PasswordBox.Password;
            string confirmPassword = ConfirmPasswordBox.Password;

            // Проверка заполнения всех полей
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверка совпадения паролей
            if (password != confirmPassword)
            {
                MessageBox.Show("Пароли не совпадают.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (var context = new AppDbContext())
                {
                    // Проверка на существование пользователя с таким именем
                    var existingUser = await context.Users.FirstOrDefaultAsync(u => u.Username == username);
                    if (existingUser != null)
                    {
                        MessageBox.Show("Пользователь с таким именем уже существует.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    // Хеширование пароля с генерацией соли
                    PasswordHelper.CreatePasswordHash(password, out string passwordHash, out string salt);

                    var newUser = new User
                    {
                        Username = username,
                        PasswordHash = passwordHash,
                        Salt = salt
                    };

                    context.Users.Add(newUser);
                    await context.SaveChangesAsync();

                    MessageBox.Show("Регистрация успешна.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    // После успешной регистрации можно закрыть окно регистрации и открыть окно авторизации/основное окно приложения
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при регистрации: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

<!-- App.config (пример строки подключения для EF6) -->
<configuration>
  <connectionStrings>
    <add name="DefaultConnection"
         connectionString="Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MyDatabase;Integrated Security=True;MultipleActiveResultSets=True"
         providerName="System.Data.SqlClient" />
  </connectionStrings>
  <!-- Остальные настройки -->
</configuration>
