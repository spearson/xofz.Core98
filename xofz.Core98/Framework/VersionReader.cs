namespace xofz.Framework
{
    using System.Reflection;
    using System.Text;

    public class VersionReader
    {
        public VersionReader()
        {
        }

        public VersionReader(
            Assembly executingAssembly)
        {
            this.executingAssembly = executingAssembly;
        }

        public virtual string Read()
        {
            var ea = this.executingAssembly;
            return this.readProtected(ea);
        }

        public virtual System.Version ReadAsVersion()
        {
            var ea = this.executingAssembly;
            return this.ReadAsVersion(ea);
        }

        public virtual string ReadCoreVersion()
        {
            var ea = Assembly.GetExecutingAssembly();
            return this.readProtected(ea);
        }

        public virtual System.Version ReadCoreVersionAsVersion()
        {
            var ea = Assembly.GetExecutingAssembly();
            return this.ReadAsVersion(ea);
        }

        public virtual string Read(
            Assembly assembly)
        {
            return this.readProtected(assembly);
        }

        public virtual System.Version ReadAsVersion(
            Assembly assembly)
        {
            if (assembly == null)
            {
                return new System.Version(
                    zero,
                    zero,
                    zero,
                    zero);
            }

            var an = new AssemblyName(assembly.FullName);
            return an.Version;
        }

        protected virtual string readProtected(
            Assembly assembly)
        {
            var v = this.ReadAsVersion(assembly);
            if (v == null)
            {
                v = new System.Version(
                    zero,
                    zero,
                    zero,
                    zero);
            }

            const char period = '.';
            var versionBuilder = new StringBuilder();
            versionBuilder.Append(v.Major);
            versionBuilder.Append(period);
            versionBuilder.Append(v.Minor);
            versionBuilder.Append(period);
            versionBuilder.Append(v.Build);
            versionBuilder.Append(period);
            versionBuilder.Append(v.Revision);

            return versionBuilder.ToString();
        }

        protected readonly Assembly executingAssembly;
        protected const byte zero = 0;
    }
}