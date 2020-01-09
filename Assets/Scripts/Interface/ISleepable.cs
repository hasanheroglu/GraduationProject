using System.Collections;
using Attribute;
using Interactable.Base;
using Interactable.Creatures;

namespace Interface
{
    public interface ISleepable
    {
        IEnumerator Sleep(Responsible responsible);
    }
}
