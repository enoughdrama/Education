using System;
using System.Windows;

namespace AppAuthorization
{
    public partial class RegistrationWindow : Window
    {
        private readonly RegistrationService _registrationService;

        public RegistrationWindow()
        {
            InitializeComponent();
            _registrationService = new RegistrationService();
        }

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text.Trim();
            string password = PasswordBox.Password;
            string confirmPassword = ConfirmPasswordBox.Password;

            try
            {
                var result = await _registrationService.RegisterUserAsync(username, password, confirmPassword);

                if (result.Success)
                {
                    MessageBox.Show("Регистрация успешна.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show(result.ErrorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при регистрации: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}