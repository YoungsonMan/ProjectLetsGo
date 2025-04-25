using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class TaskGoToTarget : Node
{
    private Transform _transform;
    
    // 추적 실패시 빠꾸를 위한 변수
    private bool _isTargetInFOVRange;
    private static int _targetLayerMask = 1 << 6; 

    public TaskGoToTarget(Transform transform)
    {
        _transform = transform; // 자신의 transform 가져오기
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");    // 타겟 트랜스폼 접수
        Collider targetCollider = (Collider)GetData("targetCollider");    // 타겟 콜라이더 접수
        
        #region 디버그로그
        Debug.Log($"몬스터Node_타겟추격 : 타겟설정 = {target} ");
        Debug.Log($"몬스터Node_타겟추격 : TargetPoisition = {target.position} ");
        #endregion
        
        // TODO: 조건문을 만들어서 레이를 벗어나면 복귀시켜야함 or FAILURE상태로. 
        
    //    Collider[] colliders = Physics.OverlapSphere(_transform.position, MonsterBT.fovRange,_targetLayerMask);
    //    if (colliders.Length > 0)       //충돌체 감지 되면
    //    {
    //        Debug.Log($"GET DATA _ 타겟/콜라이더 저장전 = {GetData("target")}");
    //        parent.parent.SetData("target", colliders[0].transform); // 딕셔너리에 "target"key로 '충돌체 transform' 추가
    //        // 콜라이더가 추가된게 콜라이더를 나간다고 삭제가 안됨
    //        Debug.Log($"target: {colliders[0].name}");
    //        Debug.Log($"GET DATA _ target = {GetData("target")}");
    //        // animation 추가
    //        state = NodeState.SUCCESS;      // state 변경 => SUCCESS
    //        return state;    
    //    }
        
        
        RaycastHit hit;
        Debug.Log($"타겟타겟타겟: {target}");
        if(Physics.SphereCast(_transform.position, MonsterBT.fovRange, _transform.forward, out hit, 0.1f, _targetLayerMask ))
        {
            Debug.Log($"!!! 추격중 레이케스트 테스팅 로그!!!");
            
            Debug.Log($"GTT Target: {target.name}");
            Debug.Log($"GTT collider {hit.collider.gameObject.name}");
            if (hit.collider == targetCollider)   
            {
                Debug.Log($"플레이어를 놓졌다!!!");
                state = NodeState.FAILURE;
                return state;
            }
        }
        if (Vector3.Distance(_transform.position, target.position) > 0.1f) // 몹to타겟 거리 > 지정 거리(지금은.1)보다 멀면 접근
        {
            Debug.Log($"몬스터Node_GTT : TargetLocation = {target.position} ");
            _transform.position = Vector3.MoveTowards(_transform.position, target.position, MonsterBT.speed * Time.deltaTime); // target.posiotn, 다음에 몹속도 * Time.deltaTime
            _transform.LookAt(target.position);         // 타겟 바라보기
            Debug.Log($"몬스터Node_GTT : 타겟과의 거리 = {Vector3.Distance(target.position, _transform.position)} ");
            // 시야범위에서 벗어나면 취소시켜야함.
        }

    //    if (Vector3.Distance(_transform.position, target.position) > 0.1f) // 몹to타겟 거리 > 지정 거리(지금은.1)보다 멀면 접근
    //    {
    //        _transform.position = Vector3.MoveTowards(_transform.position, target.position, MonsterBT.speed * Time.deltaTime); // target.posiotn, 다음에 몹속도 * Time.deltaTime
    //        _transform.LookAt(target.position);         // 타겟 바라보기
    //        Debug.Log($"몬스터Node_타겟추격 : 타겟과의 거리 = {Vector3.Distance(target.position, _transform.position)} ");
    //        
    //        // 시야범위에서 벗어나면 취소시켜야함.
    //        
    //    }
    // Debug.Log($"히트히트히트: {hit}");    
        state = NodeState.RUNNING;
        Debug.Log("몬스터Node_GTT : RUNNING");
        return state;
    }
    
}
