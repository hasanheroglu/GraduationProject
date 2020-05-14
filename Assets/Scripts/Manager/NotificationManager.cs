using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{
	[SerializeField] 
	private GameObject notificationPanel;
	[SerializeField]
	private GameObject notification;
	
	public static NotificationManager Instance { get; private set; }

	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(this.gameObject);
		}
		else
		{
			Instance = this;
		}
	}
	
	public void Notify(String message, Transform objTransform)
	{
		var newNotification = Instantiate(notification, notificationPanel.transform);
		newNotification.GetComponent<NotificationInfo>().Set(message, objTransform);
		AdjustPositioning();
	}

	private void AdjustPositioning()
	{
		var childCount = notificationPanel.transform.childCount;
		for (var i = 0; i < childCount; i++)
		{
			var child = notificationPanel.transform.GetChild(i);
			var notificationRect = child.gameObject.GetComponent<RectTransform>().rect;
			var notificationPosition = new Vector3(0,-1 * (childCount - i - 1) * notificationRect.height, 0);
			child.GetComponent<RectTransform>().anchoredPosition = notificationPosition;
		}
	}
}
