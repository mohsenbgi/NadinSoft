namespace NadinSoft.Domain.Common
{
    public sealed class ValidationResult : Result, IValidationResult
    {
        private ValidationResult(string[] errors)
            : base(false, IValidationResult.ValidationError) =>
            Errors = errors;

        public string[] Errors { get; }

        public static ValidationResult WithErrors(string[] errors) => new(errors);
    }

    public sealed class ValidationResult<TValue> : Result<TValue>, IValidationResult
    {
        private ValidationResult(string[] errors)
            : base(default, false, IValidationResult.ValidationError) =>
            Errors = errors;

        public string[] Errors { get; }

        public static ValidationResult<TValue> WithErrors(string[] errors) => new(errors);
    }

    public interface IValidationResult
    {
        public static readonly string ValidationError = "A validation problem occurred.";

        string[] Errors { get; }
    }
}
