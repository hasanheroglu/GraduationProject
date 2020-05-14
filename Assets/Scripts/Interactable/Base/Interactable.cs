using System;
using System.Collections.Generic;
using System.Reflection;
using Attribute;
using UnityEngine;

namespace Interactable.Base
{
	public abstract class Interactable : MonoBehaviour
	{

		public int health = 100;
		public GameObject interactionPoint;

		protected int instanceNo;
		
		[SerializeField] private string groupName;
		private int _inUse;
		private HashSet<MethodInfo> _methods;
		
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
		
		public MethodInfo FindAllowedAction(Responsible responsible, ActivityType activityType)
		{
			MethodInfo methodInfo = null;
			
			if (_methods.Count == 0) return null;
			
			foreach (var method in _methods)
			{
				ActivityAttribute activityAttribute =
					System.Attribute.GetCustomAttribute(method, typeof(ActivityAttribute)) as ActivityAttribute;
				
				if (activityAttribute == null || activityAttribute.ActivityType != activityType || (InUse <= 0)) continue;
				
				InteractableAttribute[] interactableAttributes =
					System.Attribute.GetCustomAttributes(method, typeof (InteractableAttribute)) as InteractableAttribute [];

				bool typeExist = false;

				if (interactableAttributes != null && interactableAttributes.Length <= 0) continue;
				
				foreach (var attribute in interactableAttributes)
				{
					if (attribute.InteractableType == responsible.GetType() || attribute.InteractableType == responsible.GetType().BaseType)
					{
						typeExist = true;
						methodInfo = method;
						break;
					}
				}
				
				if(typeExist) break;
			}

			return methodInfo;
		}

		public string GetGroupName()
		{
			return groupName;
		}

		public void SetGroupName(string groupName)
		{
			this.groupName = groupName;
		}
		
		public string GetInstanceName()
		{
			return groupName + "_" + instanceNo;
		}
	}
}
