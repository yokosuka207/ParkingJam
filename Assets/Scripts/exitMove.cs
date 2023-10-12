using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exitMove : MonoBehaviour
{
    [SerializeField] public GameObject[] roads;
    [SerializeField] private GameObject coin;
    [SerializeField] private float moveSpeed = 0;
    private int num = 0;
    private int i = 0;
    private int time = 0;
    private GameObject child;
    private bool isMove = false;
    private bool isBack = false;
    private bool isCoin = false;
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
            //transform.LookAt(new Vector3(roads[num].transform.position.x, car.transform.position.y, roads[num].transform.position.z));
            //transform.position += transform.forward * Time.deltaTime * moveSpeed;

            // 現在のターゲットを取得
            Transform currentTarget = roads[num].transform;

            // ターゲットの方向を向く
            Vector3 targetDirection = currentTarget.position - transform.position;
            targetDirection.y = car.transform.position.y;
            Quaternion rotationToTarget = Quaternion.LookRotation(targetDirection);
            Quaternion invertedRotation = Quaternion.Inverse(rotationToTarget);

            // 徐々に回転する
            transform.rotation = Quaternion.Slerp(transform.rotation, rotationToTarget, 5.0f * Time.deltaTime);

            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);          
        }
    }

    private void OnTriggerEnter(Collider other)
    {   
        if (other.transform.CompareTag("Road"))
        {
            for (i = 0; i <= roads.Length; i++)
            {
                if (other.gameObject == roads[i])
                {
                    num = i + 1;
                    isMove = true;

                    if(!isCoin)
                    {
                        Instantiate(coin, roads[num - 1].transform.position, Quaternion.Euler(0, 0, 90));
                        isCoin = true;
                    }
                    
                    
                    break;
                }
            }
        }
    }

    public bool GetMove()
    {
        return isMove;
    }
}
