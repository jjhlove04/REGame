using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="EnemyInfo", menuName ="EnemyManagement")]
public class EnemyInfo : ScriptableObject
{

    public List<EnemyStatus> EnemyList = new List<EnemyStatus>();

    [System.Serializable]
   public class EnemyStatus
   {
       [Header("적 게임 오브젝트")]
       public GameObject enemyObj;

       [Space(10)]
       [Header("적 스테이터스 수치 관리")]
       [Tooltip("적 공격력")]
       public int enemyATK;

       [Space(5)]
       [Tooltip("적 체력")]
        public int enemyHp;

       [Space(5)]
       [Tooltip("적 딜레이")]
       public int enemyAtkDelay;

       [Space(5)]
       [Tooltip("적 공격시간")]
       public int enemyAtkHoldTime;

       [Space(5)]
       [Tooltip("적 사거리")]
       public int enemyAtkDistance;

       [Space(5)]
       [Tooltip("적 드롭 경험치")]
       public int enemyDropEXP;

       [Space(5)]
       [Tooltip("적 드롭 골드")]
       public int enemyDropGold;

       [Space(5)]
       [Tooltip("적 타입 설정")]
       public EnemyType type;
       
   }

   public enum EnemyType
   {
       Hide,
       common,
       Boss,
       SubBoss
   }
}
