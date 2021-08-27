namespace xofz.Tests.Framework.Transformation
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;
    using xofz.Framework.Transformation;
    using Ploeh.AutoFixture;
    using Xunit;

    public class EnumerableDraggerTests
    {
        public class Context
        {
            protected Context()
            {
                this.dragger = new EnumerableDragger();
                this.fixture = new Fixture();
            }

            protected readonly EnumerableDragger dragger;
            protected readonly Fixture fixture;
        }

        public class When_Drag_is_called : Context
        {
            [Fact]
            public void Yield_breaks_if_source_is_null()
            {
                var noItems = true;
                foreach (var item in this.dragger.Drag<object>(
                    null, new ArrayLot<int>(
                        new[] { 3, 2, 1 })))
                {
                    noItems = false;
                }

                Assert.True(noItems);
            }

            [Fact]
            public void Yield_returns_original_items_if_dragLengths_is_null()
            {
                var items = new object[]
                {
                    new object(),
                    new object(),
                    new object(),
                    new object(),
                    new object(),
                    new object(),
                    new object()
                };
                long counter = 0;
                foreach (var item in this.dragger.Drag(items, null))
                {
                    Assert.Same(item, items[counter]);
                    ++counter;
                }
            }

            [Fact]
            public void Otherwise_drags_out_the_enumerable_by_the_drag_lengths()
            {
                var f = this.fixture;
                var item1 = new LabeledObject
                {
                    Label = f.Create<string>()
                };
                var item2 = new LabeledObject
                {
                    Label = f.Create<string>()
                };
                var item3 = new LabeledObject
                {
                    Label = f.Create<string>()
                };
                var items = new[]
                {
                    item1, item2, item3
                };

                ICollection<LabeledObject> collection = XLinkedList<LabeledObject>.Create(
                    this.dragger.Drag(
                        items,
                        new ArrayLot<int>(new[] { 5, 4, 6 })));
                ICollection<LabeledObject> itemOnes = XLinkedList<LabeledObject>.Create(
                    EnumerableHelpers.Where(
                        collection,
                        lo => ReferenceEquals(lo, item1)));
                ICollection<LabeledObject> itemTwos = XLinkedList<LabeledObject>.Create(
                    EnumerableHelpers.Where(
                        collection,
                        lo => ReferenceEquals(lo, item2)));
                ICollection<LabeledObject> itemThrees = XLinkedList<LabeledObject>.Create(
                    EnumerableHelpers.Where(
                        collection,
                        lo => ReferenceEquals(lo, item3)));
                Assert.Equal(5, itemOnes.Count);
                Assert.Equal(4, itemTwos.Count);
                Assert.Equal(6, itemThrees.Count);
            }

            public class LabeledObject
            {
                public virtual string Label { get; set; }
            }
        }
    }
}
