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

    private exitMove exit;

    public float rotationAngle = 30.0f; // 回転角度
    public float rotationSpeed = 60.0f; // 回転速度（度/秒）
    public string triggerTag = "特定のオブジェクトのタグ";

    private Quaternion originalRotation;
    private bool isRotating = true;
    bool isReturning = false;
    private float rotationTimer = 0.0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        exit = GetComponent<exitMove>();

        // オブジェクトの初期回転を保存
        originalRotation = transform.rotation;
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
        if (isMoving && !exit.GetMove())
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
        }


        if (isRotating)
        {
            if (rotationTimer < Mathf.Abs(rotationAngle) / rotationSpeed)
            {
                // 回転中の処理
                float rotationDirection = Mathf.Sign(rotationAngle);
                float rotationAmount = rotationDirection * rotationSpeed * Time.deltaTime;
                transform.Rotate(Vector3.up, rotationAmount);
                rotationTimer += Time.deltaTime;
            }
            else
            {
                // 回転が終了したら元の角度に戻す
                transform.rotation = originalRotation;
                isRotating = false;
                isReturning = true;
                rotationTimer = 0.0f;
            }
        }
        else if (isReturning)
        {
            if (rotationTimer < Mathf.Abs(rotationAngle) / rotationSpeed)
            {
                // 元の角度に戻る中の処理
                float rotationDirection = -Mathf.Sign(rotationAngle);
                float rotationAmount = rotationDirection * rotationSpeed * Time.deltaTime;
                transform.Rotate(Vector3.up, rotationAmount);
                rotationTimer += Time.deltaTime;
            }
            else
            {
                // 再度回転を開始
                //isRotating = true;
                isReturning = false;
                rotationTimer = 0.0f;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Car") || collision.transform.CompareTag("Wall")
            && isMoving && !exit.GetMove())
        {
            // 衝突した法線ベクトルを取得して、バウンド方向を計算
            Vector3 bounceDirection = collision.contacts[0].normal;
            // バウンド力を適用
            rb.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);
            // 方向リセット
            moveDirection = Vector3.zero;

            isMoving = false;
        }

        if (collision.transform.CompareTag("Car") || collision.transform.CompareTag("Wall")
            && !isMoving && !exit.GetMove())
        {
            

            isMoving = false;
        }
    }

    public void SetMoveFlag(bool isMove)
    {
        isMoving = isMove;
    }

    public bool GetMoveFlag()
    {
        return isMoving;
    }
}