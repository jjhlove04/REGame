using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="TurrentInfo", menuName ="TurretInfo")]
public class TurretInfo : ScriptableObject
{
    public List<TurretName> turretList = new List<TurretName>();
    
    [System.Serializable]
    public class TurretName
    {
        [Header("터렛 오브젝트")]
        public GameObject turretObj;
        [Space(10)]
        [Header("터렛 스테이터스")]
        [Tooltip("터렛 공격력")]
        public float turretATK;
        [Space(5)]
        [Tooltip("터렛 총알수")]
        public float ammoCount;
        [Space(5)]
        [Tooltip("터렛 사거리")]
        public float atkDistance;
        [Space(5)]
        [Tooltip("터렛 설치비용")]
        public int installPrice;
        [Space(5)]
        [Tooltip("터렛 재장전비용")]
        public int reroadPrice;
        [Space(5)]
        [Tooltip("터렛 치명타확률")]
        public int cirticlPER;
        [Space(5)]
        [Tooltip("터렛 타입")]
        public TurretType type;
        
    }
    public enum TurretType
        {
            common,
            rader,
            buff,
            debuff
            
            }

    
}

    
