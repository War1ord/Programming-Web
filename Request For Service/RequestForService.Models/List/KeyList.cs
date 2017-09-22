using System.Collections.Generic;

namespace RequestForService.Models.List
{
    public class KeyList<TKey, TEntity> : List<TEntity>
    {
        public TKey Key { get; set; }

        public KeyList(TKey key, IEnumerable<TEntity> collection) : base(collection)
        {
            Key = key;
        }

        public KeyList(TKey key) : base(new List<TEntity>())
        {
            Key = key;
        }

        public KeyList() : base(new List<TEntity>())
        {
            Key = default (TKey);
        }

    }
}