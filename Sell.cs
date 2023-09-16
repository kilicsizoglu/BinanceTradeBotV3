using Binance.Net.Clients;
using Binance.Net.Enums;
using Binance.Net.Objects.Models.Futures;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
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
    public async Task ExecuteAsync(String coin, decimal rsi)
    {
        if (rsi != 0)
        {
            string coinName = "";

            if (coin == "BTCUSDT")
                coinName = "Bitcoin";
            if (coin == "ETHUSDT")
                coinName = "Ethereum";
            if (coin == "BNBUSDT")
                coinName = "BNB";

            if (coin != "")
            {

                if (binanceRestClient != null)
                {
                    try
                    {
                        WebCallResult<BinanceFuturesPlacedOrder> res;
                        if (coin == "BTCUSDT")
                        {
                            decimal price = GetPrice.GetCoinPrice(coinName);
                            decimal quantity = 0.001m;
                            res = await binanceRestClient.UsdFuturesApi.Trading.PlaceOrderAsync(
                                coin,
                                OrderSide.Sell,
                                FuturesOrderType.Limit,
                                quantity,
                                Math.Round(price),
                                timeInForce: TimeInForce.FillOrKill
                            );
                            Console.WriteLine(res.ToString());
                            Console.WriteLine("Buy " + coinName + " " + price);
                        }
                        if (coin == "ETHUSDT")
                        {
                            decimal price = GetPrice.GetCoinPrice(coinName);
                            decimal quantity = 0.18m;
                            res = await binanceRestClient.UsdFuturesApi.Trading.PlaceOrderAsync(
                                coin,
                                OrderSide.Sell,
                                FuturesOrderType.Limit,
                                quantity,
                                Math.Round(price),
                                timeInForce: TimeInForce.FillOrKill
                            );
                            Console.WriteLine(res.ToString());
                            Console.WriteLine("Buy " + coinName + " " + price);
                        }
                        if (coin == "BNBUSDT")
                        {
                            decimal price = GetPrice.GetCoinPrice(coinName);
                            decimal quantity = 1.4m;
                            res = await binanceRestClient.UsdFuturesApi.Trading.PlaceOrderAsync(
                                coin,
                                OrderSide.Sell,
                                FuturesOrderType.Limit,
                                quantity,
                                Math.Round(price),
                                timeInForce: TimeInForce.FillOrKill
                            );
                            Console.WriteLine(res.ToString());
                            Console.WriteLine("Buy " + coinName + " " + price);
                        }
                        if (coin == "BTCUSDT" || coin == "ETHUSDT" || coin == "BNBUSDT")
                        {
                            SaveOrder.SaveOrderData(coin, rsi);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }
    }
}