using System.Collections;
using Interactable.Creatures;

namespace Interface
{
	public interface IChoppable
	{
		IEnumerator Chop(Human human);
	}
}
