using FluentValidation.Results;

namespace Hoursly.UnitTests.Common
{
    public class ValidationResultMock : ValidationResult
    {
        public ValidationResultMock(bool validationResult)
        {
            IsValid = validationResult;
        }

        public override bool IsValid { get; }
    }
}