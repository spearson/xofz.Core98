namespace xofz.Framework
{
    public class Nomenclature
    {
        public virtual Nameable Rename(
            Nameable n,
            Gen<string> readNewName)
        {
            if (n == null)
            {
                return n;
            }

            n.Name = readNewName?.Invoke();
            return n;
        }

        public virtual Nameable ApplyName(
            Gen<Nameable> newNameable,
            string name)
        {
            var n = newNameable?.Invoke();
            if (n == null)
            {
                return n;
            }

            n.Name = name;
            return n;
        }
    }
}
