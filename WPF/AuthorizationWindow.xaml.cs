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
        MainWindow MainWindow = new MainWindow();
        MainWindow.Show();
        this.Close();
    }

    #endregion
}
