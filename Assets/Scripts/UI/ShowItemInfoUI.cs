using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowItemInfoUI : MonoBehaviour
{
    [Header("�г�_�̸�"), SerializeField] TextMeshProUGUI _nameText;
    [Header("�г�_Ÿ��"), SerializeField] TextMeshProUGUI _typeText;
    [Header("�г�_����"), SerializeField] TextMeshProUGUI[] _statTexts;
    [Header("�г�_����"), SerializeField] TextMeshProUGUI _buffText;
    [Header("������"), SerializeField] Image _iconImg;

    private void Awake()
    {
        gameObject.SetActive(false);
    }
    public void SetInfoText(CItem item)
    {
        _nameText.text = item._Name;
        _typeText.text = GetTypeToString(item._Type);
        _statTexts[0].text = (item._Att_Power == 0) ? string.Empty : "���ݷ� : " + item._Att_Power.ToString();
        _statTexts[1].text = (item._Att_Speed == 0) ? string.Empty : "���� �ӵ� : " + item._Att_Speed.ToString();
        _statTexts[2].text = (item._Def == 0) ? string.Empty : "���� : " + item._Def.ToString();
        _buffText.text = string.IsNullOrEmpty(item._BuffInfo) ? string.Empty : item._BuffInfo;
    }
    string GetTypeToString(eITEMTYPE type)
    {
        switch(type)
        {
            case eITEMTYPE.One_Hand_Sword:
                return "�Ѽ� ��";
            case eITEMTYPE.Two_Hand_Sword:
                return "�μ� ��";
            case eITEMTYPE.Bow:
                return "Ȱ";
            case eITEMTYPE.Shield:
                return "����";
            case eITEMTYPE.Artifact:
                return "��Ƽ��Ʈ";
            default:
                return string.Empty;

        }
    }
}
