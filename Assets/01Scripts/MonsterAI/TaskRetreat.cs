using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class TaskRetreat : Node
{
    private Transform _transform;
    private Transform _spawnPoint;

    public TaskRetreat(Transform transform, Transform spawnPoint)
    {
        _transform = transform;
        _spawnPoint = spawnPoint;
        // TODO 애니메이션 추가
    }

    public override NodeState Evaluate()
    {
        Transform spawnPoint = _spawnPoint;
        
        _transform.position = Vector3.MoveTowards(_transform.position, spawnPoint.position, 
            MonsterBT.speed * Time.deltaTime);
        Debug.Log("스폰위치로 복귀합니다.");


        if (Vector3.Distance(_transform.position, spawnPoint.position) < 0.1f)
        {
            Debug.Log("스폰위치 도착");
            state = NodeState.SUCCESS;
            return state;
        }
            
        state = NodeState.RUNNING;
        return state;
    }

}
