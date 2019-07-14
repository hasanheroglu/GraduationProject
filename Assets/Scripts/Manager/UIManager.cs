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
		var buttonRect = button.GetComponent<RectTransform>().rect;
		var panelRect = panel.GetComponent<RectTransform>().rect;
		var buttonPosition = new Vector3(1,-1 * button.transform.GetSiblingIndex() * buttonRect.height, 0);
		button.GetComponent<RectTransform>().anchoredPosition = buttonPosition;
		button.GetComponent<InteractionButton>().SetText(buttonInfo.Method.Name);
		panel.GetComponent<RectTransform>().sizeDelta = new Vector2(buttonRect.width, buttonRect.height * panel.transform.GetChildCount());
		button.GetComponent<Button>().onClick.AddListener(
			delegate
			{
				InteractionUtil.CloseInteractionMenu();
				buttonInfo.Interactable.StartCoroutine(buttonInfo.Method.Name, buttonInfo.Parameters);
				//buttonInfo.Method.Invoke(buttonInfo.Target.GetComponent<Interactable>(), buttonInfo.Parameters); 
			});
	}
}
