using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCardDisplay : MonoBehaviour
{
    public EnemyCard mainCard;

    public Text cardName;
    public Text cardFunc;

    public void ShowCard()
    {
        cardName.text = mainCard.name;
        cardFunc.text = mainCard.funcDescription;
    }

    private void Start()
    {
        ShowCard();
    }
}
