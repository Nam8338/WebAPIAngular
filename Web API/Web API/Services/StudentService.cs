using AutoMapper;
using System;
using System.Linq;
using Web_API.Models;
using Web_API.Authenticate;
using Microsoft.Extensions.Options;
using Web_API.Filters;
using Microsoft.AspNetCore.Mvc;
using Web_API.Responses;
using System.Collections.Generic;

namespace Web_API.Services
{
    public interface IStudentService
    {
        Student Add(Student student);
        PageResponse<List<StudentDTO>> GetAll([FromQuery] PaginationFilter filter);
        bool Delete(Guid id);
        Student GetById(string name);
        Student Update(Student student);
    }

    public class StudentService : IStudentService
    {
        private readonly DBContext _dbContextStudent;
        private readonly IMapper _mapper;
        private IUriService _uriService;
        private string route;

        public StudentService(IOptions<AppSettings> appSettings, DBContext dbContextStudent, IMapper mapper, IUriService uriService)
        {
            _dbContextStudent = dbContextStudent;
            _mapper = mapper;
            _uriService = uriService;
        }

        public Student Add(Student student)
        {
            var addStudent = _dbContextStudent.Students.Add(new Student()
            {
                FullName = student.FullName,
                DateOfBirth = student.DateOfBirth,
                Sex = student.Sex,
                NativeVillage = student.NativeVillage,
                PhoneNumber = student.PhoneNumber,
                Mail = student.Mail
            });

            _dbContextStudent.SaveChanges();

            return addStudent.Entity;
        }
        public PageResponse<List<StudentDTO>> GetAll([FromQuery] PaginationFilter filter)
        {

            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

            var student = _dbContextStudent.Students.ToList();

            var studentDTO = _mapper.Map<List<StudentDTO>>(student);

            var pagedData = studentDTO.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
            .Take(validFilter.PageSize).ToList();

            var totalRecords = _dbContextStudent.Students.Count();

            var pagedReponse = PageResponse<List<StudentDTO>>.CreatePagedReponse(pagedData, validFilter, totalRecords, _uriService, route);
            return pagedReponse;
        }
        public Student GetById(string name)
        {
            var getStudent = _dbContextStudent.Students.FirstOrDefault(s => s.FullName == name);
            return getStudent;
        }
        public bool Delete(Guid id)
        {
            var isstudent = _dbContextStudent.Students.Where(s => s.Id == id).FirstOrDefault();

            if (isstudent == null)
            {
                return false;
            }

            _dbContextStudent.Remove(isstudent);
            _dbContextStudent.SaveChanges();

            return true;
        }
        public Student Update(Student student)
        {
            var updateStudent = _dbContextStudent.Students.FirstOrDefault(s => s.FullName == student.FullName);
            if (updateStudent != null)
            {
                updateStudent.DateOfBirth = student.DateOfBirth;
                updateStudent.Sex = student.Sex;
                updateStudent.NativeVillage = student.NativeVillage;
                updateStudent.PhoneNumber = student.PhoneNumber;
                updateStudent.Mail = student.Mail;

                _dbContextStudent.SaveChanges();
            }
            return updateStudent;
        }

    }
}
