using Microsoft.EntityFrameworkCore;
using SVX.Data;
using SVX.Models;
using SVX.Services.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SVX.Services {
    public class ServerService {
        private readonly SVXContext _context;

        public ServerService(SVXContext context) {
            _context = context;
        }

        public async Task<List<Server>> FindAllAsync() {
            return await _context.Server.Include(obj => obj.Client).ToListAsync();
            //return await _context.Server.Include(obj => obj.Client).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task<List<Server>> FindByNameAsync(string name) {
            var result = from obj in _context.Server.Include(obj => obj.Client) select obj;
            result = result.Where(a => Regex.IsMatch(a.Client.Name, name));
            return await result.ToListAsync();          
        }

        public async Task<Server> FindByIdAsync(int id) {
            //Aplicando InnerJoin (Include) entre Server e Client
            return await _context.Server.Include(obj => obj.Client).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoveAsync(int id) {
            var obj = await _context.Server.FindAsync(id);
            _context.Server.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task InsertAsync(Server obj) {
            //obj.Client = _context.Client.First(); // Não necessário pois foi criado o atributo ClientID 
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Server obj) {
            bool hasAny = await _context.Server.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny) {
                throw new NotFoundException("Id not found");                           
            }
            try {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException e) {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
