using System.Collections;
using Interactable.Base;
using Interactable.Creatures;

namespace Interface
{
    public interface IAttackable
    {
        IEnumerator Attack(Responsible responsible);
    }
}
