using FluentValidation;
using Sat.Recruitment.Common;

namespace Sat.Recruitment.Api.Validator
{
    public class IntegerValidation : AbstractValidator<int>
    {
        public IntegerValidation()
        {
            RuleFor(x => x).Must(y => y > 0).WithMessage(ExcepcionsMenssages.IdUsermMustByGreaterThan0);
        }
    }
}
