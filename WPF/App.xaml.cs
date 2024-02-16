using System.Windows;


namespace Program;

/// <summary>
/// Логика взаимодействия для App.xaml
/// </summary>
public partial class App : Application
{

    #region Start

    [STAThread]
    static void Main(string[] args)
    {
        ConfigManager configManager = new ConfigManager();
        var app = new App();
        app.Run();
    }

    #endregion

    #region Methods

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // Создаем и отображаем новое окно
        var SelectScreen = new SelectWindow();
        SelectScreen.Show();

    }

    #endregion

}


