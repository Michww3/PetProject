namespace PetProject.GraphExecute
{
    public abstract class NodeBase
    {
        public abstract Task<object> ExecuteAsync(Dictionary<string, object> inputs);
    }
}
