using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class reset : MonoBehaviour
{
    public void Retry()//�����ɏ����������uRetry�v���I����ʂŕ\�������
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
