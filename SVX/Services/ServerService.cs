using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using SVX.Data;
using SVX.Models;
using SVX.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

namespace SVX.Services {
    public class ServerService {
        private readonly SVXContext _context;

        public ServerService(SVXContext context) {
            _context = context;
        }

        public List<Server> FindAll() {
            return _context.Server.ToList();
        }

        public void Insert(Server obj) {
            //obj.Client = _context.Client.First(); // Não necessário pois foi criado o atributo ClientID 
            _context.Add(obj);
            _context.SaveChanges();
        }

        public Server FindById(int id) {
            //Aplicando InnerJoin (Include) entre Server e Client
            return _context.Server.Include(obj => obj.Client).FirstOrDefault(obj => obj.Id == id);
        }

        public void Remove(int id) {
            var obj = _context.Server.Find(id);
            _context.Server.Remove(obj);
            _context.SaveChanges();
        }

        public void Update(Server obj) {
            if (!_context.Server.Any(x => x.Id == obj.Id)) {
                throw new NotFoundException("Id not found");                           
            }
            try {
                _context.Update(obj);
                _context.SaveChanges();
            } catch (DbUpdateConcurrencyException e) {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
