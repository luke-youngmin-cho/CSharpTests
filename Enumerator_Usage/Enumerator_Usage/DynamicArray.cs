using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enumerator_Usage
{
    internal class DynamicArray<T> : IList<T>
    {
        private T[] _data;
        public T this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private int _count;
        public int Count { get => _count; }

        public bool IsReadOnly => throw new NotImplementedException();

        public DynamicArray()
        {
            _data = new T[1];
            _count = 0;
        }

        public void Add(T item)
        {
            if (_count >= _data.Length)
            {
                int newSize = _data.Length + (int)(Math.Log10(10 * (_count % 10 + 1)));
                T[] tmpData = new T[newSize];
                int tmpLength = _data.Length;
                for (int i = 0; i < tmpLength; i++)
                    tmpData[i] = _data[i];
                _data = new T[newSize];
                for (int i = 0; i < tmpLength; i++)
                    _data[i] = tmpData[i];
            }
            _data[_count] = item;
            _count++;
        }

        public void Clear()
        {
            _data = new T[0];
        }

        public bool Contains(T item)
        {
            for (int i = 0; i < _count; i++)
                if (_data[i].Equals(item))
                    return true;
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (arrayIndex > _count - 1)
                throw new ArgumentOutOfRangeException();

            _count = arrayIndex + 1;
            for (int i = 0; i < array.Length; i++)
                Add(array[i]);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new DynamicArrayEnum<T>(_data);
        }

        public int IndexOf(T item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, T item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

    public class DynamicArrayEnum<T> : IEnumerator<T>
    {
        private bool _disposed = false;
        private readonly T[] _data;
        private int index = -1;
        public T Current
        {
            get
            {
                try
                {
                    return _data[index];
                }
                catch
                {
                    throw new InvalidOperationException();
                }
            }
        }

        object IEnumerator.Current { get => Current; }

        public DynamicArrayEnum(T[] data) { 
            _data = data;
        }

        // Dispose 에서 관리되는 영역에 대한 해제를 시도할 경우 (ex. 객체에 null 대입하기 등)
        // GC 를 실제로 호출해주지는 않음. 
        // GC.Collect() 를 호출할 경우 , GC.Collect() 는 호출되는순간 가비지를 수집하는게 아니라, 
        // 안전하게 모든 쓰레드를 멈춘 후 수집함. 
        // 즉 관리되는 영역에 대해서는 Dispose 에서 호출할 경우 성능에 전혀 도움이 되지못함.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); // 종료자 호출 하지 않도록 함 
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                // 관리되지 않는 리소스 해제                 
            }                

            _disposed = true;
        }

        public bool MoveNext()
        {
            index++;
            return (index < _data.Length);  
        }

        public void Reset()
        {
            index = -1;
        }
    }
}
