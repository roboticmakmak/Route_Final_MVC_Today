﻿using Company.Services.Interfaces.Employee.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Services.Interfaces.Departments.Dto
{
    public class DepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Code { get; set; }
        public DateTime CreateAt { get; set; }
        public ICollection<EmployeeDto> Employee { get; set; } = new List<EmployeeDto>();
    }
}
