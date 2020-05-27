using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;

public class Quest
{
    public ActivityType activityType;
    public string groupName;
    public bool completed;
    public int doneCount;
    public int repetitionCount;
    public string description; 



    public Quest(ActivityType activityType, string groupName, int repetitionCount, string description)
    {
        completed = false;
        this.activityType = activityType;
        this.groupName = groupName;
        doneCount = 0;
        this.repetitionCount = repetitionCount;
        this.description = description;
    }

    public void Progress(Job job)
    {
        if (completed) return;
        if (activityType != job.ActivityType) return;
        if (groupName != job.Target.GetGroupName()) return;
        if (++doneCount == repetitionCount) completed = true;
    }

    public void Progress(ActivityType activityType, string groupName)
    {
        if (completed) return;
        if (this.activityType != activityType) return;
        if (this.groupName != groupName) return;
        if (++doneCount == repetitionCount) completed = true;
    }
}
