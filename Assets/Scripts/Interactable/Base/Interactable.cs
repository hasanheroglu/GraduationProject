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
		
		public int InUse { get; set; }
		public HashSet<MethodInfo> Methods { get; set; }
		
		private void Awake()
		{
			SetMethods();
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
			
			if (Methods == null || Methods.Count == 0) return null;
			
			foreach (var method in Methods)
			{
				if (IsActivityAllowed(method, activityType) && CanInteractWith(method, responsible))
				{
					methodInfo = method;
				}
			}

			return methodInfo;
		}

		private bool IsActivityAllowed(MethodInfo method, ActivityType activityType)
		{
			ActivityAttribute activityAttribute =
				System.Attribute.GetCustomAttribute(method, typeof(ActivityAttribute)) as ActivityAttribute;
				
			return activityAttribute != null && activityAttribute.ActivityType == activityType && InUse > 0;
		}

		private bool CanInteractWith(MethodInfo method, Responsible responsible)
		{
			InteractableAttribute[] interactableAttributes =
				System.Attribute.GetCustomAttributes(method, typeof (InteractableAttribute)) as InteractableAttribute [];
			
			if (interactableAttributes != null && interactableAttributes.Length <= 0) return false;
				
			foreach (var attribute in interactableAttributes)
			{
				if (attribute.InteractableType == responsible.GetType() || attribute.InteractableType == responsible.GetType().BaseType)
				{
					return true;
				}
			}

			return false;
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
