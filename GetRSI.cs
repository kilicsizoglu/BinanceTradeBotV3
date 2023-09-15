using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tradebot
{
    public class GetRSI
    {
        public static decimal RSI(String CoinName)
        {
            string coinName = "";

            if (CoinName == "1000SHIBUSDT")
                coinName = "Shiba Inu";
            if (CoinName == "GALAUSDT")
                coinName = "Gala";
            if (CoinName == "DOGEUSDT")
                coinName = "Dogecoin";

            var Context = new TradeBotDbContext();
            List<decimal> lowPrice = new List<decimal>();
            List<decimal> highPrice = new List<decimal>();
            List<decimal> price = new List<decimal>();
            if (Context != null)
            {
                var time = DateTime.Now;
                time = time.AddMinutes(-15);
                Context.Currencies.Where(x => x.name == coinName).ToList().Where(x => x.date < time).ToList().ForEach(x =>
                {
                    if (x.price != null)
                    {
                        price.Add(Convert.ToDecimal(x.price));
                    }
                });
                if (price.Count != 0)
                {
                    decimal avg = 0;
                    price.ForEach(x =>
                    {
                        avg += x;
                    });
                    avg = avg / price.Count;
                    price.ForEach(x =>
                    {
                        if (x < avg)
                        {
                            lowPrice.Add(x);
                        }
                        else
                        {
                            highPrice.Add(x);
                        }
                    });
                    decimal lowAvg = 0;
                    decimal priceBackup = 0;
                    lowPrice.ForEach(x =>
                    {
                        lowAvg += x - priceBackup;
                        priceBackup = x;
                    });
                    lowAvg = lowAvg / lowPrice.Count;
                    decimal highAvg = 0;
                    priceBackup = 0;
                    highPrice.ForEach(x =>
                    {
                        highAvg += x - priceBackup;
                        priceBackup = x;
                    });
                    highAvg = highAvg / highPrice.Count;
                    decimal rs = highAvg / lowAvg;
                    decimal rsi = 100 - (100 / (1 + rs));
                    Console.WriteLine(coinName + " " + rsi);
                    return rsi;
                }
                
            }
            return 0;
        }
    }
}
