using System.Collections;
using Interactable.Creatures;

namespace Interface
{
	public interface ISocializable
	{
		IEnumerator Talk(Human human);
	}
}
