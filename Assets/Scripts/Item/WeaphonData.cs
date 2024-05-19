using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public enum eITEMTYPE
{
    None,
    One_Hand_Sword,
    Shield,
    Two_Hand_Sword,
    Bow,
    Artifact
}
public enum eCATEGORY
{
    One_Hand,
    Shield,
    Two_Hand,
    Bow,
    Artifact,
    None
}

public class CItem
{
    public int _ID { get; set; }
    public string _Name { get; set; }
    public string _Pre_Name { get; set; }
    public eCATEGORY _Category { get; set; }
    public eITEMTYPE _Type { get; set; }
    public float _Att_Power { get; set; }
    public float _Att_Speed { get; set; }
    public float _Def { get; set; }
    public int _Synergy_ID { get; set; }

    public Buff _Buff { get; set; }
    public string _BuffInfo { get; set; }

}

public class WeaphonData : MonoBehaviour
{
    Dictionary<string, CItem> _itemDatas = new Dictionary<string, CItem>();
    public Dictionary<string, CItem> _ItemDatas => _itemDatas;

    List<CItem> _items;
    [SerializeField] BuffData _buffData;

    private void Awake()
    {
        
    }

    public void ConvertCSVToDictionary(List<string> csvData)
    {
        _buffData = GetComponent<BuffData>();
        Dictionary<string, CItem> weaponDict = new Dictionary<string, CItem>();

        //string[] lines = csvData.Split('\n');
        csvData.RemoveAt(csvData.Count - 1);

        for (int i = 0; i < csvData.Count; i++)
        {
            string[] values = csvData[i].Split(',');
            for (int j = 0; j < values.Length; j++)
                values[j] = values[j].Replace("\r", "");

            CItem weapon = new CItem();

            weapon._ID = int.Parse(values[(int)eITEM_COLUMN.ID]);
            weapon._Name = values[(int)eITEM_COLUMN.Dev_Name];
            weapon._Pre_Name = values[(int)eITEM_COLUMN.Pre_Name];
            weapon._Category = Enum.Parse<eCATEGORY>(values[(int)eITEM_COLUMN.Category]);
            weapon._Type = Enum.Parse<eITEMTYPE>(values[(int)eITEM_COLUMN.Type]);
            weapon._Att_Power = string.IsNullOrEmpty(values[(int)eITEM_COLUMN.Att_Power]) ? 0f : float.Parse(values[(int)eITEM_COLUMN.Att_Power]);
            weapon._Att_Speed = string.IsNullOrEmpty(values[(int)eITEM_COLUMN.Att_Speed]) ? 0f : float.Parse(values[(int)eITEM_COLUMN.Att_Speed]);
            weapon._Def = string.IsNullOrEmpty(values[(int)eITEM_COLUMN.Def]) ? 0f : float.Parse(values[(int)eITEM_COLUMN.Def]);
            weapon._Synergy_ID = string.IsNullOrEmpty(values[(int)eITEM_COLUMN.Synergy_ID]) ? 0 : int.Parse(values[(int)eITEM_COLUMN.Synergy_ID]);
            weapon._BuffInfo = string.IsNullOrEmpty(values[(int)eITEM_COLUMN.Buff_Info]) ? string.Empty : values[(int)eITEM_COLUMN.Buff_Info];

            BUFFTYPE buffType = string.IsNullOrEmpty(values[(int)eITEM_COLUMN.Buff_Name]) ? BUFFTYPE.None : Enum.Parse<BUFFTYPE>(values[(int)eITEM_COLUMN.Buff_Name]);
            float degree = string.IsNullOrEmpty(values[(int)eITEM_COLUMN.Buff_Degree]) ? 0f : float.Parse(values[(int)eITEM_COLUMN.Buff_Degree]);
            float rate = string.IsNullOrEmpty(values[(int)eITEM_COLUMN.Buff_Rate]) ? 0f : float.Parse(values[(int)eITEM_COLUMN.Buff_Rate]);
            float percent = string.IsNullOrEmpty(values[(int)eITEM_COLUMN.Buff_Percent]) ? 0f : float.Parse(values[(int)eITEM_COLUMN.Buff_Percent]);
            float durTime = string.IsNullOrEmpty(values[(int)eITEM_COLUMN.Dur_Time]) ? 0 : int.Parse(values[(int)eITEM_COLUMN.Dur_Time]);
            float coolTime = string.IsNullOrEmpty(values[(int)eITEM_COLUMN.Cool_Time]) ? 0 : int.Parse(values[(int)eITEM_COLUMN.Cool_Time]);
            


            Buff buff = new Buff(buffType, degree, rate, percent, durTime, coolTime);
            weapon._Buff = buff;

            weaponDict.Add(values[(int)eITEM_COLUMN.Dev_Name], weapon);
            _buffData.AddBuffToList(buff);
        }

            
        _itemDatas = weaponDict;
    }

    
}
