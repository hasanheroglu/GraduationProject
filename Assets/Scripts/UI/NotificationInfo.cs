using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NotificationInfo : MonoBehaviour
{
    [SerializeField]
    private GameObject message;
    [SerializeField]
    private GameObject button;

    private float expireDuration = 60f;

    private void Start()
    {
        Destroy(gameObject, expireDuration);
    }

    private void SetMessage(string messageText)
    {
        message.GetComponent<Text>().text = messageText;
    }

    private void SetButtonAction(Vector3 camPosition)
    {
        button.GetComponent<Button>().onClick.AddListener(delegate
        {
            CameraManager.SetPosition(camPosition);
            Destroy(gameObject);
        });
    }

    private void SetPosition()
    {
        var notificationRect = GetComponent<RectTransform>().rect;
        var notificationPosition = new Vector3(0,-1 * transform.GetSiblingIndex() * notificationRect.height, 0);
        GetComponent<RectTransform>().anchoredPosition = notificationPosition;
    }
    
    public void Set(string messageText, Vector3 camPosition)
    {
        SetMessage(messageText);
        SetButtonAction(camPosition);
    }
}
