using Base.Application.Contracts;
using EduGuide.Application.Contracts.Repositories;
using EduGuide.Domain.Entities;
using FluentValidation;

namespace EduGuide.Application.CQRS.Students.Command.Update
{
    public class StudentsUpdateCommandValidator:AbstractValidator<StudentsUpdateCommand>
    {
        public StudentsUpdateCommandValidator(IGenericRepository<Student, IEduGuideContext> genericRepository, IJwtManager jwtManager)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Email)
               .NotEmpty().WithMessage("ایمیل اجباری است!")
               .EmailAddress().WithMessage("فرمت ایمیل معتبر نیست!")
               .MaximumLength(60).WithMessage("ایمیل نمی‌تواند بیشتر از 60 کاراکتر باشد!")
               .Custom((email, context) =>
                 {
                     var command = context.InstanceToValidate;
                     var isDuplicate = genericRepository.Repository
                         .Any(u => u.User.Email == email && u.Id != command.Id);

                     if (isDuplicate)
                     {
                         context.AddFailure("این ایمیل قبلاً ثبت شده است!");
                     }
                 }
                );

            RuleFor(x => x.ParentPhoneNumber)
                .NotEmpty().WithMessage("شماره تماس والد الزامی است!")
                .Matches(@"^09\d{9}$").WithMessage("شماره تماس باید با 09 شروع شود و شامل 11 رقم باشد!");

            RuleFor(x => x.StudentPhoneNumber)
                .NotEmpty().WithMessage("شماره تماس دانش‌آموز الزامی است!")
                .Matches(@"^09\d{9}$").WithMessage("شماره تماس باید با 09 شروع شود و شامل 11 رقم باشد!");

            RuleFor(x => x.AboutMe)
                .NotEmpty().WithMessage("درباره من الزامی است!")
                .MaximumLength(2500).WithMessage("حداکثر 2500 کاراکتر مجاز است!");

            RuleFor(x => x.SchoolName)
                .NotEmpty().WithMessage("نام مدرسه الزامی است!")
                .MaximumLength(50).WithMessage("حداکثر 50 کاراکتر مجاز است!");

            RuleFor(x => x.BirthDate)
                .NotEmpty().WithMessage("تاریخ تولد اجباری است!");

            RuleFor(x => x.Province)
                .NotEmpty().WithMessage("استان اجباری است!");

            RuleFor(x => x.LastGradeGPA)
                .GreaterThan(0)
                .LessThanOrEqualTo(20)
                .WithMessage("معدل باید بین 0 تا 20 باشد!");

            RuleFor(x => x.GradeLevel)
                .NotEmpty().WithMessage("پایه الزامی است!")
                .Must(grade => grade != 0).WithMessage("پایه باید یکی از دهم، یازدهم یا دوازدهم باشد!");
        }
    }
}