using System.Collections;
using Crafting;
using Interactable.Base;
using Interactable.Creatures;

namespace Interface
{
	public interface ICraftable
	{
		IEnumerator Craft(Responsible responsible);
		void SetRecipe(Recipe recipe);
	}
}
