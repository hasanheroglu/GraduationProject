using System.Collections;
using Interactable.Base;
using Interactable.Creatures;

namespace Interface
{
    public interface IHarvestable
    {
        IEnumerator Harvest(Responsible responsible);
    }
}
