using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_API.Models
{
    public class StudentDTO
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Sex { get; set; }
        public string NativeVillage { get; set; }
        public string PhoneNumber { get; set; }
        public string Mail { get; set; }
    }
}
