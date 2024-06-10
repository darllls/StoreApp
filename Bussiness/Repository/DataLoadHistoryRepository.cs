using AutoMapper;
using DataContextMetadata.Models;
using DTOs;
using Microsoft.EntityFrameworkCore;
using Business.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataContextMetadata.Data;

namespace Business.Repository
{
    public class DataLoadHistoryRepository : IDataLoadHistoryRepository
    {
        private readonly StoreMetadataContext _context;
        private readonly IMapper _mapper;

        public DataLoadHistoryRepository(StoreMetadataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<DataLoadHistoryDTO>> GetAllDataLoadHistoriesAsync()
        {
            var entities = await _context.Dataloadhistories.ToListAsync();
            return _mapper.Map<List<DataLoadHistoryDTO>>(entities);
        }

        public async Task<DataLoadHistoryDTO> GetLatestDataLoadHistoryAsync()
        {
            var entity = await _context.Dataloadhistories
                .OrderByDescending(dh => dh.LoadDatetime)
                .FirstOrDefaultAsync();
            return _mapper.Map<DataLoadHistoryDTO>(entity);
        }
    }
}
