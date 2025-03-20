using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public PlayerModel playerModel;
    [SerializeField] public PlayerView playerView;

    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private Animator _animator;


    private void Awake()
    {
        if (FindObjectsOfType<PlayerController>().Length != 1)
        {
            Destroy(gameObject);
        }
        
        
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

    
    public void Move()
    {
        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        // TODO: Animation 뷰에서 세팅해주기? 
        
        // 계속움직임 방지
        if (move.magnitude < 0.1f)
        {
            _rigidBody.velocity = Vector3.zero;
        }
        
        // 동시 입력시 다른 방향으로 움직임 제한
       // if (move.x != 0) move.z = 0;
       // if (move.z != 0) move.x = 0;
        
        // RigidBody로 이동
        _rigidBody.velocity = move.normalized; // 추후에 능력치 대로 움직이도록.
        
        // 마지막 방향 유지
        if(move.magnitude > 0.1f) transform.forward = move.normalized;

    }
}
