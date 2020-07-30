using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace Rescues
{
    public abstract class ReactiveController<T> : IController, IReactiveController
        where T : class, IInteractable
    {
        private readonly GameContext _contexts;
        private readonly ObservableCollection<T> _interactables;
        private readonly List<T> _buffer;

        protected ReactiveController(GameContext contexts)
        {
            _contexts = contexts;
            _interactables = new ObservableCollection<T>();
            _buffer = new List<T>();
        }
        
        protected abstract ReactiveType GetTrigger(T interactable);

        protected abstract bool Filter(T interactable);

        protected abstract void Execute(List<T> interactables);

        public void Execute()
        {
            if (_contexts.GetListInteractable().Count < 0)
            {
                return;
            }

            for (int i = 0; i < _contexts.GetListInteractable().Count; i++)
            {
                var temp = _contexts.GetListInteractable()[i] as T;
                if (Filter(temp))
                {
                    _buffer.Add(temp);
                }
            }
            
            if (_buffer.Count < 0)
            {
                return;
            }
            
            try
            {
                Execute(_buffer);
            }
            finally
            {
                _buffer.Clear();
            }
        }

        public void Activate()
        {
            throw new NotImplementedException();
        }

        public void Deactivate()
        {
            throw new System.NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }
    }
}
