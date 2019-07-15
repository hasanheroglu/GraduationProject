using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Manager;
using UnityEngine;
using UnityEngine.UI;


public class InteractionUtil : MonoBehaviour
{

	private static GameObject interactionPanel;
	
	public static void ShowInteractions(GameObject target, object[] parameters, Vector3 panelPosition)
	{
		GetInteractionMenu(target, parameters, panelPosition);
	}
	
	private static void GetInteractionMenu(GameObject target, object[] parameters, Vector3 panelPosition)
	{
		List<MethodInfo> methods = new List<MethodInfo>();
		var script = target.GetComponent<Interactable>();
		methods = GetTargetMethods(target);

		interactionPanel = UIManager.Instance.CreateInteractionPanel(panelPosition);
        		
		foreach (var method in methods)
		{
			ButtonInfo buttonInfo = new ButtonInfo(target, method, script, parameters);
			UIManager.Instance.AddInteractionButton(interactionPanel, buttonInfo);
		}
	}

	private static List<MethodInfo> GetTargetMethods(GameObject target)
	{
		List<MethodInfo> methods = new List<MethodInfo>();
		var script = target.GetComponent<Interactable>();
		var flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly;
		
		var type = script.GetType();
		methods.AddRange(type.GetMethods(flags));
		
		return methods;
	}

	public static void CloseInteractionMenu()
	{
		if (interactionPanel != null)
		{
			Destroy(interactionPanel);
		}
	}

	public static IEnumerator CreateCoroutine(ButtonInfo buttonInfo)
	{
		IEnumerator coroutine = null;
		try
		{
			coroutine = (IEnumerator) buttonInfo.Method.Invoke(buttonInfo.Target.GetComponent<Interactable>(),
				buttonInfo.Parameters);
		}
		catch (ArgumentException e)
		{
			Debug.Log("Argument Exception");
		}
		catch (TargetParameterCountException e)
		{
			coroutine = (IEnumerator) buttonInfo.Method.Invoke(buttonInfo.Target.GetComponent<Interactable>(), null);
		}

		return coroutine;
	}
}
