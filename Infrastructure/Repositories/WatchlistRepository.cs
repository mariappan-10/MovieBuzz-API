using Core.Entities;
using Core.ServiceContracts;
using Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Core.RepositoriesContracts;

namespace Infrastructure.Repositories
{
    public class WatchlistRepository : IWatchlistRepository
    {
        private readonly ApplicationDbContext _context;
        public WatchlistRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsAsync(Guid userId, string imdbId)
        {
            return await _context.WatchListDatas
                .AnyAsync(w => w.ApplicationUserId == userId && w.imdbId == imdbId);
        }

        public async Task AddAsync(WatchListData item)
        {
            _context.WatchListDatas.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task<List<string>> GetByUserIdAsync(Guid userId)
        {
            return await _context.WatchListDatas
         .Where(w => w.ApplicationUserId == userId)
         .Select(w => w.imdbId)
         .ToListAsync();
        }

        public async Task RemoveAsync(Guid userId, string imdbId)
        {
            var item = await _context.WatchListDatas
                .FirstOrDefaultAsync(w => w.ApplicationUserId == userId && w.imdbId == imdbId);
            if (item != null)
            {
                _context.WatchListDatas.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<WatchListData>> GetAllAsync()
        {
            return await _context.WatchListDatas.ToListAsync();
        }
    }
}
