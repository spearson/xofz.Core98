namespace xofz.Framework
{
    using System;
    using System.Reflection;
    using System.Text;

    public class VersionReader
    {
        public VersionReader(
            Assembly executingAssembly)
        {
            if (executingAssembly == null)
            {
                throw new ArgumentNullException(
                    nameof(executingAssembly));
            }

            this.executingAssembly = executingAssembly;
        }

        public virtual string Read()
        {
            var ea = this.executingAssembly;
            return this.readInternal(ea);
        }

        public virtual string ReadCoreVersion()
        {
            var ea = Assembly.GetExecutingAssembly();
            return this.readInternal(ea);
        }

        private string readInternal(Assembly assembly)
        {
            var an = new AssemblyName(assembly.FullName);
            var v = an.Version;
            var versionBuilder = new StringBuilder();
            versionBuilder.Append(v.Major);
            versionBuilder.Append('.');
            versionBuilder.Append(v.Minor);
            versionBuilder.Append('.');
            versionBuilder.Append(v.Build);
            versionBuilder.Append('.');
            versionBuilder.Append(v.Revision);

            return versionBuilder.ToString();
        }

        private readonly Assembly executingAssembly;
    }
}
