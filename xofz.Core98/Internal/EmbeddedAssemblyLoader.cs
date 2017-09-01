namespace xofz.Internal
{
    using System.Reflection;

    internal static class EmbeddedAssemblyLoader
    {
        public static Assembly Load(string name)
        {
            if (name.EndsWith("Retargetable=Yes"))
            {
                return Assembly.Load(new AssemblyName(name));
            }

            var container = Assembly.GetExecutingAssembly();
            var path = new AssemblyName(name).Name + ".dll";

            using (var stream = container.GetManifestResourceStream(path))
            {
                if (stream == null)
                {
                    return null;
                }

                var bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
                return Assembly.Load(bytes);
            }
        }
    }
}
