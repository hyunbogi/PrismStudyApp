using EmployeeModule.Models;

namespace EmployeeModule.Services
{
    public class EmployeeDataService : IEmployeeDataService
    {
        private Employees _employees;
        private Projects _projects;

        public Employees GetEmployees()
        {
            if (_employees == null)
            {
                _employees = new Employees
                {
                    new Employee
                    {
                        Id = "1",
                        Name = "John",
                        LastName = "Smith",
                        Phone = "(425) 555 8912",
                        Email = "John.Smith@Contoso.com"
                    },
                    new Employee()
                    {
                        Id = "2",
                        Name = "Bonnie",
                        LastName = "Skelly",
                        Phone = "(206) 555 7301",
                        Email = "Bonnie.Skelly@Contoso.com"
                    },
                    new Employee()
                    {
                        Id = "3",
                        Name = "Dana",
                        LastName = "Birkby",
                        Phone = "(425) 555 7492",
                        Email = "Dana.Birkby@Contoso.com"
                    },
                    new Employee()
                    {
                        Id = "4",
                        Name = "David",
                        LastName = "Probst",
                        Phone = "(425) 555 2836",
                        Email = "David.Probst@Contoso.com"
                    },
                };
            }

            return _employees;
        }

        public Projects GetProjects()
        {
            if (_projects == null)
            {
                _projects = new Projects
                {
                    new Project() { Id = "1", ProjectName = "Project 1", Role = "Dev Lead" },
                    new Project() { Id = "1", ProjectName = "Project 2", Role = "Tech Reviewer" },
                    new Project() { Id = "2", ProjectName = "Project 1", Role = "Test Lead" },
                    new Project() { Id = "2", ProjectName = "Project 2", Role = "Tech Reviewer" },
                    new Project() { Id = "3", ProjectName = "Project 1", Role = "Architect" },
                    new Project() { Id = "3", ProjectName = "Project 2", Role = "Tech Reviewer" },
                    new Project() { Id = "3", ProjectName = "Project 3", Role = "Tech Reviewer" },
                    new Project() { Id = "4", ProjectName = "Project 1", Role = "Test Lead" },
                    new Project() { Id = "4", ProjectName = "Project 2", Role = "Tech Reviewer" },
                    new Project() { Id = "4", ProjectName = "Project 3", Role = "Tech Reviewer" },
                    new Project() { Id = "4", ProjectName = "Project 4", Role = "Tech Reviewer" }
                };
            }

            return _projects;
        }
    }
}
