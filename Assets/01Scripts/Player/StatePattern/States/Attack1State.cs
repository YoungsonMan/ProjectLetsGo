using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack1State : PlayerState
{
    public Attack1State(PlayerController playerController) : base(playerController)
    {
        
    }

    public override void Enter()
    {
        float aniTime = 0;
        RuntimeAnimatorController ac = playerController.animator.runtimeAnimatorController;
        float clickTime = Time.time;
        
        Debug.Log("Atack 111111 ");
        // playerController.animator.SetBool("attack1",true);
        playerController.animator.SetTrigger("Attack1");
        
        
    }
    public override void Update()
    {
        
        //Debug.Log("콤보 1 어택");
        //playerController.animator.SetBool("attack1",true);
        //playerController.animator.SetTrigger("Attack1");
    }
    public override void Exit()
    {
       // playerController.animator.SetBool("attack1",false);
       
    }
}
