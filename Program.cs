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
            List<String> CoinList = new List<String>();
            CoinList.Add("1000SHIBUSDT");
            CoinList.Add("GALAUSDT");
            CoinList.Add("DOGEUSDT");

            CoinList?.ForEach(x =>
            {
                Console.WriteLine("USDT : " + new Balance(apiKey, apiSecretKey).GetUSDTBalance());
                Console.WriteLine("Data Reading...");
                GetPrice.SavePrice();

                Buy buy = new Buy(apiKey, apiSecretKey);
                BuyableCoin buyableCoin = new BuyableCoin();
                _ = buy.ExecuteAsync(x, GetRSI.RSI(x));
                if (buyableCoin.GetBuyableCoin(x))
                {
                    _ = buy.ExecuteAsync(x, GetRSI.RSI(x));
                }

                Thread.Sleep(180000);
            });

            CoinList?.ForEach(x =>
            {
                Console.WriteLine("USDT : " + new Balance(apiKey, apiSecretKey).GetUSDTBalance());
                Console.WriteLine("Data Reading...");
                GetPrice.SavePrice();

                Sell sell = new Sell(apiKey, apiSecretKey);
                SellableCoin sellableCoin = new SellableCoin();
                if (sellableCoin.GetSellableCoin(x))
                {
                    sell.Execute(x, GetRSI.RSI(x));
                }

                Thread.Sleep(180000);
            });
        }

    }
}