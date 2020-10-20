using System;

namespace Model
{
    [Serializable]
    public class Response<T>
    {
        public int code;
        public T data;
    }
}