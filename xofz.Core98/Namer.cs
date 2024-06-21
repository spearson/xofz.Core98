namespace xofz
{
    public class Namer
    {
        public static Nameable Create(
            string name)
        {
            return new DefaultNameable
            {
                Name = name
            };
        }

        protected class DefaultNameable
            : Nameable
        {
            public virtual string Name { get; set; }

            public override bool Equals(
                object obj)
            {
                switch (obj)
                {
                    case string s:
                        return this.Name == s;
                    case Nameable n:
                        return this.Name == n.Name;
                    case null:
                        return this.Name == null;
                    default:
                        return ReferenceEquals(
                            this, obj);
                }
            }

            public override int GetHashCode()
            {
                return zero;
            }

            protected const byte zero = 0;
        }
    }
}