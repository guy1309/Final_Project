namespace EmedicineEB.Controllers.Models
{
    public class Users
    {
        internal object? actionType;

        public Users()
        {
        }

        public Users(object? actionType)
        {
            this.actionType = actionType;
        }

        public int ID { get; set; }
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Password { get; set;  }

        public string? Email { get; set; }   

        public decimal  Fund { get; set; }   

        public string? Type { get; set; }

        public int Status { get; set; }

        public DateTime CreatedOn { get; set; }
        public string? OrderDate { get; internal set; }

    }
}
