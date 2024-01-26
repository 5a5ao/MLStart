namespace App
{
    public abstract class Securities
    {
        double currentCost;
        public Securities(string name)
        {
            this.name = name;
            this.countMonth = 12;
        }
        public Securities(string name, int countMonth)
        {
            this.name = name;
            this.countMonth = countMonth;
        }

        public string name { get; set; }
        public double cost { get; set; }
        public int count { get; set; }
        public int countMonth { get; set; }
        public double Profitability { get; set; }
        public virtual void CurrentCostCalculator()
        {
            currentCost = cost * (Math.Pow(1 + (Profitability / 100), (countMonth / 12)));
        }

    }
}