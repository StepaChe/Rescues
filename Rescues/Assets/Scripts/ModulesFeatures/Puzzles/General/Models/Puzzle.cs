using System;
using System.Collections;
using UnityEngine;


namespace Rescues
{
    /// <summary>
    /// От этого класса наследуются модели пазлов
    /// </summary>
    public abstract class Puzzle : MonoBehaviour
    {
        #region Fileds

        [SerializeField] private float _delayAfterFinish = 3f;
        public event Action<Puzzle> Activated = puzzle => { };
        public event Action<Puzzle> Closed = puzzle => { };
        public event Action<Puzzle> Finished = puzzle => { };
        public event Action<Puzzle> ResetValuesToDefault = puzzle => { };
        public event Action<Puzzle> CheckCompleted = puzzle => { };

        public bool IsFinished = false;

        #endregion
        
        
        #region Methods

        [ContextMenu("Activate puzzle")]
        public void Activate()
        {
            //TODO сюда добавить команду на ограничение управления в игре, паузу итп.
            Activated.Invoke(this);
        }

        public void Close()
        {
            if (_delayAfterFinish > 0)
                StartCoroutine(CloseWithDelay());
            else
                Closed.Invoke(this);
        }

        public void Finish()
        {
            Finished.Invoke(this);
        }

        public void ResetValues()
        {
            ResetValuesToDefault.Invoke(this);
        }

        public void CheckComplete()
        {
            CheckCompleted.Invoke(this);
        }

        private IEnumerator CloseWithDelay()
        {
            yield return new WaitForSeconds(_delayAfterFinish);
            Closed.Invoke(this);
            gameObject.SetActive(false);
        }

        public void ForceClose()
        {
            gameObject.SetActive(false);
        }
        
        #endregion
    }
}