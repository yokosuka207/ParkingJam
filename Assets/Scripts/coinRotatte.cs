using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinRotatte : MonoBehaviour
{
    private GameObject car;
    private exitMove exit;
    public float riseSpeed = 2.0f; // �㏸���x
    public float moveSpeed = 0.0f; // ��]���x
    private float rotationSpeed = 90.0f; // ��]���x

    private void Start()
    {
        car = GameObject.Find("Car");
        exit = car.GetComponent<exitMove>();
    }
    void Update()
    {
        // �㏸
        transform.position += (Vector3.up * riseSpeed * Time.deltaTime);
        // z������̉�]
        transform.Rotate(new Vector3(1.0f, 0.0f, 0.0f) * moveSpeed * rotationSpeed * Time.deltaTime);

        Destroy(gameObject, 1.5f);
    }
}
