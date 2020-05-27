using Interactable.Base;
using Interactable.Creatures;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Factory
{
    public static class HumanFactory
    {
        private static GameObject humanPrefab = Resources.Load<GameObject>("Prefabs/Interactables/Creatures/Human");

        public static GameObject GetHuman(Vector3 position)
        {
            GameObject human = Object.Instantiate(humanPrefab, position, Quaternion.identity);
            return human;
            /*
            human.GetComponent<Human>().CharacterName = humanName.GetComponent<Text>().text;
            human.GetComponent<Human>().IsPlayer = true;
            human.GetComponent<Human>().Quests.Add(new Quest(ActivityType.Chop, "tree", 3, "Chop 3 trees!"));
            human.GetComponent<Human>().Quests.Add(new Quest(ActivityType.Kill, "zombie", 3, "Kill 3 zombies!"));
            human.GetComponent<Human>().Quests.Add(new Quest(ActivityType.Harvest, "plant", 3, "Harvest 3 flowers!"));
            human.GetComponent<Human>().Quests.Add(new Quest(ActivityType.Plant, "seed", 3, "Plant 3 seeds!"));
            SetNeeds(human.GetComponent<Human>());
            */
        }

        private static void SetNeeds(Responsible human)
        {
            human.Needs.Add(NeedType.Hunger, NeedFactory.GetHunger());
            human.Needs.Add(NeedType.Fun, NeedFactory.GetFun());
            human.Needs.Add(NeedType.Energy, NeedFactory.GetEnergy());
            human.Needs.Add(NeedType.Social, NeedFactory.GetSocial());
            human.Needs.Add(NeedType.Bladder, NeedFactory.GetBladder());
            human.Needs.Add(NeedType.Hygiene, NeedFactory.GetHygiene());
        }
    }
}
