﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw10.DTOs.Responses
{
    public class PromoteStudentsResponse
    {
        public int IdEnrollment { get; set; }

        public int Semester { get; set; }

        public string Studies { get; set; }

        public DateTime StartDate { get; set; }
    }
}
