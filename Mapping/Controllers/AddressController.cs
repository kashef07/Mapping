using Mapping.Data;
using Mapping.Model;
using Mapping.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mapping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly ApplicationDB _applicationDB;
        public AddressController(ApplicationDB applicationDB)
        {
            _applicationDB = applicationDB;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAddress()
        {
            var address = await _applicationDB.Addresses.ToListAsync();
            return Ok(address);
        }

        [HttpGet("GetAddressById")]
        public async Task<IActionResult> GetAddressById(int adressid)
        {
            var address = await _applicationDB.Addresses.Where(x => x.AddressId == adressid).ToListAsync();
            return Ok(address);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAddress(AddressRequest addressRequest)
        {
            Address address = new Address()
            {
                AddressDetails = addressRequest.AddressDetails,
                City = addressRequest.City,
                ZipCode = addressRequest.ZipCode,
                State = addressRequest.State,
                Country = addressRequest.Country,
            };
            await _applicationDB.Addresses.AddAsync(address);
            await _applicationDB.SaveChangesAsync();
            return Ok(address);
        }
    }
}
