using System.Collections;
using Interactable.Creatures;

namespace Interface
{
    public interface ICarriable{
        IEnumerator Carry(Human human);
        IEnumerator Hold(Human human);
    }
}
