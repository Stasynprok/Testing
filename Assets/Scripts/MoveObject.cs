using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class MoveObject : MonoBehaviour
{
    [SerializeField]
    private Vector3 movePosition;
    [SerializeField]
    [Range(0, 1)]
    private float moveProgress;
    [SerializeField]
    private float moveSpeed = 0.2f;
    private Vector3 _initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        _initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        moveProgress = Mathf.PingPong(Time.time * moveSpeed, 1);
        transform.position = _initialPosition + movePosition * moveProgress;
    }
}
