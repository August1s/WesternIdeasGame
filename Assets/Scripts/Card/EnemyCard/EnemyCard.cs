using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCard
{
    public int id;
    public string name;
    public string funcDescription;

    public string user; // 敌方卡牌使用者

    // 对玩家属性值的影响
    public int lifeValueEffect;     
    public int actionValueEffect;   
    public int spiritValueEffect;    
    public int searchValueEffect;

    // 对玩家属性上限值的影响
    public int lifeMaxValueEffect;     
    public int actionMaxValueEffect;   
    public int spiritMaxValueEffect;

    // 自身属性的影响
    public int selfLifeValueEffect;
}
