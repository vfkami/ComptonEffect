using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUIManager : MonoBehaviour
{
    public GameObject ApplicationWindow;
    public GameObject ApplicationButtons;
    public GameObject ApplicationContent;
    public GameObject CreditsContent;
    
    public GameObject[] Contents = new GameObject[5];
    
    
    public void StartButtonBehavior()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene", LoadSceneMode.Additive);
    }

    public void CloseButtonBehavior()
    {
        ApplicationButtons.SetActive(false);
        ApplicationContent.SetActive(false);
        ApplicationWindow.SetActive(false);
    }

    public void BackButtonContentBehavior()
    {
        ApplicationButtons.SetActive(true);
        ApplicationContent.SetActive(false);
    }

    public void ApplicationButtonBehavior()
    {
        ApplicationWindow.SetActive(true);
        ApplicationButtons.SetActive(true);
        ApplicationContent.SetActive(false);
    }

    public void ContentManager(int index)
    {
        ApplicationButtons.SetActive(false);
        ApplicationContent.SetActive(true);
        for (int i = 0; i < Contents.Length; i++)
        {
            Contents[i].SetActive(false);
            if (index == i)
                Contents[index].SetActive(true);
        }
    }

    public void ApplicationQuit()
    {
        Application.Quit();
    }

    public void OpenCreditsScreen()
    {
        CreditsContent.SetActive(true);
    }

    public void CloseCreditsContent()
    {
        CreditsContent.SetActive(false);
    }

}
