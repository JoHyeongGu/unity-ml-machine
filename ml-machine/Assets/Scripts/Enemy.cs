using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float speed = 1f;
    private Vector3 moveTo = new Vector3(0f, 0f, 0f);

    void Update()
    {
        SetMoveTo();
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveTo), Time.deltaTime * speed * 10);
        transform.position += moveTo * speed * Time.deltaTime;
    }

    private void SetMoveTo()
    {
        float dis = 0.1f;
        float differX = transform.position.x - target.position.x;
        float differZ = transform.position.z - target.position.z;
        if (differX < -dis)
        {
            moveTo.x = 1f;
        }
        else if (differX > dis)
        {
            moveTo.x = -1f;
        }
        if (differZ < dis)
        {
            moveTo.z = 1f;
        }
        else if (differZ > -dis)
        {
            moveTo.z = -1f;
        }
    }
}
