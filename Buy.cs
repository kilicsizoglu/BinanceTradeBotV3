using Binance.Net.Clients;
using Binance.Net.Enums;
using CryptoExchange.Net.Authentication;
using tradebot;

public class Buy
{
    string apiKey = "";
    string apiSecretKey = "";
    BinanceRestClient? binanceRestClient = null;
    public Buy(string apiKey, string apiSecretKey)
    {
        binanceRestClient = new BinanceRestClient();
        BinanceRestClient.SetDefaultOptions(options =>
        {
            options.ApiCredentials = new ApiCredentials(apiKey, apiSecretKey); 
        });
        this.apiKey = apiKey;
        this.apiSecretKey = apiSecretKey;
    }
    public async Task ExecuteAsync(String coin, decimal rsi)
    {
        string coinName = "";

        if (coin == "1000SHIBUSDT")
            coinName = "Shiba Inu";
        if (coin == "GALAUSDT")
            coinName = "Gala";
        if (coin == "DOGEUSDT")
            coinName = "Dogecoin";

        if (coin != "")
        {

            if (binanceRestClient != null)
            {
                try
                {
                    decimal price =  GetPrice.GetCoinPrice(coinName);
                    decimal quantity = Math.Round(10 / price);
                    var res = await binanceRestClient.UsdFuturesApi.Trading.PlaceOrderAsync(
                        coin,
                        OrderSide.Buy,
                        FuturesOrderType.Limit,
                        quantity,
                        Math.Round(price, 6),
                        timeInForce: TimeInForce.FillOrKill
                    );
                    Console.WriteLine(res.ToString());
                    Console.WriteLine("Buy " + coinName + " " + price);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }


}