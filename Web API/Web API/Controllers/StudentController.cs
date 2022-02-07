using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using Web_API.Filters;
using Web_API.Models;
using Web_API.Services;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class StudentController : ControllerBase
    {
        private IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // POST: api/<StudentController>
        [HttpPost("createStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Create(Student student)
        {
            if (!ModelState.IsValid)
            {
                throw new System.Web.Http.HttpResponseException(HttpStatusCode.NotFound);
            }
            var createUser = _studentService.Add(student);
            if (createUser == null)
                throw new System.Web.Http.HttpResponseException(HttpStatusCode.NotFound);
            return Ok(createUser);
        }


        // GET: api/<StudentController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll([FromQuery] PaginationFilter filter)
        {
            var getStudent = _studentService.GetAll(filter);
            if (getStudent == null)
                throw new System.Web.Http.HttpResponseException(HttpStatusCode.NotFound);
            return Ok(getStudent);
        }

        [HttpGet("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetById(string name)
        {
            var getStudent = _studentService.GetById(name);
            if (getStudent == null)
                throw new System.Web.Http.HttpResponseException(HttpStatusCode.NotFound);
            return Ok(getStudent);
        }


        // DELETE: api/<StudentController>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(Guid id)
        {
            var isStudent = _studentService.Delete(id);
            if (isStudent == false)
                throw new System.Web.Http.HttpResponseException(HttpStatusCode.NotFound);
            return Ok(isStudent);
        }

        // PUT: api/<StudentController>
        [HttpPut("updateStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Update(Student student)
        {
            var updateStudent = _studentService.Update(student);
            if (updateStudent == null)
                throw new System.Web.Http.HttpResponseException(HttpStatusCode.NotFound);
            return Ok(updateStudent);
        }
    }
}
