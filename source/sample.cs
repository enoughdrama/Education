<!-- MainWindow.xaml -->
<Window x:Class="YourNamespace.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="Главное окно" Height="400" Width="600"
        WindowStartupLocation="CenterScreen">
  <Window.Resources>
    <!-- Стиль для кнопок главного окна -->
    <Style x:Key="MainButtonStyle" TargetType="Button">
      <Setter Property="Width" Value="220"/>
      <Setter Property="Height" Value="40"/>
      <Setter Property="Margin" Value="10"/>
      <Setter Property="FontSize" Value="16"/>
      <Setter Property="Foreground" Value="White"/>
      <Setter Property="Background" Value="#FF3B5998"/>
      <Setter Property="BorderBrush" Value="Transparent"/>
      <Setter Property="Cursor" Value="Hand"/>
      <Setter Property="Template">
        <Setter.Value>
          <ControlTemplate TargetType="Button">
            <Border Background="{TemplateBinding Background}" 
                    CornerRadius="5" 
                    BorderBrush="{TemplateBinding BorderBrush}">
              <ContentPresenter HorizontalAlignment="Center" VerticalAlignment="Center"/>
            </Border>
          </ControlTemplate>
        </Setter.Value>
      </Setter>
    </Style>
  </Window.Resources>
  <Grid>
    <!-- Градиентный фон -->
    <Grid.Background>
      <LinearGradientBrush StartPoint="0,0" EndPoint="0,1">
        <GradientStop Color="#FF8EC5FC" Offset="0"/>
        <GradientStop Color="#FFE0C3FC" Offset="1"/>
      </LinearGradientBrush>
    </Grid.Background>
    <StackPanel VerticalAlignment="Center" HorizontalAlignment="Center">
      <Button Content="Войти" Style="{StaticResource MainButtonStyle}" Click="LoginButton_Click"/>
      <Button Content="Зарегистрироваться" Style="{StaticResource MainButtonStyle}" Click="RegisterButton_Click"/>
    </StackPanel>
  </Grid>
</Window>

<!-- LoginWindow.xaml -->
<Window x:Class="YourNamespace.LoginWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="Авторизация" Height="350" Width="450"
        WindowStartupLocation="CenterScreen">
  <Window.Resources>
    <!-- Общие стили для элементов формы -->
    <Style TargetType="TextBlock">
      <Setter Property="FontSize" Value="14"/>
      <Setter Property="Margin" Value="0,5,0,5"/>
    </Style>
    <Style TargetType="TextBox">
      <Setter Property="Height" Value="30"/>
      <Setter Property="FontSize" Value="14"/>
      <Setter Property="Margin" Value="0,0,0,10"/>
      <Setter Property="Padding" Value="5"/>
    </Style>
    <Style TargetType="PasswordBox">
      <Setter Property="Height" Value="30"/>
      <Setter Property="FontSize" Value="14"/>
      <Setter Property="Margin" Value="0,0,0,10"/>
      <Setter Property="Padding" Value="5"/>
    </Style>
    <!-- Стиль для кнопки формы -->
    <Style x:Key="FormButtonStyle" TargetType="Button">
      <Setter Property="Height" Value="35"/>
      <Setter Property="FontSize" Value="14"/>
      <Setter Property="Background" Value="#FF3B5998"/>
      <Setter Property="Foreground" Value="White"/>
      <Setter Property="BorderBrush" Value="Transparent"/>
      <Setter Property="Cursor" Value="Hand"/>
      <Setter Property="Template">
        <Setter.Value>
          <ControlTemplate TargetType="Button">
            <Border Background="{TemplateBinding Background}" 
                    CornerRadius="5" 
                    BorderBrush="{TemplateBinding BorderBrush}">
              <ContentPresenter HorizontalAlignment="Center" VerticalAlignment="Center"/>
            </Border>
          </ControlTemplate>
        </Setter.Value>
      </Setter>
    </Style>
    <!-- Эффект тени для обрамления формы -->
    <DropShadowEffect x:Key="DropShadowEffect" Color="Black" BlurRadius="10" ShadowDepth="2" Opacity="0.5"/>
  </Window.Resources>
  <Grid Background="WhiteSmoke">
    <Border CornerRadius="10" Background="White" Padding="20" 
            HorizontalAlignment="Center" VerticalAlignment="Center" 
            Effect="{StaticResource DropShadowEffect}">
      <StackPanel>
        <TextBlock Text="Имя пользователя:"/>
        <TextBox x:Name="UsernameTextBox"/>
        <TextBlock Text="Пароль:"/>
        <PasswordBox x:Name="PasswordBox"/>
        <Button Content="Войти" Style="{StaticResource FormButtonStyle}" Click="LoginButton_Click" HorizontalAlignment="Center"/>
      </StackPanel>
    </Border>
  </Grid>
