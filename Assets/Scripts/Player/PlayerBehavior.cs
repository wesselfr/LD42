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
    Land,
    Mine,
    Eat

}

public class PlayerBehavior : MonoBehaviour
{
    // player
    public IState _State;
    public PlayerAnimationState _AnimationState;
    public Animator _Animator;
    public SpriteRenderer _Renderer;
    public Rigidbody2D _RigidBody;
    public BoxCollider2D _Collider;

    //PickAxe
    public Transform _PickAxe;
    public Animator _AxeAnim;
    public SpriteRenderer _AxeRenderer;


    [SerializeField] public BasePlayerStats _BaseStats;
    public float _AccelerationMultiplier, _FrictionMultiplier;
    public float _FallForceMultiplier;
    public float _JumpForce, _AirDodgeForce;
    public float _MineRange;
    public float _MineCoolDown;
    public Vector2 _Velocity, _PreVelocity, _MaxVelocity;

    float _MineTimer;

    public LayerMask _StandableMasks;
    public bool _Grounded;
    //public bool _CanDodge;
    

    void Awake()
    {
        SetStats();
        GetComponents();
        _State = new Idle(this);
    }

    private void SetStats()
    {
        _AccelerationMultiplier = _BaseStats._AccelerationMultiplier;
        _FrictionMultiplier = _BaseStats._FrictionMultiplier;
        _FallForceMultiplier = _BaseStats._FallForceMultiplier;
        _JumpForce = _BaseStats._JumpForce;
        _AirDodgeForce = _BaseStats._AirDodgeForce;
        _MineRange = _BaseStats._MineRange;
        _MineCoolDown = _BaseStats._MineCoolDown;
        _MaxVelocity = _BaseStats._MaxVelocity;
    }

    void GetComponents()
    {
        _RigidBody = GetComponent<Rigidbody2D>();
        _Collider = GetComponent<BoxCollider2D>();

        _AxeAnim = _PickAxe.GetComponent<Animator>();
        _AxeRenderer = _PickAxe.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        _State.Update();
    }

    void FixedUpdate()
    {
        Mine();
        GroundCheck();

        _State.FixedUpdate();

        SideCheck();
        ApplyGravity();
        ApplyFriction();

        SetRigidBodyVelocity();
        SideCheck();
        FrictionEnd();
        SideCheck();
        SetPreviousValues();
    }

    public void Mine()
    {
        _MineTimer -= Time.deltaTime;

        Vector3 pos = transform.position + new Vector3(0, _Collider.bounds.size.y * 0.5f);

        Vector3 dir = (InputManager.instance.GetMousePosition2DWorldSpace() - pos).normalized;

        Debug.DrawRay(pos, dir * _MineRange, Color.red);

        RaycastHit2D hit = Physics2D.Raycast(pos, dir, _MineRange, _StandableMasks);

        if (hit)
        {
            Debug.DrawLine(hit.transform.position, pos);
            if (_MineTimer <= 0)
            {
                if (InputManager.instance._MineButtonDown || InputManager.instance._MineButtonHeld)
                {
                    _AxeAnim.Play("Pickaxe");
                    hit.transform.GetComponent<SmallBlock>().MineBlock();
                    _MineTimer = _MineCoolDown;
                }
            }
        }
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
        //if (_AnimationState != PlayerAnimationState.Dash)
        {
            if (Math.Sign(_Velocity.x) != Math.Sign(_PreVelocity.x) && _PreVelocity.x != 0)
            {
                _Velocity.x = 0;
            }
        }
    }

