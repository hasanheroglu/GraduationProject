using System.Collections;
using Interactable.Base;
using Interactable.Creatures;

namespace Interface
{
	public interface IChoppable
	{
		IEnumerator Chop(Responsible responsible);
	}
}
