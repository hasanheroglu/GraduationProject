using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InteractionUI : MonoBehaviour
{
	private List<MethodInfo> methods;

	public GameObject canvas;
	public GameObject interactionButton;
	public GameObject interactionPanel;

	private GameObject interaction;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown)
		{
			//CloseInteractions();
		}
	}

	private void GetInteractions(GameObject target)
	{
		methods = new List<MethodInfo>();
		var scripts = target.GetComponents<MonoBehaviour>();
		var flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly;
		foreach (var script in scripts)
		{
			var type = script.GetType();
			methods.AddRange(type.GetMethods(flags));
		}
		
		foreach (var m in methods)
		{
			interaction = Instantiate(interactionPanel, canvas.transform);
			AddButton(target, m);
			Debug.Log(m.Name);
		}
	}

	private void AddButton(GameObject target, MethodInfo method)
	{
		var button = Instantiate(interactionButton, interaction.transform);
		button.GetComponent<InteractionButton>().SetText(method.Name);
		//var action = (UnityAction) Delegate.CreateDelegate(typeof(Action), );
		
		button.GetComponent<Button>().onClick.AddListener(delegate { method.Invoke(this, null); });
	}

	public void ShowInteractions(GameObject target)
	{
		GetInteractions(target);
	}

	private void CloseInteractions()
	{
		Destroy(interaction);
	}
	
	
}
