using Code.Library.Application.Exceptions;

namespace AspNetCoreApp.Application
{
    public class ApplicationLogic
    {
        public void GenerateValidationError()
        {
            throw new ValidationException();
        }
    }
}