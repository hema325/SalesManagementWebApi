namespace Application.Common.Exceptions
{
    public abstract class CustomExceptionBase: Exception
    {
        public abstract int Status { get;}

        public Dictionary<string, string[]> Errors { get; protected set; }

        public CustomExceptionBase(string message) : base(message) { }

        public CustomExceptionBase(string message, Exception innerException): base(message, innerException) { }

    }
}
