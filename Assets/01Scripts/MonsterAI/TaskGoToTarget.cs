using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class TaskGoToTarget : Node
{
    private Transform _transform;

    public TaskGoToTarget(Transform transform)
    {
        _transform = transform; // 자신의 transform 가져오기
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        Debug.Log($"몬스터Node_타겟추격 : 타겟설정 = {target} ");
        Debug.Log($"몬스터Node_타겟추격 : TargetPoisition = {target.position} ");

        if (Vector3.Distance(_transform.position, target.position) > 0.1f) // 지정 거리보다 멀면 접근
        {
            _transform.position = Vector3.MoveTowards(_transform.position, target.position, 0.1f); // target.posiotn, 다음에 몹속도 * Time.deltaTime
            _transform.LookAt(target.position);         // 타겟 바라보기
            Debug.Log($"몬스터Node_타겟추격 : 범위에 들어옴 targetPosition = {target.position} ");
        }

        state = NodeState.RUNNING;
        Debug.Log("몬스터Node_타겟추격 : RUNNING");
        return state;
    }
}
