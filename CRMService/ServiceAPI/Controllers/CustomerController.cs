using BusinessLayer.Logics;
using DAL.DTO;
using DAL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        ILogger<CustomerController> logger;
        UnitOfWork unitOfWork;
        CustomerLogic customerLogic;
        public CustomerController(ILogger<CustomerController> _logger,UnitOfWork ofWork,CustomerLogic _logic)
        {
            logger = _logger;
            unitOfWork = ofWork;
            customerLogic = _logic;
        }

        [HttpGet("GetRawAllCustomers")]
        public IActionResult GetRawAllCustomers()
        {
            return Ok(unitOfWork.CustomerRepository.GetAll());
        }
        [HttpGet("GetAllCustomers")]
        public IActionResult GetAllCustomers()
        {
           var rslt= customerLogic.GetAll();
            return Ok(rslt);
        }

        [HttpGet("GetCustomersByName")]
        public IActionResult GetCustomersByName([FromQuery] string searchName)
        {
            var rslt = customerLogic.SearchByName(searchName);
            return Ok(rslt);
        }

        [HttpGet("GetCustomersByGender")]
        public IActionResult GetCustomersByGender([FromQuery] string searchName)
        {
            var rslt = customerLogic.SearchByGender(searchName);
            return Ok(rslt);
        }

        [HttpPost("CreateOrUpdateCustomer")]
        public IActionResult CreateOrUpdateCustomer([FromBody] CustomerDto customer)
        {
            var validationresult = validateCustomer(customer);
            if (string.IsNullOrEmpty(validationresult))
            {
                var rslt = customerLogic.InsertUpdateCustomer(customer);
                return Ok(rslt);
            }
            throw new InvalidOperationException(validationresult);
        }

        [HttpDelete("DeleteCustomer")]
        public IActionResult DeleteCustomer([FromQuery] int custNo) {
            if (custNo > 0)
                return Ok(customerLogic.DeleteCustomer(custNo));
            else
                throw new InvalidOperationException("Customer number must be greather than zero");
        }


        private string validateCustomer(CustomerDto customer) {
            if (string.IsNullOrEmpty(customer.CustomerName))
                return "Customer name is empty";
            if (string.IsNullOrEmpty(customer.Gender))
                return "Gender is empty";
            return string.Empty;
        }
    }
}
