using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tradebot
{
    public class SaveOrder
    {
        public static void SaveOrderData(string symbol, decimal rsi)
        {
            if (rsi != 0)
            {
                var Context = new TradeBotDbContext();
                if (Context != null)
                {
                    Context.Orders.Add(new SaveOrderTable
                    {
                        symbol = symbol,
                        rsi = rsi
                    });
                    Context.SaveChanges();
                }
            }
        }
    }
}
