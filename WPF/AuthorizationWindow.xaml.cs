using System.Windows;

namespace Program;

/// <summary>
/// Логика взаимодействия для AuthorizationWindow.xaml
/// </summary>
public partial class AuthorizationWindow : Window
{
    #region .ctor

    public AuthorizationWindow()
    {
        InitializeComponent();

    }

    #endregion

    #region Methods

    private void authorization(object sender, RoutedEventArgs e)
    {

        bool chekedUser = BDInteraction.UserCheckAuthorization(loginTextBox.Text, passwordTextBox.Text);

        if (chekedUser)
        {
            MainWindow MainWindow = new MainWindow();
            MainWindow.Show();
            this.Close();
        }
        else
        {
            MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        };
    }


    #endregion
}
