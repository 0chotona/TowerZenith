using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eITEM_COLUMN
{
    ID,
    Dev_Name,
    Pre_Name,
    Category,
    Type,
    Att_Power,
    Att_Speed,
    Def,
    Buff_Name,
    Buff_Degree,
    Buff_Rate,
    Buff_Percent,
    Dur_Time,
    Cool_Time,
    Synergy_ID,
    Buff_Info
}
public enum eSYNERGY_COLUMN
{
    ID,
    Dev_Name,
    Pre_Name,
    Buff_Name,
    Buff_Degree,
    Buff_Rate,
    Buff_Percent,
    Dur_Time,
    Cool_Time,
    Synergy_Item1,
    Synergy_Item2,
    Synergy_Item3,
    Item_Count,
    Synergy_Info
}
public enum eBUFF_COLUMN
{
    ID,
    Buff_Name,
    Buff_Degree,
    Buff_Rate,
    Buff_Percent,
    Dur_Time,
    Cool_Time,
    Item_ID
}
public class CSVReader : MonoBehaviour
{
    [SerializeField] TextAsset _itemDataFile; 
    [Header("½Ã³ÊÁö"), SerializeField] TextAsset _synergyDataFile;

    List<string> _rowItemData = new List<string>();
    List<string> _rowSynergyData = new List<string>();

    List<string> _titleName = new List<string>();

    [SerializeField] WeaphonData _weaphonData;
    [SerializeField] SynergyData _synegyData;
    

    private void Awake()
    {
        ReadItemCSV();
        ReadSynergyCSV();
        SetData();
    }
    void ReadItemCSV()
    {
        string[] lines = System.Text.Encoding.UTF8.GetString(_itemDataFile.bytes).Split('\n');
        bool isTitle = true;
        foreach (string line in lines)
        {
            if(isTitle)
            {
                isTitle = false;
                _titleName.Add(line);
            }
            else
                _rowItemData.Add(line);
        }

    }
    void ReadSynergyCSV()
    {
        string[] lines = System.Text.Encoding.UTF8.GetString(_synergyDataFile.bytes).Split('\n');
        bool isTitle = true;
        foreach(string line in lines)
        {
            if (isTitle)
                isTitle = false;
            else
                _rowSynergyData.Add(line);
        }
    }
    void SetData()
    {
        _weaphonData.ConvertCSVToDictionary(_rowItemData);
        _synegyData.ConvertCSVToDictionary(_rowSynergyData);
        //_weaphonData.ConvertCSVToDictionary(_rowData);
    }
   
}
