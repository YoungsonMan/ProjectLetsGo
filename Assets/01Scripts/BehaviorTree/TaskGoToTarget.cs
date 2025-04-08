using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class TaskGoToTarget : Node
{
    private Transform _transform;

    public TaskGoToTarget(Transform transform)
    {
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");

        if (Vector3.Distance(_transform.position, target.position) < 0.1f)
        {
            _transform.position = Vector3.MoveTowards(_transform.position, target.position, 0.1f); // target.posiotn, 다음에 몹속도 * Time.deltaTime
            _transform.LookAt(target.position);
        }

        state = NodeState.SUCCESS;
        return state;
    }
}
