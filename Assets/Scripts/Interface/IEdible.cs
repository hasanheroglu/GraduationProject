using System.Collections;
using Attribute;
using Interactable.Base;
using Interactable.Creatures;
using UnityEngine;

namespace Interface
{
	public interface IEdible
	{
		IEnumerator Eat(Responsible responsible);
	}
}
