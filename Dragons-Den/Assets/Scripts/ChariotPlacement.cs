using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChariotPlacement : MonoBehaviour
{
    [SerializeField] Scoring fs;

    [SerializeField] GameObject playerChariot;
    [SerializeField] GameObject silverChariot;
    [SerializeField] GameObject goldChariot;
    [SerializeField] GameObject finishLine;

    List<GameObject> chariots = new List<GameObject>();

    [SerializeField] float moveSpeed;
    [SerializeField] float xPosDissapear;

    private void Start()
    {
        chariots.Add(playerChariot);
        chariots.Add(silverChariot);
        chariots.Add(goldChariot);  
    }

    private void FixedUpdate()
    {
        if (playerChariot != null)
            MoveChariots();

        if (playerChariot.transform.position.x > finishLine.transform.position.x)
        {          
            StartCoroutine(fs.GetComponent<Scoring>().DisplayFinalScoreElements((float)ButtonManager.GetPlayerScore(), (float)ButtonManager.GetQuestionCount(), ButtonManager.GoldPassingPercentage, ButtonManager.SilverPassingPercentage));       
        }
    }

    void MoveChariots()
    {

        for (int i = 0; i < 3; i++)
        {

            if (chariots[i].transform.position.x < xPosDissapear)
            {
                chariots[i].transform.position = Vector2.MoveTowards(chariots[i].transform.position, new Vector2(chariots[i].transform.position.x + 100, chariots[i].transform.position.y), moveSpeed);
            }
        }
    }
}

