using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public interface IState
{
    void OnEnter();
    void Update();
    void FixedUpdate();
    void OnExit();
}

public enum PlayerAnimationState
{
    Idle,
    Walk,
    Slide,
    Squad,
    Jump,
    Fall,
    Dash,
    Land,
    Eat
}

public class PlayerBehavior : MonoBehaviour
{

    public IState _State;
    public PlayerAnimationState _AnimationState;
    public Animator _Animator;
    public SpriteRenderer _Renderer;
    public Rigidbody2D _RigidBody;
    public BoxCollider2D _Collider;

    [SerializeField] public BasePlayerStats _BaseStats;
    public float _AccelerationMultiplier, _FrictionMultiplier;
    public float _FallForceMultiplier;
    public float _JumpForce, _AirDodgeForce;
    public Vector2 _Velocity, _PreVelocity, _MaxVelocity;

    public LayerMask _StandableMasks;
    public bool _Grounded;
    public bool _CanDodge;
    public bool _CanChain;

    public Vector2 _HitBoxPosition;
    public Vector2 _HitBoxSize;
    public LayerMask _EnemyLayer;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((Vector2)transform.position + _HitBoxPosition, _HitBoxSize);
    }

    void Awake()
    {
        SetStats();
        GetComponents();
        _State = new Idle(this);
    }

    void Start()
    {

    }

    void GetComponents()
    {
        _Animator = GetComponent<Animator>();
        _Renderer = GetComponent<SpriteRenderer>();
        _RigidBody = GetComponent<Rigidbody2D>();
        _Collider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        _State.Update();
    }

    void FixedUpdate()
    {
        GroundCheck();

        _State.FixedUpdate();
        ApplyGravity();
        ApplyFriction();

        SetRigidBodyVelocity();

        FrictionEnd();
        SetPreviousValues();
    }

    private void SetStats()
    {
        _AccelerationMultiplier = _BaseStats._AccelerationMultiplier;
        _FrictionMultiplier = _BaseStats._FrictionMultiplier;
        _FallForceMultiplier = _BaseStats._FallForceMultiplier;
        _JumpForce = _BaseStats._JumpForce;
        _AirDodgeForce = _BaseStats._AirDodgeForce;
        _MaxVelocity = _BaseStats._MaxVelocity;
    }

    private void SetRigidBodyVelocity()
    {
        _RigidBody.velocity = _Velocity;
    }

    private void ApplyGravity()
    {
        if (!_Grounded)
        {
            if (_Velocity.y >= -_MaxVelocity.y)
            {
                _Velocity.y -= _FallForceMultiplier * Time.fixedDeltaTime;
            }
            else
            {
                _Velocity.y = -_MaxVelocity.y;
            }
        }
    }
    private void ApplyFriction()
    {

        if (_Velocity.x != 0)
        {
            _Velocity.x -= _FrictionMultiplier * Mathf.Sign(_Velocity.x) * Time.fixedDeltaTime;
        }

    }

    private void SetPreviousValues()
    {
        _PreVelocity = _Velocity;
    }
    private void FrictionEnd()
    {
        if (_AnimationState != PlayerAnimationState.Dash)
        {
            if (Math.Sign(_Velocity.x) != Math.Sign(_PreVelocity.x) && _PreVelocity.x != 0)
            {
                _Velocity.x = 0;
            }
        }
    }

    public void GroundCheck()
    {
        for (int i = 0; i < 4; i++)
        {
            Vector2 pos = (new Vector2(_Collider.bounds.center.x, _Collider.bounds.center.y) - new Vector2(_Collider.bounds.extents.x, 0)) + new Vector2(1, 0) * _Collider.bounds.size.x / (4 - 1) * i;
            pos.y -= _Collider.bounds.size.y * 0.5f;
            Debug.DrawRay(pos, -transform.up * 0.1f, Color.red);
            if (Physics2D.Raycast(pos, -transform.up, 0.1f, _StandableMasks))
            {
                _Grounded = true;
                break;
            }
            else
            {
                _Grounded = false;
            }
        }
    }

    public void SetAnimation(PlayerAnimationState aninemationstate)
    {
        _AnimationState = aninemationstate;
    }

    public void SwitchState(IState state)
    {
        _State.OnExit();
        _State = state;
        _State.OnEnter();
        _Animator.SetInteger("State", (int)_AnimationState);
    }

    public void ReturnToNeutral()
    {
        if (_Grounded)
        {
            SwitchState(new Idle(this));
        }
        else
        {
            SwitchState(new Fall(this));
        }
    }

    public void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll((Vector2)transform.position + _HitBoxPosition, _HitBoxSize, 0, _EnemyLayer);

        for (int i = 0; i < hits.Length; i++)
        {
            print("Hit");
        }   
    }
}



public class Idle : IState
{
    PlayerBehavior _Player;
    GameManager _GameManager;

