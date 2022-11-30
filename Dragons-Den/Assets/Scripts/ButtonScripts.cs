using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScripts : MonoBehaviour
{
    [SerializeField] Animator animator;
    bool slide;
    public void BringMenu()
    {
        animator.SetBool("SlideBack", slide);
        slide = !slide;
    }
}
