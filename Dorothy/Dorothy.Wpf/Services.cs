using Dorothy.Proxy.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Wolf.Utility.Core.Persistence.EntityFramework.Core;
using Dorothy.Core.Controllers;
using Dorothy.Proxy.Models;
using Dorothy.Proxy.Controllers;
using Microsoft.EntityFrameworkCore;

namespace Dorothy.Wpf
{
    public class Services
    {
        /// <summary>
        /// Gets the instance of Services. If it hasn't yet been called will create an instance, and then return it.
        /// </summary>
        public static Services Instance
        {
            get
            {
                if (services != null) return services;
                Initialize();
                return services;
            }
        }

        private static Services services { get; set; }

        private static void Initialize()
        {
            if (services == null) services = new Services();
        }
        private Services()
        {
            Context = new SuperSearchContext();
            Context.Database.Migrate();

            Handler = new SuperSearchHandler(Context);
            SearchController = new SearchControllerProxy(Handler);
        }

        /// <summary>
        /// Handles interaction with the Database.
        /// </summary>
        public IHandler Handler { get; set; }
        /// <summary>
        /// Handles Communication with the Search Api, when needed. 
        /// Also handles the searching on the device. 
        /// Finaly ensures any search performed are saved to database, along with any result that are found. This is done via the Handler.
        /// </summary>
        public ISearchController<ResultProxy, SearchProxy> SearchController{ get; set;}

        private SuperSearchContext Context { get; set; }
    }
}
