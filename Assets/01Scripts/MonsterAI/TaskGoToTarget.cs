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
        Transform target = (Transform)GetData("target"); // 타겟접수

        #region 디버그로그
        Debug.Log($"몬스터Node_타겟추격 : 타겟설정 = {target} ");
        Debug.Log($"몬스터Node_타겟추격 : TargetPoisition = {target.position} ");
        #endregion
        
        RaycastHit hit;
        if (Physics.SphereCast(_transform.position, MonsterBT.fovRange, _transform.forward,out hit, 0, _targetLayerMask ))
        {
            Debug.Log($"충돌포착: {hit.collider.gameObject.name}");
            
        }
        if (Vector3.Distance(_transform.position, target.position) > 0.1f) // 몹to타겟 거리 > 지정 거리(지금은.1)보다 멀면 접근
        {
            _transform.position = Vector3.MoveTowards(_transform.position, target.position, 0.1f); // target.posiotn, 다음에 몹속도 * Time.deltaTime
            _transform.LookAt(target.position);         // 타겟 바라보기
            Debug.Log($"몬스터Node_타겟추격 : 타겟과의 거리 = {Vector3.Distance(target.position, _transform.position)} ");
            
            // 시야범위에서 벗어나면 취소시켜야함.
            
        }
        state = NodeState.RUNNING;
        Debug.Log("몬스터Node_타겟추격 : RUNNING");
        return state;
    }
    
}
