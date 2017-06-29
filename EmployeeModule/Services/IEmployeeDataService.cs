using EmployeeModule.Models;

namespace EmployeeModule.Services
{
    public interface IEmployeeDataService
    {
        Employees GetEmployees();
        Projects GetProjects();
    }
}
