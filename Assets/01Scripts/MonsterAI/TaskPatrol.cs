using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;


public class TaskPatrol : Node
{
    private Transform _transform;
    private Transform[] _waypoints; 
    // TODO: 애니메이터 추가해야함, 애니메이션 찾으면 활성화 시키기
    // TODO - private Animator _animator;
    
    private int _currentWaypointIndex = 0;
    private float _waitTIme = 1f;
    private float _waitTimer = 0f;
    private bool _waiting = false;
    
    public TaskPatrol(Transform transform, Transform[] waypoints)
    {
        _transform = transform;
        _waypoints = waypoints;
        // TODO - _animator = _transform.GetComponent<Animator>();
    }
    public override NodeState Evaluate()
    {
        if (_waiting) // 기다림 활성화되면
        {
            _waitTimer += Time.deltaTime;   // 시간을 채움
            if (_waitTimer >= _waitTIme)    // 지정된 시간까지 찬다면
            {
                _waiting = false;       // 기다림 비활성화
                // TODO - _animator.SetBool("Patrol", true);
            }
        }
        else
        {
            Transform wp = _waypoints[_currentWaypointIndex];                   // 웨이포인트 번호대로
            if (Vector3.Distance(_transform.position, wp.position) < 0.01f)     // 본인 위치 -> 웨이포인트 코앞 까지 도착하면
            {
                _transform.position = wp.position;                                          // 웨이포인트 위치 동기화
                _waitTimer = 0f;                                                            // 대기 시간 초기화
                _waiting = true;                                                            //  대기
                _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;    // 다음 인덱스로 
                // TODO - _animator.SetBool("Patrol", false);
            }
            else
            {
                _transform.position = Vector3.MoveTowards(_transform.position, wp.position, MonsterBT.speed * Time.deltaTime); 
                // TODO: 위에 _waitTimer 추후에 몬스터speed로 교체
                _transform.LookAt(wp.position);
            }
        }

        state = NodeState.RUNNING;
        Debug.Log("몬스터Node_Patrol : SUCCESS");
        return state;
    }
}
