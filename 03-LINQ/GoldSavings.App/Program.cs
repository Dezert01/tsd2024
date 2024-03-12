using GoldSavings.App.Model;
using GoldSavings.App.Client;
using System.Xml.Linq;
namespace GoldSavings.App;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, Gold Saver!");

        GoldClient goldClient = new GoldClient();

        GoldPrice currentPrice = goldClient.GetCurrentGoldPrice().GetAwaiter().GetResult();
        Console.WriteLine($"The price for today is {currentPrice.Price}");

        List<GoldPrice> thisMonthPrices = goldClient.GetGoldPrices(new DateTime(2024, 03, 01), new DateTime(2024, 03, 11)).GetAwaiter().GetResult();
        foreach(var goldPrice in thisMonthPrices)
        {
            Console.WriteLine($"The price for {goldPrice.Date} is {goldPrice.Price}");
        }

        List<GoldPrice> thisYearPrices = goldClient.GetGoldPrices(new DateTime(2023, 01, 01), new DateTime(2024, 01, 01)).GetAwaiter().GetResult();

        Console.WriteLine("zad 3. Lowest");
        // METHOD
        // var topThreeLowestPrices = thisYearPrices.OrderBy(p => p.Price).Take(3);
        // QUERY
        var topThreeLowestPrices = (from price in thisYearPrices
                             orderby price.Price
                             select price).Take(3);

        foreach (var price in topThreeLowestPrices)
        {
            Console.WriteLine($"Top Lowest Price: Date: {price.Date}, Price: {price.Price}");
        }

        Console.WriteLine("zad 3. Highest");
        // METHOD
        // var topThreeHighestPrices = thisYearPrices.OrderByDescending(p => p.Price).Take(3);
        // QUERY
        var topThreeHighestPrices = (from price in thisYearPrices
                             orderby price.Price descending
                             select price).Take(3);

        foreach (var price in topThreeHighestPrices)
        {
            Console.WriteLine($"Top Highest Price: Date: {price.Date}, Price: {price.Price}");
        }

        Console.WriteLine("zad.4");
        List<GoldPrice> january2020Prices = goldClient.GetGoldPrices(new DateTime(2020, 01, 01), new DateTime(2020, 02, 01)).GetAwaiter().GetResult();

        // METHOD
        // var profitableDays = january2020Prices.Where(p => ((currentPrice.Price / p.Price )* 100) > 105);

        // if (profitableDays.Any())
        // {
        //     Console.WriteLine("Days in january 2020 with profit 5% comparing to today:");
        //     foreach (var price in profitableDays)
        //     {
        //          Console.WriteLine($"\t- {price.Date}: {price.Price}, increase: {currentPrice.Price/price.Price * 100 - 100}%");
        //     }
        // }
        // else
        // {
        //     Console.WriteLine("No data");
        // }
        // QUERY
        var profitableDays = (from price in january2020Prices
                          where ((currentPrice.Price / price.Price) * 100) > 105
                          select price).ToList();
         if (profitableDays.Any())
        {
            Console.WriteLine("Days in january 2020 with profit 5% comparing to today:");
            foreach (var price in profitableDays)
            {
                Console.WriteLine($"\t- {price.Date}: {price.Price}, increase: {currentPrice.Price/price.Price * 100 - 100}%");
            }
        }
        else
        {
            Console.WriteLine("No data");
        }

        Console.WriteLine("zad. 5");
        var gold2019 = goldClient.GetGoldPrices(new DateTime(2019, 01, 01), new DateTime(2019, 12, 31)).GetAwaiter().GetResult();
        var gold2020 = goldClient.GetGoldPrices(new DateTime(2020, 01, 01), new DateTime(2020, 12, 31)).GetAwaiter().GetResult();
        var gold2021 = goldClient.GetGoldPrices(new DateTime(2021, 01, 01), new DateTime(2021, 12, 31)).GetAwaiter().GetResult();

        // METHOD
        // var top3FromSecondTen2019 = gold2019
        //     .OrderByDescending(g => g.Price)
        //     .Skip(10)
        //     .Take(3);

        // var index = 11;
        // foreach (var goldPrice in top3FromSecondTen2019)
        // {
        //     Console.WriteLine($"{index}. {goldPrice.Date} {goldPrice.Price}");
        //     index++;
        // }

        // var top3FromSecondTen2020 = gold2020
        //     .OrderByDescending(g => g.Price)
        //     .Skip(10)
        //     .Take(3);

        // index = 11;
        // foreach (var goldPrice in top3FromSecondTen2020)
        // {
        //     Console.WriteLine($"{index}. {goldPrice.Date} {goldPrice.Price}");
        //     index++;
        // }

        // var top3FromSecondTen2021 = gold2021
        //     .OrderByDescending(g => g.Price)
        //     .Skip(10)
        //     .Take(3);

        // index = 11;
        // foreach (var goldPrice in top3FromSecondTen2021)
        // {
        //     Console.WriteLine($"{index}. {goldPrice.Date} {goldPrice.Price}");
        //     index++;
        // }

        // QUERY 
        var top3FromSecondTen2019 = (from g in gold2019
        orderby g.Price descending
        select g).Skip(10).Take(3);

        var top3FromSecondTen2020 = (from g in gold2020
        orderby g.Price descending
        select g).Skip(10).Take(3);

        var top3FromSecondTen2021 = (from g in gold2021
        orderby g.Price descending
        select g).Skip(10).Take(3);

        var index = 11;
        foreach (var goldPrice in top3FromSecondTen2019)
        {
            Console.WriteLine($"{index}. {goldPrice.Date} {goldPrice.Price}");
            index++;
        }

        index = 11;
        foreach (var goldPrice in top3FromSecondTen2020)
        {
            Console.WriteLine($"{index}. {goldPrice.Date} {goldPrice.Price}");
            index++;
        }

        index = 11;
        foreach (var goldPrice in top3FromSecondTen2021)
        {
            Console.WriteLine($"{index}. {goldPrice.Date} {goldPrice.Price}");
            index++;
        }

        Console.WriteLine("zad. 6");
        List<GoldPrice> prices2021 = goldClient.GetGoldPrices(new DateTime(2021, 01, 01), new DateTime(2022, 01, 01)).GetAwaiter().GetResult();
        List<GoldPrice> prices2022 = goldClient.GetGoldPrices(new DateTime(2022, 01, 01), new DateTime(2023, 01, 01)).GetAwaiter().GetResult();
        List<GoldPrice> prices2023 = goldClient.GetGoldPrices(new DateTime(2023, 01, 01), new DateTime(2024, 01, 01)).GetAwaiter().GetResult();

        //METHOD
        // double averagePrice2021 = prices2021.Average(p => p.Price);
        // Console.WriteLine($"Average gold price in 2021: {averagePrice2021:0.00}");
        // double averagePrice2022 = prices2022.Average(p => p.Price);
        // Console.WriteLine($"Average gold price in 2022: {averagePrice2022:0.00}");
        // double averagePrice2023 = prices2023.Average(p => p.Price);
        // Console.WriteLine($"Average gold price in 2023: {averagePrice2023:0.00}");

        // QUERY
        var averagePrice2021 = (from p in prices2021 select p.Price).Average();
        Console.WriteLine($"Average gold price in 2021: {averagePrice2021:0.00}");
        var averagePrice2022 = (from p in prices2022 select p.Price).Average();
        Console.WriteLine($"Average gold price in 2022: {averagePrice2022:0.00}");
        var averagePrice2023 = (from p in prices2023 select p.Price).Average();
        Console.WriteLine($"Average gold price in 2023: {averagePrice2023:0.00}");

        Console.Write("zad. 7");
        DateTime startDate = new DateTime(2019, 01, 01);
        DateTime endDate = new DateTime(2023, 12, 31);

        for (int year = startDate.Year; year <= endDate.Year; year++)
        {
            DateTime yearStart = new DateTime(year, 1, 1);
            DateTime yearEnd = new DateTime(year + 1, 1, 1);

            List<GoldPrice> prices = goldClient.GetGoldPrices(yearStart, yearEnd).GetAwaiter().GetResult();

            // METHODS
            if (prices != null && prices.Any())
            {
                var minPrice = prices.OrderBy(p => p.Price).First();
                var maxPrice = prices.OrderByDescending(p => p.Price).First();

                var profitablePrices = prices.Where(p => p.Date > minPrice.Date && p.Date < maxPrice.Date);

                if (profitablePrices.Any())
                {
                    var buyDate = profitablePrices.OrderBy(p => p.Date).First().Date;
                    var sellDate = profitablePrices.OrderByDescending(p => p.Date).First().Date;

                    Console.WriteLine($"\nBest Time to Buy and Sell Gold in {year}:");
                    Console.WriteLine($"Buy on: {buyDate}, Price: {minPrice.Price}");
                    Console.WriteLine($"Sell on: {sellDate}, Price: {maxPrice.Price}");

                    decimal profit = (decimal)(maxPrice.Price - minPrice.Price);
                    decimal ROI = (profit / (decimal)minPrice.Price) * 100;
                    Console.WriteLine($"Return on Investment: {ROI}%");
                }
                else
                {
                    Console.WriteLine($"No data.");
                }
            }
            else
            {
                Console.WriteLine($"No data.");
            }

            // QUERY
            if (prices != null && prices.Any())
            {
                var minPrice = (from p in prices
                                orderby p.Price
                                select p).First();
                var maxPrice = (from p in prices
                                orderby p.Price descending
                                select p).First();

                var profitablePrices = from p in prices
                                    where p.Date > minPrice.Date && p.Date < maxPrice.Date
                                    select p;

                if (profitablePrices.Any())
                {
                    var buyDate = (from p in profitablePrices
                                orderby p.Date
                                select p.Date).First();
                    var sellDate = (from p in profitablePrices
                                    orderby p.Date descending
                                    select p.Date).First();

                    Console.WriteLine($"\nBest Time to Buy and Sell Gold in {year}:");
                    Console.WriteLine($"Buy on: {buyDate}, Price: {minPrice.Price}");
                    Console.WriteLine($"Sell on: {sellDate}, Price: {maxPrice.Price}");

                    decimal profit = (decimal)(maxPrice.Price - minPrice.Price);
                    decimal ROI = (profit / (decimal)minPrice.Price) * 100;
                    Console.WriteLine($"Return on Investment: {ROI}%");
                }
                else
                {
                    Console.WriteLine($"No data.");
                }
            }
            else
            {
                Console.WriteLine($"No data.");
            }
        }

       


        Console.WriteLine("zad. 8");
        SavePricesToXml(prices2021, "test");
        Console.WriteLine("pries loaded to test file");

        Console.WriteLine("zad. 9");
        var loadedPrices = LoadPricesFromXml("test");
        foreach (var price in loadedPrices)
        {
            Console.WriteLine($"Date: {price.Date}, Price: {price.Price}");
        }
    }
    // ZADANIE 8
    static void SavePricesToXml(List<GoldPrice> prices, string filePath)
    {
        XElement root = new XElement("GoldPrices");

        foreach (var price in prices)
        {
            XElement price2 = new XElement("GoldPrice");
            XElement priceElement = new XElement("Price", price.Price);
            XElement dateElement = new XElement("Date", price.Date);
            price2.Add(priceElement);
            price2.Add(dateElement);
            root.Add(price2);
        }

        XDocument doc = new XDocument(root);
        doc.Save(filePath);
    }
    // ZADANIE 9
    static List<GoldPrice> LoadPricesFromXml(string filePath)
    {
    return XElement.Load(filePath).Descendants("GoldPrice")
        .Select(price => new GoldPrice 
        { 
            Price = double.Parse(price.Element("Price").Value), 
            Date = DateTime.Parse(price.Element("Date").Value)
        })
        .ToList();
    }
}
