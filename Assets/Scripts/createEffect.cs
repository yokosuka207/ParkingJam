using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createEffect : MonoBehaviour
{
    public float moveSpeed = 15.0f; // �ړ����x
    public float maxXDistance = 10.0f; // x�������̍ő�ړ�����
    public float maxYDistance = 10.0f; // y�������̍ő�ړ�����
    public float maxZDistance = 10.0f; // y�������̍ő�ړ�����

    private Vector3 randomDirection;
    private Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        // �����_���Ȉړ������𐶐�
        randomDirection = new Vector3(Random.Range(-maxXDistance, maxXDistance), 
            Random.Range(-maxYDistance, maxYDistance), Random.Range(-maxZDistance, maxZDistance));
    }

    // Update is called once per frame
    void Update()
    {
        // �����_���Ȉʒu�Ɍ������Ĉړ�
        transform.position += randomDirection.normalized * moveSpeed * Time.deltaTime;
        Destroy(gameObject, 0.1f);
    }
    private void OnDestroy()
    {
        randomDirection = new Vector3(Random.Range(-maxXDistance, maxXDistance),
            Random.Range(-maxYDistance, maxYDistance), Random.Range(-maxZDistance, maxZDistance));
    }
}
