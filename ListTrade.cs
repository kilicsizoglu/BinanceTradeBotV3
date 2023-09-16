using Binance.Net.Clients;
using Binance.Net.Objects.Models.Futures;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tradebot
{
    public class ListTrade
    {
        BinanceRestClient? binanceRestClient = null;
        public ListTrade(string apiKey, string apiSecretKey)
        {
            binanceRestClient = new BinanceRestClient();
            BinanceRestClient.SetDefaultOptions(options =>
            {
                options.ApiCredentials = new ApiCredentials(apiKey, apiSecretKey);
            });
        }

        public async Task<BinanceFuturesOrder> ExecuteAsync(string coinName)
        {
            CallResult<BinanceFuturesOrder>? res = new CallResult<BinanceFuturesOrder>(new BinanceFuturesOrder());
            if (binanceRestClient != null)
            {
                try
                {
                    res = await binanceRestClient.UsdFuturesApi.Trading.GetOrderAsync(coinName);
                    return res.Data;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return res.Data;
        }
    }
}
