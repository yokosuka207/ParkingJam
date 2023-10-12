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

    public float bounceForce = 10f; // �o�E���h�����
    private Rigidbody rb;

    private exitMove exit;

    public float rotationAngle = 30.0f; // ��]�p�x
    public float rotationSpeed = 60.0f; // ��]���x�i�x/�b�j
    public string triggerTag = "����̃I�u�W�F�N�g�̃^�O";

    private Quaternion originalRotation;
    private bool isRotating = true;
    bool isReturning = false;
    private float rotationTimer = 0.0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        exit = GetComponent<exitMove>();

        // �I�u�W�F�N�g�̏�����]��ۑ�
        originalRotation = transform.rotation;
    }

    void Update()
    {
        // �^�b�`�܂��̓}�E�X�̃N���b�N�����o
        if (Input.GetMouseButtonDown(0))
        {
            // ���C�L���X�g���g�p���ă^�b�v�����ʒu�ɃI�u�W�F�N�g�����邩���`�F�b�N
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // ���C�L���X�g�Ńq�b�g�����I�u�W�F�N�g�����̃X�N���v�g���A�^�b�`���ꂽ�I�u�W�F�N�g�Ɠ����ł���Γ�����
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

                // forward �� back �̕����ɐ���
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

        // �I�u�W�F�N�g���ړ����̏ꍇ�A�O��Ɉړ�
        if (isMoving && !exit.GetMove())
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
        }


        if (isRotating)
        {
            if (rotationTimer < Mathf.Abs(rotationAngle) / rotationSpeed)
            {
                // ��]���̏���
                float rotationDirection = Mathf.Sign(rotationAngle);
                float rotationAmount = rotationDirection * rotationSpeed * Time.deltaTime;
                transform.Rotate(Vector3.up, rotationAmount);
                rotationTimer += Time.deltaTime;
            }
            else
            {
                // ��]���I�������猳�̊p�x�ɖ߂�
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
                // ���̊p�x�ɖ߂钆�̏���
                float rotationDirection = -Mathf.Sign(rotationAngle);
                float rotationAmount = rotationDirection * rotationSpeed * Time.deltaTime;
                transform.Rotate(Vector3.up, rotationAmount);
                rotationTimer += Time.deltaTime;
            }
            else
            {
                // �ēx��]���J�n
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
            // �Փ˂����@���x�N�g�����擾���āA�o�E���h�������v�Z
            Vector3 bounceDirection = collision.contacts[0].normal;
            // �o�E���h�͂�K�p
            rb.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);
            // �������Z�b�g
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