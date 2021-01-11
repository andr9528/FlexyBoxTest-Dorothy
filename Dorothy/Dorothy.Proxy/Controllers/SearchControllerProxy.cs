﻿using System;
using System.Collections.Generic;
using System.Text;
using Wolf.Utility.Core.Web;
using Dorothy.Core;
using Dorothy.Core.Controllers;
using Dorothy.Proxy.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wolf.Utility.Core.Persistence.EntityFramework.Core;
using RestSharp;
using Wolf.Utility.Core.Extensions.Methods;
using Wolf.Utility.Core.Exceptions;
using Newtonsoft.Json;
using System.Linq;
using System.IO;
using Dorothy.Core.Models;

namespace Dorothy.Proxy.Controllers
{
    /// <summary>
    /// This Proxy should be usable by any type of client, be it Web, Xamarin, Console, Wpf etc.
    /// Performing the Web search on an Api, enables previously not supported platforms to do the search.
    /// </summary>
    public class SearchControllerProxy : ControllerProxy, ISearchController<ResultProxy, SearchProxy>
    {
        public delegate void SearchComplete(SearchProxy search);
        public event SearchComplete OnSearchComplete;

        public SearchControllerProxy(IHandler handler) : base($"https://localhost:5001/", Core.Enums.Controllers.Search.ToString(), handler)
        {

        }

        public async Task<ActionResult<List<ResultProxy>>> GetResults(SearchProxy search)
        {
            try
            {
                if (search.ExecutedAt == default) search.ExecutedAt = DateTime.Now;

                // Adds the Search to database, and retrieves it again, to get version with Id generated by the database.
                _ = await handler.Add(search);
                search = await handler.Find(new SearchProxy()
                {
                    Target = search.Target,
                    DesiredAmount = search.DesiredAmount,
                    Term = search.Term,
                    Path = search.Path,
                    ExecutedAt = search.ExecutedAt
                });

                var results = new List<ResultProxy>();
                Task<ResultProxy> web = null;
                Task<ResultProxy> files = null;
                Task<ResultProxy> folders = null;

                // Initiats different tasks to search specified areas.
                switch (search.Target)
                {
                    case Core.Enums.SearchTarget.All:
                        web = SearchWeb(search);
                        files = SearchFiles(search);
                        folders = SearchFolders(search);
                        break;
                    case Core.Enums.SearchTarget.WebAndFiles:
                        web = SearchWeb(search);
                        files = SearchFiles(search);
                        break;
                    case Core.Enums.SearchTarget.WebAndFolder:
                        web = SearchWeb(search);
                        folders = SearchFolders(search);
                        break;
                    case Core.Enums.SearchTarget.FilesAndFolders:
                        files = SearchFiles(search);
                        folders = SearchFolders(search);
                        break;
                    case Core.Enums.SearchTarget.Web:
                        web = SearchWeb(search);
                        break;
                    case Core.Enums.SearchTarget.Files:
                        files = SearchFiles(search);
                        break;
                    case Core.Enums.SearchTarget.Folders:
                        folders = SearchFolders(search);
                        break;
                    default:
                        break;
                }

                // Awaits the different tasks, and adds the ResultProxy to database and return list.
                // A Check to ensure the tasks have completed succefully, would be an possible improvement
                if (web != null)
                {
                    var awaitedWeb = await web;
                    _ = await handler.Add(awaitedWeb);
                    results.Add(awaitedWeb);
                }

                if (files != null)
                {
                    var awaitedFiles = await files;
                    _ = await handler.Add(awaitedFiles);
                    results.Add(awaitedFiles);
                }

                if (folders != null)
                {
                    var awaitedFolders = await folders;
                    _ = await handler.Add(awaitedFolders);
                    results.Add(awaitedFolders);
                }

                OnSearchComplete?.Invoke(search);
                return new OkObjectResult(results);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private async Task<ResultProxy> SearchWeb(SearchProxy search) 
        {
            var request = new RestRequest(Method.GET);
            
            // I specify which properties i care about, and only add these to the Query. 
            // There is no need to send more information than needed on the Api
            var properties = new string[] { nameof(search.DesiredAmount), nameof(search.Term), nameof(search.Id) };
            request.AddQueryParametersFromObject(search, properties);            

            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessful)
            {
                var json = response.Content;
                var deserialized = JsonConvert.DeserializeObject<List<ResultProxy>>(json);
                if (deserialized.Count == 0) throw new TaskFailedException(GetType().GetMethod(nameof(SearchWeb)), 
                    $"Succesfully deserialized response content to a List of ResultProxies, but list contains no elements");
                
                // Having checked for there to be 0 elements and thrown in that case, i know at this point there is at least one element in the list. 
                // The list should only contain one element.
                var result = deserialized.First();

                return result;
            }
            else throw new TaskFailedException(GetType().GetMethod(nameof(SearchWeb)), $"Should have searched the net for the specified search term, " +
                $"but did not receive a 'OK' status code from request. The returned status code is: {response.StatusCode}");
        }

        private async Task<ResultProxy> SearchFiles(SearchProxy search)
        {
            if (search.IncludeSubFolders == null)
                throw new NullReferenceException($"Property {nameof(search.IncludeSubFolders)} of {nameof(search)} Cannot be null during {nameof(SearchFiles)}");
            
            var options = search.IncludeSubFolders == true ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            var allFiles = Directory.GetFiles(search.Path, "*.*", options);
            var results = new List<ResultStringProxy>();

            foreach (var file in allFiles)
            {
                var info = new FileInfo(file);

                if (info.Name.Contains(search.Term)) 
                {
                    var obj = new ResultStringProxy() { Path = info.FullName };
                    results.Add(obj);
                }
            }
            var result = new ResultProxy() { SearchId = search.Id, Results = new List<IResultString>(results), ResultType = Core.Enums.ResultType.Files, ToltalResults = allFiles.Length };
            return result;
        }

        private async Task<ResultProxy> SearchFolders(SearchProxy search)
        {
            if (search.IncludeSubFolders == null)
                throw new NullReferenceException($"Property {nameof(search.IncludeSubFolders)} of {nameof(search)} Cannot be null during {nameof(SearchFolders)}");

            var options = search.IncludeSubFolders == true ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            var allfolders = Directory.GetDirectories(search.Path, "*", options);
            var results = new List<ResultStringProxy>();

            foreach (var folder in allfolders)
            {
                var info = new FileInfo(folder);

                if (info.Name.Contains(search.Term))
                {
                    var obj = new ResultStringProxy() { Path = info.FullName };
                    results.Add(obj);
                }
            }

            var result = new ResultProxy() { SearchId = search.Id, Results = new List<IResultString>(results), ResultType = Core.Enums.ResultType.Folder, ToltalResults = allfolders.Length };
            return result;
        }
    }
}
