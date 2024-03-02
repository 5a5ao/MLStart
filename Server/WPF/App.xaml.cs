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

        var mainWindow = new MainWindow();
        mainWindow.Show();

        app.Run();

    }

    #endregion

    #region Methods

    #endregion

}


