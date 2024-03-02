using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    public class Storyteller
    {
        public event EventHandler<string> TextUpdated;
        private StringBuilder fullTextBuilder = new StringBuilder();

        public async Task TellStoryAsync(NetworkStream stream)
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
            await OnTextUpdatedAsync(stream, customer.outputBuy);
            await CountCustomerAsync(stream);

            firstSeller.Sell(currencyCustomer, stock, rand.Next(), allCustomers, sellerStorage);
            await OnTextUpdatedAsync(stream, firstSeller.outputSell);
            exchanger.Exchenge(currencyCustomer, sellerStorage, exchangerStorage);
            await OnTextUpdatedAsync(stream, exchanger.outputExchenge);

            await OnTextUpdatedAsync(stream, $"Текущая ставка акций {stock.name} составляет {stock.profitability}");
        }

        private async Task CountCustomerAsync(NetworkStream stream)
        {
            Random rand = new Random();
            var x = new double[13];
            await OnTextUpdatedAsync(stream, $"За {x.Length} дней количество покупателей изменялось в процентном соотношении");

            var x1 = new double[13];
            for (int i = 0; i < x.Length; i++)
            {
                x1[i] = Math.Round(-12.0 + rand.NextDouble() * (15.0 + 12.0), 1);
                await OnTextUpdatedAsync(stream, $"В {i + 1} день {x1[i]}%");
            }
        }

        private async Task OnTextUpdatedAsync(NetworkStream stream, string newText)
        {
            try
            {
                if (!string.IsNullOrEmpty(newText))
                {
                    fullTextBuilder.AppendLine(newText);
                }

                string fullText = fullTextBuilder.ToString();

                byte[] bytesToSend = Encoding.UTF8.GetBytes(fullText);
                await stream.WriteAsync(bytesToSend, 0, bytesToSend.Length);

                // Добавляем задержку после отправки данных
                await Task.Delay(100);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при отправке данных клиенту: " + ex.Message);
            }
        }


    }
}