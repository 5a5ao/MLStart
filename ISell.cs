namespace App
{
    interface ISell
    {
        void Sell(CurrencyName currencyItem, Securities securitiesItem,int countSecurities, Person customerPerson, Storage sellerStorage);
    }
}