using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class TaskAttack : Node
{
    private Transform _lastTarget;

    private float _attackTime = 1f;
    private float _attackCounter = 0f;
        
  //  private Animator _animator;       TODO: 추후 애니메이션 추가 후 

    public TaskAttack(Transform transform)
    {
       // _animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        if (target != _lastTarget)
        {
            _lastTarget = target;
            Debug.Log("타겟공격");
        }
        _attackCounter += Time.deltaTime;
        if (_attackCounter >= _attackTime)
        {
            bool enemyIsDead = true; // 몬스터 공격하는거 구현후 공격메서드 
            if (enemyIsDead)
            {
                ClearData("target");
               // _animator.SetBool("Attacking", false);
               // _animator.SetBool("Walking", true);
            }
            else
            {
                _attackCounter = 0f;
            }
        }

        state = NodeState.RUNNING;
        return state;
    }
    
    
}
