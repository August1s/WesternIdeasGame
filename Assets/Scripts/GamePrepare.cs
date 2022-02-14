using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// GamePrepare应该挂载到场景中的GamePrepareObj中
/// 从本地读取卡牌信息，生成一个总卡牌库
/// 从本卡牌库中进行读取，生成初始背包和初始搜索区
/// </summary>

public class GamePrepare : MonoBehaviour
{
    public TextAsset cardData;      // 本地存储的卡牌数据

    public GameObject arenaPrefab;
    public void LoadCardData()
    {
        string[] dataRow = cardData.text.Split('\n');
        foreach (var row in dataRow)
        {
            if (row == "")
                continue;
            string[] rowArray = row.Split(',');

            if (rowArray[0] == "id")
                continue;

            Card newCard = new Card();
            newCard.id = int.Parse(rowArray[0]);
            newCard.name = rowArray[1];
            newCard.funcDescription = rowArray[2];

            newCard.lifeValueCost = int.Parse(rowArray[3]);
            newCard.actionValueCost = int.Parse(rowArray[4]);
            newCard.spiritValueCost = int.Parse(rowArray[5]);

            newCard.searchValuePayment = int.Parse(rowArray[6]);
            newCard.lifeValuePayment = int.Parse(rowArray[7]);
            newCard.actionValuePayment = int.Parse(rowArray[8]);
            newCard.spiritValuePayment = int.Parse(rowArray[9]);


            newCard.haveAttributeEffect = bool.Parse(rowArray[10]);
            newCard.lifeValueEffect = int.Parse(rowArray[11]);
            newCard.actionValueEffect = int.Parse(rowArray[12]);
            newCard.spiritValueEffect = int.Parse(rowArray[13]);
            newCard.searchValueEffect = int.Parse(rowArray[14]);

            newCard.haveHandCardEffect = bool.Parse(rowArray[15]);
            newCard.drawNewCard = int.Parse(rowArray[16]);

            newCard.haveEnemyEffect = bool.Parse(rowArray[17]);
            newCard.damageEffectToEnemy = int.Parse(rowArray[18]);

            //Debug.Log(newCard.name);
            Global.cardDataBase.Add(newCard);
        }
    }





    public void InitArena()
    {
        GameObject.Instantiate(arenaPrefab);
    }
    
    

    void Awake()
    {
        LoadCardData();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("game prepare");
        //InitArena();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
