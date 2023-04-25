using Mapping.Data;
using Mapping.Model;
using Mapping.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mapping.Controllers
{
    public class CustomerAndCusAddController : ControllerBase
    {
        private readonly ApplicationDB _applicationDB;
        public CustomerAndCusAddController(ApplicationDB applicationDB)
        {
            _applicationDB = applicationDB;
        }

        [HttpGet("GetAllCustomer")]
        public async Task<IActionResult> GetAllCustomer()
        {
            var customers = await (from c in _applicationDB.Customers
                                   select new
                                   {
                                       Id = c.Id,
                                       FirstName = c.FirstName,
                                       LastName = c.LastName,
                                       Phone = c.Phone,
                                   }).ToListAsync();
            return Ok(customers);
        }

        [HttpGet("GetAllCustomerAddresses")]
        public async Task<IActionResult> GetAllCustomerAddresses()
        {
            var customerAddresses = await (from ca in _applicationDB.CustomersAddresses
                                           select new
                                           {
                                               Id = ca.Id,
                                               Country = ca.Country,
                                               City = ca.City,
                                           }).ToListAsync();
            return Ok(customerAddresses);
        }

        [HttpGet("GetCustomerById")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var customer = await (from c in _applicationDB.Customers
                                  select new
                                  {
                                      Id = c.Id,
                                      FirstName = c.FirstName,
                                      LastName = c.LastName,
                                      Phone = c.Phone,
                                  }).FirstOrDefaultAsync(x => x.Id == id);
            return Ok(customer);
        }

        [HttpGet("GetAddressById")]
        public async Task<IActionResult> GetAddressById(int id)
        {
            var address = await (from ad in _applicationDB.CustomersAddresses
                                 select new
                                 {
                                     Id = ad.Id,
                                     Country = ad.Country,
                                     City = ad.City,
                                 }).FirstOrDefaultAsync(x => x.Id == id);
            return Ok(address);
        }

        [HttpGet("GetAllDetails")]
        public async Task<IActionResult> GetAllDetails()
        {
            var customer = await _applicationDB.Customers.Include(x => x.CustomerAddresses).ToListAsync();
            return Ok(customer);
        }

        [HttpPost("CreateCustomer")]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerRequest customerRequest)
        {
            Customer customer = new Customer()
            {
                FirstName = customerRequest.FirstName,
                LastName = customerRequest.LastName,
                Phone = customerRequest.Phone,
            };
            await _applicationDB.Customers.AddAsync(customer);
            await _applicationDB.SaveChangesAsync();
            return Ok(customer);
        }

        [HttpPost("CreateAddresses")]
        public async Task<IActionResult> CreateAddresses([FromBody] CustomerAddressesRequest customerAddressesRequest)
        {
            CustomerAddresses customerAddresses = new CustomerAddresses()
            {
                City = customerAddressesRequest.City,
                Country = customerAddressesRequest.Country,
                CustomerId = customerAddressesRequest.CustomerId,
            };
            await _applicationDB.AddAsync(customerAddresses);
            await _applicationDB.SaveChangesAsync();
            return Ok(customerAddresses);
        }

        [HttpPut("UpdateCustomer")]
        public async Task<IActionResult>UpdateCustomer(int id, CustomerRequest customerRequest)
        {
            var customer = await _applicationDB.Customers.FindAsync(id);
            if(customer != null)
            {
                customer.FirstName = customerRequest.FirstName;
                customer.LastName = customerRequest.LastName;
                customer.Phone = customerRequest.Phone;

                _applicationDB.Update(customer);
                await _applicationDB.SaveChangesAsync();
                return Ok(customer);
            }
            return NotFound();
        }

        [HttpPut("UpdateCustomerAddress")]
        public async Task<IActionResult>UpdateCustomerAddress(int id, CustomerAddressesRequest customerAddressesRequest)
        {
            var CusAdd = await _applicationDB.CustomersAddresses.FindAsync(id);
            if(CusAdd != null)
            {
                CusAdd.City = customerAddressesRequest.City;
                CusAdd.Country = customerAddressesRequest.Country;
                CusAdd.CustomerId = customerAddressesRequest.CustomerId;
                _applicationDB.Update(CusAdd);
                await _applicationDB.SaveChangesAsync();
                return Ok(CusAdd);
            }
            return NotFound();
        }

        [HttpDelete("DeleteCustomer")]
        public async Task<IActionResult> DeleteCustomer(int id )
        {
            var customer = await _applicationDB.Customers.FindAsync(id);
            if(customer != null)
            {          
                _applicationDB.Remove(customer);    
                await _applicationDB.SaveChangesAsync();
                return Ok(customer);
            }
            return NotFound();
        }

        [HttpDelete("DeleteCustomerAddress")]
        public async Task<IActionResult> DeleteCustomerAddress(int id)
        {
            var CusAdd = await _applicationDB.CustomersAddresses.FindAsync(id);
            if(CusAdd != null)
            {
                _applicationDB.Remove(CusAdd);
                await _applicationDB.SaveChangesAsync();
                return Ok(CusAdd);
            }
            return NotFound();
        }
    }
}