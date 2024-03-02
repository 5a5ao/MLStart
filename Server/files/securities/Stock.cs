using Program;
namespace Server;

public class Stock : Securities
{
    #region Data

    private Storage sellerStorage;

    #endregion

    #region .ctor
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

    #endregion

    #region Methods
    private void ProfitabilityRandomiser()
    {
        #region MethodData
        var k_ = new double[8, 13];
        int N = int.Parse(Program.ConfigManager.GetConfig("N"));
        int L = int.Parse(Program.ConfigManager.GetConfig("L"));

        double kMin = 0;
        int jAvg = L % 13;
        int iMin = N % 8;
        double sum = 0;
        int count = 0;
        #endregion

        for (int i = 0; i < k_.GetLength(0); i++)
        {
            for (int j = 0; j < k_.GetLength(1); j++)
            {
                double x_ = j;
                if (i == 9)
                {
                    k_[i, j] = Math.Sin(Math.Sin(Math.Pow(x_ / (x_ + 1 / 2), x_)));
                }
                else if (i == 5 || k_[i, j] == 7 || k_[i, j] == 11 || k_[i, j] == 15)
                {
                    k_[i, j] = Math.Pow((0.5 / (Math.Tan(2 * x_) + (2 / 3))), Math.Pow(Math.Pow(x_, 1 / 3), 1 / 3));
                }
                else
                {
                    k_[i, j] = Math.Tan(Math.Pow(((Math.Pow(Math.E, (1 - x_) / Math.PI) / 3) / 4), 3));
                }

            }
        }

        for (int j = 0; j < k_.GetLength(1); j++)
        {
            if (k_[iMin, j] < kMin)
            {
                kMin = k_[iMin, j];
            }
        }

        for (int i = 0; i < k_.GetLength(0); i++)
        {
            sum = k_[i, jAvg] + sum;
            count++;
        }
        double kAvg = sum / count;
        profitability = Math.Round(kMin + kAvg, 4);
    }

    #endregion
}
