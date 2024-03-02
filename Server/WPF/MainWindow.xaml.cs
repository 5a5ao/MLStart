using System.Collections.ObjectModel;
using System.Net;
using System.Net.Sockets;
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
        while (true)
        {
            try
            {
                TcpClient client = await server.AcceptTcpClientAsync();
                // Get client IP and Port
                IPEndPoint endPoint = (IPEndPoint)client.Client.RemoteEndPoint;
                ClientInfo clientInfo = new ClientInfo { IPAddress = endPoint.Address.ToString(), Port = endPoint.Port };
                clients.Add(clientInfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error accepting clients: " + ex.Message);
            }
        }
    }
}

public class ClientInfo
{
    public string IPAddress { get; set; }
    public int Port { get; set; }
}

