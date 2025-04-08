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
        object obj = GetData("target");
        if (obj == null)
        {
            state = NodeState.FAILURE;
            return state;
        }
        Transform target = (Transform)obj;
        if (Vector3.Distance(_transform.position, target.position) <= MonsterBT.attackRange)
        {
          //  _animator.SetBool("Attacking", true);
          //  _animator.SetBool("Walking", false);
            state = NodeState.SUCCESS;
            return state;
        }
        
        state = NodeState.FAILURE;
        return state;
    }
    
}
