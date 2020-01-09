using System.Collections;
using Interactable.Base;
using Interactable.Creatures;

namespace Interface
{
	public interface IDestroyable
	{
		IEnumerator Destroy(Responsible responsible);

	}
}
