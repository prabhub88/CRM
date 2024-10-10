using DAL.DTO;
using DAL.Logics;

namespace BusinessLayer.Logics
{
    public class CustomerLogic
    {
        public CustomerLogicDal logic;
        public CustomerLogic(CustomerLogicDal _logic)
        {
            logic = _logic;
        }
        public List<CustomerDto> GetAll() { 
        return logic.GetAll();
        }
        public List<CustomerDto> SearchByName(string lookForName)
        {
            return logic.SearchByName(lookForName);
        }

        public List<CustomerDto> SearchByGender(string lookForName)
        {
            return logic.SearchByGender(lookForName);
        }

        public List<CustomerDto> SearchByCustomerId(int lookForNumber)
        {
            return logic.SearchByCustomerId(lookForNumber);
        }

        public string InsertUpdateCustomer(CustomerDto customer)
        {
            return logic.InsertUpdateCustomer(customer) ? "Successfully Created" : "Failed to create";
        }
        public string DeleteCustomer(int customerNo)
        {
            return logic.DeleteCustomer(customerNo) == 1 ? "Successfully Deleted" : "Failed to Delete";
        }
    }
}
