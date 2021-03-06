namespace PetShop.Model
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FullName { get { return $"{Name} {Surname}"; } }
        public string Phone { get; set; }
        public string TIN { get; set; }
        public List<Transaction> Transactions { get; set; }
        public Customer()
        {

        }
        public Customer(string name , string surname, string phone , string tin)
        {
            Name = name;
            Surname = surname;
            Phone = phone;
            TIN = tin;
        }
    }
}
