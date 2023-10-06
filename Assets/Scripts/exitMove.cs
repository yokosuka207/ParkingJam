using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exitMove : MonoBehaviour
{
    [SerializeField] public GameObject[] roads;
    private int num;
    private bool isMove = false;
    carMove car;

    // Update is called once per frame
    void Start()
    {
        car = GetComponent<carMove>();
    }

    private void Update()
    {
        if (isMove && num < roads.Length)
        {
            transform.LookAt(new Vector3(roads[num].transform.position.x, car.transform.position.y, roads[num].transform.position.z));
            transform.position += transform.forward * Time.deltaTime * 10;
        }
    }

    private void OnTriggerEnter(Collider other)
    {   
        //if(roads == null)
        //{
        //    return;
        //}

        for (int i = 0; i <= roads.Length; i++)
        {
            if (other.gameObject == roads[i])
            {
                num = i + 1;
                isMove = true;
                car.SetMoveFlag(false);
                break;
            }
        }
    }
}
