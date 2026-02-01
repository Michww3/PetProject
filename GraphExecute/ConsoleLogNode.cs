using Serilog;
using System.Text.Json;

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

            //need rework
            // если это JsonElement
            if (value is JsonElement je)
            {
                switch (je.ValueKind)
                {
                    case JsonValueKind.String:
                        value = je.GetString() ?? string.Empty; //??
                        break;
                    case JsonValueKind.Number:
                        value = je.GetInt32(); // или GetDouble() в зависимости от ожидаемого типа
                        break;
                    case JsonValueKind.True:
                    case JsonValueKind.False:
                        value = je.GetBoolean();
                        break;
                    default:
                        value = je.ToString(); // fallback
                        break;
                }
            }

            _logger.LogInformation("ConsoleLogNode output: {@Value}", value);
            return Task.FromResult(value);
        }
    }
}
