using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokerNamesData : MonoBehaviour
{
    private string _pokerNamesString;
    private static List<string> PokerNames = new List<string>();

    private void OnEnable()
    {
        ReadPokerNamesDataFile();
        WriteInManager();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void ReadPokerNamesDataFile()
    {
        // json文件路径
        string mahjongJsonFilePath = Application.streamingAssetsPath + "/Poker/PokerNamesData.json";
        // 获取json内容string
        _pokerNamesString = System.IO.File.ReadAllText(mahjongJsonFilePath);
        _pokerNamesString = _pokerNamesString.Trim('"');
        // 分隔string并存为List
        foreach (var _mahjongName in _pokerNamesString.Split(' '))
        {
            PokerNames.Add(_mahjongName);
        }
    }

    private void WriteInManager()
    {
        
    }
}
