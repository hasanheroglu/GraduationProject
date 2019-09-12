using System.Collections;
using Interactable.Creatures;

namespace Interface
{
    public interface IHarvestable
    {
        IEnumerator Harvest(Human human);
    }
}
