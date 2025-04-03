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
      //  Debug.Log("Walk State Enter");
    }
    public override void Update()
    {
        playerController.animator.SetBool("Walk",true);
        // Debug.Log("Walk State");
    }
    public override void Exit()
    {
        playerController.animator.SetBool("Walk",false);
       // Debug.Log("Walk State Exit");
    }
}
