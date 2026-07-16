using EduGuide.Domain.Entities;
using FarsiLibrary.Utils;
using System.Linq.Expressions;

namespace EduGuide.Application.CQRS.Payments
{
    public class PaymentListDTO
    {
        public long Id { get; set; }
        public decimal Amount { get; set; }
        public bool IsPaid { get; set; }
        public int CounselingDuration { get; set; }
        public string PayableTo { get; set; }
        public string PaymentDate { get; set; }
        public long CounselorId { get; set; }

        public static Expression<Func<Payment, PaymentListDTO>> Selector =>
            model => new PaymentListDTO
            {
                Id = model.Id,
                Amount = model.Amount,
                IsPaid = model.IsPaid,
                CounselingDuration = model.CounselingDuration,
                PayableTo = model.Counselor.User.FirstName + ' ' + model.Counselor.User.LastName,
                PaymentDate = model.IsPaid
                            ? $"{PersianDateConverter.ToPersianDate(model.LastModifyDate.Value):yyyy/MM/dd} " +
                              $"{model.LastModifyDate.Value.AddHours(3).AddMinutes(30):HH:mm}"
                            : null,
                CounselorId = model.CounselorId,
            };
    }
}