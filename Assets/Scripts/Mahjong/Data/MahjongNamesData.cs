using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MahjongNamesData : MonoBehaviour
{
    private string _mahjongNamesString;
    public static List<string> MahjongNames = new List<string>();
    private void OnEnable()
    {
        ReadMahjongNamesDataFile();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ReadMahjongNamesDataFile()
    {
        // json文件路径
        string mahjongJsonFilePath = Application.streamingAssetsPath + "/Mahjong/MahjongNamesData.json";
        // 获取json内容string
        _mahjongNamesString = System.IO.File.ReadAllText(mahjongJsonFilePath);
        _mahjongNamesString = _mahjongNamesString.Trim('"');
        // 分隔string并存为List
        foreach (var _mahjongName in _mahjongNamesString.Split(' '))
        {
            MahjongNames.Add(_mahjongName);
        }
    }
}
