using System;
using System.Collections;

namespace VoyageFramework.Collection
{
    public class Enumerator : IEnumerator
    {
        private ArrayList _list = new ArrayList();
        private int _cursor;

        public Enumerator(Array arr)
        {
            _list.AddRange(arr);
            _cursor = -1;
        }
        public object Current
        {
            get
            {
                return _list[_cursor];
            }
        }

        public bool MoveNext()
        {
            return ++_cursor != _list.Count;
        }

        public void Reset()
        {
            _cursor = -1;
        }
    }
}
