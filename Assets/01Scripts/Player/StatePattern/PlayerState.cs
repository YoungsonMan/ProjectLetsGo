using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerState : BaseState
{
    public PlayerController playerController;
    // public Animator animator;
    public PlayerModel playerModel; // 추후에 버프스킬에 능력치 보정해야되면 사용?

    public PlayerState(PlayerController player)
    {
        this.playerController = player;
    }

    
}
