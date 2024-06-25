namespace EmedicineEB.Controllers.Models
{
    public class Cart
    {

        public int ID { get; set; }

        public string Email { get; set; }


        public int UserId { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Discount { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }

        public int MedicineId { get; set; }


        public string? ImageUrl { get; set; }
        public string? Manufacturer { get; internal set; }
        public string? MedicineName { get; internal set; }
    }
}
