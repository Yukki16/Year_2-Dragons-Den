using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{

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
    [SerializeField] Vector2 playerEndPosition;

    private float moveInterval;

    [SerializeField] GameObject[] opponentChariots;

    public Question[] Questions;
    private Question temp;

    private int correctAnswer;

    private int playerScore;

    int index;
    
    private void Awake()
    {
        QuestionBoxText = QuestionBox.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        AnswerBox1Text = AnswerBox1.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        AnswerBox2Text = AnswerBox2.GetComponentInChildren<TMPro.TextMeshProUGUI>();
    }
    void Start()
    {
        if (playerStartPosition != Vector2.zero)
        {
            playerChariot.transform.position = playerStartPosition;
        }
        else
        {
            playerStartPosition = playerChariot.transform.position;
        }

        Debug.Log(Questions.Length);

        moveInterval = (playerEndPosition.x - playerStartPosition.x) / Questions.Length;

        ShuffleQuestions(Questions);
        NextQuestion();
    }

    void NextQuestion()
    {
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
        index++;
    }

    //Activated through button 1
    public void ActivateButton1()
    {
        if (correctAnswer == 1)
        {           
            playerScore++;
            StartCoroutine(MovePlayerChariot());
            Debug.Log("Correct");
        }
        else
        {
            Debug.Log("Incorrect");
        }

        NextQuestion();
    }

    //Activated through button 2    
    public void ActivateButton2()
    {
        if (correctAnswer == 2)
        {
            playerScore++;
            StartCoroutine(MovePlayerChariot());
            Debug.Log("Correct");
        }
        else
        {
            Debug.Log("Incorrect");
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

    public int GetPlayerScore()
    {
        return playerScore;
    }
    
    public float GetPlayerAverage()
    {
        return (playerScore / index) * 100;
    }

    IEnumerator MovePlayerChariot()
    {
        playerChariot.transform.position += new Vector3(moveInterval, 0, 0);

        yield return new WaitForFixedUpdate();
    }

}
