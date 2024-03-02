using System.Collections.ObjectModel;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;

namespace Server;

public partial class MainWindow : Window
{
    private TcpListener server;
    private ObservableCollection<ClientInfo> clients = new ObservableCollection<ClientInfo>();

    public MainWindow()
    {
        InitializeComponent();
        ClientListView.ItemsSource = clients;
    }

    private async void StartButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            // Start the server
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            int port = 8888;
            server = new TcpListener(ipAddress, port);
            server.Start();
            StartButton.IsEnabled = false;
            StopButton.IsEnabled = true;
            await AcceptClientsAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error starting the server: " + ex.Message);
        }
    }

    private void StopButton_Click(object sender, RoutedEventArgs e)
    {
        // Stop the server
        server.Stop();
        StartButton.IsEnabled = true;
        StopButton.IsEnabled = false;
    }

    private async Task AcceptClientsAsync()
    {
        while (server != null && server.Server.IsBound)
        {
            try
            {
                if (server.Pending())
                {
                    TcpClient client = await server.AcceptTcpClientAsync();
                    // Получаем IP и порт клиента
                    IPEndPoint endPoint = (IPEndPoint)client.Client.RemoteEndPoint;
                    ClientInfo clientInfo = new ClientInfo { IPAddress = endPoint.Address.ToString(), Port = endPoint.Port };
                    clients.Add(clientInfo);

                    // Ожидаем завершения обработки связи с клиентом
                    await Task.Run(() => HandleClientCommunication(client));
                }
                else
                {
                    await Task.Delay(1000); // Ждем некоторое время перед следующей попыткой
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при принятии клиентов: " + ex.Message);
            }
        }
    }

    private async Task HandleClientCommunication(TcpClient client)
    {
        NetworkStream stream = client.GetStream();

        while (true) // Бесконечный цикл для обновления данных
        {
            // Создаем экземпляр Storyteller
            Storyteller storyteller = new Storyteller();

            StringBuilder stringBuilder = new StringBuilder();

            // Подписываемся на событие обновления текста
            storyteller.TextUpdated += (sender, newText) =>
            {
                // Добавляем новый текст к общему тексту
                stringBuilder.Append(newText);
            };

            // Запускаем метод TellStoryAsync, который генерирует и отправляет строки клиенту
            await storyteller.TellStoryAsync(stream);

            try
            {
                // Конвертируем общий текст в массив байтов и отправляем клиенту
                byte[] bytesToSend = Encoding.UTF8.GetBytes(stringBuilder.ToString());
                await stream.WriteAsync(bytesToSend, 0, bytesToSend.Length);
                stream.Flush(); // Отправляем данные из буфера
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при отправке данных клиенту: " + ex.Message);
            }

            // Очищаем StringBuilder после отправки всего текста
            stringBuilder.Clear();

            await Task.Delay(1000); // Пауза перед следующей итерацией обновления данных
        }
    }

}

public class ClientInfo
{
    public string IPAddress { get; set; }
    public int Port { get; set; }
}