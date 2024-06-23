using System.Collections;
using System.Collections.Generic;

namespace Utilities
{
    /// <summary>
    /// Кольцевой двусвязный список
    /// </summary>
    /// <typeparam name="T">Тип содержимого структуры данных</typeparam>
    public class CircularDoublyLinkedList<T> : IEnumerable<T>
    {
        public DoublyNode<T> Head { get; private set; }
        public int Count => _count;
        public bool IsEmpty => _count == 0;

        private int _count;
 
        public void Add(T data)
        {
            var node = new DoublyNode<T>(data);
 
            if (Head == null)
            {
                Head = node;
                Head.Next = node;
                Head.Previous = node;
            }
            else
            {
                node.Previous = Head.Previous;
                node.Next = Head;
                Head.Previous.Next = node;
                Head.Previous = node;
            }
            _count++;
        }
        
        public bool Remove(T data)
        {
            var current = Head;
 
            DoublyNode<T> removedItem = null;
            if (_count == 0) return false;
 
            do
            {
                if (current.Data.Equals(data))
                {
                    removedItem = current;
                    break;
                }
                current = current.Next;
            }
            while (current!=Head);
 
            if (removedItem != null)
            {
                if (_count == 1)
                {
                    Head = null;
                }
                else
                {
                    if(removedItem==Head)
                    {
                        Head = Head.Next;
                    }
                    removedItem.Previous.Next = removedItem.Next;
                    removedItem.Next.Previous = removedItem.Previous;
                }
                _count--;
                return true;
            }
            return false;
        }
 
        public void Clear()
        {
            Head = null;
            _count = 0;
        }
 
        public bool Contains(T data)
        {
            var current = Head;
            if (current == null) return false;
            do
            {
                if (current.Data.Equals(data)) return true;
                current = current.Next;
            }
            while (current != Head);
            return false;
        }
 
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this).GetEnumerator();
        }
 
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            var current = Head;
            do
            {
                if (current != null)
                {
                    yield return current.Data;
                    current = current.Next;
                }
            }
            while (current != Head);
        }
    }
    
    public class DoublyNode<T>
    {
        public DoublyNode(T data)
        {
            Data = data;
        }
        public T Data { get; set; }
        public DoublyNode<T> Previous { get; set; }
        public DoublyNode<T> Next { get; set; }
    }
}