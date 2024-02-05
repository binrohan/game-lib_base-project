using FluentValidation.Results;

namespace GameLib.Core.Exceptions;

public class ValidationException : Exception
{
    public ValidationException()
        : base("One or more validation failures have occurred.")
    {
        Errors = [];
    }

    public ValidationException(IEnumerable<ValidationFailure> failures)
        : this()
    {
        Errors = failures.Select(x => x.ErrorMessage).ToList();
    }

    public IList<string> Errors { get; }
}