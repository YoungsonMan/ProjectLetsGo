using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;
using Tree = BehaviorTree.Tree;


[System.Serializable]
public class MonsterBT : Tree
{
    public UnityEngine.Transform[] waypoints;
    
    [SerializeField] private string _currentNode;

    public static float speed = 5f;         // TODO 추후 몬스터 틀 만들면 거기에 스탯들 맞게
    public static float fovRange = 6f;
    public static float attackRange = 1f;

    protected override Node SetupTree()
    {
        // Node root = new TaskPatrol(transform, waypoints);  // 패트롤 정상작동 확인.
        
        Node root = new Selector(new List<Node>                                       // 선택
        {
            new Sequence(new List<Node>                                               // Sequence Node 1
            {
                new CheckTargetInAttackRange(transform), new TaskAttack(transform),             // 공격범위체크 => 공격
            }),
            new Sequence(new List<Node>                                               // Sequence Node 2
            {
               new CheckTargetInFOVRange(transform), new TaskGoToTarget(transform),             // 시야범위체크 => 타겟추격
            }),
            new TaskPatrol(transform, waypoints)                                                // Node 패트롤
        });
        _currentNode = root.ToString();
        Debug.Log(_currentNode);
        return root;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, MonsterBT.fovRange); // 시야 범위
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, MonsterBT.attackRange); // 공격가능 범위

    }
}
