using System;

namespace DBWatcher.Core.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException() { }
        public EntityNotFoundException(string message) : base(message) { }
    }

    public class EntityNotFoundException<T, TKey> : EntityNotFoundException
    {
        protected const string msgTemplate = "Entity of class {0} with id {1} was not found";

        public EntityNotFoundException(TKey key) : base(string.Format(msgTemplate, typeof(T).Name, key)) { }
    }
}