using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 存储一些全局游戏相关的变量
// 有可能需要存入本地？
public static class Global
{
    // 包含所有卡牌种类的库
    public static List<Card> cardDataBase = new List<Card>();  

    // 包含所有敌人卡牌的哈希表，根据敌人名字作为key进行访问
    public static Dictionary<string, List<EnemyCard>> enemyCardDataBase= new Dictionary<string, List<EnemyCard>>();

}