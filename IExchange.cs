namespace App
{
    interface IExchange
    {
        void Exchenge(CurrencyName currencyItem, CurrencyType currencyType,Storage sellerStorage, Storage exchengerStorage);
    }
}