using System.Collections.Generic;

namespace SVX.Models {
    public class ServerFormViewModel {
        public Server Server { get; set; }
        public ICollection<Client> Clients { get; set; }    
    }
}
