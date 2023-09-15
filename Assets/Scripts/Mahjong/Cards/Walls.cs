using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls : MonoBehaviour
{
    private CardsContainer _cardsContainer;
    private int _wallNum;
    private bool _canBuildHere;
    public Transform transStartBuildPos;
    public List<Vector3> v3RestBuildPos = new List<Vector3>();
    private Vector3 _v3BuiltWallTargetPos;
    private float _builtWallMoveSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        InitBuildPositions();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitBuildPositions()
    {
        _cardsContainer = GetComponentInParent<CardsContainer>();
        _wallNum = _cardsContainer.transWalls.IndexOf(this);
        _builtWallMoveSpeed = MahjongParameters.BuiltWallMoveSpeed;
        int groupTimes = 0;
        switch (_wallNum)
        {
            // bottom wall
            case 0 :
                _v3BuiltWallTargetPos = Vector3.back * 50f;
                for (int i = 0; i < MahjongParameters.CardCount / 4; i++)
                {
                    switch (i % 4)
                    {
                        // 左 下
                        case 0 :
                            Vector3 v3Middle = new Vector3();
                            v3Middle = transStartBuildPos.position - new Vector3(groupTimes * MahjongParameters.CardsBuildWeight, 0, 0);
                            v3RestBuildPos.Add(v3Middle);
                            break;
                        // 左 上
                        case 1 :
                            Vector3 v3Up = new Vector3();
                            v3Up = transStartBuildPos.position + new Vector3(-groupTimes * MahjongParameters.CardsBuildWeight, MahjongParameters.CardsBuildHeight, 0);
                            v3RestBuildPos.Add(v3Up);
                            break;
                        // 右 下
                        case 2 :
                            Vector3 v3Right = new Vector3();
                            v3Right = transStartBuildPos.position + new Vector3((groupTimes + 1) * MahjongParameters.CardsBuildWeight, 0, 0);
                            v3RestBuildPos.Add(v3Right);
                            break;
                        // 右 上
                        case 3 :
                            Vector3 v3UpRight = new Vector3();
                            v3UpRight = transStartBuildPos.position + new Vector3((groupTimes + 1) * MahjongParameters.CardsBuildWeight, MahjongParameters.CardsBuildHeight, 0);
                            v3RestBuildPos.Add(v3UpRight);
                            groupTimes++;
                            break;
                    }
                }
                break;
            // left wall
            case 1 :
                _v3BuiltWallTargetPos = Vector3.left * 50f;
                for (int i = 0; i < MahjongParameters.CardCount / 4; i++)
                {
                    switch (i % 4)
                    {
                        // 近 下
                        case 0 :
                            Vector3 v3Middle = new Vector3();
                            v3Middle = transStartBuildPos.position - new Vector3(0, 0, groupTimes * MahjongParameters.CardsBuildWeight);
                            v3RestBuildPos.Add(v3Middle);
                            break;
                        // 近 上
                        case 1 :
                            Vector3 v3Up = new Vector3();
                            v3Up = transStartBuildPos.position + new Vector3(0, MahjongParameters.CardsBuildHeight, -groupTimes * MahjongParameters.CardsBuildWeight);
                            v3RestBuildPos.Add(v3Up);
                            break;
                        // 远 下
                        case 2 :
                            Vector3 v3Right = new Vector3();
                            v3Right = transStartBuildPos.position + new Vector3(0, 0, (groupTimes + 1) * MahjongParameters.CardsBuildWeight);
                            v3RestBuildPos.Add(v3Right);
                            break;
                        // 远 上
                        case 3 :
                            Vector3 v3UpRight = new Vector3();
                            v3UpRight = transStartBuildPos.position + new Vector3(0, MahjongParameters.CardsBuildHeight, (groupTimes + 1) * MahjongParameters.CardsBuildWeight);
                            v3RestBuildPos.Add(v3UpRight);
                            groupTimes++;
                            break;
                    }
                }
                break;
            // top wall
            case 2 :
                _v3BuiltWallTargetPos = Vector3.forward * 50f;
                for (int i = 0; i < MahjongParameters.CardCount / 4; i++)
                {
                    switch (i % 4)
                    {
                        // 左 下
                        case 0 :
                            Vector3 v3Middle = new Vector3();
                            v3Middle = transStartBuildPos.position - new Vector3(groupTimes * MahjongParameters.CardsBuildWeight, 0, 0);
                            v3RestBuildPos.Add(v3Middle);
                            break;
                        // 左 上
                        case 1 :
                            Vector3 v3Up = new Vector3();
                            v3Up = transStartBuildPos.position + new Vector3(-groupTimes * MahjongParameters.CardsBuildWeight, MahjongParameters.CardsBuildHeight, 0);
                            v3RestBuildPos.Add(v3Up);
                            break;
                        // 右 下
                        case 2 :
                            Vector3 v3Right = new Vector3();
                            v3Right = transStartBuildPos.position + new Vector3((groupTimes + 1) * MahjongParameters.CardsBuildWeight, 0, 0);
                            v3RestBuildPos.Add(v3Right);
                            break;
                        // 右 上
                        case 3 :
                            Vector3 v3UpRight = new Vector3();
                            v3UpRight = transStartBuildPos.position + new Vector3((groupTimes + 1) * MahjongParameters.CardsBuildWeight, MahjongParameters.CardsBuildHeight, 0);
                            v3RestBuildPos.Add(v3UpRight);
                            groupTimes++;
                            break;
                    }
                }
                break;
            // right wall
            case 3 :
                _v3BuiltWallTargetPos = Vector3.right * 50f;
                for (int i = 0; i < MahjongParameters.CardCount / 4; i++)
                {
                    switch (i % 4)
                    {
                        // 近 下
                        case 0 :
                            Vector3 v3Middle = new Vector3();
                            v3Middle = transStartBuildPos.position - new Vector3(0, 0, groupTimes * MahjongParameters.CardsBuildWeight);
                            v3RestBuildPos.Add(v3Middle);
                            break;
                        // 近 上
                        case 1 :
                            Vector3 v3Up = new Vector3();
                            v3Up = transStartBuildPos.position + new Vector3(0, MahjongParameters.CardsBuildHeight, -groupTimes * MahjongParameters.CardsBuildWeight);
                            v3RestBuildPos.Add(v3Up);
                            break;
                        // 远 下
                        case 2 :
                            Vector3 v3Right = new Vector3();
                            v3Right = transStartBuildPos.position + new Vector3(0, 0, (groupTimes + 1) * MahjongParameters.CardsBuildWeight);
                            v3RestBuildPos.Add(v3Right);
                            break;
                        // 远 上
                        case 3 :
                            Vector3 v3UpRight = new Vector3();
                            v3UpRight = transStartBuildPos.position + new Vector3(0, MahjongParameters.CardsBuildHeight, (groupTimes + 1) * MahjongParameters.CardsBuildWeight);
                            v3RestBuildPos.Add(v3UpRight);
                            groupTimes++;
                            break;
                    }
                }
                break;
        }

        if (v3RestBuildPos.Count > 0)
        {
            _canBuildHere = true;
        }
    }
    
    public void BuildWall(Card card)
    {
        if (_canBuildHere)
        {
            card.v3BuildPos = v3RestBuildPos[0];
            card.v3BuildRot = transStartBuildPos.eulerAngles;
            v3RestBuildPos.RemoveAt(0);
            card.transform.SetParent(transform);
            card.buildState = 1;
            // 检测是否能继续在此build
            if (v3RestBuildPos.Count <= 0)
            {
                _canBuildHere = false;
                _cardsContainer.ifWallsBuilt[_wallNum] = true;
            }
        }
    }
}
