using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowItemInfoUI : MonoBehaviour
{
    [Header("패널_이름"), SerializeField] TextMeshProUGUI _nameText;
    [Header("패널_타입"), SerializeField] TextMeshProUGUI _typeText;
    [Header("패널_스탯"), SerializeField] TextMeshProUGUI[] _statTexts;
    [Header("패널_버프"), SerializeField] TextMeshProUGUI _buffText;
    [Header("아이콘"), SerializeField] Image _iconImg;

    private void Awake()
    {
        gameObject.SetActive(false);
    }
    public void SetInfoText(CItem item)
    {
        _nameText.text = item._Name;
        _typeText.text = GetTypeToString(item._Type);
        _statTexts[0].text = (item._Att_Power == 0) ? string.Empty : "공격력 : " + item._Att_Power.ToString();
        _statTexts[1].text = (item._Att_Speed == 0) ? string.Empty : "공격 속도 : " + item._Att_Speed.ToString();
        _statTexts[2].text = (item._Def == 0) ? string.Empty : "방어력 : " + item._Def.ToString();
        _buffText.text = string.IsNullOrEmpty(item._BuffInfo) ? string.Empty : item._BuffInfo;
    }
    string GetTypeToString(eITEMTYPE type)
    {
        switch(type)
        {
            case eITEMTYPE.One_Hand_Sword:
                return "한손 검";
            case eITEMTYPE.Two_Hand_Sword:
                return "두손 검";
            case eITEMTYPE.Bow:
                return "활";
            case eITEMTYPE.Shield:
                return "방패";
            case eITEMTYPE.Artifact:
                return "아티팩트";
            default:
                return string.Empty;

        }
    }
}
