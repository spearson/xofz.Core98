namespace xofz.Tests.Framework
{
    using System;
    using System.Threading;
    using xofz.Framework;
    using xofz.Framework.Computation;
    using xofz.Presentation;
    using Xunit;

    public class ThreadSafeMethodWebTests
    {
        [Fact]
        public void GoHard()
        {
            var w = new ThreadSafeMethodWeb();
            ThreadPool.QueueUserWorkItem(o =>
            {
                for (short i = 0; i < 0xFFF; ++i)
                {
                    w.RegisterDependency(new object());
                }
            });

            for (short i = 0; i < 0xFFF; ++i)
            {
                w.RegisterDependency(new object());
                w.Run<QuickSorter>();
            }

            w.RegisterDependency(new QuickSorter());
            ThreadPool.QueueUserWorkItem(
                o => w.RegisterDependency(
                    new Navigator(w)));
            ThreadPool.QueueUserWorkItem(
                o => w.RegisterDependency(
                    new AccessController(w)));
            ThreadPool.QueueUserWorkItem(o =>
            {
                for (byte j = 0; j < 0xFF; ++j)
                {
                    w.Run<QuickSorter>();
                    w.Run<AccessController>();
                }
            });
            w.Run<AccessController, QuickSorter>();
            var sorted = false;
            w.Run<QuickSorter>(sorter =>
            {
                sorter.Sort(new long[]
                    {0xFFFFF, 0xDFFFF, 0xF, 0xFF, 0xFFF, 0xFFFF});
                sorted = true;
            });

            for (short i = 0; i < 0x1FF; ++i)
            {
                w.RegisterDependency(new object());
                w.Unregister<object>();
            }

            if (!sorted)
            {
                throw new InvalidOperationException();
            }
        }
    }
}
