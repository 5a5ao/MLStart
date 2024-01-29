namespace App
{
    public record class Person: IExchange,ISell
    {
            public string Name { get; set; }
            public Person(string name) => Name = name;
            int a;

            public void Exchenge(CurrencyName currencyItem, Securities securitiesItem,int currencyCount, int securitiesCount)
            {
                
            }
            public void Sell(CurrencyName currencyItem, Securities securitiesItem,int currencyCount, int securitiesCount){

            }

    }
}
