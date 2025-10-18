namespace Guardian.Api.Services
{
    public class InvestmentSimulator
    {
        // Juros compostos mensais: FV = P * ((1 + i)^n - 1) / i
        public decimal FutureValue(decimal monthlyContribution, decimal annualRatePercent, int months)
        {
            var i = (double)annualRatePercent / 100.0 / 12.0;
            if (Math.Abs(i) < 1e-9)
                return monthlyContribution * months;
            var fv = (double)monthlyContribution * (Math.Pow(1 + i, months) - 1) / i;
            return (decimal)fv;
        }

        public decimal LumpSumFutureValue(decimal principal, decimal annualRatePercent, int months)
        {
            var i = (double)annualRatePercent / 100.0 / 12.0;
            var fv = (double)principal * Math.Pow(1 + i, months);
            return (decimal)fv;
        }
    }
}
