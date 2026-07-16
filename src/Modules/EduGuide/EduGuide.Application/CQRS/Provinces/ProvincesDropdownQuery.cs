using Base.Application.Contracts.DTOs.Common;
using MediatR;

namespace EduGuide.Application.CQRS.Provinces
{
    public class ProvincesDropdownQuery : IRequest<Result<List<string>>>
    {
        public string Text { get; set; }
    }

    public class CountriesDropdownQueryHandler : IRequestHandler<ProvincesDropdownQuery, Result<List<string>>>
    {
        private static readonly List<string> _provincesOfIran = new List<string>
        {
            "آذربایجان شرقی",
            "آذربایجان غربی",
            "اردبیل",
            "اصفهان",
            "البرز",
            "ایلام",
            "بوشهر",
            "تهران",
            "چهارمحال و بختیاری",
            "خراسان جنوبی",
            "خراسان رضوی",
            "خراسان شمالی",
            "خوزستان",
            "زنجان",
            "سمنان",
            "سیستان و بلوچستان",
            "فارس",
            "قزوین",
            "قم",
            "کردستان",
            "کرمان",
            "کرمانشاه",
            "کهگیلویه و بویراحمد",
            "گلستان",
            "گیلان",
            "لرستان",
            "مازندران",
            "مرکزی",
            "هرمزگان",
            "همدان",
            "یزد"
        };

        public Task<Result<List<string>>> Handle(ProvincesDropdownQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Text))
                return Task.FromResult(Result<List<string>>.Success(_provincesOfIran));

            var searchText = request.Text.Trim();
            var filteredProvinces = _provincesOfIran
                .Where(p => p.StartsWith(searchText, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return Task.FromResult(Result<List<string>>.Success(filteredProvinces));
        }
    }
}