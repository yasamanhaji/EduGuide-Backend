using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Base.Application.Contracts;
using EduGuide.Application.CQRS.RequestCounselors.Query.GetList;
using EduGuide.Domain.Entities;

namespace EduGuide.Application.Contracts.Repositories
{
    public interface IRequestCounselorRepository : IRepository<RequestCounselor, IEduGuideContext>
    {
    }
}
