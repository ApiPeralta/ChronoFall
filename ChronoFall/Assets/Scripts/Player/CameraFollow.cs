// CameraFollow.cs
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 pos = target.position;
            pos.z = transform.position.z;
            transform.position = pos;
        }
    }
}

