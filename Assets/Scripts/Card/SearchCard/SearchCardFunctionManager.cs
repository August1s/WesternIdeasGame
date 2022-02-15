using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;




/// <summary>
/// ������������ƺ󣬿���Ч���ĺ����ϼ���������Ա仯��UI�仯��
/// </summary>
public class SearchCardFunctionManager : MonoSingleton<SearchCardFunctionManager>
{
    public UnityEvent<GameObject, Card> SearchAreaCardClickEvent = new UnityEvent<GameObject, Card>();


    public void CardEffectExecution(GameObject cardObj, Card mainCard)
    {
        // �Ƿ����������������еĿ�
        if(mainCard.searchValuePayment <= ArenaManager.instance.player.searchValue &&
            mainCard.lifeValuePayment <= ArenaManager.instance.player.lifeValue &&
            mainCard.actionValuePayment <= ArenaManager.instance.player.actionValue &&
            mainCard.spiritValuePayment <= ArenaManager.instance.player.spiritValue)
        {
            ArenaManager.instance.player.searchValue -= mainCard.searchValuePayment;
            ArenaManager.instance.player.lifeValue -= mainCard.lifeValuePayment;
            ArenaManager.instance.player.actionValue -= mainCard.actionValuePayment;
            ArenaManager.instance.player.spiritValue -= mainCard.spiritValuePayment;
            PlayerStatementUIEventManager.instance.PlayerValueChangeEvent.Invoke(ArenaManager.instance.player);
            
            ArenaManager.instance.player.cardsInSearchPool[mainCard] += 1;
            MoveCardToHandArea(cardObj, mainCard);
        }
    }


    void Awake()
    { 
        SearchAreaCardClickEvent.AddListener(CardEffectExecution);
    }

    
    
    // ���������ڱ������Ӧ�ñ������������뱳����
    public void MoveCardToHandArea(GameObject cardObj, Card mainCard)
    {
        ArenaManager.instance.cardsInSearchArea.Remove(mainCard);
        CardAreaUIEventManager.instance.CardRemoveEvent.Invoke(cardObj);


        ArenaManager.instance.cardsInHandArea.Add(mainCard);
        CardAreaUIEventManager.instance.HandCardAddEvent.Invoke(mainCard);

    }
}
