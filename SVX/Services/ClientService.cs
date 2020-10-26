using Microsoft.EntityFrameworkCore;
using SVX.Data;
using SVX.Models;
using SVX.Services.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SVX.Services {
    public class ClientService {
        private readonly SVXContext _context;
        public ClientService(SVXContext context) {
            _context = context;
        }
        public async Task<List<Client>> FindAllAsync() {
            return await _context.Client.OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<List<Client>> FindByNameAsync(string name) {
            var result = from obj in _context.Client select obj;
            result = result.Where(a => Regex.IsMatch(a.Name, name));
            return await result.ToListAsync();
        }

        public async Task<Client> FindByIdAsync(int id) {
            //Aplicando InnerJoin (Include) entre Server e Client
            return await _context.Client.FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task InsertAsync(Client obj) {
            //obj.Client = _context.Client.First(); // Não necessário pois foi criado o atributo ClientID 
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }


        public async Task RemoveAsync(int id) {
            // Try = Tratando erro de chave estrangeira
            try {
                var obj = await _context.Client.FindAsync(id);
                _context.Client.Remove(obj);
                await _context.SaveChangesAsync();
            } catch (DbUpdateException) {
                throw new IntegrityExceptions("This client is being used by another table");
            }
        }

        public async Task UpdateAsync(Client obj) {
            bool hasAny = await _context.Client.AnyAsync(x => x.Id == obj.Id);
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
