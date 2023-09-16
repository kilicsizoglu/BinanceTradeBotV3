using tradebot;

public class Program
{
    public static void Main(string[] args)
    {

        string apiKey = "";
        string apiSecretKey = "";

       
        Balance balance = new Balance(apiKey, apiSecretKey);
        while (true)
        {
            Console.WriteLine("USDT : " + new Balance(apiKey, apiSecretKey).GetUSDTBalance());
            Console.WriteLine("Data Reading...");
            GetPrice.SavePrice();

            List<String> CoinList = new List<String>();
            CoinList.Add("BTCUSDT");
            CoinList.Add("ETHUSDT");
            CoinList.Add("BNBUSDT");



            CoinList?.ForEach(x =>
            {

                Buy buy = new Buy(apiKey, apiSecretKey);
                BuyableCoin buyableCoin = new BuyableCoin();
                if (buyableCoin.GetBuyableCoin(x))
                {
                    _ = buy.ExecuteAsync(x, GetRSI.RSI(x));
                }

                
            });

            CoinList?.ForEach(x =>
            {
                

                Sell sell = new Sell(apiKey, apiSecretKey);
                SellableCoin sellableCoin = new SellableCoin();
                if (sellableCoin.GetSellableCoin(x))
                {
                    _ = sell.ExecuteAsync(x, GetRSI.RSI(x));
                }

            });

            CoinList?.ForEach(async x =>
            {
                ListTrade listTrade = new ListTrade(apiKey, apiSecretKey);
                var res = await listTrade.ExecuteAsync(x);
                if (res != null)
                {
                    TradeBotDbContext tradeBotDbContext = new TradeBotDbContext();
                    foreach (var item in tradeBotDbContext.Orders.ToList())
                    {
                        if (item.symbol != null)
                        {
                            decimal rsi = GetRSI.RSI(item.symbol);
                            if (item.rsi < 35)
                            {
                                if (rsi > 70)
                                {
                                    CancalTrade cancalTrade = new CancalTrade(apiKey, apiSecretKey);
                                    _ = cancalTrade.ExecuteAsync(item.symbol, res.Id);
                                    tradeBotDbContext.Orders.Remove(item);
                                }
                             }

                            if (item.rsi > 60)
                            {
                                if (rsi < 35)
                                {
                                    CancalTrade cancalTrade = new CancalTrade(apiKey, apiSecretKey);
                                    _ = cancalTrade.ExecuteAsync(item.symbol, res.Id);
                                    tradeBotDbContext.Orders.Remove(item);
                                }
                            }
                        }
                    };
                }
            });
            
            Thread.Sleep(30000);
        }

    }
}