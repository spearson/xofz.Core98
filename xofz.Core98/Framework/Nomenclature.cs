namespace xofz.Framework
{
    public class Nomenclature
    {
        public virtual void Rename(
            Nameable n,
            Gen<string> readNewName)
        {
            if (n == null)
            {
                return;
            }

            n.Name = readNewName?.Invoke();
        }

        public virtual void ApplyName(
            Gen<Nameable> newNameable,
            string name)
        {
            var n = newNameable?.Invoke();
            if (n == null)
            {
                return;
            }

            n.Name = name;
        }
    }
}
