using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders;

public class MahjongGameManager : MonoBehaviour
{
    private GameObject _objPrefab;
    private Transform _transWashCards;
    // Start is called before the first frame update
    void Start()
    {
        _transWashCards = GameObject.Find("CardsContainer").transform.Find("WashCards");
        _objPrefab = Resources.Load<GameObject>("Mahjong/Prefabs/Card");
        InitCardsMatrix(MahjongParameters.CardCount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitCardsMatrix(int initNum)
    {
        int counter = 0;
        for (float z = 26.73468f; z >= -26.73468f; z -= 3.81924f)
        {
            for (float x = -34.1f; x <= 34.1f; x += 6.2f)
            {
                var position = new Vector3(x, 0, z);
                var instantiation = Instantiate(_objPrefab, position, Quaternion.identity);
                instantiation.transform.SetParent(_transWashCards);
                instantiation.name = MahjongNamesData.MahjongNames[counter];
                CardsContainer.TransInitedCards.Add(instantiation.transform);
                counter++;
                if (counter >= initNum)
                {
                    return;
                }
            }
        }
    }
}
