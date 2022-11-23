using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] Button closeText;
    
    public void NPCText(string textToWrite)
    {
        text.text = textToWrite;
        text.enabled = true;
        closeText.enabled = true;
    }

    public void CloseText()
    {
        closeText.enabled = false;
        text.enabled = false;
    }
}
