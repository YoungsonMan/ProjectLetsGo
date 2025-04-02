using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public enum State {Idle, Walk, Run, Dodge, Hit, Attack, Skill1, Skill2, Skill3, Ult, Dead, Size }
    [SerializeField] private State _currentState;
    PlayerState[] _states = new PlayerState[(int)State.Size];
    
    [SerializeField] public PlayerModel playerModel; // 인스펙터 확인용 TODO: 추후 삭제
    [SerializeField] public PlayerView playerView;

    [SerializeField] private Rigidbody _rigidBody;

    public InputAction playerControls;
    private Vector3 _moveDirection;
    private PlayerModel _playerModel;
    private PlayerView _playerView;


    [SerializeField] private Animator _animator;
    // Combo Attack 관련
    public float cooldownTime = 2f;
    private float _nextFireTime = 0f;
    public static int numOfClicks = 0;
    private static readonly int Attack1 = Animator.StringToHash("attack1");
    private static readonly int Attack2 = Animator.StringToHash("attack2");
    private static readonly int Attack3 = Animator.StringToHash("attack3");
    private float _lastClickedTime = 0;
    private float _maxComboDelay = 1;

    /// <summary>
    ///  상태변화 다음상태받고 지금상태 Exit()을 실행 => 다음상태 Enter()실행...
    /// </summary>
    /// <param name="nextState"></param>
    public void ChangeState(State nextState)
    {
        _states[(int)_currentState].Exit();
        _currentState = nextState;
        _states[(int)_currentState].Enter();
    }

    private void Awake()
    {


        if (FindObjectsOfType<PlayerController>().Length != 1)
        {
            Destroy(gameObject);
        }
        _playerModel = GetComponent<PlayerModel>();
        
        // 상태 넘버링
         _states[(int)State.Idle] = new IdleState(this);
         _states[(int)State.Walk] = new WalkState(this);
         _states[(int)State.Run] = new RunState(this);
         _states[(int)State.Dodge] = new DodgeState(this);
         _states[(int)State.Hit] = new HitState(this);
         _states[(int)State.Attack] = new AttackState(this);
         _states[(int)State.Skill1] = new Skill1State(this);
         _states[(int)State.Skill2] = new Skill2State(this);
         _states[(int)State.Skill3] = new Skill3State(this);
         _states[(int)State.Ult] = new UltState(this);
         _states[(int)State.Dead] = new DeadState(this);
    }

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _states[(int)State.Idle].Enter();
    }

    // Update is called once per frame
    void Update()
    {
       Move();
       
       _states[(int)_currentState].Update();
       
       
       ComboAttack();
       // Combo관련
    //   if(_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > .7f && _animator.GetCurrentAnimatorStateInfo(0).IsName("attack1"))
    //   {
    //       _animator.SetBool(Attack1, false);
    //   }
    //   if(_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > .7f && _animator.GetCurrentAnimatorStateInfo(0).IsName("attack2"))
    //   {
    //       _animator.SetBool(Attack2, false);
    //   }
    //   if(_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > .7f && _animator.GetCurrentAnimatorStateInfo(0).IsName("attack3"))
    //   {
    //       _animator.SetBool(Attack3, false);
    //       numOfClicks = 0;
    //   }
    //
    //   if (Time.time > _nextFireTime)
    //   {
    //     //  OnAttack();
    //     //  Debug.Log("공격");
    //   }
       
    }
    
    public void OnMove(InputValue value)
    {
        // 방향키만 입력받고 업데이트에서 계속 굴리기
        Vector2 input = value.Get<Vector2>();
        
        _moveDirection = new Vector3(input.x, 0, input.y);
        // RigidBody로 이동        // TODO: Animation View 세팅 
        
        ChangeState(State.Walk);
    }
    public void Move()
    {
        _rigidBody.velocity = _moveDirection.normalized * _playerModel.speed; 
        // 계속움직임 방지
        if (_moveDirection.magnitude < 0.1f)
        {
            _rigidBody.velocity = Vector2.zero;
        }
        // 마지막 방향 유지
        // transform.forward = _moveDirection.normalized;
        if(_moveDirection.magnitude > 1f) transform.forward = _moveDirection;
        // if (_moveDirection.magnitude > 0.2f) transform.forward = _moveDirection;
        // 0,0,0
    }
    public void OnDodge(InputValue value)
    {
        Debug.Log("Dodge키가 입력됐습니다.");
    }

    public void ComboAttack()
    {
        _lastClickedTime = Time.time;
        numOfClicks++;
        if(_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > .7f && _animator.GetCurrentAnimatorStateInfo(0).IsName("attack1"))
        {
            _animator.SetBool(Attack1, false);
        }
        if(_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > .7f && _animator.GetCurrentAnimatorStateInfo(0).IsName("attack2"))
        {
            _animator.SetBool(Attack2, false);
        }
        if(_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > .7f && _animator.GetCurrentAnimatorStateInfo(0).IsName("attack3"))
        {
            _animator.SetBool(Attack3, false);
            numOfClicks = 0;
        }

        if (Time.time - _lastClickedTime > _maxComboDelay)
        {
            numOfClicks = 0;
        }
        if (Time.time > _nextFireTime)
        {
            OnAttack();
            //  Debug.Log("공격");
        }
    }

    public void OnAttack()
    {
        //Debug.Log("inputAction 공격");
        _lastClickedTime = Time.time;
        numOfClicks++;
        if (numOfClicks == 1)
        {
            _animator.SetBool(Attack1, true);
            Debug.Log("콤보1");
        }
        numOfClicks = Mathf.Clamp(numOfClicks, 0, 3);
        if (numOfClicks >= 2 && _animator.GetCurrentAnimatorStateInfo(0).normalizedTime > .7f &&
            _animator.GetCurrentAnimatorStateInfo(0).IsName("attack1"))
        {
            _animator.SetBool(Attack1, false);
            _animator.SetBool(Attack2, true);
            Debug.Log("콤보2");
        }
        if (numOfClicks >= 3 && _animator.GetCurrentAnimatorStateInfo(0).normalizedTime > .7f &&
            _animator.GetCurrentAnimatorStateInfo(0).IsName("attack2"))
        {
            _animator.SetBool(Attack2, false);
            _animator.SetBool(Attack3, true);
            Debug.Log("콤보3");
        }
        
        
        
        
    }


}
