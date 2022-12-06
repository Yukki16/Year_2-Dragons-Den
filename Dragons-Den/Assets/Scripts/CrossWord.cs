using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CrossWord : MonoBehaviour
{
    [SerializeField] Camera cam;

    char[,] letters;
    [SerializeField] List<string> wordsToFind;
    [SerializeField] GUIStyle style;
    Rect[] boxes;

    GameObject[] lines;
    LineRenderer lineRenderer;
    bool drawLine;
    bool inRect;

    bool finishedBuilding;
    int gameLenght;
    int boxSizeX;
    int boxSizeY;

    Vector2 crossStart;
    Vector2 crossEnd;

    int boxIndex = 0;
    int lineIndex = 0;

    int LBSindex = 0;
    int LBEindex = 0;

    private void Start()
    {
        //background = new Texture2D(imageBackground);
        boxSizeX = (int)(2.5f * Screen.width) / 100;
        boxSizeY = (int)(4.5f * Screen.height) / 100;

        lines = new GameObject[wordsToFind.Count];
        
        Debug.Log("box size X: " + boxSizeX);
        Debug.Log("box size Y: " + boxSizeY);
        CreateCross(16);
        //CreateCross(LongestWordLenght() * 2);
    }

    private void CreateCross(int _gameLenght)
    {
        gameLenght = _gameLenght;
        crossStart = new Vector2(transform.position.x + 300, transform.position.y + 100);
        crossEnd = new Vector2(transform.position.x + 300 + gameLenght * boxSizeX, transform.position.y + 100 + gameLenght * boxSizeY);
        boxes = new Rect[gameLenght * gameLenght];

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
                if (counter > 10)
                {
                    throw new System.Exception("No space for the word!");
                }
            }
            AddWordToPuzzle(direction, x, y, i);
        }
        finishedBuilding = true;
    }

    private void OnGUI()
    {
        if (!finishedBuilding)
        {
            return;
        }

        for (int i = 0; i < gameLenght; i++)
        {
            for (int j = 0; j < gameLenght; j++)
            {
                if (boxIndex < gameLenght * gameLenght)
                {
                    var rect = new Rect(transform.position.x + 300 + boxSizeX * i, transform.position.y + 100 + boxSizeY * j, boxSizeX, boxSizeY);
                    boxes[boxIndex] = rect;
                    //Debug.Log(boxIndex);
                    boxIndex++;
                }

                GUI.Box(boxes[i * gameLenght + j], letters[i, j].ToString(), style);
            }
        }

        if(Enumerable.Range((int)crossStart.x, (int)crossEnd.x).Contains((int)Input.mousePosition.x) 
            && Enumerable.Range((int)crossStart.y, (int)crossEnd.y).Contains((int)Input.mousePosition.y))
        {
            if(Input.GetMouseButtonDown(0) && !drawLine)
            {
                drawLine = true;
                for (int i = 0; i < boxes.Length; i++)
                {
                    if (boxes[i].Contains(Input.mousePosition))
                    {
                        LBSindex = i;
                        lines[lineIndex] = new GameObject();
                        lineRenderer = lines[lineIndex].AddComponent<LineRenderer>();
                        lineRenderer.positionCount = 2;
                        lineRenderer.SetPosition(0, cam.ScreenToWorldPoint(boxes[i].center));
                        lineIndex++;
                        break;
                    }
                }
            }
        }


    }

    private void Update()
    {
        while(drawLine && lineRenderer != null && Input.GetMouseButtonUp(0))
        { 
            lineRenderer.SetPosition(1, Input.mousePosition);
            /*if(Input.GetMouseButtonUp(0))
            {
                for (int i = 0; i < boxes.Length; i++)
                {
                    if (boxes[i].Contains(Input.mousePosition))
                    {
                        inRect = true;
                        lineRenderer.SetPosition(1, cam.ScreenToWorldPoint(boxes[i].center));
                        LBEindex = i;
                    }
                }

                if(!inRect)
                {
                    lineIndex--;
                    Destroy(lines[lineIndex]);
                }
                else
                {

                }
                drawLine = false;
            }*/
        }
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

    private int LongestWordLenght()
    {
        int wLenght = 0;

        for (int i = 0; i < wordsToFind.Count; i++)
        {
            if (wordsToFind[i].Length > wLenght)
            {
                wLenght = wordsToFind[i].Length;
            }
        }
        return wLenght;
    }
}
