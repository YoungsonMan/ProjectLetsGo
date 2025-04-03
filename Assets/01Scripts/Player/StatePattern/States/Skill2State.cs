using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill2State : PlayerState
{
    public Skill2State(PlayerController playerController) : base(playerController)
    {
        
    }

    public override void Enter()
    {
        Debug.Log("스킬 22222222 ");
        playerController.animator.SetTrigger("Skill2");
    }
    public override void Update()
    {
        
    }
    public override void Exit()
    {
        
    }
}
