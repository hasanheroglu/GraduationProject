using System.Collections;
using Interactable.Creatures;

namespace Interface
{
    public interface ISleepable
    {
        IEnumerator Sleep(Human human);
    }
}
