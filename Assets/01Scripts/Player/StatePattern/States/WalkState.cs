using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : PlayerState
{
    public WalkState(PlayerController playerController) : base(playerController)
    {
        
    }

    public override void Enter()
    {
        
    }
    public override void Update()
    {
        Debug.Log("Walk State");
    }
    public override void Exit()
    {
        
    }
}
