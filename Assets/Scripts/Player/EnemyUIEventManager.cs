using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class EnemyUIEventManager : MonoSingleton<EnemyUIEventManager>
{
    public Enemy mainEnemy;

    public Text enemyName;

    public Text lifeText;

    public UnityEvent<Enemy> EnemyValueChangeEvent = new UnityEvent<Enemy>();

    private void ShowEnemy(Enemy enemy)
    {
        enemyName.text = enemy.name;
        lifeText.text = enemy.lifeValue.ToString();
    }

    private void Awake()
    {
        EnemyValueChangeEvent.AddListener(ShowEnemy);
    }
}
