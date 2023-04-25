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
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDB _applicationDB;
        public EmployeeController(ApplicationDB applicationDB)
        {
            _applicationDB = applicationDB;
        }

        [HttpGet]
        public async Task<IActionResult>GetAllEmployee()
        {
            var employee = await _applicationDB.Employees.ToListAsync();
            return Ok(employee);
        }

        [HttpGet("GetEmployeeById")]
        public async Task<IActionResult> GetEmployeeById(int employeeid)
        {
           var empid = await _applicationDB.Employees.Where(x => x.EmployeeId == employeeid).ToListAsync();
            return Ok(empid);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(EmployeeRequest employee1)
        {
            Employee employee = new Employee()
            {
               Name = employee1.Name,
            };
            await _applicationDB.Employees.AddAsync(employee);
            await _applicationDB.SaveChangesAsync();
            return Ok(employee);
        }
        [HttpPut]
        [Route ("Empupdate")]
      
        public async Task<IActionResult> UpdateEmployee(int id, EmployeeRequest employeeRequest)
        {
            var employee = await _applicationDB.Employees.FindAsync(id);
            if (employee != null)
            {
                employee.Name = employeeRequest.Name;

                _applicationDB.Update(employee);
                await _applicationDB.SaveChangesAsync();
                return Ok(employee);
            }
            return NotFound();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee(int id, EmployeeRequest employeeRequest)
        {
            var employee = await _applicationDB.Employees.FindAsync(id);
            if(employee != null)
            {
                employee.Name = employeeRequest.Name;

                _applicationDB.Remove(employee);
                await _applicationDB.SaveChangesAsync();
                return Ok(employee);
            }
            return NotFound();
        }
    }
}
