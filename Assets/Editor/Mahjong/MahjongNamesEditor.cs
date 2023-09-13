using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEditor;
using Newtonsoft.Json;

public class MahjongNamesEditor : Editor
{
    private static List<string> MahjongNames = new List<string>();
    private static StringBuilder MahjongNamesStringBuilder = new StringBuilder();
    private static string MahjongNamesString;
    
    // Creat list and json file
    [MenuItem("Generate Json/Mahjong Names")]
    public static void ReadEnemiesExcelFileAndCreatJsonFile()
    {
        CreatMahjongNamesList();
        CreatMahjongDataJsonFile();
    }

    private static void CreatMahjongNamesList()
    {
        MahjongNames.Clear();
        // 条 筒 万
        for (int i = 0; i < 4; i++)
        {
            for (int j = 1; j <= 9; j++)
            {
                MahjongNames.Add(j + "Tiao " + i);
                MahjongNamesStringBuilder.Append(j + "Tiao" + i + " ");
            }
        }
        for (int i = 0; i < 4; i++)
        {
            for (int j = 1; j <= 9; j++)
            {
                MahjongNames.Add(j + "Tong " + i);
                MahjongNamesStringBuilder.Append(j + "Tong" + i + " ");
            }
        }
        for (int i = 0; i < 4; i++)
        {
            for (int j = 1; j <= 9; j++)
            {
                MahjongNames.Add(j + "Wan " + i);
                MahjongNamesStringBuilder.Append(j + "Wan" + i + " ");
            }
        }

        // 中东南西北 白板 发
        for (int i = 0; i < 4; i++)
        {
            MahjongNames.Add("Zhong " + i);
            MahjongNamesStringBuilder.Append("Zhong" + i + " ");
        }
        for (int i = 0; i < 4; i++)
        {
            MahjongNames.Add("Dong " + i);
            MahjongNamesStringBuilder.Append("Dong" + i + " ");
        }
        for (int i = 0; i < 4; i++)
        {
            MahjongNames.Add("Nan " + i);
            MahjongNamesStringBuilder.Append("Nan" + i + " ");
        }
        for (int i = 0; i < 4; i++)
        {
            MahjongNames.Add("Xi " + i);
            MahjongNamesStringBuilder.Append("Xi" + i + " ");
        }
        for (int i = 0; i < 4; i++)
        {
            MahjongNames.Add("Bei " + i);
            MahjongNamesStringBuilder.Append("Bei" + i + " ");
        }
        for (int i = 0; i < 4; i++)
        {
            MahjongNames.Add("Baiban " + i);
            MahjongNamesStringBuilder.Append("Baiban" + i + " ");
        }
        for (int i = 0; i < 4; i++)
        {
            MahjongNames.Add("Fa " + i);
            MahjongNamesStringBuilder.Append("Fa" + i + " ");
        }
        
        // 春夏秋冬 梅兰竹菊
        MahjongNames.Add("Chun");
        MahjongNames.Add("Xia");
        MahjongNames.Add("Qiu");
        MahjongNames.Add("Dong");
        MahjongNames.Add("Mei");
        MahjongNames.Add("Lan");
        MahjongNames.Add("Zhu");
        MahjongNames.Add("Ju");
        MahjongNamesStringBuilder.Append("Chun" + " ");
        MahjongNamesStringBuilder.Append("Xia" + " ");
        MahjongNamesStringBuilder.Append("Qiu" + " ");
        MahjongNamesStringBuilder.Append("Dong" + " ");
        MahjongNamesStringBuilder.Append("Mei" + " ");
        MahjongNamesStringBuilder.Append("Lan" + " ");
        MahjongNamesStringBuilder.Append("Zhu" + " ");
        MahjongNamesStringBuilder.Append("Ju" + " ");
        MahjongNamesString = MahjongNamesStringBuilder.ToString();
    }
    private static string _mahjongDataFilePath;
    private static void CreatMahjongDataJsonFile()
    {
        // 获取数据文件路径
        _mahjongDataFilePath = Path.Combine(Application.streamingAssetsPath + "/Mahjong", "MahjongNamesData.json");
        string directory = Path.GetDirectoryName(_mahjongDataFilePath);
        if (!Directory.Exists(directory))
        {
            Debug.Log("Didn't find mahjong data file");
            Directory.CreateDirectory(directory);
            Debug.Log("Successfully created mahjong data file at "+Application.streamingAssetsPath+"/Mahjong/MahjongNamesData.json");
        }
        string jsonData = JsonConvert.SerializeObject(MahjongNamesString);
        File.WriteAllText(_mahjongDataFilePath, jsonData);
        AssetDatabase.Refresh();
        Debug.Log("Successfully overwrote mahjong data file at "+Application.streamingAssetsPath+"/Mahjong/MahjongNamesData.json");
    }
}
