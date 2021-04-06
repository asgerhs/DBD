using System.Linq;
using System.Threading.Tasks;
using assignment_1.DataModels;
using Microsoft.EntityFrameworkCore;

namespace assignment_1.Persistent.Repositories
{
    public class BaseRepository<T>
        where T : BaseModel
    {
        protected readonly DatabaseContext databaseContext;

        public BaseRepository(DatabaseContext databaseContext) {
            this.databaseContext = databaseContext;
        }

        public virtual async Task<T> GetAsync(long entityId, bool includeRelated = false){
            if(!includeRelated)
                return await this.databaseContext.Set<T>().FindAsync(entityId);

            var query = this.databaseContext.Set<T>().AsQueryable();

            query = this.ApplyRelations(query);

            return await query.SingleOrDefaultAsync(x => x.id == entityId);
        }

        #region Filters / Relations
        protected virtual IQueryable<T> ApplyRelations(IQueryable<T> query){
            return query;
        }
        #endregion

        public void Add(T entity) {
            this.databaseContext.Set<T>().Add(entity);
        }
        public void Update(T entity) {
            this.databaseContext.Set<T>().Update(entity);
        }
        public void Remove(T entity) {
            this.databaseContext.Set<T>().Remove(entity);
        }
    }
}