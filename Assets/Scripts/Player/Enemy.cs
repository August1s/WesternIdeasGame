using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 敌人类，记录敌人的血量
/// 
/// 后期会记录敌人的卡组和攻击状态
/// </summary>
public class Enemy
{
    public string name;
    public int lifeValue;

    // 敌人所拥有的卡牌
    public List<EnemyCard> cards;
}
