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

    public RuntimeAnimatorController[] animCon;

    //public MiniFriendData[] Pets = null;
    [SerializeField]
    Text nameText, gradeText, normalCoinText, rareCoinText;

    private GameObject addedPet;
    public GameObject[] preSpawn, spawn, additional, postSpawn, deSpawn; // 이펙트 저장
    private GameObject preSpawnEffect, spawnEffect, additionalEffect, postSpawnEffect, deSpawnEffect; // ����Ʈ ���� ���� �Ҵ�
    public GameObject canvas = null;
    public GameObject gachaCamera = null;

    private enum GRADE { GRADE_C = 0, GRADE_B = 1, GRADE_A = 2 };

    private int grade = 0, petNum = 0, petNumMax = 17, effectType = 0;

    bool isGachaActive = false; // 가챠 실행 중에 또다른 가챠 실행 방지
    bool isSpawnEnd = false;    // ���� ������ ���� ���� ����
    bool isDespawned = false;   // ���� �Ŀ� �ٽ� ���� ����

    Coroutine NoCoinCoroutine;
    float noCoinCount = 0.0f;
    const int startCoinFontSize = 35;

    private void Start()
    {
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

    private bool PetSpawnButton()
    {
        if (!isGachaActive)
        {
            petNum = SetPetNumber();
            if (petNum > -1)    // petNum이 -1이면 뽑기 불가능
            {
                petRender.SetActive(true);
                isGachaActive = true;
                effectType = Random.Range(0, 3);
                gachaCamera.SetActive(true);
                StartCoroutine(PetSpawn());
                return true;
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    private int SetPetNumber()
    {
        int[] unHavePets = new int[petNumMax];  // �� ���� �� �ε����� ���� �迭
        int count = 0;  // �� ���� �� ����

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
            Debug.Log("이미 모든 친구를 보유중입니다.");
            return -1;  // 뽑기 불가능할 때 -1 리턴
        }
    }

    private void coinTextUpdate()
    {
        normalCoinText.text = $"X {UserDataManager.instance.coin1}";
        rareCoinText.text = $"X {UserDataManager.instance.coin2}";
    }

    public void NormalPetSpawnButton()
    {
        if (UserDataManager.instance.CL2S_UserCoinUpdate(0, -5))    // 일반코인 5개 감소 후 뽑기, 감소 실패 시 else문 실행
        {
            SetNormalGrade();
            PetSpawnButton();
            //if (!PetSpawnButton())
            //{
            //    UserDataManager.instance.CL2S_UserCoinUpdate(0, 5); // 뽑기 실패 시 코인 반환
            //    Debug.Log("코인 반환");
            //}
        }
        else
        {
            AudioManager.Inst.PlaySFX("ErrorSound_01");
            if (NoCoinCoroutine != null)
            {
                StopCoroutine(NoCoinCoroutine);
            }
            normalCoinText.fontSize = startCoinFontSize;
            noCoinCount = 0.0f;
            NoCoinCoroutine = StartCoroutine(NoCoin(normalCoinText));
        }
    }

    public void RarePetSpawnButton()
    {
        if (UserDataManager.instance.CL2S_UserCoinUpdate(1, -1))    // 특수코인 1개 감소 후 뽑기, 감소 실패 시 else문 실행
        {
            SetRareGrade();
            PetSpawnButton();
            //if (!PetSpawnButton())
            //{
            //    UserDataManager.instance.CL2S_UserCoinUpdate(1, 1); // 뽑기 실패 시 코인 반환
            //}
        }
        else
        {
            AudioManager.Inst.PlaySFX("Gacha_ErrorSound_01");
            if (NoCoinCoroutine != null)
            {
                StopCoroutine(NoCoinCoroutine);
            }
            normalCoinText.fontSize = startCoinFontSize;
            noCoinCount = 0.0f;
            NoCoinCoroutine = StartCoroutine(NoCoin(rareCoinText));
        }
    }    

    IEnumerator NoCoin(Text text)
    {
        while (true)
        {            
            // 글자 색깔 흰색 <-> 붉은색 반복
            text.color = new Color(1, Mathf.Abs(Mathf.Cos(noCoinCount)), Mathf.Abs(Mathf.Cos(noCoinCount)));
            // 글자 크기 35 ~ 45 반복
            text.fontSize = Mathf.FloorToInt(startCoinFontSize + 15.0f * Mathf.Abs(Mathf.Sin(noCoinCount)));
            noCoinCount += Time.deltaTime * 10.0f;
            // noCoinCount가 파이의 2배 보다 커지면(2번 반복하면)종료
            if (noCoinCount > Mathf.PI * 2)
            {
                break;
            }
            yield return null;
        }
        // 글자 색깔, 크기 초기화
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
        AudioManager.Inst.PlaySFX("Gacha_CoinSound_01");
        nameText.text = MFDataManager.instance.mfarr[petNum].friendName;
        MFDataManager.instance.Send_HaveMF(petNum);
        StartCoroutine(UserDataManager.instance.AchivementCheck(0));

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
        addedPet = MakeObject(MFDataManager.instance.mfarr[petNum].prefab, petSpawner); // 펫 생성 및 petPosition의 자식으로 설정
        anim = addedPet.GetComponentInChildren<Animator>();
        if(petNum < 17)
        {
            anim.runtimeAnimatorController = animCon[0];
        }
        else
        {
            anim.runtimeAnimatorController = animCon[petNum - 16];
        }


        anim.SetBool("Spawn", true);
        spawnEffect = MakeObject(spawn[3 * effectType + grade], effectSpawner);

        yield return Utill.WaitForSeconds(1f);

        postSpawnEffect = MakeObject(postSpawn[3 * effectType + grade], effectSpawner);
        additionalEffect = MakeObject(additional[3 * effectType + grade], effectSpawner);
        canvas.SetActive(true);
        isSpawnEnd = true;

        NetManager.instance.AddRollingMSG("미니친구", $"미니친구 {MFDataManager.instance.mfarr[petNum].friendName}를 뽑았습니다.");
    }

    IEnumerator PetDespawn()
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
