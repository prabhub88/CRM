using DAL.Models;

namespace DAL.DTO
{
    public class CustomerDto
    {

        public long CustomerNumber { get; set; }

        public string? CustomerName { get; set; }

        public DateOnly? Dob { get; set; }

        public string? Gender { get; set; }

    }
}
