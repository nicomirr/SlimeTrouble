using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackScreenAnimations : MonoBehaviour
{
    [SerializeField] private bool fadeIn;

    private readonly int fadeInHash = Animator.StringToHash("fadeIn");

    private Animator animator;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetBool(fadeInHash, fadeIn);
    }

}
