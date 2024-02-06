using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.DataTransferObject.Generic
{

    public enum ResultType
    {
        Successful,
        Failed
    }

    public class Result
    {
        private const string EmptyErrorsMessage = "errors cannot be empty";

        public ResultType Type { get; }

        public Dictionary<string, string> Errors { get; } = new();

        protected Result(ResultType type, Dictionary<string, string> errors)
        {
            Type = type;
            Errors = (errors);
        }

        public static Result Successful() => new(ResultType.Successful, null);

        public static Result Failed(Dictionary<string, string> errors) =>
            new(ResultType.Failed, ValidateErrors(errors));

        protected static Dictionary<string, string> ValidateErrors(Dictionary<string, string> errors) =>
            errors switch
            {
                null => throw new ArgumentNullException(nameof(errors)),
                { Count: 0 } => throw new ArgumentException(EmptyErrorsMessage, nameof(errors)),
                _ => errors
            };
    }

    public class Result<T> : Result
    {
        private const string NoValueOnFailedOperationMessage = "There is no value on a failed operation";

        private readonly T _value;

        public T Value
        {
            get
            {
                return _value;
                //return Success
                //    ? _value
                //    : throw new InvalidOperationException(NoValueOnFailedOperationMessage);
            }
        }

        private Result(ResultType type, T value, Dictionary<string, string> errors) : base(type, errors)
        {
            _value = value;
        }

        public static Result<T> Successful(T value) => new(ResultType.Successful, ValidateValue(value), null);

        public static new Result<T> Failed(Dictionary<string, string> errors) =>
            new(ResultType.Failed, default, ValidateErrors(errors));

        private static T ValidateValue(T value) => value ?? throw new ArgumentNullException(nameof(value));
    }
}
