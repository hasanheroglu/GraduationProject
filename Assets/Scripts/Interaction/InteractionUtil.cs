using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;


public class InteractionUtil : MonoBehaviour
{

	private static GameObject interactionPanel;
	
	public static void ShowInteractions(GameObject target, object[] parameters)
	{
		GetInteractionMenu(target, parameters);
	}
	
	private static void GetInteractionMenu(GameObject target, object[] parameters)
	{
		List<MethodInfo> methods = new List<MethodInfo>();
		methods = GetTargetMethods(target);

		interactionPanel = UIManager.Instance.CreateInteractionPanel();
        		
		foreach (var method in methods)
		{
			ButtonInfo buttonInfo = new ButtonInfo(target, method, parameters);
			UIManager.Instance.AddButton(interactionPanel, buttonInfo);
		}
	}

	private static List<MethodInfo> GetTargetMethods(GameObject target)
	{
		List<MethodInfo> methods = new List<MethodInfo>();
		var scripts = target.GetComponents<MonoBehaviour>();
		var flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly;
		foreach (var script in scripts)
		{
			var type = script.GetType();
			methods.AddRange(type.GetMethods(flags));
		}

		return methods;
	}

	public static void CloseInteractionMenu()
	{
		if (interactionPanel != null)
		{
			Destroy(interactionPanel);
		}
	}
}
