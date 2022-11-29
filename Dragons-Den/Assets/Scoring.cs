using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoring : MonoBehaviour
{

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

        goldTrophyCard.GetComponent<SpriteRenderer>().color = new Color(191, 191, 191, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator DisplayFinalScoreElements(char placement)
    {
        switch (placement)
        {
            case 'g':
                StartCoroutine(IncreaseAlpha(goldTrophyCard));
                goldTrophyCard.SetActive(true);
                break;
        }

        yield break;
    }

    IEnumerator IncreaseAlpha(GameObject go)
    {

        go.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, alpha);


        alpha += 0.0001f * FadeInSpeed;

        if (alpha >= 255)
            yield break;

        yield return new WaitForEndOfFrame();
        StartCoroutine(IncreaseAlpha(go));

    }
}
