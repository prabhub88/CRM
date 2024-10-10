using System.Data;
using System.Data.SqlClient;
using DAL.DTO;
using DAL.Models;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.Logics
{
    public class CustomerLogicDal
    {
        private CRMDbContext Context;
        private ISqlDataHelper sqlHelper;
        private UnitOfWork unitOfWork;
        public CustomerLogicDal(CRMDbContext _context, ISqlDataHelper DataHelper,UnitOfWork work)
        {
            Context = _context;
            sqlHelper = DataHelper;
            unitOfWork = work;
        }
        public List<CustomerDto> GetAll()
        {
            return Context.Customers.Include(x => x.GenderNavigation)
                .Select(x =>
                  new CustomerDto
                  {
                      CustomerName = x.CustomerName,
                      CustomerNumber = x.CustomerNumber,
                      Gender = x.GenderNavigation.Descriptions,
                      Dob = x.Dob
                  }).ToList();
        }
        public List<CustomerDto> SearchByName(string lookForName)
        {
            return Context.Customers.Include(x => x.GenderNavigation).Where(x => x.CustomerName.Contains(lookForName))
                .Select(x =>
                  new CustomerDto
                  {
                      CustomerName = x.CustomerName,
                      CustomerNumber = x.CustomerNumber,
                      Gender = x.GenderNavigation.Descriptions,
                      Dob = x.Dob
                  }).ToList();
        }

        public List<CustomerDto> SearchByGender(string lookForName)
        {
            return Context.Customers.Include(x => x.GenderNavigation).Where(x => x.GenderNavigation.Descriptions.Contains(lookForName))
                .Select(x =>
                  new CustomerDto
                  {
                      CustomerName = x.CustomerName,
                      CustomerNumber = x.CustomerNumber,
                      Gender = x.GenderNavigation.Descriptions,
                      Dob = x.Dob
                  }).ToList();
        }

        public List<CustomerDto> SearchByCustomerId(int lookForNumber)
        {
            return Context.Customers.Include(x => x.GenderNavigation).Where(x => x.CustomerNumber == lookForNumber)
                .Select(x =>
                  new CustomerDto
                  {
                      CustomerName = x.CustomerName,
                      CustomerNumber = x.CustomerNumber,
                      Gender = x.GenderNavigation.Descriptions,
                      Dob = x.Dob
                  }).ToList();
        }

        public int DeleteCustomer(int customerNo)
        {
            var cust = Context.Customers.Where(x=>x.CustomerNumber == customerNo).FirstOrDefault();
            if (cust != null) 
                { 
                Context.Customers.Remove(cust);
                Context.SaveChanges();
                return 1;
                }
            else return 0;
        }
        public bool InsertUpdateCustomer(CustomerDto customer)
        {
            var dob = Convert.ToDateTime(customer.Dob.Value.ToString()).ToString("yyyy-MM-dd");
            SqlParameter[] sp = new SqlParameter[] {
                    new SqlParameter(parameterName:"@CustomerNo",value:customer.CustomerNumber),
                    new SqlParameter(parameterName:"@CustomerName",value:customer.CustomerName),
                    new SqlParameter(parameterName:"@Dob",value: dob),
                    new SqlParameter(parameterName:"@Gender",value:customer.Gender),
                };
            return sqlHelper.InsertOrUpdate(CRMDbConstants.SP_InsertUpdateCust, sp);
        }

    }
}
