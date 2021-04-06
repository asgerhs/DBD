using System.Linq;
using assignment_1.DataModels;
using Microsoft.EntityFrameworkCore;

namespace assignment_1.Persistent.Repositories
{
    public class AddressRepository : BaseRepository<Address>
    { public AddressRepository(DatabaseContext context) : base(context)
        {
        }

        #region ApplyRelations
        protected override IQueryable<Address> ApplyRelations(IQueryable<Address> query)
        {
            query = query
                .Include(x => x.city);

            return base.ApplyRelations(query);
        }
        #endregion
    }
}