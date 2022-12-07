using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scoring : MonoBehaviour
{

    [SerializeField] AudioManager am;

    [SerializeField] GameObject ArtifactRewardOverlay;
    [SerializeField] GameObject FinalScoreOverlay;

    [SerializeField] TMPro.TextMeshProUGUI placementText;
    [SerializeField] TMPro.TextMeshProUGUI continueText;

    [SerializeField] ParticleSystem particle1Mach;
    [SerializeField] ParticleSystem particle2Mach;

    [SerializeField] GameObject TransparentBG;

    [SerializeField] GameObject goldTrophyCard;
    [SerializeField] GameObject silverTrophyCard;
    [SerializeField] GameObject bronzeTrophyCard;

    [SerializeField] int FadeInSpeed;

    private float alpha = 0;

    private bool canExit;

    private bool transitionToReward;

    [SerializeField] private float rewardStarShootDuration;

    private void Awake()
    {
        ArtifactRewardOverlay.SetActive(false);
        continueText.gameObject.SetActive(false);
    }

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
        particle1Mach.Pause();
        particle2Mach.Pause();
    }

    void Update()
    {
        if (canExit)
        {
            if (Input.anyKeyDown)
            {
                if (transitionToReward == true)
                {

                    FinalScoreOverlay.SetActive(false);
                    ArtifactRewardOverlay.SetActive(true);
                    StartCoroutine(StopRewardStars());
                    StartCoroutine(DelayTransition());
                }

                if (transitionToReward == false)
                {
                    Debug.Log("Marker");

                    SceneManager.LoadScene("MainRoad", LoadSceneMode.Single);
                }
                
            }
        }
    }

    IEnumerator StopRewardStars()
    {
        am.Play("Achievement");
        yield return new WaitForSeconds(rewardStarShootDuration);
        ArtifactRewardOverlay.GetComponentInChildren<ParticleSystem>().Stop();
        Debug.Log("Marker2");
        yield break;
    }

    IEnumerator DelayTransition()
    {
        yield return new WaitForSeconds(0.5f);
        transitionToReward = false;
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
            transitionToReward = true;
            Debug.Log("Gold");
            placement = 'g';
        }

        if (correct == 0 && questions == 0)
        {
            passingPercentage = 100;
            placement = 'g';
        }

        switch (placement)
        {
            case 'g':
                StartCoroutine(IncreaseAlpha(goldTrophyCard));
                particle2Mach.Play();
                particle1Mach.Play();
                am.Play("Gold");
                break;

            case 's':
                StartCoroutine(IncreaseAlpha(silverTrophyCard));
                particle2Mach.Play();
                particle1Mach.Play();
                am.Play("Silver");
                break;

            case 'b':
                StartCoroutine(IncreaseAlpha(bronzeTrophyCard));
                am.Play("Bronze");
                break;

            default:
                am.Play("Gold");
                break;

        }
        continueText.gameObject.SetActive(true);
        StartCoroutine(ShowPlacement(placementText, correct, questions));
        //Allows the player to exit the game at this point
        canExit = true;
        yield break;
    }

    IEnumerator IncreaseAlpha(GameObject go, int max = 255)
    {
        go.SetActive(true);
        Color tempColor;
        tempColor = go.GetComponent<SpriteRenderer>().color;
        tempColor.a = alpha;
        go.GetComponent<SpriteRenderer>().color = tempColor;

        alpha += (1f * FadeInSpeed) * Time.deltaTime;

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

        alpha += (1f * FadeInSpeed) * Time.deltaTime;

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
        yield break;

    }
}
