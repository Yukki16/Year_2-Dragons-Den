using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossWord : MonoBehaviour
{
    char[,] letters;
    [SerializeField] List<string> wordsToFind;
    //[SerializeField] Image imageBackground;
    //Texture2D background;

    bool finishedBuilding;
    int gameLenght;
    int boxSizeX;
    int boxSizeY;

    private void Start()
    {
        //background = new Texture2D(imageBackground);
        boxSizeX = (2 * Screen.width) / 100;
        boxSizeY = (4 * Screen.height) / 100;
        Debug.Log("box size X: " + boxSizeX);
        Debug.Log("box size Y: " + boxSizeY);
        CreateCross(20);
        //CreateCross(LongestWordLenght() * 2);
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
            
            while(!CheckWordSpace(direction, x, y, i))
            {
                DirectionOfWord(_gameLenght,ref x, ref y, ref direction, i);
                counter++;
                if(counter > 10)
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
        else
        {
            GUIStyle textStyle = new GUIStyle();
            textStyle.alignment = TextAnchor.MiddleCenter;
            textStyle.fontSize = 40;
            textStyle.normal.textColor = Color.white;
            textStyle.normal.background = GUI.skin.GetStyle("box").normal.background;
            
            GUI.color = Color.white;
            for (int i = 0; i < gameLenght; i++)
            {
                for (int j = 0; j < gameLenght; j++)
                {
                    GUI.Box(new Rect(transform.position.x + 200 + boxSizeX * i, transform.position.y + 100 + boxSizeY * j, boxSizeX, boxSizeY), letters[i, j].ToString(), textStyle);
                }
            }
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
