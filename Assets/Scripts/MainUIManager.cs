using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUIManager : MonoBehaviour
{
    public GameObject applicationButtons;
    public GameObject applicationContent;
    public GameObject applicationWindow;

    public GameObject[] contents = new GameObject[5];
    public GameObject creditsWindow;

    public void StartButtonBehavior()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }

    public void CloseButtonBehavior()
    {
        applicationButtons.SetActive(false);
        applicationContent.SetActive(false);
        applicationWindow.SetActive(false);
    }

    public void BackButtonContentBehavior()
    {
        applicationButtons.SetActive(true);
        applicationContent.SetActive(false);
    }

    public void ApplicationButtonBehavior()
    {
        applicationWindow.SetActive(true);
        applicationButtons.SetActive(true);
        applicationContent.SetActive(false);
    }

    public void ContentManager(int index)
    {
        applicationButtons.SetActive(false);
        applicationContent.SetActive(true);
        for (var i = 0; i < contents.Length; i++)
        {
            contents[i].SetActive(false);
            if (index == i)
                contents[index].SetActive(true);
        }
    }

    public void ApplicationQuit()
    {
        Application.Quit();
    }

    public void OpenCreditsScreen()
    {
        creditsWindow.SetActive(true);
    }

    public void CloseCreditsScreen()
    {
        creditsWindow.SetActive(false);
    }
}