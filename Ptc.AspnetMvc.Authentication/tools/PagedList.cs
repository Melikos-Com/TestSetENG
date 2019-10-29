using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{

    public class PagedList<T> : IReadOnlyList<T>
    {
        private readonly int _pageIndex;
        private readonly int _pageSize;
        private readonly int _totalCount;
        private readonly List<T> _collection;

        public PagedList() { }

        public PagedList(IEnumerable<T> collection)
        {
            _collection = new List<T>(collection);
            _pageIndex = 0;
            _pageSize = 0;
            _totalCount = collection.Count();
        }

        public PagedList(IEnumerable<T> collection, int pageIndex, int pageSize)
        {
            if (pageSize == 0)
            {
                _collection = new List<T>(collection);

            }
            else
            {
                _collection = new List<T>(collection.Skip(pageIndex * pageSize).Take(pageSize));
                _pageIndex = pageIndex;
                _pageSize = pageSize;
            }
            _totalCount = collection.Count();


        }

        public PagedList(IEnumerable<T> collection, int pageIndex, int pageSize, int totalCount)
        {
            _collection = new List<T>(collection);
            _pageIndex = pageIndex;
            _pageSize = pageSize;
            _totalCount = totalCount;
        }

        public int TotalCount
        {
            get
            {
                return _totalCount;
            }
        }

        public int PageIndex
        {
            get
            {
                return _pageIndex;
            }
        }

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
        }

        public T this[int index]
        {
            get
            {
                return _collection[index];
            }
        }

        public int Count
        {
            get
            {
                return _collection.Count;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return (_collection as IEnumerable<T>).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _collection.GetEnumerator();
        }


    }
}
