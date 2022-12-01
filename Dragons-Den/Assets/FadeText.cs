using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeText : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI text;
    [SerializeField] private bool fadeOut;
    [SerializeField] private float fadeSpeed;
    [SerializeField] private float bounceDist;

    void Start()
    {
        if (text == null)
            text = GetComponent<TMPro.TextMeshProUGUI>();

        text.alpha = 0;
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        text.alpha += 0.0001f * fadeSpeed;
        if (text.alpha <= 1)
        {
            yield return new WaitForEndOfFrame();
            StartCoroutine(FadeIn());
        }
        else
        {
            if (fadeOut)
            StartCoroutine(FadeOut());
            yield break;
        }         
    }

    IEnumerator FadeOut()
    {
        text.alpha -= 0.0001f * fadeSpeed;
        if (text.alpha >= bounceDist / 100)
        {
            yield return new WaitForEndOfFrame();
            StartCoroutine(FadeOut());
        }
        else
        {
            StartCoroutine(FadeIn());
            yield break;
        }
    }
}
