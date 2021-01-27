using cw10.DAL;
using cw10.DTOs.Requests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw10.Controllers
{
    [Route("api/enrollments")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private IStudentsDbService _service;

        public EnrollmentsController(IStudentsDbService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult EnrollStudent(EnrollStudentRequest request)
        {


            var response = _service.EnrollStudent(request);

            if (response != null)
            {
                return Created("", response);
            }
            else
            {
                return BadRequest();
            }

        }


        [HttpPost("/api/enrollments/promotions")]
        public IActionResult PromoteStudents(PromoteStudentsRequest request)
        {

            var response = _service.PromoteStudents(request);

            if (response != null)
            {
                return Created("", response);
            }
            else
            {
                return NotFound();
            }

        }
    }
}
