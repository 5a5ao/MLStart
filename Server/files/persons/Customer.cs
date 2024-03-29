namespace Server;

public record class Customer : Person, IBuy
{
    #region Data

    public string? outputBuy;

    #endregion

    #region .ctor

    public Customer(string name) : base(name)
    {
        Name = name;
    }

    #endregion

    #region Methods

    public void Buy(string currencyItem, Securities securitiesItem, int countSecurities, Person sellerPerson, Storage sellerStorage)
    {
        double moneyProfit;
        Person customerPerson = this;
        if (securitiesItem.count != 0 & (securitiesItem.count - countSecurities < securitiesItem.count))
        {
            securitiesItem.count = securitiesItem.count - countSecurities;
            moneyProfit = securitiesItem.currentCost * countSecurities;
            sellerStorage.wallet = sellerStorage.wallet + moneyProfit;
            outputBuy = $"{customerPerson.Name} купил у {sellerPerson.Name} Акции '{securitiesItem.name}' в количестве: {countSecurities}";
        }
        else
        {
            outputBuy = $"{customerPerson.Name} не смог купить {sellerPerson.Name} Акции '{securitiesItem.name}' в количестве: {countSecurities}, потому что не хватает: {(securitiesItem.count - countSecurities) * (-1)}";
        }
    }

    #endregion

}

