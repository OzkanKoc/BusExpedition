using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//will generated ienumerator
namespace VoyageFramework.Collections
{
    public class ListCollection<TEntity> : IEnumerable where TEntity : class
    {
        private TEntity[] _array = new TEntity[0];
        public int Count
        {
            get
            {
                return _array.Length;
            }
        }

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
                if (tObject == _array[i])
                {
                    return true;
                }
            }
            return false;
        }

        public int IndexOf(TEntity tObject)
        {
            return Array.IndexOf(_array, tObject);
        }

        public IEnumerator GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
