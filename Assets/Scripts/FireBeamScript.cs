using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBeamScript : MonoBehaviour
{
    [SerializeField] private bool hasGottenDamaged;
    [SerializeField] private int damageRate = 1;
    
    private Animator _animator;
    private static readonly int Appear = Animator.StringToHash("Appear");

    private void Start()
    {
        hasGottenDamaged = false;
        
        _animator = GetComponent<Animator>();
    }

    public void ShootFireBeam()
    {
        hasGottenDamaged = false;
        
        _animator.SetTrigger(Appear);
        
        if (!hasGottenDamaged && !GameManager.Instance.isInSafeZone)
        {
            hasGottenDamaged = true;
            Invoke(nameof(GetDamagedHP), 4.5f);
        }
    }
    
    private void GetDamagedHP()
    {
        GameManager.Instance.GetDamagedHP(damageRate);
        // 대충 animation
    }

    // 평소엔 투명
    // 함수가 호출되면 나타나서 깜빡거리다가 
    // 깜빡거림이 끝나면 빔 쏘기
    // 빔 쏘기 : 딱 한번만 때리기, 세이프존 밖에 있으면 때리기 아니면 그냥 말기
    // 사라지기
}
