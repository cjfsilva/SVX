namespace SVX.Models {
    public class Server {
        public int Id { get; set; }
        public Client Client { get; set; }
        public int ClientId { get; set; }
        public string Type { get; set; }
        public string Hardware { get; set; }
        public string Hostname { get; set; }
        public string Ip { get; set; }
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
