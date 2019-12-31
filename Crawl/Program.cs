using Fizzler.Systems.HtmlAgilityPack;
using Flurl.Http;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Crawl
    {
        public static async Task<List<string>> Deadline(string username, string password)
        {
            var client = new FlurlClient("https://courses.uit.edu.vn/").EnableCookies();

            var get = await client.Request("login/index.php").GetAsync().ReceiveString();

            HtmlDocument doc = new HtmlDocument();

            doc.LoadHtml(get);

            var node = doc.DocumentNode.SelectSingleNode("//input[@name='logintoken']");

            var token = node.Attributes["value"].Value;

            await client.Request("login/index.php").PostMultipartAsync(data => data
                .AddString("logintoken", token)
                .AddString("username", username)
                .AddString("password", password));

            var response = await client.Request("calendar/view.php?view=month").GetAsync().ReceiveString();

            doc.LoadHtml(response);

            var dayItems = doc.DocumentNode.QuerySelectorAll("a[data-action=view-event] > span.eventname").ToList();
            //var dayItems = doc.DocumentNode.SelectNodes("//td[@class='day text-center']").ToList();

            var dls = new List<string>();
            foreach (var item in dayItems)
            {
                dls.Add(item.InnerText);
            }

            return dls;
        }

    }
    class Program
    {
        
        
        static void Main(string[] args)
        {
            //Console.Write("Hello world!");

            var username = "17520997";

            var password =
                "1911513393";

            var content = Crawl.Deadline(username, password);
            Console.WriteLine(content);
            

            Console.ReadLine();
        }
    }
}
