namespace PetProject.Exceptions
{
    public class NodeInputException : BaseException
    {
        public NodeInputException(string message) : base(message, StatusCodes.Status400BadRequest)
        {

        }
    }
}
