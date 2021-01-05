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

        protected override Task<IQueryable<T>> AbstractFind<T>(T predicate)
        {
            throw new NotImplementedException();
        }
    }
}
