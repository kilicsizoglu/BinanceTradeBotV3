using Binance.Net.Clients;
using CryptoExchange.Net.Authentication;

public class Balance
{

    BinanceRestClient? binanceRestClient = null;
    public Balance(string apiKey, string apiSecretKey)
    {
        binanceRestClient = new BinanceRestClient();
        BinanceRestClient.SetDefaultOptions(options =>
        {
            options.ApiCredentials = new ApiCredentials(apiKey, apiSecretKey);
        });;
    }

    public decimal GetUSDTBalance()
    {
        decimal usdtBalance = 0;
        if (binanceRestClient != null)
        {
            var balance = binanceRestClient.UsdFuturesApi.Account.GetBalancesAsync().Result.Data;
            if (balance != null)
            {
                foreach (var item in balance)
                {
                    if (item.Asset == "USDT")
                    {
                        usdtBalance = item.WalletBalance;
                    }
                }

            }
        }
        return usdtBalance;
    }

    public decimal GetBalance(string coin)
    {
        decimal coinBalance = 0;
        if (binanceRestClient != null)
        {
            var balance = binanceRestClient.UsdFuturesApi.Account.GetBalancesAsync().Result.Data;
            if (balance != null)
            {
                foreach (var item in balance)
                {
                    if (item.Asset == coin)
                    {
                        coinBalance = item.WalletBalance;
                    }
                }

            }
        }
        return coinBalance;
    }

    internal bool WithAValueOf10USDT()
    {
        bool has5USDT = false;
        if (binanceRestClient != null)
        {
            var balance = binanceRestClient.UsdFuturesApi.Account.GetBalancesAsync().Result.Data;
            if (balance != null)
            {
                foreach (var item in balance)
                {
                    if (item.WalletBalance < 10)
                    {
                        has5USDT = true;
                    }
                }

            }
        }
        return has5USDT;
    }
}