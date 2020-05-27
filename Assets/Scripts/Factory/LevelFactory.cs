using System.Collections;
using System.Collections.Generic;
using Factory;
using Interactable.Base;
using UnityEngine;

public static class LevelFactory
{
    public static List<GameObject> GetLevel(int level)
    {
        switch (level)
        {
            case 0:
                return GetLevel0();
            case 1:
                return GetLevel1();
            case 2:
                return GetLevel2();
            default:
                return null;
        }
    }
    
    private static GameObject CreateCharacter(Vector3 position)
    {
        var character = HumanFactory.GetHuman(position);
        character.GetComponent<Responsible>().isPlayer = true;
        return character;
    }      
    
    public static List<GameObject> GetLevel0()
    {
        var characters = new List<GameObject>();
        
        var char1 = CreateCharacter(Vector3.up);
        characters.Add(char1);
        
        char1.GetComponent<Responsible>().quests.Add(new Quest(ActivityType.Chop, "tree", 3, "Chop 3 trees!"));
        
        return characters;
    }
    
    public static List<GameObject> GetLevel1()
    {
        var characters = new List<GameObject>();
        
        var char1 = CreateCharacter(Vector3.up);
        characters.Add(char1);
        
        char1.GetComponent<Responsible>().quests.Add(new Quest(ActivityType.Plant, "seed", 3, "Plant 3 seeds!"));
        
        return characters;
    }
    
    public static List<GameObject> GetLevel2()
    {
        var characters = new List<GameObject>();
        
        var char1 = CreateCharacter(Vector3.up);
        characters.Add(char1);
        
        char1.GetComponent<Responsible>().quests.Add(new Quest(ActivityType.Chop, "tree", 3, "Chop 3 trees!"));
        
        var char2 = CreateCharacter(Vector3.up + 2 * Vector3.right);
        characters.Add(char2);
        
        char2.GetComponent<Responsible>().quests.Add(new Quest(ActivityType.Plant, "seed", 3, "Plant 3 seeds!"));
        
        return characters;
    }
}
