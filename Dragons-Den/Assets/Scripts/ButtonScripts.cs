using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScripts : MonoBehaviour
{
    #region Burger_menu_panel
    [SerializeField] Animator animator;
    bool slide;
    #endregion

    //Burger menu panel that slides
    public void BringMenu()
    {
        animator.SetBool("SlideBack", slide);
        slide = !slide;
    }

    public void ExitAplicationm()
    {
        Application.Quit();
    }
}
