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
      //  Debug.Log("Idle 진입");
    }
    public override void Update()
    {
        playerController.animator.SetBool("Idle",true);
      //  Debug.Log("Idle");
    }
    public override void Exit()
    {
        playerController.animator.SetBool("Idle",false);
       // Debug.Log("Idle 해제");
    }
}
