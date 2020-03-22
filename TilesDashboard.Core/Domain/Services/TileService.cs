using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TilesDashboard.Core.Storage;

namespace TilesDashboard.Core.Domain.Services
{
    public class TileService : ITileService
    {
        private readonly ITileContext _context;

        public TileService(ITileContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<IList<object>> GetAllAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