    public void GroundCheck()
    {
        int rays = 4;
        for (int i = 0; i < rays; i++)
        {
            Vector2 pos = (new Vector2(_Collider.bounds.center.x, _Collider.bounds.center.y) - new Vector2(_Collider.bounds.extents.x, 0)) + new Vector2(1, 0) * _Collider.bounds.size.x / (rays - 1) * i;
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

    public void SideCheck()
    {
        int rays = 4;
        //bool firstLeft = false;
        //bool lastLeft = false;
        //bool firstRight = false;
        //bool lastRight = false;

        for (int i = 0; i < rays; i++)
        {
            //left

            Vector2 leftPos = new Vector2(_Collider.bounds.min.x, _Collider.bounds.min.y) + new Vector2(0, 1) * _Collider.bounds.size.y / (rays - 1) * i;
            Debug.DrawRay(leftPos, -transform.right * 0.2f, Color.red);
            RaycastHit2D leftHit = Physics2D.Raycast(leftPos, -transform.right, 0.2f, _StandableMasks);
            if (leftHit)
            {
                //if (i == 0)
                //{
                //    firstLeft = true;
                //}
                //if (i == rays -1)
                //{
                //    lastLeft = true;
                //}

                if (_Velocity.x < 0)
                {
                    _Velocity.x = 0;
                }
            }

            //right

            Vector2 rightPos = new Vector2(_Collider.bounds.max.x, _Collider.bounds.min.y) + new Vector2(0, 1) * _Collider.bounds.size.y / (rays - 1) * i;
            Debug.DrawRay(rightPos, transform.right * 0.2f, Color.red);
            RaycastHit2D rightHit = Physics2D.Raycast(rightPos, transform.right, 0.2f, _StandableMasks);
            if (rightHit)
            {

                //if (i == 0)
                //{
                //    firstRight = true;
                //}
                //if (i == rays -1)
                //{
                //    lastRight = true;
                //}

                if (_Velocity.x > 0)
                {
                    _Velocity.x = 0;
                }
            }
        }

        //if (firstLeft == true && lastLeft == false)
        //{
        //    if (InputManager.instance.HorizontalDirection() == -1)
        //    {
        //        _Velocity.y = 10;
        //    }
        //}
        //else
        //{

        //}

        //if (firstRight == true && lastRight == false)
        //{
        //    if (InputManager.instance.HorizontalDirection() == 1)
        //    {
        //        _Velocity.y = 10;
        //    }
        //}
        //else
        //{

        //}
    }

    public void SealingCheck()
    {
        int rays = 4;
        for (int i = 0; i < rays; i++)
        {
            Vector2 pos = new Vector2(_Collider.bounds.min.x, _Collider.bounds.max.y) + new Vector2(1, 0) * _Collider.bounds.size.x / (rays - 1) * i;

            Debug.DrawRay(pos, -transform.up * 0.1f, Color.red);

            if (Physics2D.Raycast(pos, transform.up, 0.3f, _StandableMasks))
            {
                _Velocity.y = 0;
                break;
            }
        }
    }

    public void CorrectFalling()
    {
        Vector3 leftDir = new Vector3(-1, -1).normalized;
        Vector3 rightDir = new Vector3(1, -1).normalized;
        Vector3 leftPos = new Vector2(_Collider.bounds.min.x, _Collider.bounds.min.y);
        Vector3 rightPos = new Vector3(_Collider.bounds.max.x, _Collider.bounds.min.y);

        Debug.DrawRay(leftPos, leftDir * 0.1f, Color.red);
        Debug.DrawRay(rightPos, rightDir * 0.1f, Color.red);

        if (Physics2D.Raycast(leftPos, leftDir, 0.1f, _StandableMasks))
        {
            _Velocity.x += 2f;
        }
        else if (Physics2D.Raycast(rightPos, rightDir, 0.1f, _StandableMasks))
        {
            _Velocity.x -= 2f;
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
}

public class Idle : IState
{
    PlayerBehavior _Player;
    GameManager _GameManager;

    public Idle(PlayerBehavior player)
    {
        _Player = player;
    }

    public void OnEnter()
    {
        //_Player._CanDodge = true;
        _Player.SetAnimation(PlayerAnimationState.Idle);
    }

    public void Update()
    {
        if (InputManager.instance.HorizontalDirection() != 0)
        {
            _Player.SwitchState(new Walk(_Player));
        }
        if (InputManager.instance._JumpButtonDown || InputManager.instance._UpButtonDown)
        {
            _Player.SwitchState(new Squad(_Player));
        }

    }

    public void FixedUpdate()
    {
        _Player.Mine();
        _Player._MaxVelocity = Vector2.Lerp(_Player._MaxVelocity, _Player._BaseStats._MaxVelocity, Time.fixedDeltaTime * 4);

        if (!_Player._Grounded)
        {
            _Player.SwitchState(new Fall(_Player));
        }
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
    }

    public void OnEnter()
    {
        // _Player._CanDodge = true;
        _Player.SetAnimation(PlayerAnimationState.Walk);
    }

    public void Update()
    {
        if (InputManager.instance.HorizontalDirection() == 0)
        {
            if (_Player._Velocity.x >= _Player._MaxVelocity.x * 0.8f || _Player._Velocity.x <= -_Player._MaxVelocity.x * 0.8f)
            {
                _Player.SwitchState(new Slide(_Player));
            }
            else
            {
                _Player.SwitchState(new Idle(_Player));
            }
        }
        if (InputManager.instance._JumpButtonDown || InputManager.instance._UpButtonDown)
        {
            _Player.SwitchState(new Squad(_Player));
        }

        if (Math.Sign(_Player._Velocity.x) > 0)
        {
            _Player._Renderer.flipX = true;
            _Player._AxeRenderer.flipX = true;
        }
        else if ((Math.Sign(_Player._Velocity.x) < 0))
        {
            _Player._Renderer.flipX = false;
            _Player._AxeRenderer.flipX = false;
        }
    }

    public void FixedUpdate()
    {
        _Player.Mine();

        _Player._MaxVelocity = Vector2.Lerp(_Player._MaxVelocity, _Player._BaseStats._MaxVelocity, Time.fixedDeltaTime * 4);

        if (_Player._Velocity.x >= -_Player._MaxVelocity.x && _Player._Velocity.x <= _Player._MaxVelocity.x)
        {
            _Player._Velocity.x += Math.Sign(InputManager.instance.HorizontalDirection()) * _Player._AccelerationMultiplier * Time.fixedDeltaTime;
        }
        else if (_Player._Velocity.x < -_Player._MaxVelocity.x)
        {
            _Player._Velocity.x = -_Player._MaxVelocity.x;
        }
        else if (_Player._Velocity.x > _Player._MaxVelocity.x)
        {
            _Player._Velocity.x = _Player._MaxVelocity.x;
        }

        if (!_Player._Grounded)
        {
            _Player.SwitchState(new Fall(_Player));
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
        //_Player._CanDodge = true;
        _Player.SetAnimation(PlayerAnimationState.Slide);
    }

    public void Update()
    {
        if (InputManager.instance.HorizontalDirection() != 0)
        {
            _Player.SwitchState(new Walk(_Player));
        }
        if (InputManager.instance._JumpButtonDown || InputManager.instance._UpButtonDown)
        {
            _Player.SwitchState(new Squad(_Player));
        }

        if (Math.Sign(_Player._Velocity.x) > 0)
        {
            _Player._Renderer.flipX = false;
            _Player._AxeRenderer.flipX = false;
        }
        else if ((Math.Sign(_Player._Velocity.x) < 0))
        {
            _Player._Renderer.flipX = true;
            _Player._AxeRenderer.flipX = true;

        }
    }

    public void FixedUpdate()
    {
        _Player.Mine();

        if (!_Player._Grounded)
        {
            _Player.SwitchState(new Fall(_Player));
        }
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
        ShortHopWindow = 0.32f;
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
        //if (InputManager.instance.HorizontalDirection() != 0 && InputManager.instance._DashButtonDown|| InputManager.instance.VericalDirection() != 0 && InputManager.instance._DashButtonDown)
        //{
        //    if (_Player._CanDodge)
        //        _Player.SwitchState(new AirDodge(_Player));
        //}

        if (Math.Sign(_Player._Velocity.x) > 0)
        {
            _Player._Renderer.flipX = true;
            _Player._AxeRenderer.flipX = true;

        }
        else if ((Math.Sign(_Player._Velocity.x) < 0))
        {
            _Player._Renderer.flipX = false;
            _Player._AxeRenderer.flipX = false;

        }
    }

    public void FixedUpdate()
    {
        _Player.SealingCheck();

        if (_Player._Velocity.y <= 0)
        {
            _Player.SwitchState(new Fall(_Player));
        }

        if (_Player._Velocity.x >= -_Player._MaxVelocity.x && _Player._Velocity.x <= _Player._MaxVelocity.x)
        {
            _Player._Velocity.x += Math.Sign(InputManager.instance.HorizontalDirection()) * _Player._AccelerationMultiplier * 0.5f * Time.fixedDeltaTime;
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
        //if (InputManager.instance.HorizontalDirection() != 0 && InputManager.instance._DashButtonDown || InputManager.instance.VericalDirection() != 0 && InputManager.instance._DashButtonDown)
        //{
        //    if (_Player._CanDodge)
        //        _Player.SwitchState(new AirDodge(_Player));
        //}

        if (Math.Sign(_Player._Velocity.x) > 0)
        {
            _Player._Renderer.flipX = true;
            _Player._AxeRenderer.flipX = true;

        }
        else if ((Math.Sign(_Player._Velocity.x) < 0))
        {
            _Player._Renderer.flipX = false;
            _Player._AxeRenderer.flipX = false;

        }
    }

    public void FixedUpdate()
    {
        _Player.CorrectFalling();

        if (_Player._Grounded)
        {
            _Player._Velocity.y = 0;
            _Player._FrictionMultiplier = _Player._BaseStats._FrictionMultiplier;
            _Player.SwitchState(new Land(_Player));
        }

        if (_Player._Velocity.x >= -_Player._MaxVelocity.x && _Player._Velocity.x <= _Player._MaxVelocity.x)
        {
            _Player._Velocity.x += Math.Sign(InputManager.instance.HorizontalDirection()) * _Player._AccelerationMultiplier * 0.5f * Time.fixedDeltaTime;
        }
    }

    public void OnExit()
    {

    }
}

//public class AirDodge : IState
//{
//    PlayerBehavior _Player;
//    Vector2 _Direction;

//    public AirDodge(PlayerBehavior player)
//    {
//        _Player = player;
//    }

//    public void OnEnter()
//    {
//        _Player.SetAnimation(PlayerAnimationState.Dash);
//        _Player._CanDodge = false;
//        _Player._MaxVelocity = new Vector2(_Player._BaseStats._MaxVelocity.x * 3, _Player._BaseStats._MaxVelocity.y);
//        _Direction = new Vector2(InputManager.instance.HorizontalDirection(), InputManager.instance.VericalDirection()).normalized;
//        _Player._Velocity = _Direction * _Player._AirDodgeForce;
//    }

//    public void Update()
//    {

//    }

//    public void FixedUpdate()
//    {
//        if (_Player._Grounded)
//        {
//            _Player._Velocity.y = 0;
//            _Player._FrictionMultiplier = _Player._BaseStats._FrictionMultiplier;
//            _Player.SwitchState(new Slide(_Player));
//        }
//    }

//    public void OnExit()
//    {

//    }
//}

public class Land : IState
{
    PlayerBehavior _Player;
    float _TimeInSquad;
    public Land(PlayerBehavior player)
    {
        _Player = player;
        _TimeInSquad = 0.3f;
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