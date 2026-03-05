using SimpleStoryMangementSystem.Core.Models;
using SimpleStoryMangementSystem.Services.Interfaces;
using SimpleStoryMangementSystem.Services.Utils;


namespace SimpleStoryMangementSystem.Services.Implementations
{
    
    public class CustomerService : ICustomerService
    {
        private readonly List<Employee> _employees;
        private readonly ExcelHelper _excelHelper;
        private int _nextId;

        public CustomerService()
        {
            _employees = new List<Employee>();
            _excelHelper = new ExcelHelper();
            _nextId = 1;
        }
        public void AddEmployee(Employee employee)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee));

            employee.Id = _nextId++;
            _employees.Add(employee);
        }
        public void DeleteEmployee(int id)
        {
            var employee = _employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
                throw new ArgumentException($"Employee with ID {id} not found.");

            _employees.Remove(employee);
        }
        public void UpdateEmployee(Employee employee)
        {
            var existing = _employees.FirstOrDefault(e => e.Id == employee.Id);
            if (existing == null)
                throw new ArgumentException($"Employee with ID {employee.Id} not found.");

            existing.Name = employee.Name;
            existing.JobTitle = employee.JobTitle;
            existing.Salary = employee.Salary;
        }
        public List<Employee> GetAllEmployees()
        {
            return new List<Employee>(_employees);
        }
        public void ImportFromExcel(string filePath)
        {
            var importedEmployees = _excelHelper.ReadEmployeesFromExcel(filePath);

            foreach (var emp in importedEmployees)
            {
                AddEmployee(emp);
            }
        }
    }
}