using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private static Camera _camera;
    
    [Range(5f, 10f)]
    public float minOrtographicSize = 5f;
    [Range(20f, 30f)]
    public float maxOrtographicSize = 30f;
    public float zoomAmount = 0.3f;
    public float moveAmount = 0.3f;
    public GameObject camera;
    public GameObject cameraCenter;
    
    // Start is called before the first frame update
    void Start()
    {
        _camera = camera.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _camera.transform.RotateAround(cameraCenter.transform.position, Vector3.up, -90);
        }
        else
        {
            _camera.transform.position += new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")) * moveAmount;
        }
        
        if (Input.mouseScrollDelta.y < 0)
        {
            ZoomIn();
        }
        else if(Input.mouseScrollDelta.y > 0)
        {
            ZoomOut();
        }
    }
    
    private void ZoomIn()
    {
        _camera.orthographicSize += zoomAmount;

        if (_camera.orthographicSize > maxOrtographicSize)
        {
            _camera.orthographicSize = maxOrtographicSize;
        }
    }

    private void ZoomOut()
    {
        _camera.orthographicSize -= zoomAmount;
        
        if (_camera.orthographicSize < minOrtographicSize)
        {
            _camera.orthographicSize = minOrtographicSize;
        }
    }

    public static void SetPosition(Vector3 camPosition)
    {
        var timer = 0f;

        camPosition.y = _camera.transform.position.y;
        while (timer < 5f)
        {
            timer += Time.deltaTime;
            _camera.transform.position = Vector3.Slerp(_camera.transform.position, camPosition, Time.deltaTime * 5);
        }
    }
}
