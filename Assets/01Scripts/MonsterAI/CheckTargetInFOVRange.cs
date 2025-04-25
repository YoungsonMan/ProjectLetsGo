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
             //타겟 콜라이더
        //Debug.Log($"몬스터Node_시야체크On : target = {obj}");
        if (obj == null)        // 타겟이 없으면 
        {
            Collider[] colliders = Physics.OverlapSphere(_transform.position, MonsterBT.fovRange,_targetLayerMask);  //시야범위 콜라이더 : 거리 , 타겟
            if (colliders.Length > 0)       //충돌체 감지 되면
            {
                //Debug.Log($"GET DATA _ 타겟/콜라이더 저장전 = {GetData("target")}");
                parent.parent.SetData("target", colliders[0].transform); // 딕셔너리에 "target"key로 '충돌체 transform' 추가
                parent.parent.SetData("targetCollider", colliders[0].GetComponent<Collider>());
                // 콜라이더가 추가된게 콜라이더를 나간다고 삭제가 안됨
                //Debug.Log($"GET DATA _ target = {GetData("target")}");
                //Debug.Log($"target: {colliders[0].name}");
                // animation 추가
                state = NodeState.SUCCESS;      // state 변경 => SUCCESS
                return state;    
            }
            
            state = NodeState.FAILURE;
            return state;
        }

        state = NodeState.SUCCESS;
        //Debug.Log("몬스터Node_시야범위체크 : SUCCESS");
        return state;
    }
    
}
