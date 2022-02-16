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
    public TextAsset handCardData;      // 本地存储的卡牌数据
    public TextAsset enemyCardData;     // 本地存储的敌人卡牌数据

    public GameObject arenaPrefab;
    public void LoadCardData()
    {
        string[] dataRow = handCardData.text.Split('\n');
        foreach (var row in dataRow)
        {
            if (row == "")
                continue;
            string[] rowArray = row.Split(',');

            if (rowArray[0] == "id")
                continue;

            Card newcard = new Card();
            newcard.id = int.Parse(rowArray[0]);
            newcard.name = rowArray[1];
            newcard.funcDescription = rowArray[2];

            newcard.lifeValueCost = int.Parse(rowArray[3]);
            newcard.actionValueCost = int.Parse(rowArray[4]);
            newcard.spiritValueCost = int.Parse(rowArray[5]);

            newcard.searchValuePayment = int.Parse(rowArray[6]);
            newcard.lifeValuePayment = int.Parse(rowArray[7]);
            newcard.actionValuePayment = int.Parse(rowArray[8]);
            newcard.spiritValuePayment = int.Parse(rowArray[9]);


            newcard.haveAttributeEffect = bool.Parse(rowArray[10]);
            newcard.lifeValueEffect = int.Parse(rowArray[11]);
            newcard.actionValueEffect = int.Parse(rowArray[12]);
            newcard.spiritValueEffect = int.Parse(rowArray[13]);
            newcard.searchValueEffect = int.Parse(rowArray[14]);

            newcard.haveHandCardEffect = bool.Parse(rowArray[15]);
            newcard.drawNewCard = int.Parse(rowArray[16]);

            newcard.haveEnemyEffect = bool.Parse(rowArray[17]);
            newcard.damageEffectToEnemy = int.Parse(rowArray[18]);

            //Debug.Log(newCard.name);
            Global.cardDataBase.Add(newcard);
        }
    }


    public void LoadEnemyCardData()
    {
        string[] dataRow = enemyCardData.text.Split('\n');
        foreach (var row in dataRow)
        {
            if (row == "")
                continue;
            string[] rowArray = row.Split(',');
            if (rowArray[0] == "id")
                continue;

            EnemyCard newcard = new EnemyCard();
            newcard.id = int.Parse(rowArray[0]);
            newcard.name = rowArray[1];
            newcard.funcDescription = rowArray[2];
            newcard.user = rowArray[3];

            newcard.lifeValueEffect = int.Parse(rowArray[4]);
            newcard.actionValueEffect = int.Parse(rowArray[5]);
            newcard.spiritValueEffect = int.Parse(rowArray[6]);
            newcard.searchValueEffect = int.Parse(rowArray[7]);
            newcard.lifeMaxValueEffect = int.Parse(rowArray[8]);
            newcard.actionMaxValueEffect = int.Parse(rowArray[9]);
            newcard.spiritMaxValueEffect = int.Parse(rowArray[10]);

            newcard.selfLifeValueEffect = int.Parse(rowArray[11]);

            if(!Global.enemyCardDataBase.ContainsKey(newcard.user))
            {
                Global.enemyCardDataBase.Add(newcard.user, new List<EnemyCard>());
            }
            Global.enemyCardDataBase[newcard.user].Add(newcard);
        }
    }





    public void InitArena()
    {
        GameObject.Instantiate(arenaPrefab);
    }
    
    

    void Awake()
    {
        LoadCardData();
        LoadEnemyCardData();
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
