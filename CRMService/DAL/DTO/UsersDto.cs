using DAL.Models;

namespace DAL.DTO
{
    public class UsersDto
    {
        public long Id { get; set; }

        public string UserName { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string? SecondName { get; set; }

        public string UserType { get; set; }
        public virtual UserType UserTypeNavigation { get; set; } = null!;
    }
}
