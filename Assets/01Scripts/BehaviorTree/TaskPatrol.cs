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
    private float _waitTIme = 0.5f;
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
        if (_waiting)
        {
            _waitTimer += Time.deltaTime;
            if (_waitTimer >= _waitTIme)
            {
                _waiting = false;
                // TODO - _animator.SetBool("Patrol", true);
            }
        }
        else
        {
            Transform wp = _waypoints[_currentWaypointIndex];
            if (Vector3.Distance(_transform.position, wp.position) < 0.01f)
            {
                _transform.position = wp.position;
                _waitTimer = 0f;
                _waiting = true;
                _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
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
        return state;
    }
}
