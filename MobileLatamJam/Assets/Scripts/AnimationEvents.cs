using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public Animator anim;
    

    void DoneAttacking()
    {
        anim.SetBool("isAttacking", false);
    }

}
