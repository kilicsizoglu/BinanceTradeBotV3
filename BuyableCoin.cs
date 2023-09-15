using Binance.NetCore;
using Binance.NetCore.Entities;
using tradebot;

public class BuyableCoin
{
    private TradeBotDbContext? Context;
    public BuyableCoin()
    {
        Context = new TradeBotDbContext();
    }

    public bool GetBuyableCoin(string CoinName)
    {
                decimal rsi = GetRSI.RSI(CoinName);

        if (rsi < 35)
        {
            return true;
        }
       
        return false;
    
    } 
}