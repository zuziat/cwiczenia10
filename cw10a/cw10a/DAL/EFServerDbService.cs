using cw10.DTOs.Requests;
using cw10.DTOs.Responses;
using cw10.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw10.DAL
{
    public class EFServerDbService
    {
        public bool DeleteStudent(string index)
        {
            var db = new s18919Context();

            var s = db.Student.Where(d => d.IndexNumber == index).FirstOrDefault();

            if (s == null)
            {
                return false;
            }
            else
            {
                db.Remove(s);
                db.SaveChanges();
                return true;
            }
        }

        public EnrollStudentResponse EnrollStudent(EnrollStudentRequest request)
        {
            var db = new s18919Context();
            EnrollStudentResponse response = new EnrollStudentResponse();

            var s = db.Student.Where(x => x.IndexNumber == request.IndexNumber).FirstOrDefault();

            if (s != null)
            {
                return null;
            }

            var t = db.Studies.Where(x => x.Name == request.Studies).Select(x => x.IdStudy).FirstOrDefault();
            int e = db.Enrollment.Where(x => x.IdStudy == t && x.Semester == 1).Select(x => x.IdEnrollment).FirstOrDefault();
            int f;

            if (e == 0)
            {
                var max = (db.Enrollment.Max(x => x.IdEnrollment)) + 1;
                var enroll = new Enrollment
                {
                    IdEnrollment = max,
                    Semester = 1,
                    IdStudy = t,
                    StartDate = DateTime.Now
                };
                db.Enrollment.Add(enroll);
                f = max;
            }
            else
            {
                f = e;
            }

            var stud = new Student
            {
                IndexNumber = request.IndexNumber,
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthDate = request.BirthDate,
                IdEnrollment = f
            };

            db.Student.Add(stud);
            db.SaveChanges();

            response.IndexNumber = stud.IndexNumber;
            response.IdEnrollment = stud.IdEnrollment;
            response.Semester = 1;
            response.Studies = request.Studies;
            response.StartDate = DateTime.Now;

            return response;
        }

        public List<Student> GetStudents()
        {
            var db = new s18919Context();

            var res = db.Student.ToList();

            return res;
        }

        public ModifyStudentResponse ModifyStudent(string index, ModifyStudentRequest request)
        {
            var db = new s18919Context();

            var s = db.Student.Where(d => d.IndexNumber == index).FirstOrDefault();

            if (s == null)
            {
                return null;
            }
            else
            {

                s.FirstName = request.FirstName;
                s.LastName = request.LastName;
                s.BirthDate = request.BirthDate;
                db.SaveChanges();

                ModifyStudentResponse response = new ModifyStudentResponse();
                response.IndexNumber = s.IndexNumber;
                response.FirstName = s.FirstName;
                response.LastName = s.LastName;
                response.BirthDate = s.BirthDate;

                return response;
            }
        }

        public PromoteStudentsResponse PromoteStudents(PromoteStudentsRequest request)
        {
            var db = new s18919Context();
            var response = new PromoteStudentsResponse();

            var t = db.Studies.Where(x => x.Name == request.Studies).Select(x => x.IdStudy).First();
            var e = db.Enrollment.Where(x => x.IdStudy == t && x.Semester == request.Semester).FirstOrDefault();

            if (e == null)
            {
                return null;
            }

            var studs = db.Student.Where(x => x.IdEnrollment == e.IdEnrollment).ToList();
            int nextSemester = request.Semester + 1;
            e = db.Enrollment.Where(x => x.IdStudy == t && x.Semester == nextSemester).FirstOrDefault();
            int f;

            if (e == null)
            {
                var max = (db.Enrollment.Max(x => x.IdEnrollment)) + 1;
                var enroll = new Enrollment
                {
                    IdEnrollment = max,
                    Semester = nextSemester,
                    IdStudy = t,
                    StartDate = DateTime.Now
                };
                db.Enrollment.Add(enroll);
                f = max;

            }
            else
            {
                f = e.IdEnrollment;
            }


            foreach (var s in studs)
            {
                s.IdEnrollment = f;
                db.Attach(s);
                db.Entry(s).Property("IdEnrollment").IsModified = true;
                db.SaveChanges();
            }

            response.IdEnrollment = f;
            response.Semester = nextSemester;
            response.Studies = request.Studies;
            response.StartDate = DateTime.Now;

            return response;
        }
    }
}
