using System.Threading.Tasks;

namespace assignment_1.Persistent
{
    public class UnitOfWork
    {
        private readonly DatabaseContext databaseContext;

        public UnitOfWork(DatabaseContext databaseContext){
            this.databaseContext = databaseContext;
        }

        public async Task CompleteAsync() {
            await this.databaseContext.SaveChangesAsync();
        }
    }
}