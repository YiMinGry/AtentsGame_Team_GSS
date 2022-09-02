using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gacha : MonoBehaviour
{
    [SerializeField]
    private GameObject petRender;
    [SerializeField]
    private Transform petSpawner = null;
    [SerializeField]
    private Transform effectSpawner = null;

    private Animator anim = null;

    //public MiniFriendData[] Pets = null;
    [SerializeField]
    Text nameText, gradeText, normalCoinText, rareCoinText;

    private GameObject addedPet;
    public GameObject[] preSpawn, spawn, additional, postSpawn, deSpawn; // 이펙트 저장
    private GameObject preSpawnEffect, spawnEffect, additionalEffect, postSpawnEffect, deSpawnEffect; // 이펙트 생성 위해 할당
    public GameObject canvas = null;
    public GameObject gachaCamera = null;

    private enum GRADE { GRADE_C = 0, GRADE_B = 1, GRADE_A = 2 };

    private int grade = 0, petNum = 0, petNumMax = 17, effectType = 0;

    bool isGachaActive = false; // 가챠 실행 중에 또다른 가챠 실행 방지
    bool isSpawnEnd = false;    // 스폰 끝나기 전에 디스폰 방지
    bool isDespawned = false;   // 디스폰 후에 다시 디스폰 방지

    Coroutine NoCoinCoroutine;
    float noCoinCount = 0.0f;
    const int startCoinFontSize = 35;

    private void Start()
    {
        //petNumMax = Pets.Length;
        petNumMax = MFDataManager.instance.mfarr.Length;        
    }

    private void OnEnable()
    {
        coinTextUpdate();
    }

    private void OnDisable()
    {
        isSpawnEnd = false;
        isDespawned = false;
    }

    private void SetNormalGrade()
    {
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
    }
    private void SetRareGrade()
    {
        int rand = Random.Range(0, 100);
        if (rand < 10)
        {
            grade = (int)GRADE.GRADE_C;
        }
        else if (rand < 55)
        {
            grade = (int)GRADE.GRADE_B;
        }
        else
        {
            grade = (int)GRADE.GRADE_A;
        }
    }

    private void PetSpawnButton()
    {
        if (!isGachaActive)
        {
            petNum = SetPetNumber();
            if (petNum > -1)    // petNum에 -1이 리턴되면 뽑기 불가능
            {
                petRender.SetActive(true);
                isGachaActive = true;
                effectType = Random.Range(0, 3); // 이펙트 타입 현재 3개
                //petNum = Random.Range(0, petNumMax);
                gachaCamera.SetActive(true);
                StartCoroutine(PetSpawn());
            }
        }
    }

    private int SetPetNumber()
    {
        int[] unHavePets = new int[petNumMax];  // 안 가진 펫 인덱스만 넣을 배열
        int count = 0;  // 안 가진 펫 개수

        for (int i = 0; i < petNumMax; i++)
        {
            if(MFDataManager.instance.mfarr[i].isHave == false && MFDataManager.instance.mfarr[i].isChoose == false)
            {
                unHavePets[count] = i;
                count++;
            }
        }
        if(count > 0)
        {
            return unHavePets[Random.Range(0, count)];
        }
        else
        {
            Debug.Log("이미 모든 친구를 가지고 있습니다.");
            return -1;  // 뽑기가 불가능하면 -1 리턴
        }
    }

    private void coinTextUpdate()
    {
        normalCoinText.text = $"X {UserDataManager.instance.coin1}";
        rareCoinText.text = $"X {UserDataManager.instance.coin2}";
    }

    public void NormalPetSpawnButton()
    {
        if (UserDataManager.instance.coin1 >= 50)
        {
            UserDataManager.instance.coin1 -= 50;
            SetNormalGrade();
            PetSpawnButton();
        }
        else
        {
            if (NoCoinCoroutine != null)
            {
                StopCoroutine(NoCoinCoroutine);
            }
            normalCoinText.fontSize = startCoinFontSize;
            noCoinCount = 0.0f;
            NoCoinCoroutine = StartCoroutine(NoCoin(normalCoinText));
            Debug.Log("코인이 부족합니다.");
        }
    }

    public void RarePetSpawnButton()
    {
        if (UserDataManager.instance.coin2 >= 50)
        {
            UserDataManager.instance.coin2 -= 50;
            SetRareGrade();
            PetSpawnButton();
        }
        else
        {
            if (NoCoinCoroutine != null)
            {
                StopCoroutine(NoCoinCoroutine);
            }
            normalCoinText.fontSize = startCoinFontSize;
            noCoinCount = 0.0f;
            NoCoinCoroutine = StartCoroutine(NoCoin(rareCoinText));
            Debug.Log("코인이 부족합니다.");
        }
    }    

    IEnumerator NoCoin(Text text)
    {
        while (true)
        {            
            // 글자 색깔 흰색 <-> 빨간색 반복
            text.color = new Color(1, Mathf.Abs(Mathf.Cos(noCoinCount)), Mathf.Abs(Mathf.Cos(noCoinCount)));
            // 글자 사이즈 35 ~ 45 반복
            text.fontSize = Mathf.FloorToInt(startCoinFontSize + 15.0f * Mathf.Abs(Mathf.Sin(noCoinCount)));
            noCoinCount += Time.deltaTime * 10.0f;
            // noCoinCount가 파이의 2배가 되면(2번 반복하면) 코루틴 종료
            if (noCoinCount > Mathf.PI * 2)
            {
                break;
            }
            yield return null;
        }
        // 코루틴 종료 시 글자 색, 크기 초기화
        text.color = Color.white;
        text.fontSize = startCoinFontSize;
        yield return null;
    }


    public void PetDespawnButton()
    {
        if(isSpawnEnd && !isDespawned)
        {
            StartCoroutine(PetDespawn());
        }
    }

    IEnumerator PetSpawn() // 펫 뽑기기계에서 소환
    {
        //nameText.text = Pets[petNum].friendName;
        nameText.text = MFDataManager.instance.mfarr[petNum].friendName;
        MFDataManager.instance.mfarr[petNum].isHave = true;

        switch (grade)
        {
            case (int)GRADE.GRADE_C:
                gradeText.text = "C";
                break;
            case (int)GRADE.GRADE_B:
                gradeText.text = "B";
                break;
            case (int)GRADE.GRADE_A:
                gradeText.text = "A";
                break;
            default:
                gradeText.text = "Error";
                Debug.Log("grade Error");
                break;
        }
        preSpawnEffect = MakeObject(preSpawn[3 * effectType + grade], effectSpawner);

        yield return Utill.WaitForSeconds(2f);

        DeleteObject(preSpawnEffect);
        //addedPet = MakeObject(Pets[petNum].prefab, petSpawner); // 펫 생성 및 petPosition의 자식으로 설정
        addedPet = MakeObject(MFDataManager.instance.mfarr[petNum].prefab, petSpawner); // 펫 생성 및 petPosition의 자식으로 설정
        anim = addedPet.GetComponentInChildren<Animator>();
        anim.SetBool("Spawn", true); // 9번 jump 애니메이션 재생
        spawnEffect = MakeObject(spawn[3 * effectType + grade], effectSpawner);

        yield return Utill.WaitForSeconds(1f);

        postSpawnEffect = MakeObject(postSpawn[3 * effectType + grade], effectSpawner);
        additionalEffect = MakeObject(additional[3 * effectType + grade], effectSpawner);
        canvas.SetActive(true);
        isSpawnEnd = true;
    }

    IEnumerator PetDespawn() // 펫 뽑기기계에서 소환
    {
        isDespawned = true;
        anim.SetBool("Despawn", true);
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
        GameObject _effect = Instantiate(effect, Spawner);
        _effect.SetActive(true);
        return _effect;
    }

    private void DeleteObject(GameObject effect) // 이펙트 삭제 함수
    {
        effect?.SetActive(false);
        Destroy(effect);
    }
}
