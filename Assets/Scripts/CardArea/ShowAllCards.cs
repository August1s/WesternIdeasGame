using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



// 测试卡牌实例化的脚本
// 将Card类的数据传入card prefab中，然后实例化到UI控件中
// 实例化得到所有的卡

public class ShowAllCards : MonoBehaviour
{
    public GameObject cardPrefab;
    public GameObject CardGroupLayout;     

    public void ShowAllCardsInGrid()
    {
        foreach (var item in Global.cardDataBase)
        {
            GameObject newCard = GameObject.Instantiate(cardPrefab, CardGroupLayout.transform);
            newCard.GetComponent<CardDisplay>().mainCard = item;
            newCard.GetComponent<CardEffect>().mainCard = item;
            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ShowAllCardsInGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
