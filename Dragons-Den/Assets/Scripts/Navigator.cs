using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Navigator : MonoBehaviour
{
    [SerializeField] private GameObject InfoPopup;
    [SerializeField] private Mover mover;

    private bool infoOpen;

    string ColosseumTitle = "The Colosseum";
    string ColosseumInfo = "Said to hold 80,000 spectatiors, it's Construction began under the rule of emperor Vespasian and held various events such as gladiator fights, live hunting and they would even fill it up with water and show naval battles. It stood at 48 meters tall. (about 28 people on top of each other)";

    void Start()
    {
        
    }

    private void Awake()
    {
        ResetInfoPopup();
    }

    void Update()
    {
        if (infoOpen && Input.anyKey)
        {
            ResetInfoPopup();
        }
    }

    public void DisplayColosseumInfo()
    {
        StartCoroutine(ShowInfoPopup(ColosseumTitle, ColosseumInfo));
    }

    public void DisplayInfoPopup(string text)
    {
        switch (text)
        {
            case "colosseum":
                StartCoroutine(ShowInfoPopup(ColosseumTitle, ColosseumInfo));
                break;
        }
    }

    IEnumerator ShowInfoPopup(string title, string info)
    {
        mover.LockMovement(true);
        InfoPopup.SetActive(true);
        InfoPopup.transform.position = new Vector2(mover.transform.position.x, 0);
        InfoPopup.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = title;
        InfoPopup.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = info;
        yield return new WaitForSeconds(0.2f);
        infoOpen = true;
        yield break;
    }

    public void LoadChariotGame()
    {
        SceneManager.LoadScene("ChariotRace", LoadSceneMode.Single);
    }
    public void LoadCrossGame()
    {
        SceneManager.LoadScene("CrossWords", LoadSceneMode.Single);
    }

    private void ResetInfoPopup()
    {
        mover.LockMovement(false);
        infoOpen = false;
        InfoPopup.SetActive(false);
    }
}
