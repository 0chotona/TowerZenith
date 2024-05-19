using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [Header("다음 스테이지 번호"), SerializeField] int _stageNum = 0;
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
