using System.Collections;
using UnityEngine;

namespace Characters.AI
{
    public class PatrollerAI : WalkerAI
    {
        [SerializeField] private EnemyStates _currentState;
        [SerializeField] private float _idleTime;
        [SerializeField] private float _patrolTime;
        [SerializeField] private float _changeDirTime;

        protected EnemyStates CurrentState { get => _currentState; set => _currentState = value; }
        protected Vector2 CurrentDirection { get; set; }

        protected float _idleDelay;
        private bool _isPatrol;
        private bool _canChangeDir;
        private bool _canChangePatrolState;

        private void Start() => Initialize();
        private void OnValidate() => Initialize();
        protected virtual void Update() => CheckConditions();
        private void FixedUpdate() => StateExecute();

        protected virtual void Initialize()
        {
            CurrentDirection = GetRandomDirection();
            
            _canChangePatrolState = true;
            _canChangeDir = true;
        }

        protected virtual void StateExecute()
        {
            switch (_currentState)
            {
                case EnemyStates.Idle:
                    Idle();
                    break;
                case EnemyStates.Patrol:
                    Patrol();
                    break;
                case EnemyStates.Run:
                    Run(CurrentDirection);
                    break;
            }
        }

        protected virtual void CheckConditions()
        {
            if (_canChangePatrolState) StartCoroutine(ChangePatrolState());
        }

        private void Patrol()
        {
            if (_canChangeDir) StartCoroutine(ChangeDirection(GetRandomDirection()));
            
            Walk(CurrentDirection);
        }

        protected virtual void Idle()
        {
            _personAnimate.Walk(CurrentDirection, false);
        }

        protected override void Run(Vector2 direction)
        {
            StopCoroutine(ChangeDirection(direction));
            base.Run(direction);
        }

        protected IEnumerator ChangeDirection(Vector2 direction)
        {
            _canChangeDir = false;
            CurrentDirection = direction;
            yield return new WaitForSeconds(_changeDirTime);
            _canChangeDir = true;
        }

        private IEnumerator ChangePatrolState()
        {
            _canChangePatrolState = false;

            if (Random.Range(0, 2) <= 0)
            {
                _currentState = EnemyStates.Patrol;
                _idleDelay = _patrolTime;
            }
            else
            {
                _currentState = EnemyStates.Idle;
                _idleDelay = _idleTime;
            }

            yield return new WaitForSeconds(_idleDelay);
            _canChangePatrolState = true;
        }

        protected Vector2 GetOppositeDirection(Vector2 direction, bool isRandomDir)
        {
            float angle = 180;
            if (isRandomDir) angle = Random.Range(90, 270);

            return (Quaternion.AngleAxis(angle, Vector3.forward) * direction).normalized;
        }

        protected Vector2 GetRandomDirection() => (Quaternion.AngleAxis(
                Random.Range(0, 360), Vector3.forward) * Vector2.right).normalized;

        private void OnCollisionEnter2D(Collision2D other)
        {
            // ������������ ��� ����� � �����
            StopCoroutine(ChangeDirection(GetRandomDirection()));
            StartCoroutine(ChangeDirection(GetOppositeDirection(other.transform.position, false)));
        }

        protected enum EnemyStates
        {
            Idle,
            Patrol,
            Run
        }
    }
}