using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class FollowingCamera : MonoBehaviour
{
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private float speed = 1f;

    private Vector3 _nextPosition;

    private Camera _camera;
    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();
        _nextPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var screenHeight = Screen.height;
        var targetPosition = target.transform.position;
        var screenPoint = _camera.WorldToScreenPoint(targetPosition);
        var currentPosition = transform.position;
        if (screenPoint.y > screenHeight * 0.8)
        {
            var newY = currentPosition.y + (screenPoint.y - screenHeight * 0.8f) + 10;
            _nextPosition = new Vector3(currentPosition.x, newY, currentPosition.z);
        }
        else if (screenPoint.y < screenHeight * 0.1)
        {
            var newY = currentPosition.y - (screenHeight * 0.1f - screenPoint.y) - 10;
            _nextPosition = new Vector3(currentPosition.x, newY, currentPosition.z);
        }

        Vector3 newPosition = Vector3.Lerp(currentPosition, _nextPosition, speed * Time.deltaTime);
        transform.position = newPosition;
    }
}
