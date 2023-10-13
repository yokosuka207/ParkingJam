using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class emotionRotation : MonoBehaviour
{
    public RawImage imageToMove; // 2D�摜
    createEmotion colCar;

     // �������I�u�W�F�N�g�ւ̎Q��

    public void SetCreatorObject(createEmotion car)
    {
        colCar = car;
    }

    void Update()
    {
        if (imageToMove != null && colCar != null)
        {
            // 3D�I�u�W�F�N�g�̈ʒu���擾���A�����2D�摜�̈ʒu�ɔ��f
            Vector3 worldPosition = colCar.transform.position;
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
            imageToMove.transform.position = screenPosition;

            Destroy(gameObject, 0.2f);
        }
    }
}
