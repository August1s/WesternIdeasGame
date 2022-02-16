using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// 点击手牌卡牌后，卡牌的效果函数的合集（数值改变，卡池变化，UI变化）
/// 
/// 搜索区卡牌在点击时的UI变化也暂时写在了HandCardFunctionManager.cs中
/// </summary>
public class HandCardFunctionManager : MonoSingleton<HandCardFunctionManager>
{
    public UnityEvent<GameObject, Card> CardEffectLaunchEvent = new UnityEvent<GameObject, Card>();


    // 手牌在被点击后，应该先结算属性变化，然后放入出牌区中
    private void CardEffectExecution(GameObject cardObj, Card mainCard)
    {
        if (mainCard.lifeValueCost <= ArenaManager.instance.player.lifeValue &&
            mainCard.actionValueCost <= ArenaManager.instance.player.actionValue &&
            mainCard.spiritValueCost <= ArenaManager.instance.player.spiritValue)
        {
            ArenaManager.instance.player.lifeValue -= mainCard.lifeValueCost;
            if (ArenaManager.instance.player.lifeValue == 0)
            {
                ArenaManager.instance.gamePhase = GamePhase.GameEnd;
                Debug.Log("----游戏结束----");
            }
            ArenaManager.instance.player.actionValue -= mainCard.actionValueCost;
            ArenaManager.instance.player.spiritValue -= mainCard.spiritValueCost;


            if (mainCard.haveAttributeEffect)
                SelfAttributeValueEffect(mainCard.lifeValueEffect, mainCard.actionValueEffect, mainCard.spiritValueEffect, mainCard.searchValueEffect);

            if (mainCard.haveHandCardEffect)
                HandCardEffect(mainCard.drawNewCard);

            if (mainCard.haveEnemyEffect)
                EnemyAttributeValueEffect(mainCard.damageEffectToEnemy);


            PlayerStatementUIEventManager.instance.PlayerValueChangeEvent.Invoke(ArenaManager.instance.player);
            EnemyUIEventManager.instance.EnemyValueChangeEvent.Invoke(ArenaManager.instance.enemy);
            

            ArenaManager.instance.cardsInHandArea.Remove(mainCard);
            CardAreaUIEventManager.instance.CardRemoveEvent.Invoke(cardObj);
            //CardAreaUIEventManager.instance.HandCardsAreaRefreshEvent.Invoke();

            ArenaManager.instance.dropCards.Add(mainCard);
            CardAreaUIEventManager.instance.DropCardAddEvent.Invoke(mainCard);

            CardAreaUIEventManager.instance.OtherCardAreaCountUpdateEvent.Invoke(ArenaManager.instance.player.cardsInPlayerBag.Count, ArenaManager.instance.discardArea.Count);
        }
        else
        {
            // 后期会加入一些禁止出牌的动画或音效？
        }

    }


    void Awake()
    {
        CardEffectLaunchEvent.AddListener(CardEffectExecution);
    }

    
    /// <summary>
    /// 卡牌对玩家自身属性值的影响
    /// 
    /// 后期会加入对上限的影响？
    /// </summary>
    /// <param name="lifeEffect">生命值影响</param>
    /// <param name="actionEffect">行动值影响</param>
    /// <param name="spiritEffect">精神值影响</param>
    /// <param name="searchEffect">搜索值影响</param>
    private void SelfAttributeValueEffect(int lifeEffect, int actionEffect, int spiritEffect, int searchEffect)
    {
        ArenaManager.instance.player.lifeValue += Mathf.Min(
            lifeEffect, ArenaManager.instance.player.maxLifeValue - ArenaManager.instance.player.lifeValue);
        if (ArenaManager.instance.player.lifeValue <= 0)
        {
            ArenaManager.instance.gamePhase = GamePhase.GameEnd;
            Debug.Log("----游戏结束----");
        }

        ArenaManager.instance.player.actionValue += Mathf.Max(
            -ArenaManager.instance.player.actionValue,
            Mathf.Min(actionEffect, ArenaManager.instance.player.maxActionValue - ArenaManager.instance.player.actionValue));

        // 当精神值为0时，消耗等量的生命值
        ArenaManager.instance.player.spiritValue += Mathf.Min(
            spiritEffect, ArenaManager.instance.player.maxSpiritValue - ArenaManager.instance.player.spiritValue);
        if(ArenaManager.instance.player.spiritValue < 0)
        {
            ArenaManager.instance.player.lifeValue += ArenaManager.instance.player.spiritValue;
            ArenaManager.instance.player.spiritValue = 0;
        }

        ArenaManager.instance.player.searchValue += Mathf.Max(
            searchEffect, -ArenaManager.instance.player.searchValue);
            
    }

    /// <summary>
    /// 对敌方属性值的影响
    /// </summary>
    /// <param name="damageEffect">生命值影响</param>
    private void EnemyAttributeValueEffect(int damageEffect)
    {
        ArenaManager.instance.enemy.lifeValue -= damageEffect;
        if (ArenaManager.instance.enemy.lifeValue <= 0)
        {
            ArenaManager.instance.gamePhase = GamePhase.GameEnd;
            Debug.Log("----游戏结束----");
        }
    }

    /// <summary>
    /// 手牌影响，依照抽卡数量，将player背包中（剩余牌堆）中的卡移动到手牌中
    /// 
    /// 如果剩余牌堆中已经没有足够抽的牌，那么先将牌抽取掉，然后洗切废牌堆作为新的牌堆，再进行抽取
    /// </summary>
    /// <param name="drawNewCard">抽卡数量</param>
    private void HandCardEffect(int drawNewCard)
    {
        for(int i = 0; i < drawNewCard; i++)
        {
            ArenaManager.instance.cardsInHandArea.Add(ArenaManager.instance.player.cardsInPlayerBag[i]);
        }
        ArenaManager.instance.player.cardsInPlayerBag.RemoveRange(0, drawNewCard);
    }
    
}
