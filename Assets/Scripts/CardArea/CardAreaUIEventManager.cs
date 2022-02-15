using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;



/// <summary>
/// CardAreaUIEventManager.cs收集了各个区域卡牌在UI层面的变化方式
/// 在三个store中的内容改变的时候，对应的UI也应该以事件的方式刷新
/// 
/// 针对不同卡牌区内容的不同改变方式（增加，减少等）进行不同的更新做法
/// </summary>
public class CardAreaUIEventManager : MonoSingleton<CardAreaUIEventManager>
{
    public GameObject handCardPrefab;
    public GameObject handCardStore;

    public GameObject searchCardPrefab;
    public GameObject searchCardStore;

    public GameObject dropCardPrefab;
    public GameObject dropCardStore;

    public UnityEvent HandCardsAreaRefreshEvent = new UnityEvent();
    public UnityEvent SearchCardsAreaRefreshEvent = new UnityEvent();
    public UnityEvent DropCardsAreaRefreshEvent = new UnityEvent();

    public UnityEvent<Card> HandCardAddEvent = new UnityEvent<Card>();
    public UnityEvent<Card> DropCardAddEvent = new UnityEvent<Card>();

    public UnityEvent<GameObject> CardRemoveEvent = new UnityEvent<GameObject>();
    public UnityEvent CardRemoveAllEvent = new UnityEvent();

    public Text areaCount;
    public UnityEvent<int, int> OtherCardAreaCountUpdateEvent = new UnityEvent<int, int>();



    private void ClearGridInOneFrame(GameObject store)
    {
        if (store.transform.childCount > 0)
        {
            for (int i = store.transform.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(store.transform.GetChild(i).gameObject);
            }
        }
    }

    /// <summary>
    /// 清除台面上所有的卡牌
    /// </summary>
    private void ClearAllCard()
    {
        for (int i = handCardStore.transform.childCount - 1; i >= 0; i--)
        { 
            Destroy(handCardStore.transform.GetChild(i).gameObject);
        }
        for (int i = dropCardStore.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(dropCardStore.transform.GetChild(i).gameObject);
        }
        for (int i = searchCardStore.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(searchCardStore.transform.GetChild(i).gameObject);
        }

    }

    private void CreateHandCardPrefabInGrid()
    {
        ClearGridInOneFrame(handCardStore);

        foreach (var item in ArenaManager.instance.cardsInHandArea)
        {
            GameObject newCard = GameObject.Instantiate(handCardPrefab, handCardStore.transform);
            newCard.GetComponent<CardDisplay>().mainCard = item;
            newCard.GetComponent<CardEffect>().mainCard = item;
        }
    }

    private void CreateSearchCardPrefabInGrid()
    {
        ClearGridInOneFrame(searchCardStore);

        foreach (var item in ArenaManager.instance.cardsInSearchArea)
        {
            GameObject newCard = GameObject.Instantiate(searchCardPrefab, searchCardStore.transform);
            newCard.GetComponent<CardDisplay>().mainCard = item;
            newCard.GetComponent<SearchAreaCardEffect>().mainCard = item;
        }
    }

    private void CreateDropCardPrefabInGrid()
    {
        ClearGridInOneFrame(dropCardStore);

        foreach (var item in ArenaManager.instance.dropCards)
        {
            GameObject newCard = GameObject.Instantiate(dropCardPrefab, dropCardStore.transform);
            newCard.GetComponent<DropCardDisplay>().mainCard = item;
        }
    }

    private void AddHandCardPrefab(Card card)
    {
        GameObject newCard = GameObject.Instantiate(handCardPrefab, handCardStore.transform);
        newCard.GetComponent<CardDisplay>().mainCard = card;
        newCard.GetComponent<CardEffect>().mainCard = card;
    }

    private void AddDropCardPrefab(Card card)
    {
        GameObject newCard = GameObject.Instantiate(dropCardPrefab, dropCardStore.transform);
        newCard.GetComponent<DropCardDisplay>().mainCard = card;
    }

    private void RemoveCardPrefab(GameObject obj)
    {
        Destroy(obj);
    }

    private void ShowCount(int bagcount, int discardcount)
    {
        areaCount.text = "背包剩余数量: " + bagcount.ToString() + '\n'
            + "弃牌区数量: " + discardcount.ToString();
    }


    void Awake()
    {
        HandCardsAreaRefreshEvent.AddListener(CreateHandCardPrefabInGrid);
        SearchCardsAreaRefreshEvent.AddListener(CreateSearchCardPrefabInGrid);
        DropCardsAreaRefreshEvent.AddListener(CreateDropCardPrefabInGrid);

        HandCardAddEvent.AddListener(AddHandCardPrefab);
        DropCardAddEvent.AddListener(AddDropCardPrefab);

        CardRemoveEvent.AddListener(RemoveCardPrefab);
        CardRemoveAllEvent.AddListener(ClearAllCard);

        OtherCardAreaCountUpdateEvent.AddListener(ShowCount);
    }
}
