using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Infrastructure.Implementation;
using EduGuide.Application.Contracts.Repositories;
using EduGuide.Domain.Entities;

namespace EduGuide.Infrastructure.Implementation.Repositories
{
    public class StudentRepository: Repository<Student, IEduGuideContext>, IStudentRepository
    {
        public StudentRepository(IEduGuideContext dbContext) : base(dbContext)
        {
        }
    }
}
