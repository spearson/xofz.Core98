using System.Collections.Generic;

namespace xofz
{
    public interface LotterV3
        : LotterV2
    {
        GetArray<T> Index<T>(
            IEnumerable<T> finiteSource);
    }
}