using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Program;

public partial class MainWindow : Window
{
    #region Data

    private StringBuilder outputText = new StringBuilder();

    #endregion

    #region .ctor

    public MainWindow()
    {
        InitializeComponent();

      
        // Запускаем цикл while в другом потоке для избежания блокировки пользовательского интерфейса
        System.Threading.Tasks.Task.Run(() =>
        {
            while (true)
            {
                Thread.Sleep(100);
                Storyteller storyteller = new Storyteller();
                storyteller.TextUpdated += AppendToOutput;
                storyteller.TellStory();

                Thread.Sleep(int.Parse(ConfigManager.GetConfig("Thread")));

                Dispatcher.Invoke(() =>
                {
                    outputText.Clear();
                    textBlock.Text = "";
                });
            }
        });

    }

    #endregion

    #region Methods


    // Метод для добавления текста в TextBlock
    private void AppendToOutput(object sender, string newText)
    {
        Dispatcher.Invoke(() =>
        {
            outputText.AppendLine(newText);

            Dispatcher.Invoke(() =>
            {
                textBlock.Text = outputText.ToString();
            });
        });
    }


    private void menuClick(object sender, RoutedEventArgs e)
    {
        Button button = (Button)sender;
        ContextMenu contextMenu = button.ContextMenu;
        contextMenu.PlacementTarget = button;
        contextMenu.IsOpen = true;
    }

    private void pauseClick(object sender, RoutedEventArgs e)
    {
        Button pauseButton = (Button)sender;
        Button playButton = FindButtonByName("playButton");

        if (pauseButton != null && playButton != null)
        {
            pauseButton.IsEnabled = false;
            pauseButton.Opacity = 0.5;

            playButton.IsEnabled = true;
            playButton.Opacity = 1.0;
        }

    }

    private void playClick(object sender, RoutedEventArgs e)
    {
        Button playButton = (Button)sender;
        Button pauseButton = FindButtonByName("pauseButton");

        if (playButton != null && playButton != null)
        {
            playButton.IsEnabled = false;
            playButton.Opacity = 0.5;

            pauseButton.IsEnabled = true;
            pauseButton.Opacity = 1.0;
        }

    }
    private Button FindButtonByName(string name)
    {
        var button = (Button)FindName(name);
        return button;
    }

    #endregion
}

