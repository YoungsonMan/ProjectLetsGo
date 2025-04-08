using System.Collections;
using System.Collections.Generic;
using BehaviorTree;




public class MonsterBT : Tree
{
    public UnityEngine.Transform[] waypoints;

    public static float speed = 2f;         // TODO 추후 몬스터 틀 만들면 거기에 스탯들 맞게
    public static float fovRange = 6f;
    public static float attackRange = 1f;

    protected override Node SetupTree()
    {
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
        
        return root;
    }
}
