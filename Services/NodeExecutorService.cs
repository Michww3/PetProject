using PetProject.DTOs;
using PetProject.GraphExecute;
using PetProject.Services.Interfaces;
using System.Text.Json;

namespace PetProject.Services
{
    public class NodeExecutorService : INodeExecutorService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<NodeExecutorService> _logger;

        private static readonly Dictionary<string, Type> NodeTypeMap = new()
        {
            ["NumberAdd"] = typeof(NumberAddNode),
            ["StringConcat"] = typeof(StringConcatNode),
            ["ConsoleLog"] = typeof(ConsoleLogNode)
        };

        public NodeExecutorService(
            IServiceProvider serviceProvider,
            ILogger<NodeExecutorService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task<object?> ExecuteGraphAsync(string jsonData)
        {
            //generate custom ex
            var graph = JsonSerializer.Deserialize<GraphDefinition>(
                jsonData,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            if (graph == null || graph.Nodes.Count == 0)
                throw new InvalidOperationException("Graph is empty");

            var executionResults = new Dictionary<string, object?>();

            foreach (var node in graph.Nodes)
            {
                _logger.LogInformation("Executing node {NodeId} ({NodeType})", node.Id, node.Type);

                if (!NodeTypeMap.TryGetValue(node.Type, out var nodeType))
                    throw new InvalidOperationException($"Unknown node type: {node.Type}");

                var resolvedInputs = ResolveInputs(node.Inputs, executionResults);

                var nodeInstance = (NodeBase)ActivatorUtilities.CreateInstance(
                    _serviceProvider,
                    nodeType
                );

                var result = await nodeInstance.ExecuteAsync(resolvedInputs);

                executionResults[node.Id] = result;
            }

            return executionResults.Last().Value;
        }

        private Dictionary<string, object> ResolveInputs(
            Dictionary<string, object> inputs,
            Dictionary<string, object?> previousResults)
        {
            var resolved = new Dictionary<string, object>();

            foreach (var (key, value) in inputs)
            {
                if (value is JsonElement jsonElement &&
                    jsonElement.ValueKind == JsonValueKind.Object &&
                    jsonElement.TryGetProperty("from", out var fromProp))
                {
                    var fromNodeId = fromProp.GetString();

                    if (fromNodeId == null || !previousResults.ContainsKey(fromNodeId))
                        throw new InvalidOperationException($"Dependency not resolved: {fromNodeId}");

                    resolved[key] = previousResults[fromNodeId]!;
                }
                else
                {
                    resolved[key] = value!;
                }
            }

            return resolved;
        }
    }
}
