namespace PetProject.GraphExecute
{
    public class NumberAddNode : NodeBase
    {
        public override Task<object> ExecuteAsync(Dictionary<string, object> inputs)
        {
            if (!inputs.TryGetValue("A", out var a) ||
                !inputs.TryGetValue("B", out var b))
            {
                throw new ArgumentException("Inputs 'A' and 'B' are required");
            }

            if (!TryConvertToDouble(a, out var aValue) ||
                !TryConvertToDouble(b, out var bValue))
            {
                throw new ArgumentException("Inputs 'A' and 'B' must be numbers");
            }

            var result = aValue + bValue;
            return Task.FromResult<object>(result);
        }

        private static bool TryConvertToDouble(object value, out double result)
        {
            try
            {
                result = Convert.ToDouble(value);
                return true;
            }
            catch
            {
                result = 0;
                return false;
            }
        }

        //need test
        //private static bool TryConvertToDouble(object value, out double result)
        //{
        //    switch (value)
        //    {
        //        case double d:
        //            result = d;
        //            return true;
        //        case int i:
        //            result = i;
        //            return true;
        //        case long l:
        //            result = l;
        //            return true;
        //        case float f:
        //            result = f;
        //            return true;
        //        case string s when double.TryParse(s, out var parsed):
        //            result = parsed;
        //            return true;
        //        default:
        //            result = 0;
        //            return false;
        //    }
        //}
    }
}
