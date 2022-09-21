using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDictionary : MonoBehaviour
{
    public static ItemDictionary _instance = new ItemDictionary();

    //키값, item1 = 아이템 이름, item2 = 아이템 설명, item3 = 등급
    public Dictionary<int, (string, string, string, Sprite)> itemContainerCom;

    public ItemDictionary()
    {
        itemContainerCom = new Dictionary<int, (string, string, string, Sprite)>()
        {
            {0,("붉은 너트", "공격시 8%의 확률로 5의 체력을 치유해주는 너트 두 개를 생성한다 ( 중첩당 치유량 +5 )","보통",ItemSpriteContainer.Instance.itemSprites[0])},
            {1,("폭발 방패", "0.7초 안에 최대체력의 15%에 달하는 피해를 입으면 200%의 피해를 입히는 폭발을 주변에 일으킨다 ( 중첩당 피해량 + 20% )","보통",ItemSpriteContainer.Instance.itemSprites[1])},
            {2,("약점 랜즈", "공격시 10% 확률로 치명타가 발생하며 두배의 피해를 입힌다 ( 중첩당 치명타 확률 +7% )","보통",ItemSpriteContainer.Instance.itemSprites[2])},
            {3,("쿨다운", "3초간 총알을 다 소모한 포탑이 2개 이상일때 초당 20의 체력이 회복 된다 ( 중첩당 초당 회복 +5 )","보통",ItemSpriteContainer.Instance.itemSprites[3])},
            {4,("연금", "매 8초마다 1골드를 생성한다 ( 중첩당 생성 골드 +1 )","보통",ItemSpriteContainer.Instance.itemSprites[4])},
            {5,("철조망", "근처의 적에게 초당 50% 피해를입힌다 범위내 적이 많아도 단일 대상에게 피해를 주며 방어력 무시( 중첩당 범위 +20% ,피해 +10% )","보통",ItemSpriteContainer.Instance.itemSprites[5])},
            {6,("지혈제", "공격시 15%확률로 적에게 출혈을 일으켜 2초간 35 % 의 피해를 4회 입힌다 ( 중첩당 발동활률 +15% )","보통",ItemSpriteContainer.Instance.itemSprites[6])},
            {7,("스패너", "기본 체력 회복량이 초당 1.2 증가( 중첩당 회복량 +1.2 )","보통",ItemSpriteContainer.Instance.itemSprites[7])},
            {8,("박격포", "공격 시 9%의 확률로 포물선으로 날아가는  박격포 2발을 발사하려 각각 170%의 피해를 입힌다 (유도 X)( 중첩당 피해 +170% )","보통",ItemSpriteContainer.Instance.itemSprites[8])},
            {9,("승전보", "레벨이 오르면 4초간 2개의 랜덤 포탑의 공격속도와 피해량을 30% 증가시키는 버프를 준다( 중첩시 지속시간 +1초 , 지정 포탑 수 +1 )","보통",ItemSpriteContainer.Instance.itemSprites[9])},
            {10,("뱀파이어의 이빨", "적을 처치하면 체력을 3 치유 한다. ( 중첩시 치유량 +3 )","보통",ItemSpriteContainer.Instance.itemSprites[10])},
            {11,("금속 탄환", "체력이 80%가 넘는 적에게 25%추가 피해를 입힌다. ( 중첩시 피해 +25% )","보통",ItemSpriteContainer.Instance.itemSprites[11])},
            {12,("불타는 석탄", "레벨업 하면 8의개의 유도 석탄이 발사되어 가각 350% 피해를 입힌다 ( 중첩시 석탄 개수 +2 )","보통",ItemSpriteContainer.Instance.itemSprites[12])},
            {13,("복실복실 팔찌", "공격시 7% 확률로 적을 1.5초동안 속박 시키며 속박된 적은 150%피해를 받는다. ( 중첩당 지속시간 0+.5초 )","보통",ItemSpriteContainer.Instance.itemSprites[13])},
            {14,("강화합금", "최대 체력이 8%증가한다 ( 중첩당 최대 체력 +8% )","보통",ItemSpriteContainer.Instance.itemSprites[14])},
            {15,("발바닥 사탕", "공격시 8%의 확률로 적에게 폭탄을 붙여 140%의 피해를 입힌다. ( 중첩당 피해 +40% )","보통",ItemSpriteContainer.Instance.itemSprites[15])},
            {16,("오버클럭", "공격 속도가 15% 증가한다 ( 중첩당 공격속도 +15% )","보통",ItemSpriteContainer.Instance.itemSprites[16])},
            {17,("배움의 돋보기", "적 처치시 얻는 경험치가 10% 증가 합니다 ( 중첩당 획득 경험치 +10% )","희귀한",ItemSpriteContainer.Instance.itemSprites[17])},
            {18,("범죄 복면", "10%의 확률로 받는 피해 무효 ( 중첩당 회피율+5% )","보통",ItemSpriteContainer.Instance.itemSprites[18])},
            {19,("엔진오일", "적을 처치하면 불을 장판을 펼쳐 밟은 적은 2초간 60%피해를 받는다 ( 중첩당 피해 +40% )","보통",ItemSpriteContainer.Instance.itemSprites[19])},
            {20,("밧줄 속박 ","피해를 받으면 30%확률로 2초동안 가시 장판을 펼처 적의 이동 속도를 20% 감소시킨다. ( 중첩당 발동확률 +10% , 지속시간 +1초 )","보통",ItemSpriteContainer.Instance.itemSprites[20])},
            {21,("더 큰 지갑","적을 처치해 얻는 골드가 10%증가합니다.  ( 중첩당 획득 골드 +10% )","희귀한",ItemSpriteContainer.Instance.itemSprites[21])},
            {22,("형상기억 합금","적을 처치할때 마다 체력이 영구적으로 0.5 증가한다 ( 중첩당 체력 증가량 +0.5 )","희귀한",ItemSpriteContainer.Instance.itemSprites[22])},
            {23,("라바 램프","적을 처치하면 20% 확률로 용암 기둥이 생성되어 근처의 적에게 500% 데미지를 입힌다. 맞은 적은 잠깐 공중에 뜬게 된다 ( 중첩당 용암 기둥 데미지 +50% )","희귀한",ItemSpriteContainer.Instance.itemSprites[23])},
            {24,("Iowa Mk.23","공격시 10% 확률로 미사일을 발사하여 발동시킨 공격의 최종 피해량의 200%피해를 입힌다(치명타는 발생하지 않음) ( 중첩당 발동 확률 +4% )","희귀한",ItemSpriteContainer.Instance.itemSprites[24])},
            {25,("꼬리 없는 플라나리아","피해를 입히면 0.5의 체력이 치유된다 ( 중첩당 치유량 0.1증가 )","희귀한",ItemSpriteContainer.Instance.itemSprites[25])},
            {26,("펀치건","6%확률로 적을 거리 45 정도 밀어 냅니다 ( 6%확률로 적을 거리 45 정도 밀어 냅니다  )","희귀한",ItemSpriteContainer.Instance.itemSprites[26])}
        };
    }
    private void Awake() {
        _instance = this;
    }
}
