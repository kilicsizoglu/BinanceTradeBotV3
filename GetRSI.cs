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

            if (CoinName == "GALAUSDT")
                coinName = "Gala";
            if (CoinName == "ASTRUSDT")
                coinName = "Astar";
            if (CoinName == "CHZUSDT")
                coinName = "Chiliz";

            var Context = new TradeBotDbContext();
            List<decimal> lowPrice = new List<decimal>();
            List<decimal> highPrice = new List<decimal>();
            List<decimal> price = new List<decimal>();
            if (Context != null)
            {
                var time = DateTime.Now;
                time = time.AddMinutes(-5);
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
                    if (lowPrice.Count != 0)
                    {
                        lowAvg = lowAvg / lowPrice.Count;
                    }
                    decimal highAvg = 0;
                    priceBackup = 0;
                    highPrice.ForEach(x =>
                    {
                        highAvg += x - priceBackup;
                        priceBackup = x;
                    });
                    if (highPrice.Count != 0)
                    {
                        highAvg = highAvg / highPrice.Count;
                    }
                    if (lowAvg == 0)
                        lowAvg = 1;
                    if (highAvg == 0)
                        highAvg = 1;
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
