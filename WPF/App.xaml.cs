using Program;
using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MLStart.WPF
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {

        static void Main(string[] args)
        {

            ConfigManager configManager = new ConfigManager();

            Log.Logger = new LoggerConfiguration()
            .WriteTo.File("TraceLog.txt").MinimumLevel.Verbose()
            .WriteTo.File("DebugLog.txt").MinimumLevel.Debug()
            .WriteTo.File("InfoLog.txt").MinimumLevel.Information()
            .WriteTo.File("WarnLog.txt").MinimumLevel.Warning()
            .WriteTo.File("ErrorLog.txt").MinimumLevel.Error()
            .WriteTo.File("FatalLog.txt").MinimumLevel.Fatal()
            .CreateLogger();

            Log.CloseAndFlush();

            ///Этап 2 
            //  while (true)
            //  {
            var customer = new Customer("Коротышка");
            var firstSeller = new Seller("Незнайка");
            var secondSeller = new Seller("Козлик");
            var exchenger = new Seller("Мига");
            var allCustomers = new Seller("Покупателям");

            var exchengerStorage = new Storage();
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
            exchenger.Exchenge(currencyCustomer, sellerStorage, exchengerStorage);
            Console.WriteLine($"Текущая ставка акций {stock.name} составляет {stock.profitability}");
            Console.WriteLine();
            Thread.Sleep(int.Parse(ConfigManager.GetConfig("Thread")));


            // }


            void countCustomer()
            {
                Random rand = new Random();
                var x = new double[13];
                Console.WriteLine($"За {x.Length} дней количество покупателей изменялось в процентном соотношении");

                var x1 = new double[13];
                for (int i = 0; i < x.Length; i++)
                {
                    x1[i] = Math.Round(-12.0 + rand.NextDouble() * (15.0 + 12.0), 1);
                    Console.WriteLine($"В {i + 1} день {x1[i]}%");
                }
            }
        }
    }
}

