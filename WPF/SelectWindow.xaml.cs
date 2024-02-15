using System.Windows;

namespace Program;

/// <summary>
/// Логика взаимодействия для SelectWindow.xaml
/// </summary>
public partial class SelectWindow : Window
{
    #region .ctor

    public SelectWindow()
    {
        InitializeComponent();
    }

    #endregion

    #region Methods

    private void registration(object sender, RoutedEventArgs e)
    {
        // Обработка нажатия кнопки "registration"
        RegistrationWindow registrationWindow = new RegistrationWindow();
        registrationWindow.Show();
        this.Close();
    }

    private void authorization(object sender, RoutedEventArgs e)
    {
        // Обработка нажатия кнопки "authorization"
        AuthorizationWindow authorizationWindow = new AuthorizationWindow();
        authorizationWindow.Show();
        this.Close();
    }

    #endregion
}