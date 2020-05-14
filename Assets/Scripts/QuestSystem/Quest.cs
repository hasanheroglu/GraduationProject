using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;

public class Quest
{
    public ActivityType ActivityType { get; private set; }
    public string GroupName { get; private set; }
    public bool Completed { get; set; }
    public int DoneCount { get; set; }
    public int RepetitionCount { get; set; }
    public string Description { get; set; }



    public Quest(ActivityType activityType, string groupName, int repetitionCount, string description)
    {
        Completed = false;
        ActivityType = activityType;
        GroupName = groupName;
        DoneCount = 0;
        RepetitionCount = repetitionCount;
        Description = description;
    }

    public void Progress(Job job)
    {
        if (Completed) return;
        if (ActivityType != job.ActivityType) return;
        if (GroupName != job.Target.GetGroupName()) return;
        if (++DoneCount == RepetitionCount) Completed = true;
    }

    public void Progress(ActivityType activityType, string groupName)
    {
        if (Completed) return;
        if (ActivityType != activityType) return;
        if (GroupName != groupName) return;
        if (++DoneCount == RepetitionCount) Completed = true;
    }
}
