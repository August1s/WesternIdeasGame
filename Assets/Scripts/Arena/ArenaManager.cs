using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 记录战局在加载和回合进行中的状态量
/// 
/// 后期可能会在战局中加入控制其他跳转UI的状态量
/// </summary>
public enum GamePhase
{
    GameLoad,           // 游戏加载阶段，加载玩家数据，敌人数据等

    PlayerRoundPrepare, // 玩家回合开始的准备阶段，加载手牌和搜索区牌等(后期可能会结算一些来自上回合的增益？)
    PlayerRoundBegin,   // 玩家回合开始
    PlayerRoundEnd,     // 玩家回合结束阶段，清空手牌和搜索区牌等

    EnemyRoundBegin,        // 敌人回合开始阶段

    GameEnd             // 游戏结束阶段
}



/// <summary>
/// ArenaManager应该管理整个战局的数值状态
/// </summary>
public class ArenaManager : MonoSingleton<ArenaManager>
{
    public GamePhase gamePhase = GamePhase.GameLoad;
    
    public Player player = new Player();

    public Enemy enemy = new Enemy();
    
    // 公共搜索区中的卡牌
    public List<Card> cardsInSearchArea = new List<Card>();

    // 玩家的手牌
    public List<Card> cardsInHandArea = new List<Card>();

    // 当前回合打出的牌
    public List<Card> dropCards = new List<Card>();

    // 弃牌区中的牌
    public List<Card> discardArea = new List<Card>();


    // 洗牌
    private void Shuffle(List<Card> list)
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

    #region 回合数据初始化

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
        Shuffle(player.cardsInPlayerBag);

        foreach (var item in Global.cardDataBase)
        {
            if (item.id != 0 && item.id != 1)
                player.cardsInSearchPool.Add(item, 0);
        }

