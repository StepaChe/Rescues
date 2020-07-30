using System;
using System.Collections.Generic;
using UnityEngine;


namespace Rescues
{
    public abstract class BaseUi : MonoBehaviour
    {
        public Action ShowUI = delegate {  };
        public Action HideUI = delegate {  };
        public abstract void Show();
        public abstract void Hide();
        private Dictionary<Type, IPartUI> _partMainMenus;

        protected virtual void Awake()
        {
            _partMainMenus = new Dictionary<Type, IPartUI>(5);
            var partMainMenus = GetComponents<MonoBehaviour>();

            foreach (var mainMenu in partMainMenus)
            {
                if (mainMenu is IPartUI partUi)
                {
                    _partMainMenus.Add(partUi.Type, partUi);
                }
            }
        }

        public T GetPart<T>() where T : IPartUI
        {
            return (T)_partMainMenus[typeof(T)];
        }
    }
}
