using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack3State : PlayerState
{
    public Attack3State(PlayerController playerController) : base(playerController)
    {
        
    }

    public override void Enter()
    {
        Debug.Log("Atack 33333 ");
    }
    public override void Update()
    {
        Debug.Log("콤보 3 어택");
        //playerController.animator.SetBool("attack3",true);
        playerController.animator.SetTrigger("Attack3");
    }
    public override void Exit()
    {
        //playerController.animator.SetBool("attack3",false);
    }
}
