using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 효과
public class ObstacleScript : MonoBehaviour
{
    [SerializeField] private int damageRate;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.tag);
        // 캐릭터 피격 체크 : 태그=플레이어 and 피격회피시간이 다 찼는지(일정 시간에 한번씩 데미지를 받음)
        if (other.transform.CompareTag("Player") && GameManager.Instance.curDamageTime <= 0)
        {
            GetDamagedHP();
            EffectSoundManager.Instance.PlayEffect(6);
        }
    }

    private void GetDamagedHP()
    {
        GameManager.Instance.GetDamagedHP(damageRate);
        // 대충 animation
    }
}
