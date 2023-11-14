using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Data.Model
{
    public class BookingViewModel
    {
        public int UserId { get; set; }

        public string startLocation { get; set; }

        public string endLocation { get; set; }

        public string distance { get; set; }

        public string time { get; set; }

        public decimal? price { get; set; }

        public DateTime? timeBook { get; set; }

        public DateTime? orderDate { get; set; }

        public string status { get; set; }

        public string? DriverId { get; set; }

        public bool? isPaid { get; set; }
        public string? locationIP { get; set; }
    }
}
