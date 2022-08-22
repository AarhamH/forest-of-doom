using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator animator;
    public float animationPlayTransition = 0.15f;
    public float animationSmoothTime = 0.1f;

    public void AnimationPlayerInstance()
    {
        animator = GetComponent<Animator>();
    }
}
