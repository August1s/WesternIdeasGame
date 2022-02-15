using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;



/// <summary>
/// CardAreaUIEventManager.cs�ռ��˸�����������UI����ı仯��ʽ
/// ������store�е����ݸı��ʱ�򣬶�Ӧ��UIҲӦ�����¼��ķ�ʽˢ��
/// 
/// ��Բ�ͬ���������ݵĲ�ͬ�ı䷽ʽ�����ӣ����ٵȣ����в�ͬ�ĸ�������
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
    /// ���̨�������еĿ���
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
        areaCount.text = "����ʣ������: " + bagcount.ToString() + '\n'
            + "����������: " + discardcount.ToString();
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
