using System.Collections;
using System.Collections.Generic;
using Interactable.Base;
using Manager;
using UnityEngine;

public static class DamageUtil
{
    public static void ProgressKillQuests(Responsible responsible, GameObject damagable)
    {
        var killQuests = QuestManager.FindWithActivityType(responsible, ActivityType.Kill);
        foreach (var quest in killQuests)
        {
            quest.Progress(ActivityType.Kill, damagable.GetComponent<Interactable.Base.Interactable>().GetGroupName());
        }
        UIManager.Instance.SetQuests(responsible);
    }

    public static HashSet<GameObject> FindDamagables(Vector3 position, Vector3 halfExtents, GameObject weapon)
    {
        GameObject damagable = null;
        HashSet<GameObject> damagables = new HashSet<GameObject>();
        
        var colliders = Physics.OverlapBox(weapon.transform.position, weapon.transform.localScale / 2);
            
        foreach (var collider in colliders)
        {
            damagable = Util.GetDamagableFromCollider(collider);
                
            if (damagable != null && !ReferenceEquals(damagable, weapon.GetComponent<Weapon>().Responsible.gameObject))
            {
                damagables.Add(damagable);
            }
        }

        return damagables;
    }
}
