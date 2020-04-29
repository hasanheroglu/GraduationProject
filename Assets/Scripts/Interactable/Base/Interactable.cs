using System;
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
		private HashSet<MethodInfo> _methods;
		
		public GameObject interactionPoint;

		private void Awake()
		{
			SetMethods();
		}

		public int InUse
		{
			get { return _inUse; }
			set { _inUse = value; }
		}

		public HashSet<MethodInfo> Methods
		{
			get { return _methods; }
			set { _methods = value; }
		}

		protected void SetMethods()
		{
			var flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy;
			Methods = new HashSet<MethodInfo>();
			var methods = GetType().GetMethods(flags);
			foreach (var method in methods)
			{
				InteractableAttribute [] interactableAttributes =
					System.Attribute.GetCustomAttributes(method, typeof(InteractableAttribute)) as InteractableAttribute[];
				
				if(interactableAttributes.Length == 0) continue;
				
				Methods.Add(method);
			}
		}
	}
}
