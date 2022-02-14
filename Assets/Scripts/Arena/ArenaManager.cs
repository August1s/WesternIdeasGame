using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ArenaManagerӦ�ù�������ս�ֵ���ֵ״̬
/// </summary>
public class ArenaManager : MonoSingleton<ArenaManager>
{
    public Player player = new Player();

    public Enemy enemy = new Enemy();
    
    // �����������еĿ���
    public List<Card> cardsInSearchArea = new List<Card>();

    // ��ҵ�����
    public List<Card> cardsInHandArea = new List<Card>();

    // ��ǰ�غϴ������
    public List<Card> dropCards = new List<Card>();



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

        foreach (var item in Global.cardDataBase)
        {
            if (item.id != 0 && item.id != 1)
                player.cardsInSearchPool.Add(item, 0);
        }
    }

    /// <summary>
    /// ������������
    /// 
    /// ������Ҫ������ʺ�ϴ�ƣ���Ҫ���ݾ���ֵ�������ɶ�Ӧ����������
    /// </summary>
    public void InitSearchAreaCards()
    {
        for (int i = 0; i < 5; i++)
        {
            cardsInSearchArea.Add(player.cardsInSearchPool.Keys.ToArray()[Random.Range(0, player.cardsInSearchPool.Count)]);
        }
    }

    /// <summary>
    /// ���Ƴ�ʼ�����������Ա���������������<5ʱ�������������ȫ���ŵ�������
    /// ���Ƴ�ʼ�����ڿ��ܻᱻ�������غϿ�ʼ�׶�
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

    // ϴ��
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
    /// ��ʼ������
    /// </summary>
    public void InitEnemyData()
    {
        enemy.name = "���ȵĴ���";
        enemy.lifeValue = 20;
    }



    void Start()
    {
        // ��ұ����Ϳ������˳��Ӧ�ñ�֤�����ƺ�����������֮ǰ
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
