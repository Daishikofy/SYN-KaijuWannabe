using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform transformToFollow;

    private Vector2 offset;
    private Vector3 newPosition;

    [Button("MoveBackward")]
    public int moveBackward;

    private void Start()
    {
        offset.x = transformToFollow.position.x - transform.position.x;
        offset.y = transformToFollow.position.z - transform.position.z;
        newPosition.y = transform.position.y;
    }
    private void LateUpdate()
    {
        newPosition.x = transformToFollow.position.x - offset.x;
        newPosition.z = transformToFollow.position.z - offset.y;

        transform.position = newPosition;
    }

    public void MoveBackward()
    {
        newPosition.x = transformToFollow.position.x - offset.x - transform.forward.x * 5;
        newPosition.z = transformToFollow.position.z - offset.y - transform.forward.z * 5;
        newPosition.y = transform.position.y - transform.forward.y * 5;
        offset.x = transformToFollow.position.x - newPosition.x;
        offset.y = transformToFollow.position.z - newPosition.z;
        transform.position = newPosition;
    }
}
