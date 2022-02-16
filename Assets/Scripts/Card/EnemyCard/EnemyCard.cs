using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCard
{
    public int id;
    public string name;
    public string funcDescription;

    public string user; // �з�����ʹ����

    // ���������ֵ��Ӱ��
    public int lifeValueEffect;     
    public int actionValueEffect;   
    public int spiritValueEffect;    
    public int searchValueEffect;

    // �������������ֵ��Ӱ��
    public int lifeMaxValueEffect;     
    public int actionMaxValueEffect;   
    public int spiritMaxValueEffect;

    // �������Ե�Ӱ��
    public int selfLifeValueEffect;
}
