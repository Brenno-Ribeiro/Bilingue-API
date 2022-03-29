using Bilingue.Domain.DomainRegistraition;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bilingue.Domain.DomainStudent
{
    public class Student
    {
       
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }

        [NotMapped]
        public virtual ICollection<Registration> Registrations { get; set; }

        
    }
}
