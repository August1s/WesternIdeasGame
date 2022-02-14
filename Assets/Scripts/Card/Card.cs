// 数值化后的卡牌类
// Card类的成员应该与CardLoadFromLocal过程相一致

public class Card
{
    // 显示
    public int id;
    public string name;
    public string funcDescription;

    // 使用代价
    public int lifeValueCost;   // 生命值
    public int actionValueCost; // 行动值
    public int spiritValueCost;  // 精神值
    //public int searchValueCost; // 搜索值

    // 搜索代价
    public int lifeValuePayment;   
    public int actionValuePayment; 
    public int spiritValuePayment;  
    public int searchValuePayment; 


    // 数值化效果
    // 对玩家的属性值的影响
    public bool haveAttributeEffect;
    public int lifeValueEffect;     // 对自身生命值的影响
    public int actionValueEffect;   // 对自身行动值的影响
    public int spiritValueEffect;    // 对自身精神值的影响
    public int searchValueEffect;   // 对自身搜索值的影响

    // 对手牌的影响
    public bool haveHandCardEffect;
    public int drawNewCard;      // 抽卡效果，数值化为抽卡数量

    // 对敌方属性的影响
    public bool haveEnemyEffect;
    public int damageEffectToEnemy; // 伤害值，对敌方生命的影响


    

}

