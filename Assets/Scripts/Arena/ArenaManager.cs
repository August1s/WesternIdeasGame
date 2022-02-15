using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ��¼ս���ڼ��غͻغϽ����е�״̬��
/// 
/// ���ڿ��ܻ���ս���м������������תUI��״̬��
/// </summary>
public enum GamePhase
{
    GameLoad,           // ��Ϸ���ؽ׶Σ�����������ݣ��������ݵ�

    PlayerRoundPrepare, // ��һغϿ�ʼ��׼���׶Σ��������ƺ��������Ƶ�(���ڿ��ܻ����һЩ�����ϻغϵ����棿)
    PlayerRoundBegin,   // ��һغϿ�ʼ
    PlayerRoundEnd,     // ��һغϽ����׶Σ�������ƺ��������Ƶ�

    EnemyRoundBegin,        // ���˻غϿ�ʼ�׶�

    GameEnd             // ��Ϸ�����׶�
}



/// <summary>
/// ArenaManagerӦ�ù�������ս�ֵ���ֵ״̬
/// </summary>
public class ArenaManager : MonoSingleton<ArenaManager>
{
    public GamePhase gamePhase = GamePhase.GameLoad;
    
    public Player player = new Player();

    public Enemy enemy = new Enemy();
    
    // �����������еĿ���
    public List<Card> cardsInSearchArea = new List<Card>();

    // ��ҵ�����
    public List<Card> cardsInHandArea = new List<Card>();

    // ��ǰ�غϴ������
    public List<Card> dropCards = new List<Card>();

    // �������е���
    public List<Card> discardArea = new List<Card>();


    // ϴ��
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

    #region �غ����ݳ�ʼ��

    /// <summary>
    /// ��ʼ����ң���ʼ����ұ��������������ƣ���ʼ�������������������������
    /// 
    /// ��ҵ��������ͱ���������ʱͨ��Global�еĿ����������
    /// ����ں��ڿ�����Ҫ�������л����ж�ȡ
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
    /// ������������
    /// 
    /// ������Ҫ������ʣ���Ҫ���ݾ���ֵ�������ɶ�Ӧ����������
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
    /// ���Ƴ�ʼ�����������Ա���������������<5ʱ�������������ȫ���ŵ�������
    /// ���Ƴ�ʼ�����ڿ��ܻᱻ�������غϿ�ʼ�׶�
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
    /// ��ʼ������
    /// </summary>
    public void InitEnemyData()
    {
        enemy.name = "���ȵĴ���";
        enemy.lifeValue = 20;

        EnemyUIEventManager.instance.EnemyValueChangeEvent.Invoke(enemy);
    }

    #endregion

    /// <summary>
    /// ������һغϣ���button�ռ����¼��ص���Ӧ������ҵĻغϿ�ʼ��ִ��
    /// ��һغ��ڽ�����Ӧ����ճ��ϵ���������
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

        Debug.Log("----��һغϽ���----");

        BeginEnemyRound();
    }

    /// <summary>
    /// ��ʼ�з��غ�
    /// </summary>
    public void BeginEnemyRound()
    {
        if (gamePhase == GamePhase.PlayerRoundEnd)
        {
            gamePhase = GamePhase.EnemyRoundBegin;
            Debug.Log("----�з��غϿ�ʼ----");
            // ���˵���Ϊ
            Debug.Log("----�з��غϽ���----");

            PreparePlayerRound();
        }
    }

    /// <summary>
    /// ��һغ�׼����Ӧ���ڵ��˻غϽ�����ִ��
    /// 
    /// �غϿ�ʼǰ�������������������ƣ����������������ƣ��ж�ֵˢ��������
    /// 
    /// ���ڿ��ܻ����һЩ��غϵ�����
    /// </summary>
    public void PreparePlayerRound()
    {
        if (gamePhase != GamePhase.EnemyRoundBegin && gamePhase != GamePhase.GameLoad)
            return;
        
        gamePhase = GamePhase.PlayerRoundPrepare;

        // �����ж�ֵ
        player.actionValue = player.maxActionValue;
        PlayerStatementUIEventManager.instance.PlayerValueChangeEvent.Invoke(player);
            

        // ��������
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
            // ���ƶѵĿ��Ʋ�����ȡʱ��ϴ�з��ƶѣ��γ��µ��ƶѣ�Ȼ�������ȡ����
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


        // ������������
        for (int i = 0; i < 5; i++)
        {
            cardsInSearchArea.Add(player.cardsInSearchPool.Keys.ToArray()[Random.Range(0, player.cardsInSearchPool.Count)]);
        }
        CardAreaUIEventManager.instance.SearchCardsAreaRefreshEvent.Invoke();


        Debug.Log("----��һغ�׼�����----");

        gamePhase = GamePhase.PlayerRoundBegin;
        Debug.Log("----��һغϿ�ʼ----");

    }




    void Start()
    {
        gamePhase = GamePhase.GameLoad;
        // ��ұ����Ϳ������˳��Ӧ�ñ�֤�����ƺ�����������֮ǰ
        InitPlayerData();
        InitEnemyData();
        Debug.Log("----��ս׼�����----");

        //InitSearchAreaCards();
        //InitHandAreaCards();
        PreparePlayerRound();
        
    }
}

