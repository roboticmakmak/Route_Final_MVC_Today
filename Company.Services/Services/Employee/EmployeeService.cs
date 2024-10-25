using AutoMapper;
using Company.Data.Entites;
using Company.Repository.Interfaces;
using Company.Services.Interfaces;
using Company.Services.Interfaces.Employee.Dto;
using Company.Services.Services.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Company.Services.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper) {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }     

        public void Add(EmployeeDto employeeDto)
        {
           //Employee employee = new Employee
           // {
           //     Address = employeeDto.Address,
           //     Age = employeeDto.Age,
           //     DepartmentId = employeeDto.DepartmentId,
           //     Email = employeeDto.Email,
           //     HiringDate = employeeDto.HiringDate,
           //     ImageURL = employeeDto.ImageURL,
           //     Name = employeeDto.Name,
           //     PhoneNumer = employeeDto.PhoneNumer,
           //     Salary = employeeDto.Salary
           // };


            employeeDto.ImageURL = DocumentSettings.UploadFile(employeeDto.Image, "Images");
            Employee employee = _mapper .Map<Employee>(employeeDto);
            _unitOfWork.EmployeeRepository.Add(employee);
            _unitOfWork.Complete();
        }

        public void Delete(EmployeeDto employeeDto)
        {
            //Employee employee = new Employee
            //{
            //    Address = employeeDto.Address,
            //    Age = employeeDto.Age,
            //    DepartmentId = employeeDto.DepartmentId,
            //    Email = employeeDto.Email,
            //    HiringDate = employeeDto.HiringDate,
            //    ImageURL = employeeDto.ImageURL,
            //    Name = employeeDto.Name,
            //    PhoneNumer = employeeDto.PhoneNumer,
            //    Salary = employeeDto.Salary
            //};
            Employee employee = _mapper.Map<Employee>(employeeDto);

            _unitOfWork.EmployeeRepository.Delete(employee);
            _unitOfWork.Complete();
        }

        public IEnumerable<EmployeeDto> GetAll()
        {
             var employees = _unitOfWork.EmployeeRepository.GetAll();
            //var mappedEmployees = employees.Select(x => new EmployeeDto
            //{
            //    DepartmentId=x.DepartmentId,
            //    Email=x.Email,
            //    HiringDate=x.HiringDate,
            //    ImageURL=x.ImageURL,
            //    Name = x.Name,
            //    PhoneNumer=x.PhoneNumer,
            //    Salary=x.Salary,
            //    Id=x.Id,
            //    Address=x.Address, 
            //    Age=x.Age,
            //    CreateAt = x.CreateAt

            //});
            IEnumerable<EmployeeDto> mappedEmployees = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return mappedEmployees; 
        }

        public EmployeeDto GetById(int? id)
        {
            if (id is null)
                return null;

            var employee = _unitOfWork.EmployeeRepository.GetbyId(id.Value);

            if (employee is null)
                return null;


            //EmployeeDto employeeDto = new EmployeeDto
            //{
            //    Address = employee.Address,
            //    Age = employee.Age,
            //    DepartmentId = employee.DepartmentId,
            //    Email = employee.Email,
            //    HiringDate = employee.HiringDate,
            //    ImageURL = employee.ImageURL,
            //    Name = employee.Name,
            //    PhoneNumer = employee.PhoneNumer,
            //    Salary = employee.Salary,
            //    Id = employee.Id,
            //    CreateAt = employee.CreateAt
            //};

            EmployeeDto employeeDto = _mapper.Map<EmployeeDto>(employee);

            return employeeDto;
        }
          public IEnumerable<EmployeeDto> GetEmployeesByName(string name)
          {
            var employees = _unitOfWork.EmployeeRepository.GetEmployeeByName(name);
            //var mappedEmployees = employees.Select(x => new EmployeeDto
            //{
            //    DepartmentId = x.DepartmentId,
            //    Email = x.Email,
            //    HiringDate = x.HiringDate,
            //    ImageURL = x.ImageURL,
            //    Name = x.Name,
            //    PhoneNumer = x.PhoneNumer,
            //    Salary = x.Salary,
            //    Id = x.Id,
            //    Address = x.Address,
            //    Age = x.Age,
            //    CreateAt = x.CreateAt

            //});

            IEnumerable<EmployeeDto> mappedEmployees = _mapper.Map<IEnumerable<EmployeeDto>>(employees);

            return mappedEmployees;
        }
           
        
        public void Update(EmployeeDto employee)
        {
           // _unitOfWork.EmployeeRepository.Update(employee);
            _unitOfWork.Complete();
        }
  
    }
}
