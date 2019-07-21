using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    GameObject target;

    Vector3 offset;

    void LateUpdate()
    {
        if (Input.GetButton("Fire2"))
        {
            Vector2 input = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            Quaternion rotationLR = Quaternion.AngleAxis(input.x, Vector3.up);
            offset = rotationLR * offset;

            Quaternion rotationTB = Quaternion.AngleAxis(input.y, Vector3.right);
            offset = rotationTB * offset;
        }

        transform.position = target.transform.position - offset;
        transform.LookAt(target.transform);
    }

    public void SetTarget(GameObject _target)
    {
        target = _target;
        offset = new Vector3(-10, 0, 0);
    }
}
