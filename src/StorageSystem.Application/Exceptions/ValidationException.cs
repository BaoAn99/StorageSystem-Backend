using FluentValidation.Results;
using StorageSystem.Application.Models.ErrorCode;

namespace StorageSystem.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException() : base("One or more validation failures have occurred.")
        {
            Errors = new Dictionary<object, ErrorCodeOutDto[]>();
        }

        public ValidationException(List<ValidationFailure> failures) : this()
        {
            foreach (var failure in failures)
            {
                Errors.Add(failure.PropertyName, new ErrorCodeOutDto[]
                {
                    new ErrorCodeOutDto
                    {
                        ErrorCode = failure.ErrorCode,
                        Message = failure.ErrorMessage
                    }
                });
            }
            //Errors = failures
            //    .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            //    .ToDictionary(failureGroup => failureGroup.Key, failureGroup => new ErrorCodeOutDto
            //    {
            //        //Message = failureGroup.Select(x => x.),
            //        ErrorCode = failureGroup.Key
            //    });
        }

        public IDictionary<object, ErrorCodeOutDto[]> Errors { get; }
    }
}
