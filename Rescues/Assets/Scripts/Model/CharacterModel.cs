using System;
using UnityEngine;


namespace Rescues
{
    public sealed class CharacterModel
    {
        #region Fields

        private readonly float _speed;
        private SpriteRenderer _characterSprite;
        private CapsuleCollider2D _playerCollider;
        private Rigidbody2D _playerRigidbody2D;
        private State _state;
        public Timer AnimationPlayTimer;
        private Vector2 _direction;
        private Vector3 _teleportPosition;
        private HidingPlaceBehaviour _hidingPlaceBehaviour;
        private Animator _animator;        

        #endregion


        #region Properties

        public Transform Transform { get; }
        private PlayerBehaviour PlayerBehaviour { get; }
        public AudioSource PlayerSound { get; }
        public float AnimationTimer { get; set; }
        public State PlayerState { get { return _state; } }
        public InteractableObjectBehavior InteractableItem { get; set; }

        #endregion


        #region ClassLifeCycle

        public CharacterModel(Transform transform, PlayerData playerData)
        {
            _speed = playerData.Speed;
            _characterSprite = transform.GetComponent<SpriteRenderer>();
            _playerCollider = transform.GetComponent<CapsuleCollider2D>();
            _playerRigidbody2D = transform.GetComponent<Rigidbody2D>();
            _animator = transform.GetComponent<Animator>();
            AnimationPlayTimer = new Timer();
            Transform = transform;
            PlayerSound = Transform.GetComponent<AudioSource>();
            PlayerBehaviour = Transform.GetComponent<PlayerBehaviour>();

        }

        #endregion


        #region StateMachine     

        public void StateIdle()
        {
            SetState(State.Idle);
            _animator.Play("Base Layer.Idle");
        }

        public void StateHideAnimation(HidingPlaceBehaviour hidingPlaceBehaviour)
        {
            if (hidingPlaceBehaviour != null)
            {
                _hidingPlaceBehaviour = hidingPlaceBehaviour;
            }           
            SetState(State.HideAnimation);                                           
            StartHiding();
            _animator.Play("Base Layer.HideAnimation");
        }

        public void StateHiding()
        {                            
            if (Hide())
            {
                _animator.Play("Base Layer.Hiding");
                SetState(State.Hiding);                             
            }
            else
            {
                StateIdle();                
            }
        }

        public void StatePickUpAnimation(ItemBehaviour itemBehaviour)
        {
            InteractableItem = itemBehaviour;
            SetState(State.PickUpAnimation);
            _animator.Play("Base Layer.PickUpAnimation");
            AnimationPlayTimer.StartTimer(itemBehaviour.PickUpTime);           
        }

        public void StateCraftTrapAnimation(TrapBehaviour trapBehaviour)
        {
            InteractableItem = trapBehaviour;
            SetState(State.CraftTrapAnimation);
            _animator.Play("Base Layer.CraftTrapAnimation");
            AnimationPlayTimer.StartTimer(trapBehaviour.TrapInfo.BaseTrapData.CraftingTime);
        }

        public void StateTeleporting(DoorTeleporterBehaviour doorTeleporterBehaviour)
        {
            SetState(State.Teleporting);
            _animator.Play("Base Layer.Teleporting");
            _teleportPosition = doorTeleporterBehaviour.ExitPoint.position;
            AnimationPlayTimer.StartTimer(doorTeleporterBehaviour.TransferTime);
        }

        public void StateMoving(Vector2 direction)
        {
            switch (_state)
            {
                case State.Hiding:
                    {                       
                        return;
                    }
                case State.HideAnimation:
                    {                        
                        return;
                    }
                case State.PickUpAnimation:
                    {
                        return;
                    }
                case State.CraftTrapAnimation:
                    {
                        return;
                    }
                case State.Teleporting:
                    {
                        return;
                    }
            }
            SetState(State.Moving);
            _animator.Play("Base Layer.Moving");
            _direction = direction;
        }

        private void SetState(State value)
        {
            _state = value;
        }

        public void StateHandler()
        {
            CustomDebug.Log(PlayerState);
            switch (_state)
            {                              
                case State.Moving:
                    {
                        Move();
                        break;
                    }                   
            }
        }

        #endregion


        #region Methods 

        public void Teleport()
        {
            Transform.position = _teleportPosition;
        }

        private void StartHiding()
        {
            PlayerSound.clip = _hidingPlaceBehaviour.HidingPlaceData.HidingSound;
            AnimationTimer = _hidingPlaceBehaviour.HidingPlaceData.AnimationDuration;         
            PlayAnimationWithTimer();
            if (_playerRigidbody2D.bodyType == RigidbodyType2D.Dynamic)
            {
                _characterSprite.enabled = false; //чтобы спрайт выключался сразу, когда идет процесс пряток
            }
            else
            {
                _hidingPlaceBehaviour.HidedSprite.enabled = false; //чтобы спрайт хайдинг плейс бехевора выключался сразу, когда персонаж начинает вылезать
            }
        }

        private bool Hide()
        {
            bool isHided;
            _playerCollider.enabled = !_playerCollider.enabled;          
            if (_playerRigidbody2D.bodyType == RigidbodyType2D.Dynamic)
            {
                _playerRigidbody2D.bodyType = RigidbodyType2D.Static;
                isHided = true;
                _hidingPlaceBehaviour.HidedSprite.enabled = true; //чтобы спрайт хайдинг плейс бехевора включался только тогда, когда персонаж спрятался
            }
            else
            {
                _playerRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
                isHided = false;
                _characterSprite.enabled = true; //чтобы спрайт выключался только тогда, когда персонаж уже вылез
            }

            return isHided;
        }

        private void PlayAnimationWithTimer()
        {
            if (PlayerSound.clip != null)
            {
                PlayerSound.Play();
            }          
            AnimationPlayTimer.StartTimer(AnimationTimer);
        }

        private void Move()
        {
            _direction *= _speed * Time.deltaTime;

            Transform.Translate(_direction);

            if (_direction.x == 0)
            {
                StateIdle();               
            }

            if (_direction.x > 0 && _characterSprite.flipX)
            {
                Flip();               
            }
            else if (_direction.x < 0 && !_characterSprite.flipX)
            {
                Flip();            
            }
        }

        private void Flip()
        {
            _characterSprite.flipX = !_characterSprite.flipX;
        }

        #endregion
    }
}
