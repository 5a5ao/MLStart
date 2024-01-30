namespace App
{
    public record class Seller : Person, IExchange, ISell
    {
        public Seller(string name) : base(name)
        {
            Name = name;
        }
        public void Exchenge(string currencyItem, string currencyType, Storage sellerStorage, Storage exchengerStorage)
        {
            Person exchangerPerson = this;
            exchengerStorage.wallet = sellerStorage.wallet;
            sellerStorage.wallet = 0;
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
            }
            else
            {

            }
        }

    }
}
