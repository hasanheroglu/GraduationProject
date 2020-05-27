using System.Collections;
using System.Collections.Generic;
using Factory;
using Interactable.Base;
using UnityEngine;

public class GameRushManager : MonoBehaviour
{
    [SerializeField] private float timer;
    [SerializeField] private float timeLimit;
    [SerializeField] private List<GameObject> characters;
    [SerializeField] private int level;

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

        if (timer <= 0 || CheckAnyDeadCharacter())
        {
            timer = 0;
            Debug.Log("Game Over");
            return;
        }
        
        if (CheckAllQuestsDone())
        {
            Debug.Log("Completed all quests!");
        }
    }
    
      


    private bool CheckAllQuestsDone()
    {
        foreach (var character in characters)
        {
            var responsible = character.GetComponent<Responsible>();
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
        characters = LevelFactory.GetLevel(level);
    }
}
