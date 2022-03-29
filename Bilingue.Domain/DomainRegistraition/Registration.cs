using Bilingue.Domain.DomainClassroom;
using Bilingue.Domain.DomainStudent;
using System;

namespace Bilingue.Domain.DomainRegistraition
{
    public class Registration
    {     
        public Guid Id { get; set; }


        public Guid StudentId { get; set; }
        public virtual Student Student { get; set; }


        public Guid ClassroomId { get; set; }
        public virtual Classroom Classroom { get; set; }
    }
}
