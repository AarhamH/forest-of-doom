using UnityEngine;

/*
    AnimationController class: Main Animation system controller for gameobjects
    Necessary Components: Animator (Animator) => takes the animator component from the gameObject as
                                                 a serializable
*/
public class AnimationController : MonoBehaviour
{
    public Animator animator;
    public float animationPlayTransition = 0.15f;
    public float animationSmoothTime = 0.1f;

    /*
        Update Function: Instantiate the animator class, which holds the methods to perform animations
    */
    public void AnimationPlayerInstance()
    {
        animator = GetComponent<Animator>();
    }


    /*
        Input: n/A

        Output: n/A

        Functionality: - Uses a string to find a hashcode from the animation graph and plays it
                         with respect to a transition time
        
        Called In: Methods where an animation is required to be played
    */
    public void ExecuteAnimation(string animation) {
        animator.CrossFade(Animator.StringToHash(animation),animationPlayTransition);
    }


    /*
        Input: n/A

        Output: n/A

        Functionality: - Uses a string to find a hashcode from the animation graph and plays an
                         animation based on the combo inputs of x and y
        
        Called In: Methods where an animation is required to be played
    */
    public void WalkAnimation(string animationX, string animationY, float blendVectorX, float blendVectorY) {
        animator.SetFloat(Animator.StringToHash(animationX), blendVectorX);
        animator.SetFloat(Animator.StringToHash(animationY), blendVectorY);
    }
}
