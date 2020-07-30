using UnityEngine;


namespace Rescues
{
    public sealed class EnemyBehaviour : MonoBehaviour // TODO отрефакторить под конвенцию
    {
        public EnemyData EnemyData;
        public int PatrolPointState;
        public RouteData RouteData;

        private PhysicsService _physicsService;
        private Vector3 _visionDirection;
        private Animator _animator;

        private void Awake()
        {
            _physicsService = Services.SharedInstance.PhysicsService;
            _timeRemaining = new TimeRemaining (ResetWaitState, 0.0f);
            _animator = GetComponent<Animator>();
            EnemyData.StateEnemy = StateEnemy.None;
        }

        private void Update()
        {
            switch (GetWaitState())
            {
                case StateEnemy.None: 
                    {
                        _animator.Play("Base Layer.None");
                        break;
                    }
                case StateEnemy.Patrol:
                    {
                        _animator.Play("Base Layer.Patrol");
                        break;
                    }
                case StateEnemy.Inspection:
                    {
                        _animator.Play("Base Layer.Inspection");
                        break;
                    }
                case StateEnemy.Detected:
                    {
                        _animator.Play("Base Layer.Detected");
                        break;
                    }
                case StateEnemy.Dead:
                    {
                        _animator.Play("Base Layer.Dead");
                        break;
                    }
            }
        }

        public int Modificator { get => _modificator; }

        private int _modificator = 1;
        private TimeRemaining _timeRemaining;

        public void InvertModificator()
        {
            _modificator *= -1;
        }

        public void Vision()
        {
            var hit = _physicsService.VisionDetectionPlayer(transform.position, _visionDirection, EnemyData.VisionDistance);

            if (hit)
            {
                ScreenInterface.GetInstance().Execute(ScreenType.GameOver);
            }
        }

        public void SetVisionDirection(Vector3 visionDirection)
        {
            _visionDirection = visionDirection.normalized;
        }

        public StateEnemy GetWaitState()
        {
            return EnemyData.StateEnemy;
        }

        private void SetInspectionState()
        {
            if (EnemyData.StateEnemy == StateEnemy.Patrol)
            {
                EnemyData.StateEnemy = StateEnemy.Inspection;
            }
        }

        public void WaitTime(float waitTime)
        {
            SetInspectionState();
            _timeRemaining.AddTimeRemaining(waitTime);
        }

        private void ResetWaitState()
        {
            CustomDebug.Log(PatrolPointState);
            var trapInfoBaseTrapData = RouteData.GetWayPoints()[PatrolPointState].TrapInfo.BaseTrapData;
            if (trapInfoBaseTrapData != null)
            {
                trapInfoBaseTrapData.ActivateTrap(EnemyData);
                if (trapInfoBaseTrapData.IsActive)
                {
                    return;
                }
            }

            EnemyData.StateEnemy = StateEnemy.Patrol;
            PatrolPointState += Modificator;
        }
    }
}
