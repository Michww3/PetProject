using PetProject.Exceptions;
using System.Text.Json;

namespace PetProject.GraphExecute
{
    public class NumberAddNode : NodeBase
    {
        public override Task<object> ExecuteAsync(Dictionary<string, object> inputs)
        {
            if (!inputs.TryGetValue("A", out var a) ||
                !inputs.TryGetValue("B", out var b))
            {
                throw new NodeInputException("Inputs 'A' and 'B' are required");
            }

            if (!TryConvertToDouble(a, out var aValue) ||
                !TryConvertToDouble(b, out var bValue))
            {
                throw new NodeInputException("Inputs 'A' and 'B' must be numbers");
            }

            var result = aValue + bValue;
            return Task.FromResult<object>(result);
        }

        //private static bool TryConvertToDouble(object value, out double result)
        //{
        //    try
        //    {
        //        result = Convert.ToDouble(value);
        //        return true;
        //    }
        //    catch
        //    {
        //        result = 0;
        //        return false;
        //    }
        //}

        private static bool TryConvertToDouble(object value, out double result)
        {
            switch (value)
            {
                case double d:
                    result = d;
                    return true;

                case int i:
                    result = i;
                    return true;

                case long l:
                    result = l;
                    return true;

                case float f:
                    result = f;
                    return true;

                case JsonElement json when json.ValueKind == JsonValueKind.Number:
                    return json.TryGetDouble(out result);

                case JsonElement json when json.ValueKind == JsonValueKind.String:
                    return double.TryParse(json.GetString(), out result);

                default:
                    result = 0;
                    return false;
            }
        }
    }
}
