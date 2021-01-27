using cw10.DTOs.Requests;
using cw10.DTOs.Responses;
using cw10.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw10.DAL
{
    public interface IStudentsDbService
    {
        List<Student> GetStudents();

        ModifyStudentResponse ModifyStudent(string index, ModifyStudentRequest request);

        bool DeleteStudent(string index);

        EnrollStudentResponse EnrollStudent(EnrollStudentRequest request);

        PromoteStudentsResponse PromoteStudents(PromoteStudentsRequest request);
    }
}
