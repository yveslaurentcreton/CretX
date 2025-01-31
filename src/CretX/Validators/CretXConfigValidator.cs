using CretX.Configuration;
using FluentValidation;

namespace CretX.Validators;

public class CretXConfigValidator : AbstractValidator<CretXConfig>
{
    public CretXConfigValidator()
    {
        RuleFor(x => x.TargetBaseUrl)
            .NotEmpty();

        RuleFor(x => x.MacAddress)
            .NotEmpty();
    }
}