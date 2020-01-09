using System.Collections;
using Interactable.Base;
using Interactable.Creatures;

namespace Interface
{
	public interface ISocializable
	{
		IEnumerator Talk(Responsible responsible);
	}
}
