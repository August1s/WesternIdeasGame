using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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




    public void ClearGrid(GameObject store)
    {
        if (store.transform.childCount > 0)
        {
            for (int i = store.transform.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(store.transform.GetChild(i).gameObject);
            }
        }
    }

    public void CreateHandCardPrefabInGrid()
    {
        ClearGrid(handCardStore);

        foreach (var item in ArenaManager.instance.cardsInHandArea)
        {
            GameObject newCard = GameObject.Instantiate(handCardPrefab, handCardStore.transform);
            newCard.GetComponent<CardDisplay>().mainCard = item;
            newCard.GetComponent<CardEffect>().mainCard = item;
        }
    }

    public void CreateSearchCardPrefabInGrid()
    {
        ClearGrid(searchCardStore);

        foreach (var item in ArenaManager.instance.cardsInSearchArea)
        {
            GameObject newCard = GameObject.Instantiate(searchCardPrefab, searchCardStore.transform);
            newCard.GetComponent<CardDisplay>().mainCard = item;
            newCard.GetComponent<SearchAreaCardEffect>().mainCard = item;
        }
    }

    public void CreateDropCardPrefabInGrid()
    {
        ClearGrid(dropCardStore);

        foreach (var item in ArenaManager.instance.dropCards)
        {
            GameObject newCard = GameObject.Instantiate(dropCardPrefab, dropCardStore.transform);
            newCard.GetComponent<DropCardDisplay>().mainCard = item;
        }
    }

    public void AddHandCardPrefab(Card card)
    {
        GameObject newCard = GameObject.Instantiate(handCardPrefab, handCardStore.transform);
        newCard.GetComponent<CardDisplay>().mainCard = card;
        newCard.GetComponent<CardEffect>().mainCard = card;
    }

    public void AddDropCardPrefab(Card card)
    {
        GameObject newCard = GameObject.Instantiate(dropCardPrefab, dropCardStore.transform);
        newCard.GetComponent<DropCardDisplay>().mainCard = card;
    }

    public void RemoveCardPrefab(GameObject obj)
    {
        Destroy(obj);
    }


    void Awake()
    {
        HandCardsAreaRefreshEvent.AddListener(CreateHandCardPrefabInGrid);
        SearchCardsAreaRefreshEvent.AddListener(CreateSearchCardPrefabInGrid);
        DropCardsAreaRefreshEvent.AddListener(CreateDropCardPrefabInGrid);

        HandCardAddEvent.AddListener(AddHandCardPrefab);
        DropCardAddEvent.AddListener(AddDropCardPrefab);

        CardRemoveEvent.AddListener(RemoveCardPrefab);
    }
}
