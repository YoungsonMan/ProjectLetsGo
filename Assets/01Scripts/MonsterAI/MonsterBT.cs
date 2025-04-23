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

    public static float speed = 10f;         // TODO 추후 몬스터 틀 만들면 거기에 스탯들 맞게
    public static float fovRange = 6f;
    public static float attackRange = 1f;

    protected override Node SetupTree()
    {
        // Node root = new TaskPatrol(transform, waypoints);  // 패트롤 정상작동 확인.
        
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckTargetInAttackRange(transform), new TaskAttack(transform),
            }),
            new Sequence(new List<Node>
            {
               new CheckTargetInFOVRange(transform), new TaskGoToTarget(transform), 
            }),
            new TaskPatrol(transform, waypoints)
        });
        _currentNode = root.ToString();
        Debug.Log(_currentNode);
        return root;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, MonsterBT.fovRange);
    }
}
