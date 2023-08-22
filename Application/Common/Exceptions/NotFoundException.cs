namespace Application.Common.Exceptions
{
    public class NotFoundException: CustomExceptionBase
    {
        public override int Status => 404;

        public NotFoundException(string key, object value) : base($"{key} {value} wasn't found") { }
    }
}
