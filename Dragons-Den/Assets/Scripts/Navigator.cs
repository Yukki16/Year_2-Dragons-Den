using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Navigator : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void LoadChariotGame()
    {
        SceneManager.LoadScene("ChariotRace", LoadSceneMode.Single);
    }
}
