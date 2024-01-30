namespace App
{
    interface IBuy
    {
        void Buy(string currencyItem, Securities securitiesItem, int countSecurities, Person customerPerson, Storage sellerStorage);
    }
}