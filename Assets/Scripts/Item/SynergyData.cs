using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eSYNERGYTYPE
{
    None,
    Offensive_Increase,
    True_Damage,
    Stun,
    Sneak,
    Spawn_Fireball,
    Get_Speed_And_Jump,
    Throw_Ice,
    Shoot_Slash
}
public class CSynergy
{
    public eSYNERGYTYPE _synergyType;

    public int _ID { get; set; }
    public string _Name { get; set; }
    public string _Pre_Name { get; set; }
    public string _Buff_Name { get; set; }
    public float _Degree { get; set; }

    public float _Rate { get; set; }

    public float _Percent { get; set; }

    public float _DurTime { get; set; }

    public float _CoolTime { get; set; }
    public List<int> _SynergyItems { get; set; }
    public int _ItemCount { get; set; }
    public string _Synergy_Info { get; set; }

    /*
    public CSynergy(int id, string name, string preName, string buffName, float degree, float rate, float percent, float durTime, float coolTime, int[] synergyItems, int itemCount)
    {
        _ID = id;
        _Name = name;
        _Pre_Name = preName;
        _Buff_Name = buffName;
        _Degree = degree;
        _Rate = rate;
        _Percent = percent;
        _DurTime = durTime;
        _CoolTime = coolTime;
        _synergyItems = synergyItems;
        _itemCount = itemCount;
    }
    */
}
public class SynergyData : MonoBehaviour
{
    Dictionary<eSYNERGYTYPE, CSynergy> _synergies = new Dictionary<eSYNERGYTYPE, CSynergy>();
    public Dictionary<eSYNERGYTYPE, CSynergy> _Synergies => _synergies;

    public void ConvertCSVToDictionary(List<string> csvData)
    {
        Dictionary<eSYNERGYTYPE, CSynergy> synergyDict = new Dictionary<eSYNERGYTYPE, CSynergy>();

        //string[] lines = csvData.Split('\n');
        csvData.RemoveAt(csvData.Count - 1);

        for (int i = 0; i < csvData.Count; i++)
        {
            string[] values = csvData[i].Split(',');
            for (int j = 0; j < values.Length; j++)
                values[j] = values[j].Replace("\r", "");

            CSynergy synergy = new CSynergy();

            synergy._ID = int.Parse(values[(int)eSYNERGY_COLUMN.ID]);
            synergy._Name = values[(int)eSYNERGY_COLUMN.Dev_Name];
            synergy._Pre_Name = values[(int)eSYNERGY_COLUMN.Pre_Name];
            synergy._Buff_Name = values[(int)eSYNERGY_COLUMN.Buff_Name];
            //synergy._Type = Enum.Parse<eITEMTYPE>(values[(int)eSYNERGY_COLUMN.Type]);
            synergy._Degree = string.IsNullOrEmpty(values[(int)eSYNERGY_COLUMN.Buff_Degree]) ? 0f : float.Parse(values[(int)eSYNERGY_COLUMN.Buff_Degree]);
            synergy._Rate = string.IsNullOrEmpty(values[(int)eSYNERGY_COLUMN.Buff_Rate]) ? 0f : float.Parse(values[(int)eSYNERGY_COLUMN.Buff_Rate]);
            synergy._Percent = string.IsNullOrEmpty(values[(int)eSYNERGY_COLUMN.Buff_Percent]) ? 0f : float.Parse(values[(int)eSYNERGY_COLUMN.Buff_Percent]);
            synergy._DurTime = string.IsNullOrEmpty(values[(int)eSYNERGY_COLUMN.Dur_Time]) ? 0 : int.Parse(values[(int)eSYNERGY_COLUMN.Dur_Time]);
            synergy._CoolTime = string.IsNullOrEmpty(values[(int)eSYNERGY_COLUMN.Cool_Time]) ? 0f : float.Parse(values[(int)eSYNERGY_COLUMN.Cool_Time]);
            synergy._Synergy_Info = string.IsNullOrEmpty(values[(int)eSYNERGY_COLUMN.Synergy_Info]) ? string.Empty : values[(int)eSYNERGY_COLUMN.Synergy_Info];


            List<int> synergyItems = new List<int>();
            if (!string.IsNullOrEmpty(values[(int)eSYNERGY_COLUMN.Synergy_Item1]))
                synergyItems.Add(int.Parse(values[(int)eSYNERGY_COLUMN.Synergy_Item1]));
            if (!string.IsNullOrEmpty(values[(int)eSYNERGY_COLUMN.Synergy_Item2]))
                synergyItems.Add(int.Parse(values[(int)eSYNERGY_COLUMN.Synergy_Item2]));
            if (!string.IsNullOrEmpty(values[(int)eSYNERGY_COLUMN.Synergy_Item3]))
                synergyItems.Add(int.Parse(values[(int)eSYNERGY_COLUMN.Synergy_Item3]));


            synergy._SynergyItems = synergyItems;
            synergy._ItemCount = int.Parse(values[(int)eSYNERGY_COLUMN.Item_Count]);

            synergy._synergyType = Enum.Parse<eSYNERGYTYPE>(values[(int)eSYNERGY_COLUMN.Buff_Name]);

            synergyDict.Add(Enum.Parse<eSYNERGYTYPE>(values[(int)eSYNERGY_COLUMN.Buff_Name]), synergy);
        }


        _synergies = synergyDict;
    }
}