    public Idle(PlayerBehavior player)
    {
        _Player = player;
        _GameManager = GameManager.Instance;
    }

    public void OnEnter()
    {
        _Player._CanDodge = true;
        _Player.SetAnimation(PlayerAnimationState.Idle);

        var normTime = _GameManager._CurrentTime % 2;

        _Player._Animator.Play("Nibba_Idle", 0, normTime);
    }

    public void Update()
    {
        if (InputManager.instance._LStickX != 0)
        {
            _Player.SwitchState(new Walk(_Player));
        }
        if (InputManager.instance._JumpButtonDown)
        {
            _Player.SwitchState(new Squad(_Player));
        }
    }

    public void FixedUpdate()
    {
        _Player._MaxVelocity = Vector2.Lerp(_Player._MaxVelocity, _Player._BaseStats._MaxVelocity, Time.fixedDeltaTime * 4);
    }

    public void OnExit()
    {

    }
}

public class Walk : IState
{
    PlayerBehavior _Player;
    GameManager _GameManager;

    public Walk(PlayerBehavior player)
    {
        _Player = player;
        _GameManager = GameManager.Instance;
    }

    public void OnEnter()
    {
        _Player._CanDodge = true;
        _Player.SetAnimation(PlayerAnimationState.Walk);

        var normTime = _GameManager._CurrentTime % 2;

        _Player._Animator.Play("Nibba_Run", 0, normTime);
    }

    public void Update()
    {
        if (InputManager.instance._LStickX == 0)
        {
            _Player.SwitchState(new Slide(_Player));
        }
        if (InputManager.instance._JumpButtonDown)
        {
            _Player.SwitchState(new Squad(_Player));
        }

        if (Math.Sign(_Player._Velocity.x) > 0)
        {
            _Player._Renderer.flipX = false;
            _Player._HitBoxPosition.x = Mathf.Abs(_Player._HitBoxPosition.x);
        }
        else if ((Math.Sign(_Player._Velocity.x) < 0))
        {
            _Player._Renderer.flipX = true;
            _Player._HitBoxPosition.x = -Mathf.Abs(_Player._HitBoxPosition.x);
        }
    }

    public void FixedUpdate()
    {
        _Player._MaxVelocity = Vector2.Lerp(_Player._MaxVelocity, _Player._BaseStats._MaxVelocity, Time.fixedDeltaTime * 4);

        if (_Player._Velocity.x >= -_Player._MaxVelocity.x && _Player._Velocity.x <= _Player._MaxVelocity.x)
        {
            _Player._Velocity.x += Math.Sign(InputManager.instance._LStickX) * _Player._AccelerationMultiplier * Time.fixedDeltaTime;
        }
        else if (_Player._Velocity.x < -_Player._MaxVelocity.x)
        {
            _Player._Velocity.x = -_Player._MaxVelocity.x;
        }
        else if (_Player._Velocity.x > _Player._MaxVelocity.x)
        {
            _Player._Velocity.x = _Player._MaxVelocity.x;
        }
    }

    public void OnExit()
    {

    }
}

public class Slide : IState
{
    PlayerBehavior _Player;

    public Slide(PlayerBehavior player)
    {
        _Player = player;
    }

    public void OnEnter()
    {
        _Player._CanDodge = true;
        _Player.SetAnimation(PlayerAnimationState.Slide);
    }

    public void Update()
    {
        if (InputManager.instance._LStickX != 0)
        {
            _Player.SwitchState(new Walk(_Player));
        }
        if (InputManager.instance._JumpButtonDown)
        {
            _Player.SwitchState(new Squad(_Player));
        }


        if (Math.Sign(_Player._Velocity.x) > 0)
        {
            _Player._Renderer.flipX = false;
            _Player._HitBoxPosition.x = Mathf.Abs(_Player._HitBoxPosition.x);
        }
        else if ((Math.Sign(_Player._Velocity.x) < 0))
        {
            _Player._Renderer.flipX = true;
            _Player._HitBoxPosition.x = -Mathf.Abs(_Player._HitBoxPosition.x);
        }
    }

    public void FixedUpdate()
    {
        if (_Player._Velocity.x == 0)
        {
            _Player.SwitchState(new Idle(_Player));
        }
    }

    public void OnExit()
    {

    }
}

public class Squad : IState
{
    PlayerBehavior _Player;
    float ShortHopWindow;
    bool ShortHop;

    public Squad(PlayerBehavior player)
    {
        _Player = player;
        ShortHopWindow = 0.08333333333f;
    }

    public void OnEnter()
    {
        _Player._MaxVelocity = _Player._BaseStats._MaxVelocity;
        _Player.SetAnimation(PlayerAnimationState.Squad);
    }

