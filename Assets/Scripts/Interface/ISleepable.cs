using System.Collections;
using Attribute;
using Interactable.Creatures;

namespace Interface
{
    public interface ISleepable
    {
        IEnumerator Sleep(Human human);
    }
}
