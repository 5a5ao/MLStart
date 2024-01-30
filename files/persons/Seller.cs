namespace App
{
    public record class Seller : Person, IExchange, ISell
    {
        public Seller(string name) : base(name)
        {
            Name = name;
        }
        public void Exchenge(string currencyItem, Storage sellerStorage, Storage exchengerStorage)
        {
            Person exchangerPerson = this;
            exchengerStorage.wallet = sellerStorage.wallet;
            sellerStorage.wallet = 0;
            Random rand = new Random();
            CurrencyType randomCurrencyTypeFirst = (CurrencyType)rand.Next(Enum.GetValues(typeof(CurrencyType)).Length);
            string firstCurrency = randomCurrencyTypeFirst.ToString();
            CurrencyType randomCurrencyTypeSecond = (CurrencyType)rand.Next(Enum.GetValues(typeof(CurrencyType)).Length);
            string secondCurrency = randomCurrencyTypeSecond.ToString();
            Console.WriteLine($"{exchangerPerson.Name} забрал выручку в количестве {exchengerStorage.wallet}");
            Console.WriteLine($"{exchangerPerson.Name} обменял {currencyItem} c {firstCurrency} на {secondCurrency} и сложил в {exchengerStorage.name}");
        }
        public void Sell(string currencyItem, Securities securitiesItem, int countSecurities, Person customerPerson, Storage sellerStorage)
        {
            double moneyProfit;
            Person sellerPerson = this;
            if (securitiesItem.count != 0 & (securitiesItem.count - countSecurities < securitiesItem.count))
            {
                securitiesItem.count = securitiesItem.count - countSecurities;
                moneyProfit = securitiesItem.currentCost * countSecurities;
                sellerStorage.wallet = sellerStorage.wallet + moneyProfit;
                Console.WriteLine($"{sellerPerson.Name} продал {customerPerson.Name} Акции '{securitiesItem.name}' в количестве: {countSecurities}");
                Console.WriteLine($"{sellerPerson.Name} получил {currencyItem} в количестве: {moneyProfit} и сложил в {sellerStorage.name}");
                Console.WriteLine($"Текущий заработок составляет {sellerStorage.wallet}");
            }
            else
            {
                Console.WriteLine($"{sellerPerson.Name} не смог продать {customerPerson.Name} Акции '{securitiesItem.name}' в количестве: {countSecurities}, потому что не хватает: {(securitiesItem.count - countSecurities) * (-1)}");
            }
        }

    }
}

