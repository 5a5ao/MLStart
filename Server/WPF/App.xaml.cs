using System.Windows;


namespace Server;

/// <summary>
/// Логика взаимодействия для App.xaml
/// </summary>
public partial class App : Application
{

    #region Start

    [STAThread]
    static void Main(string[] args)
    {

        var app = new App();
        app.Run();
        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();

    }

    #endregion

    #region Methods

    #endregion

}


