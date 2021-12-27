using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCAG_Repositories.Entities;

namespace TCAG_Repositories.Employee_Repository
{
    public class EmployeeRepsoitory
    {
        private readonly Dictionary<int, Employee> _employeeDbContext = new Dictionary<int, Employee>();
        private int _count;

        public bool AddEmployeeToDatabase(Employee employee)
        {
            if (employee is null)
            {
                return false;
            }
            else
            {
                _count++;
                employee.ID = _count;
                _employeeDbContext.Add(employee.ID,employee);
                return true;
            }
        }

        public Dictionary<int, Employee> GetEmployees()
        {
            return _employeeDbContext;
        }

        public Employee GetEmployeeByKey(int key)
        {
            foreach (var employee in _employeeDbContext)
            {
                if (employee.Key == key)
                {
                    return employee.Value;
                }
            }
            return null;
        }

        public bool GiveEmployeeAccessToRoom(int key, string roomName)
        {
            var employee = GetEmployeeByKey(key);   
            if (employee != null)
            {
                employee.EmployeeAccess.Add(roomName);
                return true;
            }

            return false;
        }

        public bool RemoveEmployeeRoomAccess(int key, string roomName)
        {
            Employee employee = GetEmployeeByKey(key);
            if (employee != null)
            {
                foreach (var room in employee.EmployeeAccess)
                {
                    if (room == roomName)
                    {
                        employee.EmployeeAccess.Remove(room);
                        return true;
                    }

                }

            }
                return false;
        }

        public bool RemoveEmployee(int key)
        {
            foreach (var employee in _employeeDbContext)
            {
                if (employee.Key == key)
                {
                    _employeeDbContext.Remove(employee.Key);
                    return true;
                }
            } return false;
        }

        public bool ApprehendedGremlin(Gremlin gremlin, int key)
        {
            var employee = GetEmployeeByKey(key);
            if (employee != null)
            {
                employee.CapturedGremlins.Add(gremlin);
                return true;
            }

            return false;
        }
    }
}
