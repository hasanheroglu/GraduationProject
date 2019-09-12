using System.Collections;
using Interactable.Creatures;

namespace Interface
{
	public interface IWalkable
	{
		IEnumerator Walk(Human human);
	}
}
