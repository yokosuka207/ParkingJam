using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carMove : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    private Vector2 touchStartPos;
    private Vector3 swipeDirection;
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
            Vector2 swipeVector = (Vector2)Input.mousePosition - touchStartPos;

            if (isVec)
            {
                isVec = false;

                swipeDirection = transform.InverseTransformDirection(new Vector3(swipeVector.x, 0, swipeVector.y)).normalized;

                // forward と back の方向に制限
                float dotForward = Vector3.Dot(swipeDirection, Vector3.forward);
                float dotBack = Vector3.Dot(swipeDirection, Vector3.back);

                Vector3 localForward = transform.forward;
                Vector3 localBack = -localForward;

                if (dotForward > dotBack)
                {
                    moveDirection = localForward;
                }
                else
                {
                    moveDirection = localBack;
                }

            }
        }

        // オブジェクトが移動中の場合、前後に移動
        if (isMoving)
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
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

    public void SetMoveFlag(bool isMove)
    {
        isMoving = isMove;
        Debug.Log(isMoving);
    }
}