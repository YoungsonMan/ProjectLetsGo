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
        playerController.animator.Play("Run");
        // Debug.Log("Walk State");
    }
    public override void Exit()
    {
        
    }
}
