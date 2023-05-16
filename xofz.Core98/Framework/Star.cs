namespace xofz.Framework
{
    using System.Collections.Generic;

    public class Star
     : MethodRunnerV2
    {
        public Star()
            : this(
                new XLinkedList<Star>())
        {
        }

        public Star(
            ICollection<Star> subs)
        {
            this.subs = subs;
            this.name = Namer.Create(null);
        }

        public virtual Nameable N
        {
            get => this.name;

            set => this.name = value;
        }

        public virtual ICollection<Star> Stars
        {
            get => this.subs;

            protected set => this.subs = value;
        }

        public virtual MethodWeb W { get; set; }

        public virtual T AccessWeb<T>(
            Do<T> accessor = null)
            where T : MethodWeb
        {
            if (this.W is T web)
            {
                accessor?.Invoke(web);
                return web;
            }

            return default;
        }

        public T Run<T>(
            Do<T> method = null, 
            string tName = null)
        {
            var w= this.W;
            return w == null 
                ? default 
                : w.Run(method, tName);
        }

        public XTuple<T, U> Run<T, U>(
            Do<T, U> method = null, 
            string tName = null, 
            string uName = null)
        {
            return this.W?.Run(
                method,
                tName,
                uName);
        }

        public XTuple<T, U, V> Run<T, U, V>(
            Do<T, U, V> method = null, 
            string tName = null, 
            string uName = null, 
            string vName = null)
        {
            return this.W?.Run(
                method,
                tName,
                uName,
                vName);
        }

        public XTuple<T, U, V, W> Run<T, U, V, W>(
            Do<T, U, V, W> method = null, 
            string tName = null, 
            string uName = null, 
            string vName = null,
            string wName = null)
        {
            return this.W?.Run(
                method,
                tName,
                uName,
                vName,
                wName);
        }

        public virtual XTuple<T, U, V, W, X> Run<T, U, V, W, X>(
            Do<T, U, V, W, X> method = null,
            string tName = null,
            string uName = null,
            string vName = null,
            string wName = null,
            string xName = null)
        {
            return this.W?.Run(
                method,
                tName,
                uName,
                vName,
                wName,
                xName);
        }

        public XTuple<T, U, V, W, X, Y> Run<T, U, V, W, X, Y>(
            Do<T, U, V, W, X, Y> method = null, 
            string tName = null, 
            string uName = null, 
            string vName = null,
            string wName = null, 
            string xName = null, 
            string yName = null)
        {
            return this.W?.Run(
                method,
                tName,
                uName,
                vName,
                wName,
                xName,
                yName);
        }

        public XTuple<T, U, V, W, X, Y, Z> Run<T, U, V, W, X, Y, Z>(
            Do<T, U, V, W, X, Y, Z> method = null, 
            string tName = null, 
            string uName = null, 
            string vName = null,
            string wName = null, 
            string xName = null, 
            string yName = null, 
            string zName = null)
        {
            return this.W?.Run(
                method,
                tName,
                uName,
                vName,
                wName,
                xName,
                yName,
                zName);
        }

        public XTuple<T, U, V, W, X, Y, Z, AA> Run<T, U, V, W, X, Y, Z, AA>(
            Do<T, U, V, W, X, Y, Z, AA> method = null, 
            string tName = null, 
            string uName = null, 
            string vName = null,
            string wName = null, 
            string xName = null, 
            string yName = null, 
            string zName = null, 
            string aaName = null)
        {
            return this.W?.Run(
                method,
                tName,
                uName,
                vName,
                wName,
                xName,
                yName,
                zName,
                aaName);
        }

        protected ICollection<Star> subs;
        protected Nameable name;
    }
}
