using BusinessLayer.Models;
using DAL.DTO;
using DAL.Logics;
namespace BusinessLayer.Logics
{
    public class AuthenticationLogic
    {
        AuthenticationLogicDAL dalLogic;
        public AuthenticationLogic(AuthenticationLogicDAL Logic) { 
        dalLogic = Logic;
        }
        public UsersDto CanLogin(string userName, string password)
        {
          return  dalLogic.CanLogin(userName, password);
        }
    }
}
