using Microsoft.AspNetCore.Mvc;
using TaskSystem.Models;
using TaskSystem.ViewModel;

namespace TaskSystem.Controllers
{

    [ApiController]
    [Route("api/v1/employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
        }

        [HttpPost]
        public IActionResult Add(EmployeeViewModel employeeView)
        {
            var employee = new Employee(
                employeeView.name,
                employeeView.age,
                null
                );

                _employeeRepository.Add(employee);

                return Ok();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var employees = _employeeRepository.Get();

            return Ok(employees);
        }
    }
}