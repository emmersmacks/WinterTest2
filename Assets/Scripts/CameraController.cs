using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform target;
    public float leftLimit;
    public float rightLimit;
    public float topLimit;
    public float bottomLimit;

    private void Awake()
    {
        if (!target) target = FindObjectOfType<Character>().transform;
    }

    private void Update()
    {

        Vector3 pos = target.position;
        pos.z = -20f;
        pos.y += 2.5f;
        transform.position = new Vector3(
            // Положение игрового объекта, за которым мы двигаемся
            target.position.x,
            pos.y,
            // Положение камеры z должно оставать неизменным 
            pos.z
            );
        transform.position = new Vector3
            (
            Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
            Mathf.Clamp(transform.position.y, bottomLimit, topLimit),
            transform.position.z
            ) ;

    }
}
