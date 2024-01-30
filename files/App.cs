using Serilog;
using Serilog.Sinks.File;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace App
{
    internal class App
    {

        static void Main(string[] args)
        {
            ConfigManager ConfigMangaer = new ConfigManager();
            ConfigMangaer.UpdateSetting("N", "7");

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

            var customer = new Customer("Коротышка");
            var firstSeller = new Seller("Незнайка");
            var secondSeller = new Seller("Козлик");
            var exchenger = new Seller("Мига");
            var allCustomers = new Seller("Покупатели");

            var exchengerStorage = new Storage();
            var sellerStorage = new Storage();

            string currency = CurrencyName.Денежки.ToString();
            Random rand = new Random();
            CurrencyName randomCurrency = (CurrencyName)rand.Next(Enum.GetValues(typeof(CurrencyName)).Length);
            string currencyCustomer = randomCurrency.ToString();

            var nameStock = new string[]{"ПАО «Газпром»","Общество гигантских растений","Яндекс","ПАО «Сбербанк»","Тинькофф Банк","Tesla","Ростелеком","Diamond Dogs","Kojima Productions"};
            int randomIndex = rand.Next(nameStock.Length);
            Stock stock = new Stock(nameStock[randomIndex],sellerStorage);

            CurrencyType randomCurrencyType = (CurrencyType)rand.Next(Enum.GetValues(typeof(CurrencyType)).Length);
            string currencyType = randomCurrencyType.ToString();

            customer.Buy(currency,stock,10,secondSeller,sellerStorage);
            firstSeller.Sell(currencyCustomer,stock,1000,allCustomers,sellerStorage);
            exchenger.Exchenge(currencyType,currencyCustomer,sellerStorage,exchengerStorage);

            


            ///

            // /// 1 задание
            // var k = new int[8];

            // for (int i = 0; i < k.Length; i++)
            // {
            //     k[i] = 2 * (i + 2) + 1;
            // }

            // /// 2 задание
            // var x = new double[13];

            // for (int i = 0; i < x.Length; i++)
            // {
            //     x[i] = -12.0 + rand.NextDouble() * (15.0 + 12.0);
            // }

            // var x1 = new double[13];
            // for (int i = 0; i < x.Length; i++)
            // {
            //     x1[i] = Math.Round(-12.0 + rand.NextDouble() * (15.0 + 12.0), 1);
            // }


            /// 3 задание
            var k_ = new double[8, 13];

            for (int i = 0; i < k_.GetLength(0); i++)
            {
                for (int j = 0; j < k_.GetLength(1); j++)
                {
                    double x_ = k_[i, j];
                    if (k_[i, j] == 9)
                    {
                        k_[i, j] = Math.Sin(Math.Sin(Math.Pow(x_ / (x_ + 1 / 2), x_)));
                    }
                    else if (k_[i, j] == 5 || k_[i, j] == 7 || k_[i, j] == 11 || k_[i, j] == 15)
                    {
                        k_[i, j] = Math.Pow((0.5 / (Math.Tan(2 * x_) + (2 / 3))), Math.Pow(Math.Pow(x_, 1 / 3), 1 / 3));
                    }
                    else
                    {
                        k_[i, j] = Math.Tan(Math.Pow(((Math.Pow(Math.E, (1 - x_) / Math.PI) / 3) / 4), 3));
                    }

                }
            }

            /// 4 задание
            int N = int.Parse(ConfigurationManager.AppSettings.Get("N"));
            int L = int.Parse(ConfigurationManager.AppSettings.Get("L"));


            /// 5 задание

            double kMin = 0;
            int jAvg = L % 13;
            int iMin = N % 8;
            double sum = 0;
            int count = 0;

            for (int j = 0; j < k_.GetLength(1); j++)
            {
                if (k_[iMin, j] < kMin)
                {
                    kMin = k_[iMin, j];
                }
            }

            for (int i = 0; i < k_.GetLength(0); i++)
            {
                sum = k_[i, jAvg] + sum;
                count++;
            }
            double kAvg = sum / count;
            string formattedValue = (kMin + kAvg).ToString("F4");


            Console.ReadLine();

        }
    }

}
