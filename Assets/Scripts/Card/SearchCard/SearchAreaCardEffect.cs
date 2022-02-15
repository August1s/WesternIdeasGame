using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �������еĿ���ִ�У�������������Ƶĵ���¼�����
/// ������CardEffect
/// ���ص�SearchAreaCard��prefab��
/// </summary>
public class SearchAreaCardEffect : MonoBehaviour
{
    public Card mainCard;

    public void CardEffectExecution()
    {
        if (ArenaManager.instance.gamePhase == GamePhase.PlayerRoundBegin)
            SearchCardFunctionManager.instance.SearchAreaCardClickEvent.Invoke(gameObject, mainCard);
    }
}
