using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyCardFunctionManager : MonoSingleton<EnemyCardFunctionManager>
{
    public UnityEvent<EnemyCard> EnemyCardExecuteEvent = new UnityEvent<EnemyCard>();


    /// <summary>
    /// 结算敌方卡牌对于玩家的属性影响
    /// </summary>
    /// <param name="mainCard">敌方卡牌</param>
    private void CardEffectExecution(EnemyCard mainCard)
    {
        if (ArenaManager.instance.gamePhase != GamePhase.EnemyRoundBegin)
            return;

        PlayerAttributeValueEffect(mainCard.lifeValueEffect, mainCard.actionValueEffect, mainCard.spiritValueEffect, mainCard.searchValueEffect);
        SelfAttributeValueEffect(mainCard.selfLifeValueEffect);

        PlayerStatementUIEventManager.instance.PlayerValueChangeEvent.Invoke(ArenaManager.instance.player);
        EnemyUIEventManager.instance.EnemyValueChangeEvent.Invoke(ArenaManager.instance.enemy);
    }


    private void Awake()
    {
        EnemyCardExecuteEvent.AddListener(CardEffectExecution);
    }


    private void PlayerAttributeValueEffect(int lifeEffect, int actionEffect, int spiritEffect, int searchEffect)
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
        if (ArenaManager.instance.player.spiritValue < 0)
        {
            ArenaManager.instance.player.lifeValue += ArenaManager.instance.player.spiritValue;
            ArenaManager.instance.player.spiritValue = 0;
        }

        ArenaManager.instance.player.searchValue += Mathf.Max(
            searchEffect, -ArenaManager.instance.player.searchValue);
    }

    private void SelfAttributeValueEffect(int lifeEffect)
    {
        ArenaManager.instance.enemy.lifeValue += lifeEffect;
        if (ArenaManager.instance.enemy.lifeValue <= 0)
        {
            ArenaManager.instance.gamePhase = GamePhase.GameEnd;
            Debug.Log("----游戏结束----");
        }
    }
}
