using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonUtil : MonoBehaviour {

	public static void SetText(GameObject button, String text)
	{
		button.GetComponentInChildren<Text>().text = text;
	}

	public static void SetOnClickAction(GameObject button, UnityAction action)
	{
		button.GetComponent<Button>().onClick.AddListener(action);
	}
	
	//Direction 1 or -1
	public static void AdjustPosition(GameObject button, int direction)
	{
		var buttonRect = button.GetComponent<RectTransform>().rect;
		var buttonPosition = new Vector3(1,direction * button.transform.GetSiblingIndex() * buttonRect.height, 0);
		button.GetComponent<RectTransform>().anchoredPosition = buttonPosition;
	}
	
	public static void AdjustPosition(GameObject button, int direction, int iterationCount)
	{
		var buttonRect = button.GetComponent<RectTransform>().rect;
		var buttonPosition = new Vector3(1,direction * iterationCount * (buttonRect.height + 5), 0);
		button.GetComponent<RectTransform>().anchoredPosition = buttonPosition;
	}
	
	public static void DropPosition(GameObject button)
	{
		var buttonRect = button.GetComponent<RectTransform>().rect;
		var buttonPosition = new Vector3(1,(button.transform.GetSiblingIndex() - 1) * buttonRect.height, 0);
		button.GetComponent<RectTransform>().anchoredPosition = buttonPosition;
	}

	public static void Destroy(GameObject button)
	{
		if (!button.activeSelf)
		{
			Debug.Log(button.activeSelf);
			button.SetActive(true);
		}
		GameObject.Destroy(button);
	}
	
}
