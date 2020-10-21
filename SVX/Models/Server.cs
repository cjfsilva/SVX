namespace SVX.Models {
    public class Server {
        public int Id { get; set; }
        public Client Client { get; set; }
        public string Type { get; set; }
        public string Hardware { get; set; }
        public string Hostname { get; set; }
        public string Ip { get; set; }
        public int VersionSVX { get; set; }
        public string Os { get; set; }
        public string Database { get; set; }
        public string Perl { get; set; }
        public string Application { get; set; }
    }
}