        PlayerStatementUIEventManager.instance.PlayerValueChangeEvent.Invoke(player);
    }

    /// <summary>
    /// 生成搜索区牌
    /// 
    /// 后期需要加入概率，需要根据精神值进行生成对应的搜索区牌
    /// </summary>
    public void InitSearchAreaCards()
    {
        for (int i = 0; i < 5; i++)
        {
            cardsInSearchArea.Add(player.cardsInSearchPool.Keys.ToArray()[Random.Range(0, player.cardsInSearchPool.Count)]);
        }

        CardAreaUIEventManager.instance.SearchCardsAreaRefreshEvent.Invoke();
    }

    /// <summary>
    /// 手牌初始化，手牌来自背包，当背包数量<5时，将背包里的牌全部放到手牌中
    /// 手牌初始化后期可能会被调整到回合开始阶段
    /// </summary>
    public void InitHandAreaCards()
    {
        int cardnum = Mathf.Min(5, player.cardsInPlayerBag.Count);
        for (int i = 0; i < cardnum; i++)
        {
            cardsInHandArea.Add(player.cardsInPlayerBag[i]);
        }
        player.cardsInPlayerBag.RemoveRange(0, cardnum);

        CardAreaUIEventManager.instance.HandCardsAreaRefreshEvent.Invoke();
        CardAreaUIEventManager.instance.OtherCardAreaCountUpdateEvent.Invoke(player.cardsInPlayerBag.Count, discardArea.Count);
    }

    /// <summary>
    /// 初始化敌人
    /// </summary>
    public void InitEnemyData()
    {
        enemy.name = "狂热的村民";
        enemy.lifeValue = 20;

        EnemyUIEventManager.instance.EnemyValueChangeEvent.Invoke(enemy);
    }

    #endregion

    /// <summary>
    /// 结束玩家回合，由button空间点击事件回调，应该在玩家的回合开始后被执行
    /// 玩家回合在结束后应该清空场上的三个区域
    /// </summary>
    public void EndPlayerRound()
    {
        if (gamePhase != GamePhase.PlayerRoundBegin)
            return;
        
        gamePhase = GamePhase.PlayerRoundEnd;

        foreach (var card in dropCards)
        {
            discardArea.Add(card);
        }
        dropCards.Clear();
        foreach (var card in cardsInHandArea)
        {
            discardArea.Add(card);
        }
        cardsInHandArea.Clear();
        cardsInSearchArea.Clear();

        CardAreaUIEventManager.instance.CardRemoveAllEvent.Invoke();
        CardAreaUIEventManager.instance.OtherCardAreaCountUpdateEvent.Invoke(player.cardsInPlayerBag.Count, discardArea.Count);

        Debug.Log("----玩家回合结束----");

        BeginEnemyRound();
    }

    /// <summary>
    /// 开始敌方回合
    /// </summary>
    public void BeginEnemyRound()
    {
        if (gamePhase == GamePhase.PlayerRoundEnd)
        {
            gamePhase = GamePhase.EnemyRoundBegin;
            Debug.Log("----敌方回合开始----");
            // 敌人的行为
            Debug.Log("----敌方回合结束----");

            PreparePlayerRound();
        }
    }

    /// <summary>
    /// 玩家回合准备，应该在敌人回合结束后执行
    /// 
    /// 回合开始前，搜索区生成新搜索牌，手牌区生成新手牌，行动值刷新至上限
    /// 
    /// 后期可能会加入一些跨回合的收益
    /// </summary>
    public void PreparePlayerRound()
    {
        if (gamePhase != GamePhase.EnemyRoundBegin && gamePhase != GamePhase.GameLoad)
            return;
        
        gamePhase = GamePhase.PlayerRoundPrepare;

        // 更新行动值
        player.actionValue = player.maxActionValue;
        PlayerStatementUIEventManager.instance.PlayerValueChangeEvent.Invoke(player);
            

        // 生成手牌
        int remainingNumOfBag = player.cardsInPlayerBag.Count;
        if (remainingNumOfBag > 5)
        {
            for (int i = 0; i < 5; i++)
            {
                cardsInHandArea.Add(player.cardsInPlayerBag[i]);
            }
            player.cardsInPlayerBag.RemoveRange(0, 5);
        }
        else
        {
            // 当牌堆的卡牌不够抽取时，洗切废牌堆，形成新的牌堆，然后继续抽取卡牌
            for (int i = 0; i < remainingNumOfBag; i++)
            {
                cardsInHandArea.Add(player.cardsInPlayerBag[i]);
            }
            player.cardsInPlayerBag.Clear();

            if (discardArea.Count > 0)
            {
                Shuffle(discardArea);
                int temp = discardArea.Count;
                if(temp > 5 - remainingNumOfBag)
                {
                    for (int i = 0; i < 5 - remainingNumOfBag; i++)
                    {
                        cardsInHandArea.Add(discardArea[i]);
                    }
                    for(int i = 0; i < temp - 5 + remainingNumOfBag; i++)
                    {
                        player.cardsInPlayerBag.Add(discardArea[i]);
                    }
                }
                else
                {
                    for (int i = 0; i < temp; i++)
                    {
                        cardsInHandArea.Add(discardArea[i]);
                    }
                }
                discardArea.Clear();
            }
        }
        CardAreaUIEventManager.instance.HandCardsAreaRefreshEvent.Invoke();
        CardAreaUIEventManager.instance.OtherCardAreaCountUpdateEvent.Invoke(player.cardsInPlayerBag.Count, discardArea.Count);


        // 生成搜索区牌
        for (int i = 0; i < 5; i++)
        {
            cardsInSearchArea.Add(player.cardsInSearchPool.Keys.ToArray()[Random.Range(0, player.cardsInSearchPool.Count)]);
        }
        CardAreaUIEventManager.instance.SearchCardsAreaRefreshEvent.Invoke();


        Debug.Log("----玩家回合准备完成----");

        gamePhase = GamePhase.PlayerRoundBegin;
        Debug.Log("----玩家回合开始----");

    }




    void Start()
    {
        gamePhase = GamePhase.GameLoad;
        // 玩家背包和卡库加载顺序应该保证在手牌和搜索区加载之前
        InitPlayerData();
        InitEnemyData();
        Debug.Log("----对战准备完成----");

        //InitSearchAreaCards();
        //InitHandAreaCards();
        PreparePlayerRound();
        
    }
}

