using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyAnimationState
{
    Follow,
    Attack,
    Dead
}

public class EnemyBehavior : MonoBehaviour
{
    public IState _State;
    public EnemyAnimationState _AnimationState;
    public Animator _Animator;
    public SpriteRenderer _Renderer;
    public BoxCollider2D _Collider;

    public Transform _Target;
    public float _MoveSpeed, _AttackSpeed;


    private void Awake()
    {
        GetComponents();
    }

    void Start()
    {
        _State = new Follow(this, _Target);
        _State.OnEnter();
    }

    void GetComponents()
    {
        _Animator = GetComponent<Animator>();
        _Renderer = GetComponent<SpriteRenderer>();
        _Collider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        _State.Update();
    }

    void FixedUpdate()
    {
        _State.FixedUpdate();
    }

    public void SetAnimation(EnemyAnimationState aninemationstate)
    {
        _AnimationState = aninemationstate;
    }

    public void SwitchState(IState state)
    {
        _State.OnExit();
        _State = state;
        _State.OnEnter();
        // _Animator.SetInteger("State", (int)_AnimationState);
    }
}
public class Follow : IState
{
    EnemyBehavior _Enemy;
    Transform _Target;
    float _Distance;
    public Follow(EnemyBehavior enemy, Transform target)
    {
        _Enemy = enemy;
        _Target = target;
    }

    public void OnEnter()
    {
        _Enemy.SetAnimation(EnemyAnimationState.Follow);
    }

    public void Update()
    {
        if (_Distance > 3 && _Distance < 5)
        {
            _Enemy.SwitchState(new Attack(_Enemy, _Target));
        }
    }

    public void FixedUpdate()
    {
        _Distance = Vector2.Distance(_Enemy.transform.position, _Target.position);
        _Enemy.transform.position = Vector2.Lerp(_Enemy.transform.position, _Target.position, _Enemy._MoveSpeed * Time.fixedDeltaTime);
    }

    public void OnExit()
    {

    }
}

public class Attack : IState
{
    EnemyBehavior _Enemy;
    Transform _Target;
    Vector2 _Prepositon, _Direction;
    float _Delay, _PreDelay, _AttackTime, _Intensity;


    public Attack(EnemyBehavior enemy, Transform target)
    {
        _Enemy = enemy;
        _Target = target;

    }

    public void OnEnter()
    {
        _Enemy.SetAnimation(EnemyAnimationState.Attack);
        _Delay = 1.5f;
        _AttackTime = 0.5f;
        _Prepositon = _Enemy.transform.position;
    }

    public void Update()
    {

        _Delay -= Time.deltaTime;
        if (_Delay <= 0)
        {
            _AttackTime -= Time.deltaTime;
        }
        _Intensity += Time.deltaTime;
    }

    public void FixedUpdate()
    {
        if (_Delay <= 0)
        {
            if (Mathf.Sign(_PreDelay) > 0)
            {
                _Enemy.transform.position = _Prepositon;
               _Direction = (_Target.position - _Enemy.transform.position).normalized;
            }

            _Enemy.transform.Translate(_Direction * _Enemy._AttackSpeed * Time.fixedDeltaTime);


            if (_AttackTime <= 0)
            {
                _Enemy.SwitchState(new Follow(_Enemy, _Target));
            }
        }
        else
        {
            _Enemy.transform.position = _Prepositon + Random.insideUnitCircle * 0.1f * _Intensity;
        }

        _PreDelay = _Delay;
        
    }

    public void OnExit()
    {

    }
}

public class EnemyState : IState
{
    EnemyBehavior _Enemy;

    public EnemyState(EnemyBehavior enemy)
    {
        _Enemy = enemy;
    }

    public void OnEnter()
    {

    }

    public void Update()
    {

    }

    public void FixedUpdate()
    {

    }

    public void OnExit()
    {

    }
}
