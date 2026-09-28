using System;
using System.Collections.Generic;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Utils.Reactive
{
    public class ReactiveList<TItem>
    {
        // Delegates
        public event Action<TItem> Added = null;
        public event Action<TItem> Removed = null;
        public event Action Cleared = null;

        // Runtime
        private List<TItem> _items = null;

        public ReactiveList()
            => _items = new List<TItem>();

        public ReactiveList(List<TItem> list)
            => _items = new List<TItem>(list);

        // Runtime
        public TItem[] Items => _items.ToArray();
        public int Count => _items.Count;

        public void Add(TItem item)
        {
            _items.Add(item);

            Added?.Invoke(item);
        }

        public void Remove(TItem item)
        {
            _items.Remove(item);

            Removed?.Invoke(item);
        }

        public void Clear()
        {
            _items.Clear();

            Cleared?.Invoke();
        }
    }
}