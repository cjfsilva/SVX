namespace SVX.Models {
    public class Operator {
        public int Id { get; set; }
        public string Name { get; set; }

        public Operator() { }

        public Operator(int id, string name) {
            Id = id;
            Name = name;
        }
    }
}
