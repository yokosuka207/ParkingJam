using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carMove : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    private Vector2 touchStartPos;
    private Vector2 swipeDirection;
    private Vector3 moveDirection;
    private bool isMoving = false;
    private bool isVec = false;

    public float bounceForce = 10f; // バウンドする力
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // タッチまたはマウスのクリックを検出
        if (Input.GetMouseButtonDown(0))
        {
            // レイキャストを使用してタップした位置にオブジェクトがあるかをチェック
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // レイキャストでヒットしたオブジェクトがこのスクリプトがアタッチされたオブジェクトと同じであれば動かす
                if (hit.collider.gameObject == gameObject)
                {
                    isMoving = true;
                    isVec = true;
                }
            }

            touchStartPos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            swipeDirection = (Vector2)Input.mousePosition - touchStartPos;

            if (isVec)
            {
                isVec = false;

                // 前方向取得
                if (swipeDirection.y > 0)
                    moveDirection = Vector3.forward;
                // 後方向取得
                else if (swipeDirection.y < 0)
                    moveDirection = Vector3.back;
            }
        }

        // オブジェクトが移動中の場合、前後に移動
        if (isMoving)
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Car") && isMoving)
        {
            // 衝突した法線ベクトルを取得して、バウンド方向を計算
            Vector3 bounceDirection = collision.contacts[0].normal;
            // バウンド力を適用
            rb.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);
            // 方向リセット
            moveDirection = Vector3.zero;

            isMoving = false;
        }
    }
}