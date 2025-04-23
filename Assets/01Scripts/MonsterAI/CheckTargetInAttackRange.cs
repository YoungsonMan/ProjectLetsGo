using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class CheckTargetInAttackRange : Node
{
    private static int _enenmyLayerMask = 1 << 6;
    
    private Transform _transform;
   // private Animator _animator;

    public CheckTargetInAttackRange(Transform transform)
    {
        _transform = transform;
      //  _animator = _transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        object obj = GetData("target");                 // 타겟접수
        Debug.Log($"Node_공격범위체크 ON");
        if (obj == null)                                     // 타겟이 없으면
        {
            state = NodeState.FAILURE;
            Debug.Log("Node_공격범위체크 : FAILURE");
            return state;
        }
        Transform target = (Transform)obj;
        if (Vector3.Distance(_transform.position, target.position) <= MonsterBT.attackRange) //공격범위내로 들어오면 
        {
          //  _animator.SetBool("Attacking", true);
          //  _animator.SetBool("Walking", false);
            state = NodeState.SUCCESS;
            Debug.Log("Node_공격범위체크 : SUCCESS => Node_공격");
            return state;
        }
        
        state = NodeState.FAILURE;
        return state;
    }
    
}
