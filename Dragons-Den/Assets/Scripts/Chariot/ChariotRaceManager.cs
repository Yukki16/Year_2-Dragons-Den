using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ChariotRaceManager : MonoBehaviour
{

    [SerializeField] AudioManager AudioManager;

    [SerializeField] GameObject PlayerChariot;

    private SpriteRenderer sq;

    private Vector2 movementAddition;

    [SerializeField] GameObject QuestionBox;
    private TMPro.TextMeshProUGUI QuestionBoxText;

    [SerializeField] GameObject AnswerBox1;
    private TMPro.TextMeshProUGUI AnswerBox1Text;

    [SerializeField] GameObject AnswerBox2;
    private TMPro.TextMeshProUGUI AnswerBox2Text;

    [SerializeField] GameObject playerChariot;

    [SerializeField] Vector2 playerStartPosition;
    [SerializeField] Vector2 goldOppStartPosition;
    [SerializeField] Vector2 silverOppStartPosition;

    [SerializeField] Vector2 endPosition;

    private float playerMoveInterval;
    private float goldMoveInterval;
    private float silverMoveInterval;

    [SerializeField] GameObject[] opponentChariots;

    [SerializeField] float ChariotSnapSpeed = 1;

    public Question[] Questions;

    public static int questionLength;

    public static float GoldPassingPercentage = 90;

    public static float SilverPassingPercentage = 70;

    private GameObject goldChariot;
    private GameObject silverChariot;

    private Question temp;

    private int correctAnswer = 0;

    public static int playerScore = 0;

    static int index = 0;

    private Vector2 targetPosition;

    private void Awake()
    {
        index = 0;
        playerScore = 0;
        correctAnswer = 0;

        QuestionBoxText = QuestionBox.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        AnswerBox1Text = AnswerBox1.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        AnswerBox2Text = AnswerBox2.GetComponentInChildren<TMPro.TextMeshProUGUI>();
    }
    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeRight;

        AudioManager.LoopSound("RaceMusic");

        //Assign Gold and Silver chariots from array of opponent chariots
        for (int i = 0; i < opponentChariots.Length; i++)
        {
            if (i == 0)
            {
                goldChariot = opponentChariots[i];
            }

            if (i == 1)
            {
                silverChariot = opponentChariots[i];
            }
        }

        //PlayerCharPos
        if (playerStartPosition != Vector2.zero)
        {
            playerChariot.transform.position = playerStartPosition;
        }
        else
        {
            playerStartPosition = playerChariot.transform.position;
        }

        //GoldCharPos
        if (goldOppStartPosition != Vector2.zero)
        {
            goldChariot.transform.position = goldOppStartPosition;
        }

        else
        {
            goldOppStartPosition = goldChariot.transform.position;
        }

        //SilverCharPos
        if (silverOppStartPosition != Vector2.zero)
        {
            silverChariot.transform.position = silverOppStartPosition;
        }
        else
        {
            silverOppStartPosition = silverChariot.transform.position;
        }

        playerMoveInterval = (endPosition.x - playerStartPosition.x) / Questions.Length;
        goldMoveInterval = (endPosition.x - goldOppStartPosition.x) / ((Questions.Length) + Mathf.Abs(GoldPassingPercentage - 110) / 10);
        silverMoveInterval = (endPosition.x - silverOppStartPosition.x) / ((Questions.Length) + Mathf.Abs(SilverPassingPercentage - 110) / 10);

        questionLength = Questions.Length;

        ShuffleQuestions(Questions);
        NextQuestion();
    }

    private void Update()
    {
        

        if (Input.GetKeyUp(KeyCode.N))
        {
            playerScore++;
            StartCoroutine(MoveChariots());
            Debug.Log("Correct");
            NextQuestion();
        }

        if (Input.GetKeyUp(KeyCode.I))
        {
            StartCoroutine(MoveOpponentChariots());
            Debug.Log("InCorrect");
            NextQuestion();
        }
    }

    void NextQuestion()
    {

        AudioManager.StopSound((QuestionBoxText.text).ToString());

        if (index == Questions.Length)
        {
            StartCoroutine(FinishLineScene());
            return;
        }


        QuestionBoxText.text = Questions[index].DisplayQuesion;
        if (Random.Range(0, 2) == 1)
        {
            correctAnswer = 1;
            AnswerBox1Text.text = Questions[index].CorrectAnswer;
            AnswerBox2Text.text = Questions[index].WrongAnswers[Random.Range(0, Questions[index].WrongAnswers.Length)];
        }
        else
        {
            correctAnswer = 2;
            AnswerBox2Text.text = Questions[index].CorrectAnswer;
            AnswerBox1Text.text = Questions[index].WrongAnswers[Random.Range(0, Questions[index].WrongAnswers.Length)];
        }

        AudioManager.Play((QuestionBoxText.text).ToString());

        index++;
    }

    //Activated through button 1
    public void ActivateButton1()
    {
        if (index > Questions.Length)
            return;

        if (correctAnswer == 1)
        {
            StartCoroutine(CorrectAnswer());
        }
        else
        {
            StartCoroutine(WrongAnswer());
        }

        NextQuestion();
    }

    //Activated through button 2    
    public void ActivateButton2()
    {
        if (index > Questions.Length)
            return;

        if (correctAnswer == 2)
        {
            StartCoroutine(CorrectAnswer());
        }
        else
        {
            StartCoroutine(WrongAnswer());
        }

        NextQuestion();
    }

    private void ShuffleQuestions(Question[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int rand = Random.Range(0, array.Length);
            temp = array[rand];
            array[rand] = array[i];
            array[i] = temp;
        }
    }

    public static int GetPlayerScore()
    {
        return playerScore;
    }

    public static float GetPlayerAverage()
    {
        if (playerScore == 0 && index == 0)
        {
            return 1;
        }

        return (playerScore / index) * 100;
    }

    public static int GetQuestionCount()
    {
        return questionLength;
    }

    IEnumerator FinishLineScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("ChariotPlacement", LoadSceneMode.Single);
        yield break;
    }

    IEnumerator CorrectAnswer()
    {
        playerScore++;
        StartCoroutine(MoveChariots());
        AudioManager.Play("CorrectAnswer");
        yield break;
    }
    IEnumerator WrongAnswer()
    {
        StartCoroutine(MoveOpponentChariots());
        AudioManager.Play("WrongAnswer");
        yield break;
    }

    IEnumerator MoveChariots()
    {
        StartCoroutine(MoveChariot(playerChariot, playerChariot.transform.position, playerMoveInterval));
        StartCoroutine(MoveOpponentChariots());
        yield break;
    }

    IEnumerator MoveOpponentChariots()
    {
     
       StartCoroutine(MoveChariot(goldChariot, goldChariot.transform.position, goldMoveInterval));
       StartCoroutine(MoveChariot(silverChariot, silverChariot.transform.position, silverMoveInterval));
        yield break;
    }

    IEnumerator MoveChariot(GameObject c, Vector2 intl, float interval)
    {
        c.transform.position += new Vector3(0.001f * (ChariotSnapSpeed * 1000) * Time.deltaTime, 0, 0);

        if (c.transform.position.x < intl.x + interval)
        {
            yield return new WaitForEndOfFrame();
            StartCoroutine(MoveChariot(c, intl, interval));
        }
        yield break;
    }
}
