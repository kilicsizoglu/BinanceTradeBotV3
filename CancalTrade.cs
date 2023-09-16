using Binance.Net.Clients;
using CryptoExchange.Net.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tradebot
{
    public class CancalTrade
    {
        BinanceRestClient? binanceRestClient = null;
        public CancalTrade(string apiKey, string apiSecretKey)
        {
            binanceRestClient = new BinanceRestClient();
            BinanceRestClient.SetDefaultOptions(options =>
            {
                options.ApiCredentials = new ApiCredentials(apiKey, apiSecretKey);
            });
        }

        public async Task ExecuteAsync(String coin, long orderId)
        {
            if (binanceRestClient != null)
            {
                try
                {
                    var res = await binanceRestClient.UsdFuturesApi.Trading.CancelOrderAsync(coin, orderId);
                    Console.WriteLine(res.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
