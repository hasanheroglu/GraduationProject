using System.Collections;
using System.Collections.Generic;
using Interactable.Base;
using Manager;
using UnityEditor;
using UnityEngine;

public static class QuestManager 
{
    public static List<Quest> FindWithActivityType(Responsible responsible, ActivityType activityType)
    {
        List<Quest> quests = new List<Quest>();

        foreach (var quest in responsible.Quests)
        {
            if (quest.ActivityType == activityType) quests.Add(quest);
        }
        
        return quests;
    }

    public static List<Quest> FindWithGroupName(Responsible responsible, string groupName)
    {
        List<Quest> quests = new List<Quest>();
        
        foreach (var quest in responsible.Quests)
        {
            if (quest.GroupName == groupName) quests.Add(quest);
        }
        
        return quests;
    }

    public static void Progress(Responsible responsible, Job job)
    {
        foreach (var quest in responsible.Quests)
        {
            quest.Progress(job);
            //UIManager.Instance.SetQuests(responsible);
        }
    }
    
}
