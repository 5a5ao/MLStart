namespace Program;

public class Storyteller
{
    public event EventHandler<string> TextUpdated;

    public void TellStory()
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
        OnTextUpdated(customer.outputBuy);
        countCustomer();

        firstSeller.Sell(currencyCustomer, stock, rand.Next(), allCustomers, sellerStorage);
        OnTextUpdated(firstSeller.outputSell);
        exchanger.Exchenge(currencyCustomer, sellerStorage, exchangerStorage);
        OnTextUpdated(exchanger.outputExchenge);

        OnTextUpdated($"Текущая ставка акций {stock.name} составляет {stock.profitability}");
        BDInteraction.Connection();
        OnTextUpdated(BDInteraction.outputString);

    }

    public void countCustomer()
    {
        Random rand = new Random();
        var x = new double[13];
        OnTextUpdated($"За {x.Length} дней количество покупателей изменялось в процентном соотношении");

        var x1 = new double[13];
        for (int i = 0; i < x.Length; i++)
        {
            x1[i] = Math.Round(-12.0 + rand.NextDouble() * (15.0 + 12.0), 1);
            OnTextUpdated($"В {i + 1} день {x1[i]}%");
        }

    }

    void OnTextUpdated(string newText)
    {
        TextUpdated?.Invoke(sender: TellStory, e: newText);
        Thread.Sleep(100);
    }

}
