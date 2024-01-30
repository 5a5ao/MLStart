namespace App
{
    public class Stock : Securities
    {
        private Storage sellerStorage;

        public Stock(string name, Storage sellerStorage) : base(name)
        {
            Random rand = new Random();
            this.cost = Math.Round(rand.NextDouble() * (0.1 + 99999.0), 1);
            this.count = sellerStorage.getCapacity() * 1000000;
            ProfitabilityRandomiser();
            CurrentCostCalculator();
        }

        public Stock(string name, int countMonth, Storage sellerStorage) : base(name, countMonth)
        {
            Random rand = new Random();
            this.cost = Math.Round(rand.NextDouble() * (0.1 + 99999.0), 1);
            this.count = sellerStorage.getCapacity() * 1000000;
            ProfitabilityRandomiser();
            CurrentCostCalculator();
        }
        private void ProfitabilityRandomiser()
        {
            Random rand = new Random();
            var x = new double[13];

            ///1 реализация
            for (int i = 0; i < x.Length; i++)
            {
                x[i] = -12.0 + rand.NextDouble() * (15.0 + 12.0);
            }
            ///2 реализация
            var x1 = new double[13];    
            for (int i = 0; i < x.Length; i++)
            {
                x1[i] = Math.Round(-12.0 + rand.NextDouble() * (15.0 + 12.0), 1);
            }
            profitability = x1[rand.Next(x1.Length)];
        }
    }
}