namespace App
{
    public abstract class Securities
    {
        double currentCost;
        public Securities(string name)
        {
            this.name = name;
            this.count = count;

            this.countMonth = 12;
            currentCostCalculator();
            if (this.Profitability == 0) {this.currentCost = cost;};
        }
        public Securities(string name, int countMonth)
        {
            this.name = name;
 
            this.count = count;
            this.countMonth = countMonth;
            currentCostCalculator();
            if (this.Profitability == 0) {this.currentCost = cost;};
        }

        public string name { get; set; }
        public double cost { get; set; }
        public int count { get; set; }
        public int countMonth { get; set; }
        public double Profitability { get; set; }
        private double currentCostCalculator()
        {
            return currentCost = currentCost * (Math.Pow(1 + (Profitability / 100), (countMonth / 12)));
        }

    }
}