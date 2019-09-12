using System.Collections.Generic;
using UnityEngine;

namespace Interactable.Base
{
	public abstract class Interactable : MonoBehaviour {
		
		private string name;
		
		public string Name
		{
			get { return name; }
			set { name = value; }
		}
	}
}
