using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 搜索区中的卡牌执行，针对搜索区卡牌的点击事件函数
/// 类似与CardEffect
/// 挂载到SearchAreaCard的prefab上
/// </summary>
public class SearchAreaCardEffect : MonoBehaviour
{
    public Card mainCard;

    public void CardEffectExecution()
    {
        SearchCardFunctionManager.instance.SearchAreaCardClickEvent.Invoke(gameObject, mainCard);
        //Destroy(gameObject);
    }
}
