using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    // [SerializeField] private Transform focus;
    [SerializeField] private float speed = 3;
    private Vector3 moveTo;

    void Update()
    {
        moveTo = new Vector3(-Input.GetAxis("Horizontal"), 0f, -Input.GetAxis("Vertical"));
        if (Input.GetKey(KeyCode.X))
        {
            moveTo.y = 1f;
        }
        else if (Input.GetKey(KeyCode.Z) && transform.position.y > 7f)
        {
            moveTo.y = -1f;
        }
        else
        {
            moveTo.y = 0f;
        }
        transform.position += moveTo * speed * Time.deltaTime;
        // transform.position = new Vector3(focus.position.x, transform.position.y, focus.position.z);
    }
}
