using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

	private static UIManager instance;
	
	public GameObject canvas;
	
	[Header("Interaction")]
	public GameObject interactionButton;
	public GameObject interactionPanel;


	public static UIManager Instance { get { return instance; } }
	
	private void Awake()
	{
		if (instance != null && instance != this)
		{
			Destroy(this.gameObject);
		} else {
			instance = this;
		}
	}

	public GameObject CreateInteractionPanel()
	{
		return Instantiate(interactionPanel, canvas.transform);
	}

	public void AddButton(GameObject panel, ButtonInfo buttonInfo)
	{
		var button = Instantiate(interactionButton, panel.transform);
		button.GetComponent<InteractionButton>().SetText(buttonInfo.Method.Name);
		button.GetComponent<Button>().onClick.AddListener(delegate { buttonInfo.Method.Invoke(buttonInfo.Target.GetComponent<Interactable>(), buttonInfo.Parameters); });
	}
}
