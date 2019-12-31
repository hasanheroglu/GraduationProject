using System.Collections;
using Interactable.Base;
using Interactable.Creatures;

namespace Interface
{
	public interface IWalkable
	{
		IEnumerator Walk(Responsible responsible);
	}
}
