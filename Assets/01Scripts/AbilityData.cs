using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;


namespace Architeture.AbilitySystem
{
    [CreateAssetMenu(fileName = "AbilityData", menuName = "ScriptableObjects/AbilityData")]
    
    public class AbilityData : ScriptableObject
    {
        [SerializeField] public AnimationClip animationClip;
        [SerializeField, ReadOnly] public int animationHash;
        [SerializeField] public float duration;
        [SerializeField] public Sprite icon;

        private void OnValidate()
        {
            animationHash = Animator.StringToHash(animationClip.name);
        }
        
    }
    
}
