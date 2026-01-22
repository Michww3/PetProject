using Serilog;

namespace PetProject.GraphExecute
{
    public class ConsoleLogNode : NodeBase
    {
        private readonly ILogger<ConsoleLogNode> _logger;

        public ConsoleLogNode(ILogger<ConsoleLogNode> logger)
        {
            _logger = logger;
        }

        public override Task<object> ExecuteAsync(Dictionary<string, object> inputs)
        {
            var value = inputs["Value"];
            _logger.LogInformation("ConsoleLogNode output: {@Value}", value);
            return Task.FromResult(value);
        }
    }
}
