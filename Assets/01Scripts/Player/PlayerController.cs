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

    //public InputAction playerControls;
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
    

    private int hashAttackCount = Animator.StringToHash("AttackCount");

    public int attackCount
    {
        get => animator.GetInteger(hashAttackCount);
        set => animator.SetInteger(hashAttackCount, (int)value);
    }

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
        
        skill1Action = playerInput.PlayerActions.Skill1;
        skill1Action.Enable();
        skill1Action.performed += Skill1;
        skill2Action = playerInput.PlayerActions.Skill2;
        skill2Action.Enable();
        skill2Action.performed += Skill2;
        skill3Action = playerInput.PlayerActions.Skill3;
        skill3Action.Enable();
        skill3Action.performed += Skill3;
        
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
      // OnMove();
       ComboAttack();
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
    
    public void Attack(InputAction.CallbackContext context)
    {

      ChangeState(State.Attack1);
      attackCount = 0;
      

    }
    public void Skill1(InputAction.CallbackContext context)
    {
        attackCount = 1;
    }
    public void Skill2(InputAction.CallbackContext context)
    {
        attackCount = 2;
    }
    public void Skill3(InputAction.CallbackContext context)
    {
        
    }

  


}
