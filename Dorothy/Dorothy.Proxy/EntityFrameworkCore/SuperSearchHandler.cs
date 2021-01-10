using Dorothy.Core.Models;
using Dorothy.Proxy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wolf.Utility.Core.Persistence.EntityFramework;

namespace Dorothy.Proxy.EntityFrameworkCore
{
    /// <summary>
    /// The base handler handles most of the work with CRUD.
    /// It verifies that states change appropiatly when performing the CRUD operations.
    /// The BaseHandler's public CRUD operations only performs verification that the input is valid.
    /// The Operations then pass thier inputs onto Virtual methods that performs the CRUD operation, and verifies the states.
    /// Due to these methods being virtual, the way something is CRUD'ed and verified, can be overwritten, in this class.
    /// Only the Read methods make use of an Apstract method, as this is the one that generates the queries.
    /// </summary>
    public class SuperSearchHandler : BaseHandler<SuperSearchContext>
    {
        

        public SuperSearchHandler(SuperSearchContext context) : base(context)
        {
        }

        protected async override Task<IQueryable<T>> AbstractFind<T>(T predicate)
        {
            IQueryable<T> query = null;

            switch (predicate)
            {
                case ISearch s:
                    query = BuildQuery(s, Context.Searches.AsQueryable()) as IQueryable<T>;
                    break;
                case IResult r:
                    query = BuildQuery(r, Context.Results.AsQueryable()) as IQueryable<T>;
                    break;
                case IResultString rs:
                    query = BuildQuery(rs, Context.ResultStrings.AsQueryable()) as IQueryable<T>;
                    break;
                default:
                    break;
            }

            return query;
        }

        /// <summary>
        /// Check properties of the predicate, of they are set to a value other than the default for the type.
        /// If so it adds a where clause to the query, that includes the property in the search.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        private IQueryable<ISearch> BuildQuery(ISearch predicate, IQueryable<SearchProxy> query) 
        {
            if (predicate.Term != default)
                query = query.Where(x => x.Term.Contains(predicate.Term));
            if (predicate.Path != default)
                query = query.Where(x => x.Path.Contains(predicate.Path));

            if (predicate.DesiredAmount != default)
                query = query.Where(x => x.DesiredAmount == predicate.DesiredAmount);
            if (predicate.Target != default)
                query = query.Where(x => x.Target == predicate.Target);

            // Default of Nullable Bool, is Null, which is the value it will have if the target is Web
            if (predicate.IncludeSubFolders != default && predicate.Target != Core.Enums.SearchTarget.Web)
                query = query.Where(x => x.IncludeSubFolders == predicate.IncludeSubFolders);

            if (predicate.ExecutedAt != default)
                query = query.Where(x => x.ExecutedAt == predicate.ExecutedAt);

            return query;
        }

        /// <summary>
        /// Check properties of the predicate, of they are set to a value other than the default for the type.
        /// If so it adds a where clause to the query, that includes the property in the search.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        private IQueryable<IResult> BuildQuery(IResult predicate, IQueryable<ResultProxy> query)
        {
            if (predicate.RetrievedResults != default)
                query = query.Where(x => x.RetrievedResults == predicate.RetrievedResults);
            if (predicate.SearchId != default)
                query = query.Where(x => x.SearchId == predicate.SearchId);
            if (predicate.ResultType != default)
                query = query.Where(x => x.ResultType == predicate.ResultType);


            return query;
        }

        /// <summary>
        /// Check properties of the predicate, of they are set to a value other than the default for the type.
        /// If so it adds a where clause to the query, that includes the property in the search.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        private IQueryable<IResultString> BuildQuery(IResultString predicate, IQueryable<ResultStringProxy> query)
        {
            if (predicate.Path != default)
                query = query.Where(x => x.Path.Contains(predicate.Path));
            if (predicate.Title != default)
                query = query.Where(x => x.Title.Contains(predicate.Title));
            if (predicate.Link != default)
                query = query.Where(x => x.Link.Contains(predicate.Link));
            if (predicate.Snippet != default)
                query = query.Where(x => x.Snippet.Contains(predicate.Snippet));

            if (predicate.ResultId != default)
                query = query.Where(x => x.ResultId == predicate.ResultId);

            return query;
        }
    }
}
