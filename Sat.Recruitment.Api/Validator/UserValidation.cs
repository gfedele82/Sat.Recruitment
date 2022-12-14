using FluentValidation.Results;
using FluentValidation;
using Sat.Recruitment.Common;
using Sat.Recruitment.Models;

namespace Sat.Recruitment.Api.Validator
{
    public class UserValidation : AbstractValidator<User>
    {
        public UserValidation()
        {
            RuleFor(x => x.Name).Must(y => !string.IsNullOrEmpty(y)).WithMessage(ExcepcionsMenssages.NameNotNull);
            RuleFor(x => x.Email).Must(y => !string.IsNullOrEmpty(y)).WithMessage(ExcepcionsMenssages.EmailNotNull);
            RuleFor(x => x.Address).Must(y => !string.IsNullOrEmpty(y)).WithMessage(ExcepcionsMenssages.AddressNotNull);
            RuleFor(x => x.Phone).Must(y => !string.IsNullOrEmpty(y)).WithMessage(ExcepcionsMenssages.PhoneNotNull);
            RuleFor(x => x.Email).Matches(@"^\S+@\S+\.\S+$").WithMessage(ExcepcionsMenssages.EmailNotValid);
            RuleFor(x => x.Phone).Matches(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$").WithMessage(ExcepcionsMenssages.PhoneNotValid);
            RuleFor(x => x.UserType).Must(y => y.ToLower().Equals(SystemParameters.userType1.ToLower()) ||
            y.ToLower().Equals(SystemParameters.userType2.ToLower()) ||
            y.ToLower().Equals(SystemParameters.userType3.ToLower())  ).WithMessage(ExcepcionsMenssages.UserTypeNotValid);
        }

        protected override bool PreValidate(ValidationContext<User> context, ValidationResult result)
        {
            if (context.InstanceToValidate == null)
            {
                result.Errors.Add(new ValidationFailure("", ExcepcionsMenssages.UserRequired));
                return false;
            }
            return true;
        }
    }
}
