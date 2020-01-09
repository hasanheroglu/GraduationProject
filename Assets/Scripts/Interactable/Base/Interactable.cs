using System.Collections.Generic;
using System.Reflection;
using Attribute;
using UnityEngine;

namespace Interactable.Base
{
	public abstract class Interactable : MonoBehaviour
	{

		public string Name;
		public int health = 100;
		private int _inUse;
		private List<MethodInfo> _methods;
		
		public GameObject interactionPoint;

		public int InUse
		{
			get { return _inUse; }
			set { _inUse = value; }
		}

		public List<MethodInfo> Methods
		{
			get { return _methods; }
			set { _methods = value; }
		}

		protected void SetMethods()
		{
			var flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly;
			Methods = new List<MethodInfo>();
			Methods.AddRange(this.GetType().GetMethods(flags));	
		}
	}
}
