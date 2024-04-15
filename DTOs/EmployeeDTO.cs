﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class EmployeeDTO
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? StoreId { get; set; }
        public string? Phone { get; set; }
        public string? StoreName { get; set; } 
        public string? CityName { get; set; }
    }
}
