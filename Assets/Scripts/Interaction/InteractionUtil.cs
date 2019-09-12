using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Manager;
using UnityEngine;

namespace Interaction
{
	public class InteractionUtil : MonoBehaviour
	{
		public static void ShowInteractions(GameObject responsible, GameObject target, object[] parameters, Vector3 panelPosition)
		{
			List<MethodInfo> methods = new List<MethodInfo>();
			methods = GetTargetMethods(target);
			UIManager.Instance.SetInteractionPanel(responsible, methods, target, parameters, panelPosition);
		}

		private static List<MethodInfo> GetTargetMethods(GameObject target)
		{
			List<MethodInfo> methods = new List<MethodInfo>();
			var script = target.GetComponent<Interactable.Base.Interactable>();
			var flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly;
		
			var type = script.GetType();
			methods.AddRange(type.GetMethods(flags));
		
			return methods;
		}

		public static IEnumerator CreateCoroutine(ButtonInfo buttonInfo)
		{
			IEnumerator coroutine = null;
			try
			{
				coroutine = (IEnumerator) buttonInfo.Method.Invoke(buttonInfo.Target.GetComponent<Interactable.Base.Interactable>(),
					buttonInfo.Parameters);
			}
			catch (ArgumentException e)
			{
				Debug.Log("Argument Exception");
			}
			catch (TargetParameterCountException e)
			{
				coroutine = (IEnumerator) buttonInfo.Method.Invoke(buttonInfo.Target.GetComponent<Interactable.Base.Interactable>(), null);
			}

			return coroutine;
		}
	}
}
