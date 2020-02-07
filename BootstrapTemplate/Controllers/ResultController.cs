using BootstrapTemplate.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace BootstrapTemplate.Controllers
{
    public class ResultController : Controller
    {
        private readonly IList<BriefingViewModel> _list;

        public ResultController()
        {
            _list = new List<BriefingViewModel>();
        }

        public IActionResult Index()
        {
            GetBriefingsFromSharepointApiAsync();
            return View("Result", _list);
        }

        private void GetBriefingsFromSharepointApiAsync()
        {
            Uri sharePointSiteUrl = new Uri("https://easervices.sharepoint.com/sites");

            HttpWebRequest endpointRequest = (HttpWebRequest)HttpWebRequest.Create(
              "https://easervices.sharepoint.com/sites/DesktopRedSandbox/_api/web/lists/getbyTitle('" + "Test Data" + "')/items?$top=2000&$select=Title,developer");
            endpointRequest.Method = "GET";
            endpointRequest.Accept = "application/json;odata=nometadata";
            endpointRequest.Headers.Add("Authorization",
              "Bearer " + "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6IkhsQzBSMTJza3hOWjFXUXdtak9GXzZ0X3RERSIsImtpZCI6IkhsQzBSMTJza3hOWjFXUXdtak9GXzZ0X3RERSJ9.eyJhdWQiOiIwMDAwMDAwMy0wMDAwLTBmZjEtY2UwMC0wMDAwMDAwMDAwMDAvZWFzZXJ2aWNlcy5zaGFyZXBvaW50LmNvbUA2M2Q4YzczOC1kMWE2LTQzNjMtODgyZC1lOWFkNzRjMDM4YzEiLCJpc3MiOiIwMDAwMDAwMS0wMDAwLTAwMDAtYzAwMC0wMDAwMDAwMDAwMDBANjNkOGM3MzgtZDFhNi00MzYzLTg4MmQtZTlhZDc0YzAzOGMxIiwiaWF0IjoxNTgxMDY5MDU4LCJuYmYiOjE1ODEwNjkwNTgsImV4cCI6MTU4MTA5ODE1OCwiaWRlbnRpdHlwcm92aWRlciI6IjAwMDAwMDAxLTAwMDAtMDAwMC1jMDAwLTAwMDAwMDAwMDAwMEA2M2Q4YzczOC1kMWE2LTQzNjMtODgyZC1lOWFkNzRjMDM4YzEiLCJuYW1laWQiOiI4MmE5ZmUwOS0zNjgxLTRlNTctOGE5Ni0xNzlmZGExMWU5ZjVANjNkOGM3MzgtZDFhNi00MzYzLTg4MmQtZTlhZDc0YzAzOGMxIiwib2lkIjoiMTJlNTRiY2ItNzgwYi00ZGU2LWFjY2YtYmNmMDZmYjFlMTFlIiwic3ViIjoiMTJlNTRiY2ItNzgwYi00ZGU2LWFjY2YtYmNmMDZmYjFlMTFlIiwidHJ1c3RlZGZvcmRlbGVnYXRpb24iOiJmYWxzZSJ9.l1oFN-w7lPIoZjcdVl9GX6m7J4th2faqYgGKCiH33lIFkH15-Y9xco0yjcxgcmbsdIPMoockcvIxmuJ5p_svFemd91GgkZkPPGkpEXE1UmXEGlgc_2mbZFgQUNQXM4KpGF_sE1oMdHbN4Vda-lScEFjyrmqpQ5vFSrp_CUe_zwuewypC2lWtuCoH54D1m-8GFFizkuERwJn1qu1-122dgP4G_VZTwUfvhzYe9LStiUMkfgbdFKIUdXm8BPtmWBf_EOFvjNboTvZFxDvilIhIoa5zdcYcaoz7t9UBHtEbP84OAY1QtYsNMcT5P_av6A7A2cUH1kTvbg6jYp4cacnU0Q");
            HttpWebResponse endpointResponse = (HttpWebResponse)endpointRequest.GetResponse();

            StreamReader sr = new StreamReader(endpointResponse.GetResponseStream());
            string resString = sr.ReadToEnd();

            var json = JToken.Parse(resString);
            var res = json["value"].Children().ToList();

            foreach (var item in res)
            {
                var list = JsonConvert.DeserializeObject<BriefingViewModel>(item.ToString());
                _list.Add(list);
            }
        }
    }
}