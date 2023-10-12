using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinRotatte : MonoBehaviour
{
    private GameObject car;
    private exitMove exit;
    public float riseSpeed = 2.0f; // ã¸‘¬“x
    public float moveSpeed = 0.0f; // ‰ñ“]‘¬“x
    private float rotationSpeed = 90.0f; // ‰ñ“]‘¬“x

    private void Start()
    {
        car = GameObject.Find("Car");
        exit = car.GetComponent<exitMove>();
    }
    void Update()
    {
        // “Á’è‚ÌğŒ‚ğ–‚½‚µ‚½ê‡‚Éã¸‚Æ‰ñ“]‚ğŠJn
        if (exit.GetMove())
        {
            // ã¸
            transform.position += (Vector3.up * riseSpeed * Time.deltaTime);
            // z²ü‚è‚Ì‰ñ“]
            transform.Rotate(new Vector3(1.0f, 0.0f, 0.0f) * moveSpeed * rotationSpeed * Time.deltaTime);

            Destroy(gameObject, 1.5f);
        }
    }
}
