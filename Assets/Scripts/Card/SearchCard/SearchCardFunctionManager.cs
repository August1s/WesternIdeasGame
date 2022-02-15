using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;




/// <summary>
/// 点击搜索区卡牌后，卡牌效果的函数合集（玩家属性变化，UI变化）
/// </summary>
public class SearchCardFunctionManager : MonoSingleton<SearchCardFunctionManager>
{
    public UnityEvent<GameObject, Card> SearchAreaCardClickEvent = new UnityEvent<GameObject, Card>();


    public void CardEffectExecution(GameObject cardObj, Card mainCard)
    {
        // 是否可以买得起搜索区中的卡
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

    
    
    // 搜索区牌在被点击后应该被放入手牌中与背包中
    public void MoveCardToHandArea(GameObject cardObj, Card mainCard)
    {
        ArenaManager.instance.cardsInSearchArea.Remove(mainCard);
        CardAreaUIEventManager.instance.CardRemoveEvent.Invoke(cardObj);


        ArenaManager.instance.cardsInHandArea.Add(mainCard);
        CardAreaUIEventManager.instance.HandCardAddEvent.Invoke(mainCard);

    }
}
