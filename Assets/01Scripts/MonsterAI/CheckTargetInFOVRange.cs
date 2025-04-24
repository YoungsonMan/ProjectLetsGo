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
        object obj = GetData("target");     // 타겟지정변수
        object tgc;     //타겟 콜라이더
        Debug.Log($"몬스터Node_시야체크On : target = {obj}");
        if (obj == null)        // 타겟이 없으면 
        {
           // Collider[] colliders = Physics.OverlapSphere(_transform.position, MonsterBT.fovRange,_targetLayerMask);  //시야범위 콜라이더 : 거리 , 타겟
           // if (colliders.Length > 0)       //충돌체 감지 되면
           // {
           //     Debug.Log($"GET DATA _ 타겟/콜라이더 저장전 = {GetData("target")}");
           //     parent.parent.SetData("target", colliders[0].transform); // 딕셔너리에 "target"key로 '충돌체 transform' 추가
           //     // 콜라이더가 추가된게 콜라이더를 나간다고 삭제가 안됨
           //     Debug.Log($"target: {colliders[0].name}");
           //     Debug.Log($"GET DATA _ target = {GetData("target")}");
           //     // animation 추가
           //     state = NodeState.SUCCESS;      // state 변경 => SUCCESS
           //     return state;    
           // }
            
            // raycast버전
            // 위에 콜라이더로 했을때는 죽을떄까지 따라갔는데(계속 공격 state?) 지금 이거는 한대 치고 다시 다른 상태로됨
            RaycastHit hit;
            if (Physics.SphereCast(_transform.position, MonsterBT.fovRange, _transform.forward, out hit, 0.1f, _targetLayerMask ))
            {
                parent.parent.SetData("target", hit.transform);
                Debug.Log($"충돌포착: {hit.collider.gameObject.name}");
                if (hit.collider.gameObject == null)   // TODO: 복귀Node를 제대로 구성하고 이거 삭제 : 이거 여기다가 해둔게 별 작동안되는게 이미 state가 바뀌어서 안됨.
                {
                    state = NodeState.FAILURE;
                    return state;
                }
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
