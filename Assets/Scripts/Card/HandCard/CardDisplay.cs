using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 将Card类的必要数据成员显示在卡牌对应的UI上
/// CardDisplay应该挂载到HandCard与SearchAreaCard两个prefab中
/// </summary>
public class CardDisplay : MonoBehaviour
{
    //卡片数据，应该在实例化显示的时候传入Card类中的数据
    public Card mainCard;
    
    //相关UI内容
    public Text nameText;
    public Text useCostText;
    public Text searchCostText;
    public Text functionText;
    public Image backRoundImage;

    /// <summary>
    /// 将mainCard中的数值展示在UI上
    /// 如果对卡片进行分类，对类型判断来调整显示
    /// </summary>
    public void ShowCard()
    {
        nameText.text = "卡牌名称: " + mainCard.name;
        useCostText.text = "使用代价: " + mainCard.lifeValueCost + "血" + mainCard.spiritValueCost + "精" + mainCard.actionValueCost + "行";
        searchCostText.text = "搜索代价:\n" + mainCard.searchValuePayment + "搜" + +mainCard.lifeValuePayment + "血" + mainCard.spiritValuePayment + "精" + mainCard.actionValuePayment + "行"; 
        functionText.text = "卡牌功能:\n" + mainCard.funcDescription;
    }
    
    // Start is called before the first frame update
    void Start()    
    {
        ShowCard();
    }

}
