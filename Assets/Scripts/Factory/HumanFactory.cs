using Interactable.Base;
using Interactable.Creatures;
using UnityEngine;
using UnityEngine.UI;

namespace Factory
{
    public class HumanFactory : MonoBehaviour
    {
        public GameObject humanName;
        public GameObject humanPrefab;
    
        public void CreateHuman()
        {
            GameObject human = Instantiate(humanPrefab, new Vector3(0f, 5f, 0f), Quaternion.identity);
            human.GetComponent<Human>().Name = humanName.GetComponent<Text>().text;
            SetNeeds(human.GetComponent<Human>());
            SetActivities(human.GetComponent<Human>());
        }

        private void SetNeeds(Responsible human)
        {        
            human.Needs.Add(NeedType.Hunger, NeedFactory.GetHunger());
            human.Needs.Add(NeedType.Fun, NeedFactory.GetFun());
            human.Needs.Add(NeedType.Energy, NeedFactory.GetEnergy());
            human.Needs.Add(NeedType.Social, NeedFactory.GetSocial());
            human.Needs.Add(NeedType.Bladder, NeedFactory.GetBladder());
            human.Needs.Add(NeedType.Hygiene, NeedFactory.GetHygiene());
        }

        private void SetActivities(Responsible human)
        {
            human.Activities.Add(ActivityType.Eat);
            human.Activities.Add(ActivityType.Sleep);
            human.Activities.Add(ActivityType.Chop);
            human.Activities.Add(ActivityType.Harvest);
        }
    }
}
