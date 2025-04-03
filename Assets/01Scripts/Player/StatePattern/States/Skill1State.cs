using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1State : PlayerState
{
    public Skill1State(PlayerController playerController) : base(playerController)
    {
        
    }

    public override void Enter()
    {
        Debug.Log("Skill 111111 ");
        playerController.animator.SetTrigger("Skill1");
    }
    public override void Update()
    {
        
    }
    public override void Exit()
    {
        
    }
}
