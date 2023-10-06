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

    public float bounceForce = 10f; // �o�E���h�����
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
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
            swipeDirection = (Vector2)Input.mousePosition - touchStartPos;

            if (isVec)
            {
                isVec = false;

                // �O�����擾
                if (swipeDirection.y > 0)
                    moveDirection = Vector3.forward;
                // ������擾
                else if (swipeDirection.y < 0)
                    moveDirection = Vector3.back;
            }
        }

        // �I�u�W�F�N�g���ړ����̏ꍇ�A�O��Ɉړ�
        if (isMoving)
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Car") && isMoving)
        {
            // �Փ˂����@���x�N�g�����擾���āA�o�E���h�������v�Z
            Vector3 bounceDirection = collision.contacts[0].normal;
            // �o�E���h�͂�K�p
            rb.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);
            // �������Z�b�g
            moveDirection = Vector3.zero;

            isMoving = false;
        }
    }
}