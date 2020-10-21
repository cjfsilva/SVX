namespace SVX.Models {
    public class Trunk {
        public int Id { get; set; }
        public Client Client { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Ip { get; set; }

        public Trunk(string type, string name, string ip) {
            Type = type;
            Name = name;
            Ip = ip;
        }
    }
}
