namespace PetProject.Exceptions
{
    public class ConflicttException : BaseException
    {
        public ConflicttException(string message) : base(message, StatusCodes.Status409Conflict) 
        {
        
        }
    }
}
