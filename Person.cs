namespace App
{
    public record class Person: IExchange,ISell
    {
            public string Name { get; set; }
            public Person(string name) => Name = name;

            public void Exchenge(CurrencyName currencyItem, Securities securitiesItem,int currencyCount, int securitiesCount, Person customerPerson)
            {
                Person exchangerPerson = this;
            }
            public void Sell(CurrencyName currencyItem, Securities securitiesItem,int currencyCount, int securitiesCount, Person customerPerson)
            {
                Person sellerPerson = this;
            }

    }
}
