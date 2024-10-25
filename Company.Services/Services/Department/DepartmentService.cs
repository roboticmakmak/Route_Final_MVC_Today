using AutoMapper;
using Company.Data.Entites;
using Company.Repository.Interfaces;
using Company.Services.Interfaces.Departments;
using Company.Services.Interfaces.Departments.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Services.Services
{
    public class DepartmentService : IDepartmentService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentService(IUnitOfWork unitOfWork, IMapper mapper )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void Add(DepartmentDto departmentDto)
        {

            //var mappedDepartment = new DepartmentDto
            //{
            //    Code = department.Code,
            //    Name = department.Name,
            //    CreateAt = DateTime.Now,

            //};

            var mappedDepartment = _mapper.Map<Department>(departmentDto);
            _unitOfWork.DepartmentRepository.Add(mappedDepartment);
            _unitOfWork.Complete();
        }

        public void Delete(DepartmentDto departmentDto)
        {
            var mappedDepartment = _mapper.Map<Department>(departmentDto);
            _unitOfWork.DepartmentRepository.Delete(mappedDepartment);
            _unitOfWork.Complete();
        }

        public IEnumerable<DepartmentDto> GetAll()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();
            var mappedDepartment = _mapper.Map<IEnumerable<DepartmentDto>>(departments);

            return mappedDepartment; 
        }

        public DepartmentDto GetbyId(int? id)
        {
            if (id == null)
                return null;
            
            var department = _unitOfWork.DepartmentRepository.GetbyId(id.Value);

            if (department == null)
                return null;

            var mappedDepartment = _mapper.Map<DepartmentDto>(department);

            return mappedDepartment;

        }

        public void Update(DepartmentDto department)
        {

           // //var dept = GetbyId(department.Id);
           //if(dept.Name != department.Name)
           // {
           //     if (GetAll().Any(x => x.Name == department.Name))
           //         throw new Exception("DuplicateDepartmentName");
           // }
           //dept.Name = department.Name;
           // dept.Code = department.Code;

           //_unitOfWork.DepartmentRepository.Update(department);
           // _unitOfWork.Complete();
        }
    }
}
