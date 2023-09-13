using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MahjongGameManager : MonoBehaviour
{
    private GameObject _objPrefab;
    private Transform _transCards;
    // Start is called before the first frame update
    void Start()
    {
        _transCards = GameObject.Find("Cards").transform;
        _objPrefab = Resources.Load<GameObject>("Mahjong/Prefabs/Card");
        InitMatrixCards();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitMatrixCards()
    {
        int counter = 0;
        // 144 Cards
        for (float z = 20.05101f; z >= -20.05101f; z -= 3.81924f)
        {
            for (float x = -34.1f; x <= 34.1f; x += 6.2f)
            {
                var position = new Vector3(x, 0, z);
                var instantiation = Instantiate(_objPrefab, position, Quaternion.identity);
                instantiation.transform.SetParent(_transCards);
                instantiation.name = MahjongNamesData.MahjongNames[counter];
                counter++;
                if (counter >= 108)
                {
                    return;
                }
            }
        }
    }
}
