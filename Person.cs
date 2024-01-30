namespace App
{
    public record class Person: IExchange,ISell
    {
            public string Name { get; set; }
            public Person(string name) => Name = name;

            public void Exchenge(CurrencyName currencyItem,CurrencyType currencyType,Storage sellerStorage, Storage exchengerStorage)
            {
                Person exchangerPerson = this;
                exchengerStorage.wallet = sellerStorage.wallet;
                sellerStorage.wallet = 0;
            }
            public void Sell(CurrencyName currencyItem, Securities securitiesItem,int countSecurities, Person customerPerson,Storage sellerStorage)
            {
                double moneyProfit;
                Person sellerPerson = this;
                if (securitiesItem.count!=0 & (securitiesItem.count - countSecurities < securitiesItem.count)){
                    securitiesItem.count = securitiesItem.count - countSecurities;
                    moneyProfit = securitiesItem.currentCost * countSecurities;
                    sellerStorage.wallet = sellerStorage.wallet + moneyProfit;
                }
                else{

                }



            } 
    }
}
