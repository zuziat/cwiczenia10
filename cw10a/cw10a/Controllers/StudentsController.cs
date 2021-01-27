using cw10.DAL;
using cw10.DTOs.Requests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw10.Controllers
{

    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private IStudentsDbService _service;

        public StudentsController(IStudentsDbService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            var res = _service.GetStudents();

            return Ok(res);
        }

        [HttpPut("{index}")]
        public IActionResult ModifyStudent(string index, ModifyStudentRequest request)
        {

            var res = _service.ModifyStudent(index, request);

            if (res != null)
            {
                return Ok(res);
            }
            else
            {
                return NotFound("Nie ma takiego studenta");
            }



        }

        [HttpDelete("{index}")]
        public IActionResult DeleteStudent(string index)
        {
            var res = _service.DeleteStudent(index);

            if (res == true)
            {
                return Ok("Usunieto studenta o indeksie " + index);
            }
            else
            {
                return NotFound("Nie ma takiego studenta");
            }
        }
    }
}
