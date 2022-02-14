using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 玩家类，记录玩家的状态
/// 玩家类作为ArenaManager类的成员，初始化在ArenaManager.cs中
/// 
/// 未来可能会将Player作为序列化存储在本地进行读取
/// </summary>
public class Player 
{
    // 暂时初始化一些默认数值，后期根据需要做出调整
    public string name;
    
    public int lifeValue;
    public int actionValue;
    public int spiritValue;
    public int searchValue;

    public int maxLifeValue;
    public int maxActionValue;
    public int maxSpiritValue;


    // 公共搜索区的卡牌来自搜索卡池，一个玩家对应一个搜索卡池
    // 卡池是一个hashtable，以Card作为key，以玩家购买过的数量作为value
    // value>=3的话，对应的牌移出搜索池，搜索区不再显示
    // 后期精神值系统再做更改
    public Dictionary<Card, int> cardsInSearchPool = new Dictionary<Card, int>();

    // 玩家背包牌组中的牌，用于生成每回合手牌
    // 在对战过程中，cardsInPlayerBag可以用于记录牌堆剩余牌
    public List<Card> cardsInPlayerBag = new List<Card>();
}
