using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bars : MonoBehaviour
{
    [SerializeField] private AudioClip barsUpSound;
    [SerializeField] private AudioClip barsDownSound;

    private readonly int barsUpHash = Animator.StringToHash("barsUp");
    private readonly int barsDownHash = Animator.StringToHash("barsDown");

    protected AudioSource AudioSource;
    protected Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        AudioSource = GetComponent<AudioSource>();
    }

    public void BarsUp()
    {
        AudioSource.PlayOneShot(barsUpSound);
        animator.SetBool(barsUpHash, true);
    }

    public void BarsDown()
    {
        AudioSource.PlayOneShot(barsDownSound);
        animator.SetBool(barsDownHash, true);
    }
        
    private void DestroyBars()
    {
        Destroy(this.gameObject);   
    }

}
   
