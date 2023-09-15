using NoobsMuc.Coinmarketcap.Client;

namespace tradebot;

public class GetPrice
{
    public static void SavePrice()
    {
        ICoinmarketcapClient client = new CoinmarketcapClient("");
        var crypto = client.GetCurrencies();
        TradeBotDbContext context = new TradeBotDbContext();
        foreach (var c in crypto)
        {
            context.Currencies.Add(new Crypto
            {
                name = c.Name,
                date = DateTime.Now,
                price = c.Price
            });   
        }
        context.SaveChanges();
    }

    public static decimal GetCoinPrice(string CoinName)
    {
        ICoinmarketcapClient client = new CoinmarketcapClient("");
        var crypto = client.GetCurrencies();
        foreach (var c in crypto)
        {
            if (c.Name == CoinName)
                return c.Price;
        }
        return 0;
    }
}