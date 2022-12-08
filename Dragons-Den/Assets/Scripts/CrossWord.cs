using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CrossWord : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] AudioManager am;

    [SerializeField] GameObject ArtifactRewardOverlay;

    [SerializeField] Image imageToShow;
    [SerializeField] Sprite[] sprites;

    [SerializeField] TMP_Text text;
    [SerializeField] string[] textToFill;

    [System.NonSerialized] public char[,] letters;
    [SerializeField] public List<string> wordsToFind;
    [SerializeField] GameObject canvasObjectParent;
    [SerializeField] Sprite letterBackground;

    [System.NonSerialized] public GameObject[] lines;
    [System.NonSerialized] public LineRenderer lineRenderer;
    [System.NonSerialized] public bool doneOnce;
    [System.NonSerialized] public int lineIndex = 0;
    [System.NonSerialized] public string foundWord = string.Empty;
    [System.NonSerialized] public string RfoundWord = string.Empty;
    [System.NonSerialized] public bool wordFound;

    private bool transitionToReward = true;
    [SerializeField] private int rewardStarShootDuration;

    public bool finishedGame;
    int gameLenght;
    int boxSizeX;
    int boxSizeY;

    [System.NonSerialized] public Vector2 crossStart;
    [System.NonSerialized] public Vector2 crossEnd;

    int boxIndex = 0;

    private void Start()
    {
        //background = new Texture2D(imageBackground);
        boxSizeX = (int)(2f * Screen.width) / 100;
        boxSizeY = (int)(4f * Screen.height) / 100;

        lines = new GameObject[wordsToFind.Count];

        Debug.Log("box size X: " + boxSizeX);
        Debug.Log("box size Y: " + boxSizeY);
        CreateCross(16);
        //CreateCross(LongestWordLenght() * 2);
    }

    private void Update()
    {
        if(finishedGame)
        {
            if (ArtifactTracker.HasTablet())
            {
                SceneManager.LoadScene("MainRoad", LoadSceneMode.Single);
            }

            if (!transitionToReward && Input.anyKey)
            {
                SceneManager.LoadScene("MainRoad", LoadSceneMode.Single);
            }
  
            if (!ArtifactTracker.HasTablet())
            {
                ArtifactRewardOverlay.SetActive(true);
                StartCoroutine(StopRewardStars());
                StartCoroutine(DelayTransition());
                ArtifactTracker.HasTablet(true);
            }
       

            for (int i = 0; i < wordsToFind.Count; i++)
            {
                lines[i].SetActive(false);
            }

            finishedGame = false;
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

    private void CreateCross(int _gameLenght)
    {
        gameLenght = _gameLenght;

        Debug.Log("Lenght: " + _gameLenght);
        letters = new char[_gameLenght, _gameLenght];
        for (int i = 0; i < _gameLenght; i++)
        {
            for (int j = 0; j < _gameLenght; j++)
            {
                letters[i, j] = '-';
            }
        }

        int x = 0, y = 0, direction = 0, counter = 0;

        for (int i = 0; i < wordsToFind.Count; i++)
        {
            counter = 0;
            //Debug.Log("Loop number: " + i);
            DirectionOfWord(_gameLenght, ref x, ref y, ref direction, i);

            while (!CheckWordSpace(direction, x, y, i))
            {
                DirectionOfWord(_gameLenght, ref x, ref y, ref direction, i);
                counter++;
                if (counter > 15)
                {
                    throw new System.Exception("No space for the word!");
                }
            }
            AddWordToPuzzle(direction, x, y, i);
        }

                for (int i = 0; i < gameLenght; i++)
        {
            for (int j = 0; j < gameLenght; j++)
            {
                if (boxIndex < gameLenght * gameLenght)
                {
                    var gameObject = new GameObject("   ");
                    gameObject.transform.parent = canvasObjectParent.transform;

                   
                    //********************************************
                    //Adding events
                    gameObject.AddComponent<ImageEvents>().crossWord = this;
                    gameObject.GetComponent<ImageEvents>().cam = this.cam;
                    gameObject.GetComponent<ImageEvents>().positionInArray = new Vector2(i, j);

                    //***********************************************
                    //The Image child
                    Image image = gameObject.AddComponent<Image>();
                    image.sprite = letterBackground;
                    Vector2 newPos = new Vector2(gameObject.transform.parent.position.x + boxSizeX / 100.0f * i, gameObject.transform.parent.position.y - boxSizeY / 125.0f * j);
                    gameObject.transform.position = newPos;

                    image.transform.localScale = new Vector2(boxSizeX / 88.0f, boxSizeY / 88.0f);


                    //*************************************************
                    //The text child
                    var textObject = new GameObject("Text");
                    textObject.transform.parent = gameObject.transform;
                    textObject.transform.position = textObject.transform.parent.position;
                    textObject.transform.localScale = Vector2.one;


                    var text = textObject.AddComponent<TextMeshProUGUI>();
                    //text.autoSizeTextContainer = true;
                    text.enableAutoSizing = true;
                    text.fontSizeMin = 12;
                    text.fontSizeMax = 130;
                    text.alignment = TextAlignmentOptions.Center;
                    text.color = Color.white;


                   //if (letters[i, j] == '-')
                   // {
                   //     letters[i, j] = (char)(int)Random.Range(65, 91);
                   // }

                    text.text = letters[i, j].ToString();


                    text.margin = new Vector4(50, -25, 50, -25);
                    //*****************************************************
                    boxIndex++;
                }

                //GUI.Box(boxes[i * gameLenght + j], letters[i, j].ToString(), style);
            }
        }
        //finishedGame = true;
    }
   
    
    private void AddWordToPuzzle(int direction, int x, int y, int i)
    {
        switch (direction)
        {
            case 0:
                for (int j = 0; j < wordsToFind[i].Length; j++)
                {
                    letters[x, y + j] = wordsToFind[i][j];
                }
                break;

            case 1:
                for (int j = 0; j < wordsToFind[i].Length; j++)
                {
                    letters[x + j, y + j] = wordsToFind[i][j];
                }
                break;

            default:
                for (int j = 0; j < wordsToFind[i].Length; j++)
                {
                    letters[x + j, y] = wordsToFind[i][j];
                }
                break;
        }
    }

    private bool CheckWordSpace(int direction, int x, int y, int i)
    {
        switch (direction)
        {
            case 0:
                for (int j = 0; j < wordsToFind[i].Length; j++)
                {
                    if (letters[x, y + j] == '-' || letters[x, y + j] == wordsToFind[i][j])
                    {
                        continue;
                    }
                    else
                    {
                        return false;
                    }

                }
                break;

            case 1:
                for (int j = 0; j < wordsToFind[i].Length; j++)
                {
                    if (letters[x + j, y + j] == '-' || letters[x + j, y + j] == wordsToFind[i][j])
                    {
                        continue;
                    }
                    else
                    {
                        return false;
                    }
                }
                break;

            default:
                for (int j = 0; j < wordsToFind[i].Length; j++)
                {
                    if (letters[x + j, y] == '-' || letters[x + j, y] == wordsToFind[i][j])
                    {
                        continue;
                    }
                    else
                    {
                        return false;
                    }
                }
                break;
        }
        return true;
    }

    private void DirectionOfWord(int gameLenght, ref int x, ref int y, ref int direction, int i)
    {
        direction = Random.Range(0, 3);
        switch (direction)
        {
            case 0:
                x = Random.Range(0, gameLenght);
                //Debug.Log("x: " + x);
                y = Random.Range(0, gameLenght - wordsToFind[i].Length);
                //Debug.Log("y: " + y);
                break;
            case 1:
                x = Random.Range(0, gameLenght - wordsToFind[i].Length);
                //Debug.Log("x: " + x);
                y = Random.Range(0, gameLenght - wordsToFind[i].Length);
                //Debug.Log("y: " + y);
                break;
            default:
                x = Random.Range(0, gameLenght - wordsToFind[i].Length);
                //Debug.Log("x: " + x);
                y = Random.Range(0, gameLenght);
                //Debug.Log("y: " + y);   
                break;
        }
    }

    public void ShowInfo(int index)
    {
        imageToShow.enabled = true;
        imageToShow.sprite = sprites[index];
        text.text = textToFill[index];
    }
}
