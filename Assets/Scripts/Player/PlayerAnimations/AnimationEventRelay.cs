using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventRelay : MonoBehaviour
{
    private PlayerAttack playerAttack;

    private void Awake()
    {
        playerAttack = GetComponentInParent<PlayerAttack>();                   
    }

    private void OnAttackAnimationEnded()
    {
        playerAttack?.OnAttackFinishedAnimationEventRelay();        
    }
}
