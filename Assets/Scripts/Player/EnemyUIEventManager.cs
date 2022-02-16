using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// EnemyUIEventManager.cs以监听的方式实现敌人UI部分的变化
/// </summary>
public class EnemyUIEventManager : MonoSingleton<EnemyUIEventManager>
{
    public Enemy mainEnemy;

    public Text enemyName;
    public Text lifeText;

    public GameObject enemyCardStore;
    public GameObject enemyCardPrefab;

    public UnityEvent<Enemy> EnemyValueChangeEvent = new UnityEvent<Enemy>();
    public UnityEvent<EnemyCard> EnemyCardChangeEvent = new UnityEvent<EnemyCard>();

    private void ShowEnemy(Enemy enemy)
    {
        enemyName.text = enemy.name;
        lifeText.text = enemy.lifeValue.ToString();
    }

    private void CardChange(EnemyCard card)
    {
        if(enemyCardStore.transform.childCount > 0)
            Destroy(enemyCardStore.transform.GetChild(0).gameObject);

        GameObject newCard = GameObject.Instantiate(enemyCardPrefab, enemyCardStore.transform);
        newCard.GetComponent<EnemyCardDisplay>().mainCard = card;
    }

    private void Awake()
    {
        EnemyValueChangeEvent.AddListener(ShowEnemy);
        EnemyCardChangeEvent.AddListener(CardChange);
    }
}
