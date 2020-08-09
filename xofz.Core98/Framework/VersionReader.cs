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
            this.executingAssembly = executingAssembly ??
                                     throw new ArgumentNullException(
                                         nameof(executingAssembly));
        }
       
        public virtual string Read()
        {
            var ea = this.executingAssembly;
            return this.readProtected(ea);
        }

        public virtual Version ReadAsVersion()
        {
            var ea = this.executingAssembly;
            return this.ReadAsVersion(ea);
        }

        public virtual string ReadCoreVersion()
        {
            var ea = Assembly.GetExecutingAssembly();
            return this.readProtected(ea);
        }

        public virtual Version ReadCoreVersionAsVersion()
        {
            var ea = Assembly.GetExecutingAssembly();
            return this.ReadAsVersion(ea);
        }

        public virtual string Read(
            Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(
                    nameof(assembly));
            }

            return this.readProtected(assembly);
        }

        public virtual Version ReadAsVersion(
            Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(
                    nameof(assembly));
            }

            var an = new AssemblyName(assembly.FullName);
            return an.Version;
        }

        protected virtual string readProtected(
            Assembly assembly)
        {
            var v = this.ReadAsVersion(assembly);

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

        protected readonly Assembly executingAssembly;
    }
}
