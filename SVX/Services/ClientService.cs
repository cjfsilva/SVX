using Microsoft.EntityFrameworkCore;
using SVX.Data;
using SVX.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SVX.Services {
    public class ClientService {
        private readonly SVXContext _context;
        public ClientService(SVXContext context) {
            _context = context;
        }
        public async Task<List<Client>> FindAllAsync(){
            return await _context.Client.OrderBy(x => x.Name).ToListAsync();

        }
    }
}
