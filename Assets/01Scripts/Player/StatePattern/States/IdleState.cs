using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
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
        playerController.animator.SetBool("Idle",true);
       // Debug.Log("Idle");
    }
    public override void Exit()
    {
        
    }
}
