using UnityEngine;
using System.Collections;

public class AnimationMenu : MonoBehaviour 
{
    public Animator animator;

    public void AnimationPlay()
    {

        animator.SetBool("activated", true);
    }

    public void AnimationStop()
    {

        animator.SetBool("activated", false);
    }
}
