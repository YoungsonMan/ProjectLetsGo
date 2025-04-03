using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    public enum State {Idle, Walk, Run, Dodge, Hit, Attack1, Attack2, Attack3, Skill1, Skill2, Skill3, Ult, Dead, Size }
    [SerializeField] private State _currentState;
    PlayerState[] _states = new PlayerState[(int)State.Size];
    
    [SerializeField] public PlayerModel playerModel; // 인스펙터 확인용 TODO: 추후 삭제
    [SerializeField] public PlayerView playerView;

    [SerializeField] private Rigidbody _rigidBody;

    public InputAction playerControls;
    private Vector3 _moveDirection;
    private PlayerModel _playerModel;
    private PlayerView _playerView;

    // InputSystem(PlayerInput) 관련
    public PlayerInput playerInput; // InputSystem InputAction 스크립트 생성된거
    private InputAction moveAction;
    private InputAction dodgeAction;
    private InputAction attackAction;
    private InputAction skill1Action;
    private InputAction skill2Action;
    private InputAction skill3Action;
    private InputAction ultAction;

    // Animation 관련
    [SerializeField] public Animator animator;
    // Combo Attack 관련
    public float cooldownTime = 2f;
    private float _nextFireTime = 0f;
    public static int numOfClicks = 0;
    private float _lastClickedTime = 0;
    private float _maxComboDelay = 1;
    
    private static readonly int Attack1 = Animator.StringToHash("attack1");
    private static readonly int Attack2 = Animator.StringToHash("attack2");
    private static readonly int Attack3 = Animator.StringToHash("attack3");

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

        playerInput = new PlayerInput();
        
        // 상태 넘버링
         _states[(int)State.Idle] = new IdleState(this);
         _states[(int)State.Walk] = new WalkState(this);
         _states[(int)State.Run] = new RunState(this);
         _states[(int)State.Dodge] = new DodgeState(this);
         _states[(int)State.Hit] = new HitState(this);
         _states[(int)State.Attack1] = new Attack1State(this);
         _states[(int)State.Attack2] = new Attack2State(this);
         _states[(int)State.Attack3] = new Attack3State(this);
         _states[(int)State.Skill1] = new Skill1State(this);
         _states[(int)State.Skill2] = new Skill2State(this);
         _states[(int)State.Skill3] = new Skill3State(this);
         _states[(int)State.Ult] = new UltState(this);
         _states[(int)State.Dead] = new DeadState(this);
    }

    void OnEnable()
    {
        moveAction = playerInput.PlayerActions.Move;
        moveAction.Enable();
        dodgeAction = playerInput.PlayerActions.Dodge;
        dodgeAction.Enable();
        attackAction = playerInput.PlayerActions.Attack;
        attackAction.Enable();
        attackAction.performed += Attack;
    }

    private void OnDisable()
    {
        moveAction.Disable();
        dodgeAction.Disable();
        attackAction.Disable();
    }

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        _states[(int)State.Idle].Enter();
    }

    // Update is called once per frame
    void Update()
    {
       OnMove();
      // ComboAttack();
       _states[(int)_currentState].Update();
       
       
       
    }
    
    public void OnMove()
    {
        // 방향키만 입력받고 업데이트에서 계속 굴리기
        Vector2 input = moveAction.ReadValue<Vector2>();
        _moveDirection = new Vector3(input.x, 0, input.y);
        // RigidBody로 이동        // TODO: Animation View 세팅 
        ChangeState(State.Walk);
        _rigidBody.velocity = _moveDirection.normalized * _playerModel.speed; 
        // 계속움직임 방지
        if (_moveDirection.magnitude < 0.1f)
        {
            _rigidBody.velocity = Vector2.zero;
            ChangeState(State.Idle);
        }
        // 마지막 방향 유지
        // transform.forward = _moveDirection.normalized;
        if(_moveDirection.magnitude > .1f) transform.forward = _moveDirection;
        // if (_moveDirection.magnitude > 0.2f) transform.forward = _moveDirection;
        // 0,0,0
    }
    
    public void OnDodge(InputValue value)
    {
        Debug.Log("Dodge키가 입력됐습니다.");
    }

    public void ComboAttack()
    {
        bool isPressed = playerInput.PlayerActions.Attack.ReadValue<float>() > 0.1f;
        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > .7f && _currentState == State.Attack1)
        {
            animator.SetBool(Attack1, false);
        }
        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > .7f && _currentState == State.Attack2)
        {
            animator.SetBool(Attack2, false);
        }
        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > .7f && _currentState == State.Attack3)
        {
            animator.SetBool(Attack3, false);
            numOfClicks = 0;
        }
        if (Time.time - _lastClickedTime > _maxComboDelay)
        {
            numOfClicks = 0;
        }
        if (Time.time > _nextFireTime)
        {
            //bool isPressed = playerInput.PlayerActions.Attack.ReadValue<float>() > 0.1f;
            if (isPressed)
            {
              // Debug.Log("공격");
              OnAttack();
               
            }
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        Debug.Log("콜백이벤트 : 공격");
        bool isPressed = playerInput.PlayerActions.Attack.ReadValue<float>() > 0.1f;
        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > .7f && _currentState == State.Attack1)
        {
            animator.SetBool(Attack1, false);
        }
        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > .7f && _currentState == State.Attack2)
        {
            animator.SetBool(Attack2, false);
        }
        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > .7f && _currentState == State.Attack3)
        {
            animator.SetBool(Attack3, false);
            numOfClicks = 0;
        }
        if (Time.time - _lastClickedTime > _maxComboDelay)
        {
            numOfClicks = 0;
        }
        if (Time.time > _nextFireTime)
        {
            //bool isPressed = playerInput.PlayerActions.Attack.ReadValue<float>() > 0.1f;
            if (isPressed)
            {
                Debug.Log("공격버튼 눌림 확인");
                OnAttack();
               
            }
        }

    }

    public void OnAttack()
    {
        //Debug.Log("inputAction 공격");
        _lastClickedTime = Time.time;
        numOfClicks++;
        if (numOfClicks == 1)
         {
             ChangeState(State.Attack1);
             //animator.SetBool(Attack1, true);
             
         }
         numOfClicks = Mathf.Clamp(numOfClicks, 0, 3);
         if (numOfClicks >= 2 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > .8f &&
             animator.GetCurrentAnimatorStateInfo(0).IsName("attack1"))
         {
             ChangeState(State.Attack2);
            // animator.SetBool(Attack1, false);
            // animator.SetBool(Attack2, true);
            // Debug.Log("콤보2");
         }
         if (numOfClicks >= 3 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > .8f &&
             animator.GetCurrentAnimatorStateInfo(0).IsName("attack2"))
         {
             ChangeState(State.Attack3);
             // animator.SetBool(Attack2, false);
             // animator.SetBool(Attack3, true);
             // Debug.Log("콤보3");
         } 
     
    }


}
