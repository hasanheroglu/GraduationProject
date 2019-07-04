using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionButton : MonoBehaviour {

	public void SetText(String text)
	{
		GetComponentInChildren<Text>().text = text;
	}

	public void SetOnClickFunction()
	{
		
	}
	
}
