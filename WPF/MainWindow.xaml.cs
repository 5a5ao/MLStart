using Program;
using System;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Program
{
    public partial class MainWindow : Window
    {
        private StringBuilder outputText = new StringBuilder();

        public MainWindow()
        {
            InitializeComponent();

            // Запускаем ваш цикл while в другом потоке для избежания блокировки пользовательского интерфейса
            System.Threading.Tasks.Task.Run(() =>
            {
                while (true)
                {
                    var customer = new Customer("Коротышка");
                    var firstSeller = new Seller("Незнайка");
                    var secondSeller = new Seller("Козлик");
                    var exchanger = new Seller("Мига");
                    var allCustomers = new Seller("Покупателям");

                    var exchangerStorage = new Storage();
                    var sellerStorage = new Storage();

                    string currency = CurrencyName.Money.ToString();
                    Random rand = new Random();
                    CurrencyName randomCurrency = (CurrencyName)rand.Next(Enum.GetValues(typeof(CurrencyName)).Length);
                    string currencyCustomer = randomCurrency.ToString();

                    var nameStock = new string[] { "ПАО «Газпром»", "Общество гигантских растений", "Яндекс", "ПАО «Сбербанк»", "Тинькофф Банк", "Tesla", "Ростелеком", "Diamond Dogs", "Kojima Productions" };
                    int randomIndex = rand.Next(nameStock.Length);
                    Stock stock = new Stock(nameStock[randomIndex], sellerStorage);

                    customer.Buy(currency, stock, rand.Next(), secondSeller, sellerStorage);
                    countCustomer();

                    firstSeller.Sell(currencyCustomer, stock, rand.Next(), allCustomers, sellerStorage);
                    exchanger.Exchenge(currencyCustomer, sellerStorage, exchangerStorage);

                    AppendToOutput($"Текущая ставка акций {stock.name} составляет {stock.profitability}");
                    outputText.Clear();

                    Thread.Sleep(int.Parse(ConfigManager.GetConfig("Thread")));
                }
            });
        }

        // Метод для добавления текста в TextBlock
        private void AppendToOutput(string text)
        {
            outputText.AppendLine(text);
            // Обновляем TextBlock в UI потоке
            Application.Current.Dispatcher.Invoke(() => { textBlock.Text = outputText.ToString(); });
        }
        void countCustomer()
        {
            Random rand = new Random();
            var x = new double[13];
            AppendToOutput($"За {x.Length} дней количество покупателей изменялось в процентном соотношении");

            var x1 = new double[13];
            for (int i = 0; i < x.Length; i++)
            {
                x1[i] = Math.Round(-12.0 + rand.NextDouble() * (15.0 + 12.0), 1);
                AppendToOutput($"В {i + 1} день {x1[i]}%");
            }

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
    }
}

