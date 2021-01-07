using Dorothy.WebSearch.Models;
using Google.Apis.Customsearch.v1;
using Google.Apis.Services;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Wolf.Utility.Core.Persistence.EntityFramework.Core;
using Wolf.Utility.Core.Web;

namespace Dorothy.WebSearch.Controllers
{
    /// <summary>
    /// Performs Web searches usings Googles CustomsearchService. 
    /// The Nuget package used for CustomsearchService doesn't support Xamarin.
    /// As Xamarin can make Web Api request, it can however still use CustomsearchService, by calling an Api that does the search.
    /// This is just an example as to why having the search on an Api is a benefit.
    /// </summary>
    public class GoogleProxy
    {
        const string ApiKey = "AIzaSyCTeuWYOhb7BHonC9i2MTf7np4PW32vn6w";
        const string Cx = "1e1b51b3a0786699b";

        public async Task<(List<ResultString>, int)> Search(string term, int desiredAmount) 
        {
            var service = new CustomsearchService();
            var listRequest = service.Cse.List();

            listRequest.Key = ApiKey;
            listRequest.Cx = Cx;
            listRequest.Q = term;

            Google.Apis.Customsearch.v1.Data.Search search = await listRequest.ExecuteAsync();
            int.TryParse(search.SearchInformation.TotalResults, out var total);
            var results = new List<ResultString>();

            while (results.Count != desiredAmount) 
            {
                for (int i = 0; i < search.Items.Count; i++)
                {
                    if (results.Count == desiredAmount) break;
                    var item = search.Items[i];

                    var obj = new ResultString()
                    {
                        Title = item.Title,
                        Link = item.Link,
                        Snippet = item.Snippet
                    };
                    results.Add(obj);
                }

                if (results.Count != desiredAmount) 
                {
                    var index = results.Count + 1;
                    if (index > 91) break;
                    listRequest.Start = index;
                    search = await listRequest.ExecuteAsync();
                }
            }

            return (results, total);
        }        
    }
}
