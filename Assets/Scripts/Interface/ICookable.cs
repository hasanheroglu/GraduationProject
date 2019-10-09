using System.Collections;
using Interactable.Creatures;

namespace Interface
{
	public interface ICookable
	{
		IEnumerator Cook(Human human);
	}
}
