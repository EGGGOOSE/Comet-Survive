using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRotator : MonoBehaviour
{
    public float rotationSpeed;

    void Start()
    {
        transform.Rotate(new Vector3(0, 0, Random.Range(0, 360)));
        if (Random.value > 0.5)
            rotationSpeed *= -1;
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
    }
}
