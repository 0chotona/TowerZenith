using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [Header("���� �������� ��ȣ"), SerializeField] int _stageNum = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            InGameUI._Instance.SetNextStageActive(true);
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(Input.GetKey(KeyCode.F))
                SceneManager.LoadScene(_stageNum);

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            InGameUI._Instance.SetNextStageActive(false);
    }
}
