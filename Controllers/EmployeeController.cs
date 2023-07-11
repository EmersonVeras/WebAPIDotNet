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
        public IActionResult Add([FromForm] EmployeeViewModel employeeView)
        {
            var filePath = Path.Combine("Storage", employeeView.photo.FileName);

            using Stream filestream = new FileStream(filePath, FileMode.Create);
            
            employeeView.photo.CopyTo(filestream);

            var employee = new Employee(
                employeeView.name,
                employeeView.age,
                filePath
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


        [HttpPost]
        [Route("{id}/download")]
        public IActionResult DownloadPhoto(int id)
        {
            var employee = _employeeRepository.Get(id);

            var dataBytes = System.IO.File.ReadAllBytes(employee.photo);
            
            return File(dataBytes, "image/png");
        }

    }
}