using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// ��Ҫ����������CardDisplay.cs�������ݲ������Ϣչʾ��UI��
/// ����ͬ�ĵط����ڣ�������Һ͵��˵���ֵ���Ǳ仯�����ʹ�õ���+�¼������ķ�ʽ������ʾ
/// </summary>
public class PlayerStatementUIEventManager: MonoSingleton<PlayerStatementUIEventManager>
{
    public Player mainPlayer;

    public Text lifeText;
    public Text spiritText;
    public Text actionText;
    public Text searchText;

    public Image lifeImage;
    public Image spiritImage;
    public Image actionImage;

    public UnityEvent<Player> PlayerValueChangeEvent = new UnityEvent<Player>();
    
    public void ShowPlayer(Player player)
    {
        lifeText.text = player.lifeValue.ToString() + '/' + player.maxLifeValue.ToString();
        spiritText.text = player.spiritValue.ToString() + '/' + player.maxSpiritValue.ToString();
        actionText.text = player.actionValue.ToString() + '/' + player.maxActionValue.ToString();
        searchText.text = player.searchValue.ToString();

        lifeImage.fillAmount = (float)player.lifeValue / player.maxLifeValue;
        spiritImage.fillAmount = (float)player.spiritValue / player.maxSpiritValue;
        actionImage.fillAmount = (float)player.actionValue / player.maxActionValue;
    }

    void Awake()
    {
        PlayerValueChangeEvent.AddListener(ShowPlayer);
    }
}
