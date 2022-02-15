using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 手牌区中卡牌的效果执行，针对于手牌的鼠标点击事件响应函数
/// 在数据层面，改变物体的属性（比如敌人和Player的数据属性），以及Card list中的内容（手牌区list随着卡牌点击而变少，打牌区list变多）
/// 在UI层面，改变各个卡牌区域的显示内容改变属性显示内容
/// 
/// CardEffect应该被挂载到SingleCard prefab上，效果执行函数应该被绑定在对应的Button component上
/// </summary>

public class CardEffect : MonoBehaviour
{
    // 类似于CardDisplay.cs，在卡牌实例化的时候传入数据
    public Card mainCard;


    public void CardEffectExecution()
    {
        if(ArenaManager.instance.gamePhase == GamePhase.PlayerRoundBegin)
        {
            // 卡牌效果执行
            HandCardFunctionManager.instance.CardEffectLaunchEvent.Invoke(gameObject, mainCard);
        }
    }
}
