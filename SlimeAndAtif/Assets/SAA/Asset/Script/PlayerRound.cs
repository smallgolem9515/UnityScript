using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRound : MonoBehaviour
{
    Vector3 mousePoint;
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        mousePoint = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = transform.position - mousePoint;

        float rotationZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotationZ + 90);
    }
}
