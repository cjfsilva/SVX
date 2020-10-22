using SVX.Data;
using SVX.Models;
using System.Collections.Generic;
using System.Linq;

namespace SVX.Services {
    public class ClientService {
        private readonly SVXContext _context;
        public ClientService(SVXContext context) {
            _context = context;
        }
        public List<Client> FindAll(){
            return _context.Client.OrderBy(x => x.Name).ToList();

        }
    }
}
