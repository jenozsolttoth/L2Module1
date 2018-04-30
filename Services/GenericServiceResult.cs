using System;
using System.Collections.Generic;
using System.Text;
using ServiceInterfaces;
namespace Services
{
    public class GenericServiceResult<T> : IServiceResult<T>
    {
        public GenericServiceResult(T entity, bool success, string message)
        {
            Entity = entity;
            Success = success;
            Message = message;
        }
        public T Entity { get; }

        public bool Success { get; }

        public string Message { get; }
    }
}
