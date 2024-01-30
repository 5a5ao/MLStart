namespace App
{
    public record class Customer : Person,IBuy
    {
        public Customer(string name) : base(name)
        {
            Name = name;
        }

        public void Buy(string currencyItem, Securities securitiesItem, int countSecurities, Person sellerPerson, Storage sellerStorage)
        {
            double moneyProfit;
            Person customerPerson = this;
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
