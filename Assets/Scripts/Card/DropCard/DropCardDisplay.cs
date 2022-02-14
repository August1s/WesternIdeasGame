using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropCardDisplay : MonoBehaviour
{
    public Card mainCard;
    public Text nameText;

    public void ShowCard()
    {
        nameText.text = mainCard.name;
    }

    // Start is called before the first frame update
    void Start()
    {
        ShowCard();
    }
}
