using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class CheckTargetInFOVRange : Node  //Field Of View 몬스터 시야확인
{
    private static int _targetLayerMask = 1 << 6;       // 플레이어 LayerMask  
    private Transform _transform;                       
    
    // TODO: add animation

    public CheckTargetInFOVRange(Transform transform)
    {
        _transform = transform;
        Debug.Log("몬스터Node_시야체크On");
        // animation component
    }

    public override NodeState Evaluate()
    {
        object obj = GetData("target");
        if (obj == null)
        {
            Collider[] colliders = Physics.OverlapSphere(_transform.position, MonsterBT.fovRange,_targetLayerMask);
            
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
        Debug.Log("몬스터Node_시야범위체크 : SUCCESS");
        return state;
    }

    
}
