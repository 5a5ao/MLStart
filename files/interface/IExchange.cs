namespace App
{
    interface IExchange
    {
        void Exchenge(string currencyItem, string currencyType,Storage sellerStorage, Storage exchengerStorage);
    }
}