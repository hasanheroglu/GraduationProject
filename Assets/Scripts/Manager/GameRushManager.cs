using System;
using System.Collections;
using System.Collections.Generic;
using Factory;
using Interactable.Base;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameRushManager : MonoBehaviour
{
    [SerializeField] private float timer;
    [SerializeField] private float timeLimit;
    [SerializeField] private List<GameObject> characters;
    [SerializeField] private static int _level;
    
    [Header("UI Elements")]
    [SerializeField] private GameObject timerText;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject successMenu;

    // Start is called before the first frame update
    void Start()
    {
        timer = timeLimit;
        CreateLevel();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        DisplayTimer();

        if (timer <= 0 || CheckAnyDeadCharacter())
        {
            timer = 0;
            DisplayGameOver();
            return;
        }
        
        if (CheckAllQuestsDone())
        {
            DisplaySuccess();
        }
    }
    
    private bool CheckAllQuestsDone()
    {
        foreach (var character in characters)
        {
            var responsible = character.GetComponent<Responsible>();

            if (responsible.quests == null) return false;
            
            foreach (var quest in responsible.quests)
            {
                if (!quest.completed)
                    return false;
            }
        }

        return true;
    }

    private bool CheckAnyDeadCharacter()
    {
        foreach (var character in characters)
        {
            if (character == null) return true;
            
            var responsible = character.GetComponent<Responsible>();
            
            if (responsible.isDead) return true;
        }

        return false;
    }

    private void CreateLevel()
    {
        characters = LevelFactory.GetLevel(_level);
    }

    private void DisplayTimer()
    {
        var minutes = (int) timer / 60;
        var seconds = (int) timer - minutes * 60;

        timerText.GetComponent<Text>().text = String.Format("{0:D2}:{1:D2}", minutes, seconds);
    }

    private void DisplaySuccess()
    {
        Time.timeScale = 0;
        successMenu.SetActive(true);
    }

    private void DisplayGameOver()
    {
        Time.timeScale = 0;
        gameOverMenu.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("Test01");
    }

    public void NextLevel()
    {
        _level++;
        SceneManager.LoadScene("Test01");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
