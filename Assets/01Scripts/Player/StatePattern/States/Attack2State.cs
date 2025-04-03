using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack2State : PlayerState
{
    public Attack2State(PlayerController playerController) : base(playerController)
    {
        
    }

    public override void Enter()
    {
        Debug.Log("Atack 222222 ");
        playerController.animator.SetTrigger("Attack2");
    }
    public override void Update()
    {
        Debug.Log("콤보 2 어택");
        //playerController.animator.SetBool("attack2",true);
        
    }
    public override void Exit()
    {
        //playerController.animator.SetBool("attack2",false);
    }
}
