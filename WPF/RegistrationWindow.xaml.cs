using System.Windows;
using System.Windows.Controls;

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
        bool chekedUSer = BDInteraction.UserCheck(loginTextBox.Text);
        if (!chekedUSer)
        {
            BDInteraction.AddUser(loginTextBox.Text, passwordTextBox.Text);
            MessageBox.Show("Пользователь успешно зарегистрирован.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            MainWindow MainWindow = new MainWindow();
            MainWindow.Show();
            this.Close();
        }
        else {
            MessageBox.Show("Пользователь с таким именем уже существует.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        };
    }

    #endregion
}
