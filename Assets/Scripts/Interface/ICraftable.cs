using System.Collections;
using Crafting;
using Interactable.Creatures;

namespace Interface
{
	public interface ICraftable
	{
		IEnumerator Craft(Human human);
		void SetRecipe(Recipe recipe);
	}
}
