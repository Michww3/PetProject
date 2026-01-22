namespace PetProject.GraphExecute
{
    public class StringConcatNode : NodeBase
    {
        public override Task<object> ExecuteAsync(Dictionary<string, object> inputs)
        {
            if (!inputs.TryGetValue("Str1", out var str1) ||
                !inputs.TryGetValue("Str2", out var str2))
            {
                throw new ArgumentException("Inputs 'Str1' and 'Str2' are required");
            }

            var result = $"{str1}{str2}";
            return Task.FromResult<object>(result);
        }
    }
}
