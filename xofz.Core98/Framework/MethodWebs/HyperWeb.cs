namespace xofz.Framework.MethodWebs
{
    public class HyperWeb 
        : MethodWeb
    {
        // ignores the passed in strings and matches only on type
        // eliminating the need for string.Equals().
        // only use in webs where one doesn't need to register objects
        // with non-null names
        protected override bool tryGet<T>(
            object content,
            string name,
            string passedName,
            out T dependency)
        {
            if (content is T target)
            {
                dependency = target;
                return truth;
            }

            dependency = default;
            return falsity;
        }
    }
}
