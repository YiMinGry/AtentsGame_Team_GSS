using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gacha : MonoBehaviour
{
    [SerializeField]
    private GameObject petRender;
    [SerializeField]
    private Transform petSpawner = null;
    [SerializeField]
    private Transform effectSpawner = null;

    private Animator anim = null;

    public GameObject[] Pets = null;


    private GameObject addedPet;
    public GameObject[] preSpawn, spawn, additional, postSpawn, deSpawn; // 이펙트 저장
    private GameObject preSpawnEffect, spawnEffect, additionalEffect, postSpawnEffect, deSpawnEffect; // 이펙트 생성 위해 할당
    public GameObject canvas = null;
    public GameObject gachaCamera = null;

    private enum GRADE { GRADE_C = 0, GRADE_B = 1, GRADE_A = 2 };

    private int grade = 0, petNum = 0, petNumMax = 17, effectType = 0;

    bool isGachaActive = false; // 가챠 실행 중에 또다른 가챠 실행 방지

    float time = 1.0f;

    private void Awake()
    {
        //petSpawner = transform.Find("PetSpawner")?.GetComponent<Transform>();
        //effectSpawner = transform.Find("EffectSpawner")?.GetComponent<Transform>();
    }

    public void PetSpawnButton()
    {
        if (!isGachaActive)
        {
            petRender.SetActive(true);
            isGachaActive = true;
            grade = Random.Range(0, 3); // C~A 등급 중에서 랜덤하게 설정
            effectType = Random.Range(0, 3); // 이펙트 타입 현재 3개
            petNum = Random.Range(0, petNumMax);
            /* 그레이드 확률에 따라 랜덤하게 조정
            int rand = Random.Range(0, 100);
            if (rand < 50)
            {
                grade = (int)GRADE.GRADE_C;
            }
            else if (rand < 85)
            {
                grade = (int)GRADE.GRADE_B;
            }
            else
            {
                grade = (int)GRADE.GRADE_A;
            }
            */
            gachaCamera.SetActive(true);
            StartCoroutine(PetSpawn());
        }
    }

    public void PetDespawnButton()
    {
        StartCoroutine(PetDespawn());
    }

    IEnumerator PetSpawn() // 펫 뽑기기계에서 소환
    {
        preSpawnEffect = MakeObject(preSpawn[3 * effectType + grade], effectSpawner);

        yield return Utill.WaitForSeconds(2f);

        DeleteObject(preSpawnEffect);
        addedPet = MakeObject(Pets[petNum], petSpawner); // 펫 생성 및 petPosition의 자식으로 설정
        anim = addedPet.GetComponent<Animator>();
        anim.SetInteger("animation", 9); // 9번 jump 애니메이션 재생
        spawnEffect = MakeObject(spawn[3 * effectType + grade], effectSpawner);

        yield return Utill.WaitForSeconds(1f);

        anim.SetInteger("animation", 19); // 19번 IdleA 애니메이션 재생 (전투중 Idle 모션)
        postSpawnEffect = MakeObject(postSpawn[3 * effectType + grade], effectSpawner);
        additionalEffect = MakeObject(additional[3 * effectType + grade], effectSpawner);
        canvas.SetActive(true);
    }

    IEnumerator PetDespawn() // 펫 뽑기기계에서 소환
    {
        anim.SetInteger("animation", 9);
        canvas.SetActive(false);
        DeleteObject(spawnEffect);
        DeleteObject(additionalEffect);

        yield return Utill.WaitForSeconds(0.7f);

        deSpawnEffect = MakeObject(deSpawn[3 * effectType + grade], effectSpawner);
        DeleteObject(postSpawnEffect);
        Destroy(addedPet);

        yield return Utill.WaitForSeconds(1f);
        petRender.SetActive(false);
        DeleteObject(deSpawnEffect);
        gachaCamera.SetActive(false);
        this.gameObject.SetActive(false);
        isGachaActive = false;
        EventManager.Invoke("CloseUI", "Gacha");
    }

    private GameObject MakeObject(GameObject effect, Transform Spawner) // 오브젝트 생성 함수
    {
        //effect = Instantiate(effect, Spawner); // 생성 후 Spawner의 자식으로 설정
        //effect.SetActive(true);

        GameObject _effect = Instantiate(effect, Spawner);
        _effect.SetActive(true);
        return _effect;
    }

    private void DeleteObject(GameObject effect) // 이펙트 삭제 함수
    {
        effect.SetActive(false);
        Destroy(effect);
    }
}
