using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextManager : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] Button closeText;
    
    public void NPCText(string textToWrite)
    {
        text.text = textToWrite;
        text.gameObject.SetActive(true);
        closeText.gameObject.SetActive(true);
    }

    public void CloseText()
    {
        closeText.gameObject.SetActive(false);
        text.gameObject.SetActive(false);
    }
}
