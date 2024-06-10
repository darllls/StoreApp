using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataContext.Data;
using Business.Repository.IRepository;

namespace Business.Repository
{
    public class ProcedureRepository : IProcedureRepository
    {
        private readonly StoreDbContext _context;

        public ProcedureRepository(StoreDbContext context)
        {
            _context = context;
        }

        public async Task ClearWarehouseAsync()
        {
            await _context.Database.ExecuteSqlRawAsync("EXECUTE [dbo].[ClearWarehouseProcedure]");
        }

        public async Task PrimaryFillStagingAsync()
        {
            await _context.Database.ExecuteSqlRawAsync("EXECUTE [dbo].[PrimaryFillStagingProcedure]");
        }

        public async Task PrimaryFillWarehouseAsync()
        {
            await _context.Database.ExecuteSqlRawAsync("EXECUTE [dbo].[PrimaryFillWarehouseProcedure]");
        }

        public async Task IncrementalFillStagingAsync()
        {
            await _context.Database.ExecuteSqlRawAsync("EXECUTE [dbo].[IncrementalFillStagingProcedure]");
        }

        public async Task IncrementalFillWarehouseAsync()
        {
            await _context.Database.ExecuteSqlRawAsync("EXECUTE [dbo].[IncrementalFillWarehouseProcedure]");
        }
    }
}
