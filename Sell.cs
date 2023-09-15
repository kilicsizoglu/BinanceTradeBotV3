using Binance.Net.Clients;
using Binance.Net.Enums;
using CryptoExchange.Net.Authentication;
using tradebot;

public class Sell
{
    string apiKey = "";
    string apiSecretKey = "";
    BinanceRestClient? binanceRestClient = null;
    public Sell(string apiKey, string apiSecretKey)
    {
        binanceRestClient = new BinanceRestClient();
        BinanceRestClient.SetDefaultOptions(options =>
        {
            options.ApiCredentials = new ApiCredentials(apiKey, apiSecretKey);
        });
        this.apiKey = apiKey;
        this.apiSecretKey = apiSecretKey;
    }
    public void Execute(String coin, decimal rsi)
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
                    decimal price = GetPrice.GetCoinPrice(coinName);
                    binanceRestClient.UsdFuturesApi.Trading.PlaceOrderAsync(
                        coin,
                        OrderSide.Sell,
                        FuturesOrderType.Limit,
                        Math.Round(10 / price),
                        price,
                        timeInForce: TimeInForce.FillOrKill
                    );
                    Console.WriteLine("Sell " + coinName + " " + price);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}