using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ImageEvents : MonoBehaviour
{
    [SerializeField] private AudioManager am;

    public CrossWord crossWord;
    public Camera cam;

    public Vector2 positionInArray;

    //bool drawingLine;

    //LayerMask mask;

    private void Start()
    {

        am = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        //mask = LayerMask.GetMask("Default");

        var trigger = gameObject.AddComponent<EventTrigger>();
        //Drag event to constantly draw line at mouse position
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((data) => { DrawLineFromPosition((PointerEventData)data); });
        trigger.triggers.Add(entry);

        //End of drag event to check word
        /*entry.eventID = EventTriggerType.EndDrag;
        entry.callback.AddListener((data) => { EndLineAndCheckWord((PointerEventData)data); });
        trigger.triggers.Add(entry);*/

        //pointer over object
        /*entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((data) => { PointerOver((PointerEventData)data); });
        trigger.triggers.Add(entry);*/

        //pointer out of object;
        /*entry.eventID = EventTriggerType.PointerExit;
        entry.callback.AddListener((data) => { PointerExit((PointerEventData)data); });
        trigger.triggers.Add(entry);*/
    }
    public string ReverseString(string str)
    {
        char[] array = str.ToCharArray();
        Array.Reverse(array);
        return new string(array);
    }

    public void DrawLineFromPosition(PointerEventData data)
    {
        Debug.Log(transform.name + " " + transform.position.ToString());
        if(crossWord.doneOnce)
        {
            crossWord.lineRenderer.startWidth = 0.1f;
            crossWord.lineRenderer.endWidth = 0.1f;

            crossWord.lineRenderer.SetPosition(1, GetComponent<Transform>().position);
            crossWord.doneOnce = false;

            crossWord.crossEnd = positionInArray;

            if(crossWord.crossStart.x == crossWord.crossEnd.x)
            {
                crossWord.foundWord = string.Empty;
                crossWord.RfoundWord = string.Empty;
                for (int i = (int)Mathf.Min(crossWord.crossStart.y, crossWord.crossEnd.y); i <= (int)Mathf.Max(crossWord.crossStart.y, crossWord.crossEnd.y); i++)
                {
                    crossWord.foundWord += crossWord.letters[(int)crossWord.crossStart.x, i];    
                }
                crossWord.RfoundWord = ReverseString(crossWord.foundWord);

                for (int i = 0; i < crossWord.wordsToFind.Count; i++)
                {
                    if(crossWord.foundWord.Equals(crossWord.wordsToFind[i]) || crossWord.RfoundWord.Equals(crossWord.wordsToFind[i]))
                    {
                        crossWord.ShowInfo(i);
                        crossWord.wordFound = true;
                        break;
                    }
                }
            }

            if (crossWord.crossStart.y == crossWord.crossEnd.y)
            {
                crossWord.foundWord = string.Empty;
                crossWord.RfoundWord = string.Empty;
                for (int i = (int)Mathf.Min(crossWord.crossStart.x, crossWord.crossEnd.x); i <= (int)Mathf.Max(crossWord.crossStart.x, crossWord.crossEnd.x); i++)
                {
                    crossWord.foundWord +=crossWord.letters[i, (int)crossWord.crossStart.y];
                }
                crossWord.RfoundWord = ReverseString(crossWord.foundWord);

                for (int i = 0; i < crossWord.wordsToFind.Count; i++)
                {
                    if (crossWord.foundWord.Equals(crossWord.wordsToFind[i]) || crossWord.RfoundWord.Equals(crossWord.wordsToFind[i]))
                    {
                        crossWord.ShowInfo(i);
                        crossWord.wordFound = true;
                        break;
                    }
                }
            }

            if (Mathf.Abs(crossWord.crossStart.y - crossWord.crossEnd.y) == Mathf.Abs(crossWord.crossStart.x - crossWord.crossEnd.x))
            {
                crossWord.foundWord = string.Empty;
                crossWord.RfoundWord = string.Empty;
                int index = 0;
                for (int i = (int)Mathf.Min(crossWord.crossStart.x, crossWord.crossEnd.x); i <= (int)Mathf.Max(crossWord.crossStart.x, crossWord.crossEnd.x); i++)
                {
                    crossWord.foundWord +=crossWord.letters[i, (int)Mathf.Min(crossWord.crossStart.y, crossWord.crossEnd.y) + index];
                    index++;
                }
                crossWord.RfoundWord = ReverseString(crossWord.foundWord);

                for (int i = 0; i < crossWord.wordsToFind.Count; i++)
                {
                    if (crossWord.foundWord.Equals(crossWord.wordsToFind[i]) || crossWord.RfoundWord.Equals(crossWord.wordsToFind[i]))
                    {
                        crossWord.ShowInfo(i);
                        crossWord.wordFound = true;
                        break;
                    }
                }
            }

            if (!crossWord.wordFound)
            {
                Debug.Log("Words not found");
                Debug.Log(crossWord.foundWord);
                Debug.Log(crossWord.RfoundWord);
                crossWord.lineIndex--;
                crossWord.lines[crossWord.lineIndex].GetComponent<LineRenderer>().enabled = false;
                Destroy(crossWord.lines[crossWord.lineIndex]);
            }
            else
            {
                am.Play("Correct");

                crossWord.wordFound = false;
                if(crossWord.lineIndex == crossWord.wordsToFind.Count)
                {
                    crossWord.finishedGame = true;
                }
            }
        }
        else
        {
            Debug.Log("DrawingLine");
            //crossWord.drawLine = true;
            crossWord.lines[crossWord.lineIndex] = new GameObject("line" + crossWord.lineIndex);
            crossWord.lineRenderer = crossWord.lines[crossWord.lineIndex].AddComponent<LineRenderer>();
            crossWord.lineRenderer.sharedMaterial = new Material(Shader.Find("Particles/Standard Unlit"));

            crossWord.lineRenderer.alignment = LineAlignment.TransformZ;
            crossWord.lineRenderer.startWidth = 1f;
            crossWord.lineRenderer.endWidth = 1f;

            crossWord.lineRenderer.startColor = Color.red;
            crossWord.lineRenderer.endColor = Color.red;

            crossWord.lineRenderer.positionCount = 2;
            //crossWord.lineRenderer.SetPosition(0, new Vector3());
            crossWord.lineIndex++;
            crossWord.lineRenderer.SetPosition(0, new Vector3(transform.position.x, transform.position.y, -1));
            crossWord.lineRenderer.SetPosition(1, GetComponent<Transform>().position);

            crossWord.crossStart = positionInArray;
            crossWord.doneOnce = true;
        }

        
    }

        //Going left/right poz 1 left x < poz 0 x < poz 1 right x && poz 0 y == poz 1 y;
        //going up/down poz 1 up y < poz 0 y < poz 1 down y && poz 0 x == poz 1 x;
        //going sideways poz 1 x - poz 0 x == poz 1 y - poz 0 y;
        //if(crossWord.lineRenderer.GetPosition(0).x < )
        //}

    

}
