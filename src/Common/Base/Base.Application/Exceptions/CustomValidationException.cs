using Base.Application.Contracts.DTOs.Common;

namespace Base.Application.Exceptions
{
    public class CustomValidationException : Exception
    {
        public CustomValidationException(IEnumerable<Error> errors)
            : base(string.Join(" | ", errors.Select(e => $"{e.Message}")))
        {
            Errors = errors.ToList();
        }

        public IReadOnlyList<Error> Errors { get; }
    }
}