</Window>

<!-- RegistrationWindow.xaml -->
<Window x:Class="YourNamespace.RegistrationWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="Регистрация" Height="400" Width="450"
        WindowStartupLocation="CenterScreen">
  <Window.Resources>
    <!-- Общие стили для элементов формы -->
    <Style TargetType="TextBlock">
      <Setter Property="FontSize" Value="14"/>
      <Setter Property="Margin" Value="0,5,0,5"/>
    </Style>
    <Style TargetType="TextBox">
      <Setter Property="Height" Value="30"/>
      <Setter Property="FontSize" Value="14"/>
      <Setter Property="Margin" Value="0,0,0,10"/>
      <Setter Property="Padding" Value="5"/>
    </Style>
    <Style TargetType="PasswordBox">
      <Setter Property="Height" Value="30"/>
      <Setter Property="FontSize" Value="14"/>
      <Setter Property="Margin" Value="0,0,0,10"/>
      <Setter Property="Padding" Value="5"/>
    </Style>
    <!-- Стиль для кнопки формы -->
    <Style x:Key="FormButtonStyle" TargetType="Button">
      <Setter Property="Height" Value="35"/>
      <Setter Property="FontSize" Value="14"/>
      <Setter Property="Background" Value="#FF3B5998"/>
      <Setter Property="Foreground" Value="White"/>
      <Setter Property="BorderBrush" Value="Transparent"/>
      <Setter Property="Cursor" Value="Hand"/>
      <Setter Property="Template">
        <Setter.Value>
          <ControlTemplate TargetType="Button">
            <Border Background="{TemplateBinding Background}" 
                    CornerRadius="5" 
                    BorderBrush="{TemplateBinding BorderBrush}">
              <ContentPresenter HorizontalAlignment="Center" VerticalAlignment="Center"/>
            </Border>
          </ControlTemplate>
        </Setter.Value>
      </Setter>
    </Style>
    <!-- Эффект тени для обрамления формы -->
    <DropShadowEffect x:Key="DropShadowEffect" Color="Black" BlurRadius="10" ShadowDepth="2" Opacity="0.5"/>
  </Window.Resources>
  <Grid Background="WhiteSmoke">
    <Border CornerRadius="10" Background="White" Padding="20" 
            HorizontalAlignment="Center" VerticalAlignment="Center" 
            Effect="{StaticResource DropShadowEffect}">
      <StackPanel>
        <TextBlock Text="Имя пользователя:"/>
        <TextBox x:Name="UsernameTextBox"/>
        <TextBlock Text="Пароль:"/>
        <PasswordBox x:Name="PasswordBox"/>
        <TextBlock Text="Подтвердите пароль:"/>
        <PasswordBox x:Name="ConfirmPasswordBox"/>
        <Button Content="Зарегистрироваться" Style="{StaticResource FormButtonStyle}" Click="RegisterButton_Click" HorizontalAlignment="Center"/>
      </StackPanel>
    </Border>
  </Grid>
</Window>
