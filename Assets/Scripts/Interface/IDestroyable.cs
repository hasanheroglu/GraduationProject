using System.Collections;
using Interactable.Creatures;

namespace Interface
{
	public interface IDestroyable
	{
		IEnumerator Destroy(Human human);

	}
}
