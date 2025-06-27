using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsAnimations : MonoBehaviour
{
    [SerializeField] private bool attackAnimation;

    private readonly int attackingHash = Animator.StringToHash("attacking");

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        animator.SetBool(attackingHash, attackAnimation);

        if(Time.timeScale == 0f)        
            animator.enabled = false;
        
    }

    private void Update()
    {
        if(Time.timeScale == 0)
            animator.Update(Time.unscaledDeltaTime);
    }
}
