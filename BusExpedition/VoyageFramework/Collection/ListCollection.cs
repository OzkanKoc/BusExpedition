using System;
using System.Collections;

namespace VoyageFramework.Collection
{
    public class ListCollection<TEntity> : ICollection
    {
        private TEntity[] _array = new TEntity[0];
        public int Count
        {
            get
            {
                return _array.Length;
            }
        }

        public object SyncRoot => this;

        public bool IsSynchronized => false;

        public TEntity this[int index]
        {
            get
            {
                return _array[index];
            }
        }

        public void Add(TEntity tObject)
        {
            Array.Resize(ref _array, _array.Length + 1);
            _array[_array.Length - 1] = tObject;
        }

        public void Remove(TEntity tObject)
        {
            for (int i = IndexOf(tObject); i < _array.Length; i++)
            {
                _array[i] = _array[i + 1];
            }
            Array.Resize(ref _array, _array.Length - 1);
        }
        public bool Contains(TEntity tObject)
        {
            for (int i = 0; i < _array.Length; i++)
            {
                if (Equals(tObject, _array[i]))
                    return true;
            }
            return false;
        }

        public int IndexOf(TEntity tObject)
        {
            return Array.IndexOf(_array, tObject);
        }

        public void CopyTo(TEntity[] array)
        {
            CopyTo(array, 0);
        }
        public void CopyTo(Array array, int index)
        {
            if (array.GetType() != _array.GetType())
            {
                throw new InvalidCastException("Liste tipi ile dizi tipi uyuşmuyor.");
            }
            if (array.Rank != 1)
            {
                throw new RankException("Dizi boyutu uygun değil.");
            }
            if ((array.Length < _array.Length + index))
            {
                throw new ArgumentOutOfRangeException("Dizi aralık dışında.");
            }
            if ((array != null))
                Array.Copy(_array, 0, array, index, _array.Length);
        }
        IEnumerator IEnumerable.GetEnumerator() => new Enumerator(_array);
    }
}
