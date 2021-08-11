using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 효과
public class ObstacleScript : MonoBehaviour
{
    [SerializeField] private static float MaxDamageAvoidTime = 5;
    [SerializeField] private float curDamageAvoidTime;
    [SerializeField] private int damageRate;

    private void FixedUpdate()
    {
        if (curDamageAvoidTime > 0)
        {
            curDamageAvoidTime -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.collider.tag);
        // 캐릭터 피격 체크 : 태그=플레이어 and 피격회피시간이 다 찼는지(일정 시간에 한번씩 데미지를 받음)
        if (collision.transform.CompareTag("Player") && curDamageAvoidTime <= 0)
        {
            GetDamagedHP();
            curDamageAvoidTime = MaxDamageAvoidTime;
        }
    }

    private void GetDamagedHP()
    {
        GameManager.Instance.GetDamagedHP(damageRate);
        // 대충 animation
    }
}
