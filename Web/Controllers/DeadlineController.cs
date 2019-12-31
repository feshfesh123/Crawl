using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flurl.Http;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Fizzler.Systems.HtmlAgilityPack;

namespace Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DeadlineController : ControllerBase
    {
        [HttpPost]
        public async Task<List<string>> Post([FromBody] User userLogin)
        {
            var client = new FlurlClient("https://courses.uit.edu.vn/").EnableCookies();

            var get = await client.Request("login/index.php").GetAsync().ReceiveString();

            HtmlDocument doc = new HtmlDocument();

            doc.LoadHtml(get);

            var node = doc.DocumentNode.SelectSingleNode("//input[@name='logintoken']");

            var token = node.Attributes["value"].Value;

            await client.Request("login/index.php").PostMultipartAsync(data => data
                .AddString("logintoken", token)
                .AddString("username", userLogin.username)
                .AddString("password", userLogin.password));

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
    public class User
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}