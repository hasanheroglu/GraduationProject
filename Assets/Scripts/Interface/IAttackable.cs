using System.Collections;
using Interactable.Creatures;

namespace Interface
{
    public interface IAttackable
    {
        IEnumerator Attack(Human human);
    }
}
