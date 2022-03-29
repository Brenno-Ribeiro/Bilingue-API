using Bilingue.Domain.DomainRegistraition;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bilingue.Domain.DomainClassroom
{
    public class Classroom
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public int SchoolYear { get; set; }

        [NotMapped]
        public virtual ICollection<Registration> Registrations { get; set;}
    }
}
