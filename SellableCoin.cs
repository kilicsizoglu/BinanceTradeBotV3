using Binance.NetCore;
using Binance.NetCore.Entities;
using tradebot;

public class SellableCoin
{
    private TradeBotDbContext? Context;
    public SellableCoin()
    {
        Context = new TradeBotDbContext();
    }
    public bool GetSellableCoin(String CoinName)
    {
        decimal rsi = GetRSI.RSI(CoinName);
        Console.WriteLine(CoinName + " " + rsi);

        if (rsi > 60)
        {
            return true;
        }
        return false;
    }
}