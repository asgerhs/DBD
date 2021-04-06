using System.Threading.Tasks;
using assignment_1.DataModels;
using Microsoft.EntityFrameworkCore;

namespace assignment_1.Persistent.Repositories
{
    public class CityRepository : BaseRepository<City>
    {
        public CityRepository(DatabaseContext context) : base(context) {}
        
        #region GetByZip
        public async Task<City> GetByZipAsync(int zip, bool includeRelated = false)
        {
            if (!includeRelated)
                return await this.databaseContext.cities.SingleOrDefaultAsync(x => x.zip == zip);

            var query = this.databaseContext.cities.AsQueryable();

            query = this.ApplyRelations(query);

            return await query.SingleOrDefaultAsync(x => x.zip == zip);
        }
        #endregion
    }
}