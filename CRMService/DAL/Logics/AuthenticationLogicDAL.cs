using DAL.DTO;
using DAL.Models;
using Microsoft.EntityFrameworkCore;


namespace DAL.Logics
{
    public class AuthenticationLogicDAL
    {
        CRMDbContext Context;
        public AuthenticationLogicDAL(CRMDbContext context) { 
        Context = context;
        }
        public UsersDto CanLogin(string userName, string password)
        {
         return  Context.Users.Where(x => x.UserName == userName && x.Password == password)?.Select(x =>
               new UsersDto {
                   FirstName = x.FirstName,
                   SecondName=x.SecondName,
                   UserName=x.UserName,
                   Id=x.Id
               }
             ).FirstOrDefault();
        }
    }
}
