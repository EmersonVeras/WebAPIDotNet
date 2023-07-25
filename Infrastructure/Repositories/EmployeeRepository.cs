using TaskSystem.Domain.Models;
using TaskSystem.Domain.DTOs;


namespace TaskSystem.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ConnectionContext _context = new ConnectionContext();

        public void Add(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
            
        }

        public List<EmployeeDTO> Get(int pageNumber, int pageQuantity)
        {
            return _context.Employees.Skip(pageNumber*pageQuantity)
            .Take(pageQuantity)
            .Select( employee =>
            new EmployeeDTO()
            {
                Id = employee.id,
                NameEmployee = employee.name,
                Photo = employee.photo,
            }).ToList();
        }

        public Employee Get(int id)
        {
            return _context.Employees.Find(id);
        }
    }
}