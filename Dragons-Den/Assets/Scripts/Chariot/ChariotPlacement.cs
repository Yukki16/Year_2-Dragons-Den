using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChariotPlacement : MonoBehaviour
{
    [SerializeField] Scoring fs;

    [SerializeField] GameObject playerChariot;
    [SerializeField] GameObject finishLine;

    List<GameObject> chariots = new List<GameObject>();

    [SerializeField] float moveSpeed;
    [SerializeField] float xPosDissapear;

    private IEnumerator finalElements = null;

    private void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeRight;

        chariots.Add(playerChariot);
    }

    private void FixedUpdate()
    {
        if (playerChariot != null)
            MoveChariots();

        if (playerChariot.transform.position.x > finishLine.transform.position.x)
        {          
            if (finalElements == null)
            {
                finalElements = fs.GetComponent<Scoring>().DisplayFinalScoreElements((float)ChariotRaceManager.GetPlayerScore(), (float)ChariotRaceManager.GetQuestionCount(), ChariotRaceManager.GoldPassingPercentage, ChariotRaceManager.SilverPassingPercentage);
                StartCoroutine(finalElements);
            }
        }
    }

    void MoveChariots()
    {

        for (int i = 0; i < 1; i++)
        {

            if (chariots[i].transform.position.x < xPosDissapear)
            {
                chariots[i].transform.position = Vector2.MoveTowards(chariots[i].transform.position, new Vector2(chariots[i].transform.position.x + 100, chariots[i].transform.position.y), moveSpeed);
            }
        }
    }
}

