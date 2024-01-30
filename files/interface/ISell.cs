namespace App
{
    interface ISell
    {
        void Sell(string currencyItem, Securities securitiesItem,int countSecurities, Person customerPerson, Storage sellerStorage);
    }
}