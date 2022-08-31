using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// turned this into a class because I dont want to copy all of these components repeatedly,
// but acces the class once to get the functionalities
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
