﻿using FluentValidation.Results;

namespace Application.Common.Exceptions
{
    public class ValidationException : CustomExceptionBase
    {
        public override int Status => 400;

        public ValidationException(IEnumerable<ValidationFailure> failures) : base("One or more validation failures have occurred")
        {
            Errors = failures.GroupBy(f => f.PropertyName, f => f.ErrorMessage).ToDictionary(g => g.Key, g => g.ToArray());
        }

    }
}
