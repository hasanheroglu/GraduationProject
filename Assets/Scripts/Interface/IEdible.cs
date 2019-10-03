using System.Collections;
using Attribute;
using Interactable.Creatures;
using UnityEngine;

namespace Interface
{
	public interface IEdible
	{
		IEnumerator Eat(Human human);
	}
}
