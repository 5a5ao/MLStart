using System.Windows;

namespace Program;

/// <summary>
/// Логика взаимодействия для RegistrationWindow .xaml
/// </summary>
public partial class RegistrationWindow : Window
{
    #region .ctor

    public RegistrationWindow()
    {
        InitializeComponent();
    }

    #endregion

    #region Methods

    private void registration(object sender, RoutedEventArgs e)
    {
        MainWindow MainWindow = new MainWindow();
        MainWindow.Show();
        this.Close();
    }

    #endregion
}
