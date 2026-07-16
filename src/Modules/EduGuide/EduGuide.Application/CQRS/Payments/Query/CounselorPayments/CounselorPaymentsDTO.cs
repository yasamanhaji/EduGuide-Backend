using EduGuide.Domain.Entities;
using FarsiLibrary.Utils;
using System.Linq.Expressions;

namespace EduGuide.Application.CQRS.Payments
{
    public class CounselorPaymentsDTO
    {
        public decimal Amount { get; set; }
        public string PaymentDate { get; set; }
        public string StudentName { get; set; }

        public static Expression<Func<Payment, CounselorPaymentsDTO>> Selector =>
            model => new CounselorPaymentsDTO
            {
                Amount = model.Amount * 0.7m,
                PaymentDate = $"{PersianDateConverter.ToPersianDate(model.CreateDate.AddHours(3).AddMinutes(30)):yyyy/MM/dd}",
                StudentName = model.Student.User.FirstName + ' ' + model.Student.User.LastName,
            };
    }
}