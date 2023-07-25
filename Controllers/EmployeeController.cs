using Microsoft.AspNetCore.Mvc;
using TaskSystem.Domain.Models;
using TaskSystem.Application.ViewModel;
using Microsoft.AspNetCore.Authorization;

namespace TaskSystem.Controllers
{

    [ApiController]
    [Route("api/v1/employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeRepository employeeRepository, ILogger<EmployeeController> logger)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [Authorize]
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
        public IActionResult Get(int pageNumber, int pageQuantity)
        {
            // _logger.Log(LogLevel.Error, "Errorrrrrr");

            // throw new Exception("Erro de teste");

            var employees = _employeeRepository.Get(pageNumber, pageQuantity);

            return Ok(employees);
        }


        [Authorize]
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