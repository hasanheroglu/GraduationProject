using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void PlaySurvive()
    {
        SceneManager.LoadScene("Test00");
    }

    public void PlayQuestRush()
    {
        SceneManager.LoadScene("QuestRush_00");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
