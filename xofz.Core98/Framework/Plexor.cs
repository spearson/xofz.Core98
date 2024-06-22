namespace xofz.Framework
{
    using System.Collections.Generic;

    public class Plexor<T>
    {
        public Plexor()
            : this(null)
        {
        }

        public Plexor(
            ICollection<Plexor<T>> plexors)
        {
            this.subs = plexors ??
                         new XLinkedList<Plexor<T>>();
            this.stars = new XLinkedList<Star<T>>();
            this.name = Namer.Create(null);
        }

        public virtual ICollection<Plexor<T>> Plexors
        {
            get => this.subs;

            protected set => this.subs = value;
        }

        public virtual Nameable N
        {
            get => this.name;

            set => this.name = value;
        }

        public virtual T O { get; set; }

        // these should be separate, unique prime stars
        // 'prime' meaning not substars of another star
        public virtual ICollection<Star<T>> AllStars
        {
            get => this.stars;

            set => this.stars = value;
        }

        public virtual T Access(
            Do<T> accessor)
        {
            var o = this.O;
            if (o == null)
            {
                return default;
            }

            accessor?.Invoke(o);
            return o;
        }

        public virtual K Access<K>(
            Do<K> accessor)
            where K : T
        {
            if (this.O is K k)
            {
                accessor?.Invoke(k);
                return k;
            }

            return default;
        }

        public virtual Star<T> AccessStar(
            Gen<Star<T>, bool> findStar,
            Do<Star<T>> accessor)
        {
            return this.AccessStar<Star<T>>(
                findStar,
                accessor);
        }

        public virtual K AccessStar<K>(
            Gen<K, bool> findStar,
            Do<K> accessor)
            where K : Star<T>
        {
            K target = default;
            if (findStar == null)
            {
                return target;
            }

            var s = this.AllStars;
            if (s == null)
            {
                return target;
            }

            foreach (var star in s)
            {
                if (star is K k)
                {
                    if (findStar(k))
                    {
                        target = k;
                        goto access;
                    }
                }
            }

            return target;

            access:
            accessor?.Invoke(target);
            return target;
        }

        protected Nameable name;
        protected ICollection<Star<T>> stars;
        protected ICollection<Plexor<T>> subs;
    }
}