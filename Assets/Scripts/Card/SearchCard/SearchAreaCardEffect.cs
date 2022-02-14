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
        SearchCardFunctionManager.instance.SearchAreaCardClickEvent.Invoke(gameObject, mainCard);
        //Destroy(gameObject);
    }
}
