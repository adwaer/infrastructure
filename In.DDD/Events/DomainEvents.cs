using System;
using System.Collections.Generic;
using In.Common;

namespace In.DDD.Events
{
    /// <summary>
    /// http://www.udidahan.com/2009/06/14/domain-events-salvation/
    /// </summary>
    public static class DomainEvents
    {
        [ThreadStatic] //so that each thread has its own callbacks
        private static List<Delegate> _actions;

        private static IDiScope _container;

        public static void Init(IDiScope container)
        {
            _container = container;
        }

        //Registers a callback for the given domain event, used for testing only
        public static void Register<T>(Action<T> callback) where T : DomainEvent
        {
            if (_actions == null)
                _actions = new List<Delegate>();

            _actions.Add(callback);
        }

        //Clears callbacks passed to Register on the current thread
        public static void ClearCallbacks()
        {
            _actions = null;
        }

        //Raises the given domain event
        public static void Raise<T>(T args) where T : DomainEvent
        {
            if (_container != null)
                foreach (var handler in _container.ResolveAll<IEventMsgHandle<T>>())
                    handler.Handle(args);

            if (_actions == null) return;
            foreach (var action in _actions)
            {
                if (action is Action<T> action1)
                {
                    action1(args);
                }
            }
        }
    }
}