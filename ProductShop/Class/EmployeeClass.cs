using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductShop.EF
{
    public partial class Employee
    {
        public string EmployeeFullName { get => $"{LastName} {FirstName} {Patronymic}"; }
    }
}
