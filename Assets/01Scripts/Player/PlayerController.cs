using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public PlayerModel playerModel; // 인스펙터 확인용 TODO: 추후 삭제
    [SerializeField] public PlayerView playerView;

    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private Animator _animator;

    private Vector3 _moveDirection;
    private PlayerModel _playerModel;
    private PlayerView _playerView;

    private void Awake()
    {
        if (FindObjectsOfType<PlayerController>().Length != 1)
        {
            Destroy(gameObject);
        }
        _playerModel = GetComponent<PlayerModel>();
        
    }

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
       Move();
       
    }
    
    public void OnMove(InputValue value)
    {
        // 방향키만 입력받고 업데이트에서 계속 굴리기
        Vector2 input = value.Get<Vector2>();
        
        _moveDirection = new Vector3(input.x, 0, input.y);
        // RigidBody로 이동
        // Debug.Log( $"방향키입력받음. moveDir: {_moveDirection},  inputMagnitude: {input.magnitude}");
        // TODO: Animation View 세팅 
        
        
    }
    public void Move()
    {
        
        _rigidBody.velocity = _moveDirection.normalized * _playerModel.speed; 
        //Debug.Log( $"방향키입력받음. moveDir: {_moveDirection},  inputMagnitude: {_moveDirection.magnitude}");
        // 계속움직임 방지
        if (_moveDirection.magnitude < 0.1f)
        {
            _rigidBody.velocity = Vector2.zero;
        }
        // 마지막 방향 유지
        // transform.forward = _moveDirection.normalized;
        if(_moveDirection.magnitude > 1f) transform.forward = _moveDirection.normalized;
        
        // 0,0,0
        

    }
    public void OnDodge(InputValue value)
    {
        Debug.Log("Dodge키가 입력됐습니다.");
    }

    

}
