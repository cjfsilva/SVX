using System.ComponentModel.DataAnnotations;

namespace SVX.Models {
    public class Server {
        public int Id { get; set; }

        [Required]
        public Client Client { get; set; }

        [Display(Name = "Client Id")]
        public int ClientId { get; set; }

        public string Type { get; set; }
        public string Hardware { get; set; }

        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} size shoud be between {2} and {1}")]
        public string Hostname { get; set; }

        public string Ip { get; set; }

        [Display(Name = "Version SVX")]
        //[Range(0.0, 100.0, ErrorMessage = "{0 must be from {1} to {2}")]
        public double VersionSVX { get; set; }

        public string Os { get; set; }
        public string Database { get; set; }
        public string Perl { get; set; }
        public string Application { get; set; }

        public Server() { }

        public Server(int id, Client client, int clientId, string type, string hardware, string hostname, string ip, double versionSVX, string os, string database, string perl, string application) {
            Id = id;
            Client = client;
            ClientId = clientId;
            Type = type;
            Hardware = hardware;
            Hostname = hostname;
            Ip = ip;
            VersionSVX = versionSVX;
            Os = os;
            Database = database;
            Perl = perl;
            Application = application;
        }
    }
}
