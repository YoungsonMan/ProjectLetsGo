using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerState
{
    public IdleState(PlayerController playerController) : base(playerController)
    {
        
    }

    public override void Enter()
    {
        
    }
    public override void Update()
    {
       // Debug.Log("Idle");
    }
    public override void Exit()
    {
        
    }
}