    public void Update()
    {
        ShortHopWindow -= Time.deltaTime;
        if (InputManager.instance._JumpButtonUp)
        {
            ShortHop = true;
        }

        if (ShortHopWindow <= 0)
        {
            switch (ShortHop)
            {
                case false:
                    _Player.SwitchState(new Jump(_Player, _Player._JumpForce));
                    break;
                case true:
                    _Player.SwitchState(new Jump(_Player, _Player._JumpForce * 0.75f));
                    break;
            }
        }
    }

    public void FixedUpdate()
    {

    }

    public void OnExit()
    {

    }
}

public class Jump : IState
{
    PlayerBehavior _Player;
    float _JumpForce;

    public Jump(PlayerBehavior player, float jumpforce)
    {
        _Player = player;
        _JumpForce = jumpforce;
    }

    public void OnEnter()
    {
        _Player._Velocity.y = _JumpForce;
        _Player._FrictionMultiplier = _Player._FrictionMultiplier * 0.25f;
        _Player.SetAnimation(PlayerAnimationState.Jump);
    }

    public void Update()
    {
        if (InputManager.instance._LStickX != 0 && InputManager.instance._LTrigger > 0.9f || InputManager.instance._LStickY != 0 && InputManager.instance._LTrigger > 0.5f)
        {
            if (_Player._CanDodge)
                _Player.SwitchState(new AirDodge(_Player));
        }
    }

    public void FixedUpdate()
    {
        if (_Player._Velocity.y <= 0)
        {
            _Player.SwitchState(new Fall(_Player));
        }

        if (_Player._Velocity.x >= -_Player._MaxVelocity.x && _Player._Velocity.x <= _Player._MaxVelocity.x)
        {
            _Player._Velocity.x += Math.Sign(InputManager.instance._LStickX) * _Player._AccelerationMultiplier * 0.5f * Time.fixedDeltaTime;
        }
    }

    public void OnExit()
    {

    }
}

public class Fall : IState
{
    PlayerBehavior _Player;

    public Fall(PlayerBehavior player)
    {
        _Player = player;
    }

    public void OnEnter()
    {
        _Player.SetAnimation(PlayerAnimationState.Fall);
    }

    public void Update()
    {
        if (InputManager.instance._LStickX != 0 && InputManager.instance._LTrigger > 0.9f || InputManager.instance._LStickY != 0 && InputManager.instance._LTrigger > 0.5f)
        {
            if (_Player._CanDodge)
                _Player.SwitchState(new AirDodge(_Player));
        }
    }

    public void FixedUpdate()
    {
        if (_Player._Grounded)
        {
            _Player._Velocity.y = 0;
            _Player._FrictionMultiplier = _Player._BaseStats._FrictionMultiplier;
            _Player.SwitchState(new Land(_Player));
        }

        if (_Player._Velocity.x >= -_Player._MaxVelocity.x && _Player._Velocity.x <= _Player._MaxVelocity.x)
        {
            _Player._Velocity.x += Math.Sign(InputManager.instance._LStickX) * _Player._AccelerationMultiplier * 0.5f * Time.fixedDeltaTime;
        }
    }

    public void OnExit()
    {

    }
}

public class AirDodge : IState
{
    PlayerBehavior _Player;
    Vector2 _Direction;

    public AirDodge(PlayerBehavior player)
    {
        _Player = player;
    }

    public void OnEnter()
    {
        _Player.SetAnimation(PlayerAnimationState.Dash);
        _Player._CanDodge = false;
        _Player._MaxVelocity = new Vector2(_Player._BaseStats._MaxVelocity.x * 3, _Player._BaseStats._MaxVelocity.y);
        _Direction = new Vector2(InputManager.instance._LStickX, InputManager.instance._LStickY).normalized;
        _Player._Velocity = _Direction * _Player._AirDodgeForce;
    }

    public void Update()
    {
      
    }

    public void FixedUpdate()
    {
        if (_Player._Grounded)
        {
            _Player._Velocity.y = 0;
            _Player._FrictionMultiplier = _Player._BaseStats._FrictionMultiplier;
            _Player.SwitchState(new Slide(_Player));
        }
    }

    public void OnExit()
    {

    }
}

public class Land : IState
{
    PlayerBehavior _Player;
    float _TimeInSquad;
    public Land(PlayerBehavior player)
    {
        _Player = player;
        _TimeInSquad = 0.2f;
    }

    public void OnEnter()
    {
        _Player.SetAnimation(PlayerAnimationState.Land);
    }

    public void Update()
    {
        _TimeInSquad -= Time.deltaTime;
        if (_TimeInSquad <= 0)
        {
            _Player.SwitchState(new Idle(_Player));
        }
    }

    public void FixedUpdate()
    {

    }

    public void OnExit()
    {

    }
}

public class State : IState
{
    PlayerBehavior _Player;

    public State(PlayerBehavior player)
    {
        _Player = player;
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