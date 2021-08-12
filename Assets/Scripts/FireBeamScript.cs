using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FireBeamScript : MonoBehaviour
{
    [SerializeField] private bool hasGottenDamaged;
    [SerializeField] private int damageRate = 1;
    public GameObject redSprite;

    [Range(4.5f, 60f)] public float minInclusive;
    [Range(4.5f, 60f)] public float maxInclusive;

    private Animator _animator;
    
    private static readonly int Appear = Animator.StringToHash("Appear");
    private Coroutine _coroutine;
    
    private void Start()
    {
        hasGottenDamaged = false;
        redSprite.SetActive(false);
        
        _animator = GetComponent<Animator>();

        _coroutine = StartCoroutine(nameof(ShootFireBeamCyclical));
    }

    public void ShootFireBeam()
    {
        hasGottenDamaged = false;
        
        _animator.SetTrigger(Appear);
        
        if (!hasGottenDamaged && !GameManager.Instance.isInSafeZone)
        {
            hasGottenDamaged = true;
            StartCoroutine(Display());
            Invoke(nameof(GetDamagedHP), 4.5f);
        }
    }

    IEnumerator Display()
    {
        yield return new WaitForSeconds(4f);
        redSprite.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        redSprite.SetActive(false);
    }
    
    private void GetDamagedHP()
    {
        GameManager.Instance.GetDamagedHP(damageRate);
        // 대충 animation
    }

    IEnumerator ShootFireBeamCyclical()
    {
        while (true)
        {
            var time = Random.Range(minInclusive, maxInclusive);
            print($"FireBeam time : {time}");
            yield return new WaitForSeconds(time);
            
            ShootFireBeam();
            yield return new WaitForSeconds(4.5f);
        }
    }

    // 평소엔 투명
    // 함수가 호출되면 나타나서 깜빡거리다가 
    // 깜빡거림이 끝나면 빔 쏘기
    // 빔 쏘기 : 딱 한번만 때리기, 세이프존 밖에 있으면 때리기 아니면 그냥 말기
    // 사라지기
}
