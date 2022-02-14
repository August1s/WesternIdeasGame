using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ArenaManager应该管理整个战局的数值状态
/// </summary>
public class ArenaManager : MonoSingleton<ArenaManager>
{
    public Player player = new Player();

    public Enemy enemy = new Enemy();
    
    // 公共搜索区中的卡牌
    public List<Card> cardsInSearchArea = new List<Card>();

    // 玩家的手牌
    public List<Card> cardsInHandArea = new List<Card>();

    // 当前回合打出的牌
    public List<Card> dropCards = new List<Card>();



    /// <summary>
    /// 初始化玩家，初始化玩家背包用于生成手牌，初始化玩家搜索池用于生成搜索区
    /// 
    /// 玩家的搜索区和背包数据暂时通过Global中的卡库进行生成
    /// 玩家在后期可能需要本地序列化进行读取
    /// </summary>
    public void InitPlayerData()
    {
        player.name = "A";
        player.lifeValue = 50;
        player.actionValue = 5;
        player.spiritValue = 100;
        player.searchValue = 5;
        player.maxLifeValue = 50;
        player.maxActionValue = 5;
        player.maxSpiritValue = 100;
        

        for (int i = 0; i < 5; i++)
        {
            player.cardsInPlayerBag.Add(Global.cardDataBase[0]);
            player.cardsInPlayerBag.Add(Global.cardDataBase[1]);
        }

        foreach (var item in Global.cardDataBase)
        {
            if (item.id != 0 && item.id != 1)
                player.cardsInSearchPool.Add(item, 0);
        }
    }

    /// <summary>
    /// 生成搜索区牌
    /// 
    /// 后期需要加入概率和洗牌，需要根据精神值进行生成对应的搜索区牌
    /// </summary>
    public void InitSearchAreaCards()
    {
        for (int i = 0; i < 5; i++)
        {
            cardsInSearchArea.Add(player.cardsInSearchPool.Keys.ToArray()[Random.Range(0, player.cardsInSearchPool.Count)]);
        }
    }

    /// <summary>
    /// 手牌初始化，手牌来自背包，当背包数量<5时，将背包里的牌全部放到手牌中
    /// 手牌初始化后期可能会被调整到回合开始阶段
    /// </summary>
    public void InitHandAreaCards()
    {
        Shuffle(player.cardsInPlayerBag);

        int cardnum = Mathf.Min(5, player.cardsInPlayerBag.Count);
        for (int i = 0; i < cardnum; i++)
        {
            cardsInHandArea.Add(player.cardsInPlayerBag[i]);
        }
        player.cardsInPlayerBag.RemoveRange(0, cardnum);
    }

    // 洗牌
    public void Shuffle(List<Card> list)
    {
        Card temp;

        for (int i = 0; i < list.Count; i++)
        {
            int rand = Random.Range(0, i + 1);
            temp = list[rand];
            list[rand] = list[i];
            list[i] = temp;
        }
    }

    /// <summary>
    /// 初始化敌人
    /// </summary>
    public void InitEnemyData()
    {
        enemy.name = "狂热的村民";
        enemy.lifeValue = 20;
    }



    void Start()
    {
        // 玩家背包和卡库加载顺序应该保证在手牌和搜索区加载之前
        InitPlayerData();
        PlayerStatementUIEventManager.instance.PlayerValueChangeEvent.Invoke(player);
        InitEnemyData();
        EnemyUIEventManager.instance.EnemyValueChangeEvent.Invoke(enemy);

        InitHandAreaCards();
        CardAreaUIEventManager.instance.HandCardsAreaRefreshEvent.Invoke();
        InitSearchAreaCards();
        CardAreaUIEventManager.instance.SearchCardsAreaRefreshEvent.Invoke();
    }
}
