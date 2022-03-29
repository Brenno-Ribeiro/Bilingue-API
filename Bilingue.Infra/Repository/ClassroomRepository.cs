﻿using Bilingue.Domain.DomainClassroom;
using Bilingue.Domain.DomainClassroom.Repository;
using Bilingue.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Bilingue.Infra.Repository
{
    public class ClassroomRepository : BaseRepository<Classroom>, IClassroomRepository
    {
        public ClassroomRepository(BilingueContext context) : base(context)
        {
        }

        public async Task<bool> ClassroomExist(string number)
        {
            return await _context.Classrooms.AnyAsync(x => x.Number == number);
        }

        public async Task<Classroom> GetClassroomByNumber(string number)
        {
            return await _context.Classrooms
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Number.Equals(number));
        }

        public async Task<Guid> GetGuidClassroomByNumber(string number)
        {
            var classroomId = Guid.Empty;

            try
            {
                var classroom = await _context.Classrooms
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Number.Equals(number));
                classroomId = classroom.Id;

            }
            catch (Exception e)
            {

                e.Message.ToString();
            }

            return classroomId;
        }

        public async Task<int> VerifyQuantityStudentInClassroom(string number)
        {
            return await _context.Registrations
               .AsNoTracking()
               .CountAsync(x => x.Classroom.Number.Equals(number));
        }

        public async Task<int> VerifyQuantityStudentInClassroom(Guid id)
        {
            return await _context.Registrations
              .AsNoTracking()
              .CountAsync(x => x.Classroom.Id.Equals(id));
        }
    }
}
