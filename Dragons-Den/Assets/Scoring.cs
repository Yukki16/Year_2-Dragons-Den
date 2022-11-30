using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{

    [SerializeField] TMPro.TextMeshProUGUI placementText;

    [SerializeField] ParticleSystem particle1Mach;
    [SerializeField] ParticleSystem particle2Mach;

    [SerializeField] GameObject TransparentBG;

    [SerializeField] GameObject goldTrophyCard;
    [SerializeField] GameObject silverTrophyCard;
    [SerializeField] GameObject bronzeTrophyCard;

    [SerializeField] int FadeInSpeed = 1;

    private float alpha = 0;

    void Start()
    {
        goldTrophyCard.SetActive(false);
        silverTrophyCard.SetActive(false);
        bronzeTrophyCard.SetActive(false);
        TransparentBG.SetActive(false);
        placementText.SetText("");

        setTransparency(goldTrophyCard, 0);
        setTransparency(silverTrophyCard, 0);
        setTransparency(bronzeTrophyCard, 0);
        setTransparency(placementText, 0);

        particle1Mach.Pause();
        particle2Mach.Pause();
    }

    public IEnumerator DisplayFinalScoreElements(float correct, float questions, float goldReq, float silverReq)
    {
        TransparentBG.SetActive(true);

        float passingPercentage = ((correct) / questions) * 100; 

        Debug.Log("Grade: " + passingPercentage);

        char placement = 'b';

        if (passingPercentage >= silverReq)
        {
            Debug.Log("Silver");
            placement = 's';
        }

        if (passingPercentage >= goldReq)
        {
            Debug.Log("Gold");
            placement = 'g';
        }


        switch (placement)
        {
            case 'g':
                StartCoroutine(IncreaseAlpha(goldTrophyCard));
                particle2Mach.Play();
                particle1Mach.Play();
                break;

            case 's':
                StartCoroutine(IncreaseAlpha(silverTrophyCard));
                particle2Mach.Play();
                particle1Mach.Play();
                break;

            case 'b':
                StartCoroutine(IncreaseAlpha(bronzeTrophyCard));
                break;
        }

        StartCoroutine(ShowPlacement(placementText, correct, questions));

        yield break;
    }

    IEnumerator IncreaseAlpha(GameObject go, int max = 255)
    {
        go.SetActive(true);
        Color tempColor;
        tempColor = go.GetComponent<SpriteRenderer>().color;
        tempColor.a = alpha;
        go.GetComponent<SpriteRenderer>().color = tempColor;

        alpha += 0.0001f * FadeInSpeed;

        if (alpha >= max)
        {
            yield break;
        }
           
        yield return new WaitForEndOfFrame();
        StartCoroutine(IncreaseAlpha(go, max));

    }

    IEnumerator IncreaseAlpha(TMPro.TextMeshProUGUI go, int max = 255)
    {
        go.color = new Color(0, 0, 0, alpha);

        alpha += 0.0001f * FadeInSpeed;

        if (alpha >= max)
        {
            yield break;
        }

        yield return new WaitForEndOfFrame();
        StartCoroutine(IncreaseAlpha(go, max));
    }


    void setTransparency(GameObject go, int val)
    {
        Color tempColor = go.GetComponent<SpriteRenderer>().color;
        tempColor.a = val;
        go.GetComponent<SpriteRenderer>().color = tempColor;
    }

    void setTransparency(TMPro.TextMeshProUGUI go, int val)
    {
        go.color = new Color(0, 0, 0, val);
    }

    IEnumerator ShowPlacement(TMPro.TextMeshProUGUI pt, float correct, float questions)
    {
        pt.text = correct + " / " + questions;
        StartCoroutine(IncreaseAlpha(pt));
        yield break;

    }
}
