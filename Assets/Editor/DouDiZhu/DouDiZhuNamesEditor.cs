using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEditor;
using Newtonsoft.Json;

public class DouDiZhuNamesEditor : Editor
{
    private static List<string> DouDiZhuNames = new List<string>();
    private static StringBuilder DouDiZhuNamesStringBuilder = new StringBuilder();
    private static string DouDiZhuNamesString;
    
    // Creat list and json file
    [MenuItem("Generate Json/DouDiZhu Names")]
    public static void ReadEnemiesExcelFileAndCreatJsonFile()
    {
        CreatDouDiZhuNamesList();
        CreatDouDiZhuDataJsonFile();
    }

    private static void CreatDouDiZhuNamesList()
    {
        // MahjongNames.Clear();
        // 红桃 A - K
        string kind = "A";
        string num = "♥";
        for (int i = 0; i < 4; i++)
        {
            for (int j = 1; j <= 13; j++)
            {
                // Decide num
                if (j == 1)
                {
                    num = "A";
                    // Decide kind
                    if (i == 0)
                    {
                        kind = "♥";
                    }
                    else if (i == 1)
                    {
                        kind = "♠";
                    }
                    else if (i == 2)
                    {
                        kind = "♦";
                    }
                    else if (i == 3)
                    {
                        kind = "♣";
                    }
                }
                else if (j == 11)
                {
                    num = "J";
                }
                else if (j == 12)
                {
                    num = "Q";
                }                
                else if (j == 13)
                {
                    num = "K";
                }
                else
                {
                    num = j.ToString();
                }
                // DouDiZhuNames.Add(kind + num);
                DouDiZhuNamesStringBuilder.Append(kind + num + " ");
            }
        }
        DouDiZhuNamesStringBuilder.Append("小王 ");
        DouDiZhuNamesStringBuilder.Append("大王");
        DouDiZhuNamesString = DouDiZhuNamesStringBuilder.ToString();
    }
    private static string _douDiZhuDataFilePath;
    private static void CreatDouDiZhuDataJsonFile()
    {
        // 获取数据文件路径
        _douDiZhuDataFilePath = Path.Combine(Application.streamingAssetsPath + "/DouDiZhu", "DouDiZhuNamesData.json");
        string directory = Path.GetDirectoryName(_douDiZhuDataFilePath);
        if (!Directory.Exists(directory))
        {
            Debug.Log("Didn't find doudizhu data file");
            Directory.CreateDirectory(directory);
            Debug.Log("Successfully created doudizhu data file at "+Application.streamingAssetsPath+"/DouDiZhu/DouDiZhuNamesData.json");
        }
        string jsonData = JsonConvert.SerializeObject(DouDiZhuNamesString);
        File.WriteAllText(_douDiZhuDataFilePath, jsonData);
        AssetDatabase.Refresh();
        Debug.Log("Successfully overwrote doudizhu data file at "+Application.streamingAssetsPath+"/DouDiZhu/DouDiZhuNamesData.json");
    }
}
