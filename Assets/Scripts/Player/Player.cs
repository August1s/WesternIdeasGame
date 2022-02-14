using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ����࣬��¼��ҵ�״̬
/// �������ΪArenaManager��ĳ�Ա����ʼ����ArenaManager.cs��
/// 
/// δ�����ܻὫPlayer��Ϊ���л��洢�ڱ��ؽ��ж�ȡ
/// </summary>
public class Player 
{
    // ��ʱ��ʼ��һЩĬ����ֵ�����ڸ�����Ҫ��������
    public string name;
    
    public int lifeValue;
    public int actionValue;
    public int spiritValue;
    public int searchValue;

    public int maxLifeValue;
    public int maxActionValue;
    public int maxSpiritValue;


    // �����������Ŀ��������������أ�һ����Ҷ�Ӧһ����������
    // ������һ��hashtable����Card��Ϊkey������ҹ������������Ϊvalue
    // value>=3�Ļ�����Ӧ�����Ƴ������أ�������������ʾ
    // ���ھ���ֵϵͳ��������
    public Dictionary<Card, int> cardsInSearchPool = new Dictionary<Card, int>();

    // ��ұ��������е��ƣ���������ÿ�غ�����
    // �ڶ�ս�����У�cardsInPlayerBag�������ڼ�¼�ƶ�ʣ����
    public List<Card> cardsInPlayerBag = new List<Card>();
}
