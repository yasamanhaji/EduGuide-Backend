using Base.Infrastructure.Implementation;
using EduGuide.Application.Contracts.Repositories;
using EduGuide.Domain.Entities;

namespace EduGuide.Infrastructure.Implementation.Repositories
{
    public class PaymentRepository : Repository<Payment, IEduGuideContext>, IPaymentRepository
    {
        public PaymentRepository(IEduGuideContext dbContext) : base(dbContext)
        {
        }
    }
}