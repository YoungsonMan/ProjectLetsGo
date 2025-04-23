using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class CheckTargetInFOVRange : Node  //Field Of View 몬스터 시야확인
{
    private static int _targetLayerMask = 1 << 6;       // 플레이어 LayerMask  
    private Transform _transform;                       
    
    // 타겟이 시야 범위를 벗어나면 실패status로 바꿔야함  <= 추적하는 부분에서 바꿔야할 거 같음
    
    
    // TODO: add animation

    public CheckTargetInFOVRange(Transform transform)
    {
        _transform = transform;
        // animation component
    }

    public override NodeState Evaluate()
    {
        object obj = GetData("target");
        Debug.Log($"$몬스터Node_시야체크On : target = {obj}");
        if (obj == null)
        {
            Collider[] colliders = Physics.OverlapSphere(_transform.position, MonsterBT.fovRange,_targetLayerMask);  //시야범위 콜라이더 : 거리 , 타겟
            
            if (colliders.Length > 0)       //충돌체 감지시
            {
                parent.parent.SetData("target", colliders[0].transform);
                Debug.Log($"target: {colliders[0].name}");
                // animation 추가
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
