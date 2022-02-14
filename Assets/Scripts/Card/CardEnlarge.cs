using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



/// <summary>
/// 卡牌效果，鼠标悬停的时候卡牌比例变大
/// </summary>
public class CardEnlarge : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public float enlargeScale = 1.2f;

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = new Vector3(enlargeScale, enlargeScale, enlargeScale);
        
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        
    }

}
