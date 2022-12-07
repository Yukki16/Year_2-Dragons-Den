using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeText : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI text;
    [SerializeField] private bool fadeOut;
    [SerializeField] private float fadeSpeed;
    [SerializeField] private float bounceDist;

    IEnumerator runRoutine;

    void Start()
    {
        
        if (text == null)
            text = GetComponent<TMPro.TextMeshProUGUI>();
        
            text.alpha = 0;
    }

    private void Update()
    {
        if (runRoutine == null)
        {
            runRoutine = FadeIn();
            StartCoroutine(runRoutine);
        }
    }

    public IEnumerator FadeIn()
    {
        text.alpha += (0.1f * fadeSpeed) * Time.deltaTime;
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

    public IEnumerator FadeOut()
    {
        text.alpha -= (0.1f * fadeSpeed) * Time.deltaTime;
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

    private void OnDisable()
    {
        text.alpha = 0;
       runRoutine = null;
    }
}
