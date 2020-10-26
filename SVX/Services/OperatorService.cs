using Microsoft.EntityFrameworkCore;
using SVX.Data;
using SVX.Models;
using SVX.Services.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SVX.Services {
    public class OperatorService {
        private readonly SVXContext _context;
        public OperatorService(SVXContext context) {
            _context = context;
        }
        public async Task<List<Operator>> FindAllAsync() {
            return await _context.Operator.OrderBy(x => x.Name).ToListAsync();
        }

       public async Task<List<Operator>> FindByNameAsync(string name) {
            var result = from obj in _context.Operator select obj;       
            result = result.Where(a => Regex.IsMatch(a.Name, name));
            return await result.ToListAsync(); 
        }

        public async Task<Operator> FindByIdAsync(int id) {
            //Aplicando InnerJoin (Include) entre Server e Operators
            return await _context.Operator.FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task InsertAsync(Operator obj) {
            //obj.Operators = _context.Operators.First(); // Não necessário pois foi criado o atributo OperatorsID 
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id) {
            // Try = Tratando erro de chave estrangeira
            try {
                var obj = await _context.Operator.FindAsync(id);
                _context.Operator.Remove(obj);
                await _context.SaveChangesAsync();
            } catch (DbUpdateException) {
                throw new IntegrityExceptions("This Operators is being used by another table");
            }
        }

        public async Task UpdateAsync(Operator obj) {
            bool hasAny = await _context.Operator.AnyAsync(x => x.Id == obj.Id);
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
