using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class CheckTargetInFOVRange : Node
{
    private static int _targetLayerMask = 1 << 6;
    private Transform _transform;
    
    // TODO: add animation

    public CheckTargetInFOVRange(Transform transform)
    {
        _transform = transform;
        // animation component
    }

    public override NodeState Evaluate()
    {
        object obj = GetData("target");
        if (obj == null)
        {
            Collider[] colliders = Physics.OverlapSphere(_transform.position, _targetLayerMask);
            if (colliders.Length > 0)
            {
                parent.parent.SetData("target", colliders[0].transform);
                // animation
                state = NodeState.SUCCESS;
                return state;    
            }
            state = NodeState.FAILURE;
            return state;
        }

        state = NodeState.SUCCESS;
        return state;
    }
}
