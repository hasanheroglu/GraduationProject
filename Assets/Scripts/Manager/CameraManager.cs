using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Move
{
    public Move(float horizontal, float vertical)
    {
        Horizontal = horizontal;
        Vertical = vertical;
    }
    
    public float Horizontal { get; set; }
    public float Vertical { get; set; }
};

public class CameraManager : MonoBehaviour
{
    private static Camera _camera;
    
    [Range(5f, 10f)]
    public float minOrtographicSize = 5f;
    [Range(20f, 30f)]
    public float maxOrtographicSize = 30f;
    public float zoomAmount = 0.3f;
    public float moveAmount = 0.5f;
    public GameObject camera;
    public GameObject cameraCenter;
    
    private float _rotation;
    private static Vector3 _offset;
    
    // Start is called before the first frame update
    void Start()
    {
        _camera = camera.GetComponent<Camera>();
        _rotation = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        _offset = cameraCenter.transform.position - _camera.transform.position;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _camera.transform.RotateAround(cameraCenter.transform.position, Vector3.up, -90);
            _rotation -= 90;
        }
        else
        {
            Move move = GetMove();
            _camera.transform.position += Quaternion.Euler(0, _rotation, 0) * new Vector3(move.Horizontal, 0f, move.Vertical) * moveAmount;
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

    private Move GetMove()
    {
        Move move = new Move(0f, 0f);
            
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            move.Horizontal = -1;
        }
            
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            move.Horizontal = 1;
        }
            
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            move.Vertical = -1;
        }
            
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            move.Vertical = 1;
        }

        return move;
    }
    
    public static IEnumerator SetPosition(Transform objTransform)
    {
        Debug.Log(_offset);
        var endPosition = objTransform.position;
        endPosition -= _offset;
        endPosition.y = _camera.transform.position.y;
        var startPos = _camera.transform.position;
        var startTime = Time.time;
        var duration = 2f;
        
        /*
        while (Time.time < startTime + duration)
        {
            _camera.transform.position = Vector3.Lerp(startPos, camPosition, (Time.time - startTime)/duration);
            yield return null;
        }
        */

        _camera.transform.position = endPosition;
        yield return null;
    }
}
