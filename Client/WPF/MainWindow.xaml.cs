using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Program;

public partial class MainWindow : Window
{
    #region Data

    private StringBuilder outputText = new StringBuilder();
    private TcpClient client = new TcpClient();

    #endregion

    #region .ctor

    public MainWindow()
    {
        InitializeComponent();
        ConnectToServer();


        //// Запускаем цикл while в другом потоке для избежания блокировки пользовательского интерфейса
        //System.Threading.Tasks.Task.Run(() =>
        //{
        //    while (true)
        //    {
        //        Thread.Sleep(100);
        //        Storyteller storyteller = new Storyteller();
        //        storyteller.TextUpdated += AppendToOutput;
        //        storyteller.TellStory();

        //        Thread.Sleep(int.Parse(ConfigManager.GetConfig("Thread")));

        //        Dispatcher.Invoke(() =>
        //        {
        //            outputText.Clear();
        //            textBlock.Text = "";
        //        });
        //    }
        //});

    }

    #endregion

    #region Methods

    private async void ConnectToServer()
    {
        try
        {
            await client.ConnectAsync("127.0.0.1", 8888);
            ReceiveDataFromServer();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error connecting to server: " + ex.Message);
        }
    }

    private async void ReceiveDataFromServer()
    {
        try
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];

            while (true)
            {
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                // Выводим полученные данные в интерфейс
                Dispatcher.Invoke(() =>
                {
                    textBlock.Text += receivedData + Environment.NewLine;
                });
                

                Dispatcher.Invoke(() =>
                {
                    textBlock.Text = receivedData + Environment.NewLine;
                });
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error receiving data from server: " + ex.Message);
        }
    }



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