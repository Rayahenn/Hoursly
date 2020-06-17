using System;
using System.Linq;
using FluentValidation.Results;

namespace Hoursly.Common.Extensions.Validations
{
    public static class ValidationResultExtensions
    {
        public static string GetErrorsSummary(this ValidationResult result)
        {
            var errors = result.Errors
                .Select(error => error.ErrorMessage)
                .ToList();

            return string.Join(Environment.NewLine, errors);
        }
    }
}