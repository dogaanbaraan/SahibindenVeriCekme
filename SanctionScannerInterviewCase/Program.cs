using System;
using System.Collections.Generic;

namespace SanctionScannerInterviewCase
{
    class Program
    {
        static void Main(string[] args)
        {
            var baseUrl = "https://www.sahibinden.com";

            HtmlAgilityPack.HtmlWeb webSite = new HtmlAgilityPack.HtmlWeb();
            HtmlAgilityPack.HtmlDocument homePage
                
                = webSite.Load(baseUrl); 

            var itemCount = homePage.DocumentNode.SelectNodes("//*[@id=\"container\"]/div[3]/div/div[3]/div[3]/ul/li").Count;

            List<string> itemLinkList = new List<string>();

            for (int i = 0; i < itemCount; i++)
            {
                var itemData = homePage.DocumentNode.SelectNodes("//*[@id=\"container\"]/div[3]/div/div[3]/div[3]/ul/li[" + 
                    Convert.ToInt32(i + 1) + "]/a")[0].GetAttributeValue("href", string.Empty);
                itemLinkList.Add(itemData);
                Console.WriteLine(itemData);
            }

            var pCount = homePage.DocumentNode.SelectNodes("//*[@id=\"classifiedDescription\"]/p[1]/span/text()").Count;
            for (int i = 0; i < pCount; i++)
            {
                var pData = homePage.DocumentNode.SelectNodes("//*[@id=\"classifiedDescription\"]/p[1]/span/text()");
            }

            foreach (var itemLink in itemLinkList)
            {
                HtmlAgilityPack.HtmlDocument itemDocument = webSite.Load($"{baseUrl}{itemLink}"); 
                var priceHtml = itemDocument.DocumentNode.SelectSingleNode("//*[@class='classifiedInfo ']/h3/text()");
                //var priceHtml = itemDocument.DocumentNode.SelectSingleNode("//*[@class='classifiedInfo ']/");
                //priceHtml.SelectSingleNode("//*[@class='classifiedInfo ']/h3/text()");
                if (priceHtml!=null)
                {
                    var price = priceHtml.InnerText.Replace("\n", "");
                    Console.WriteLine($"{baseUrl}{itemLink}:  {price}");
                }
                
            }
        }
    }
}
