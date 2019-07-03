using System;
using System.Collections.Generic;
using System.Linq;

namespace ylang.net.db.import
{
    static class EnumerableExtensions
    {
        public static IEnumerable<IReadOnlyCollection<T>> ChunksOfSize<T>(this IEnumerable<T> collection, int size)
        {
            collection = collection ?? throw new ArgumentNullException(nameof(collection));
            size = size > 0 ? size : throw new ArgumentOutOfRangeException(nameof(size));

            List<T> chunk = null;

            foreach (var item in collection)
            {
                if (chunk == null)
                {
                    chunk = new List<T>(size);
                }

                chunk.Add(item);

                if (chunk.Count == size)
                {
                    yield return chunk;
                    chunk = null;
                }
            }

            if (chunk != null)
            {
                yield return chunk;
            }
        }
    }
}
