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
    public GameObject[] preSpawn, spawn, additional, postSpawn, deSpawn; // ����Ʈ ����
    private GameObject preSpawnEffect, spawnEffect, additionalEffect, postSpawnEffect, deSpawnEffect; // ����Ʈ ���� ���� �Ҵ�
    public GameObject canvas = null;
    public GameObject gachaCamera = null;

    private enum GRADE { GRADE_C = 0, GRADE_B = 1, GRADE_A = 2 };

    private int grade = 0, petNum = 0, petNumMax = 17, effectType = 0;

    bool isGachaActive = false; // ��í ���� �߿� �Ǵٸ� ��í ���� ����
    bool isSpawnEnd = false;    // ���� ������ ���� ���� ����
    bool isDespawned = false;   // ���� �Ŀ� �ٽ� ���� ����


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
            petRender.SetActive(true);
            isGachaActive = true;
            effectType = Random.Range(0, 3); // ����Ʈ Ÿ�� ���� 3��
            //petNum = Random.Range(0, petNumMax);
            petNum = SetPetNumber();
            gachaCamera.SetActive(true);
            StartCoroutine(PetSpawn());
        }
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
            Debug.Log("�̹� ��� ģ���� ������ �ֽ��ϴ�.");
            return 0;
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
            Debug.Log("������ �����մϴ�.");
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
            Debug.Log("������ �����մϴ�.");
        }
    }


    public void PetDespawnButton()
    {
        if(isSpawnEnd && !isDespawned)
        {
            StartCoroutine(PetDespawn());
        }
    }

    IEnumerator PetSpawn() // �� �̱��迡�� ��ȯ
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
        //addedPet = MakeObject(Pets[petNum].prefab, petSpawner); // �� ���� �� petPosition�� �ڽ����� ����
        addedPet = MakeObject(MFDataManager.instance.mfarr[petNum].prefab, petSpawner); // �� ���� �� petPosition�� �ڽ����� ����
        anim = addedPet.GetComponentInChildren<Animator>();
        anim.SetBool("Spawn", true); // 9�� jump �ִϸ��̼� ���
        spawnEffect = MakeObject(spawn[3 * effectType + grade], effectSpawner);

        yield return Utill.WaitForSeconds(1f);

        postSpawnEffect = MakeObject(postSpawn[3 * effectType + grade], effectSpawner);
        additionalEffect = MakeObject(additional[3 * effectType + grade], effectSpawner);
        canvas.SetActive(true);
        isSpawnEnd = true;
    }

    IEnumerator PetDespawn() // �� �̱��迡�� ��ȯ
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

    private GameObject MakeObject(GameObject effect, Transform Spawner) // ������Ʈ ���� �Լ�
    {
        GameObject _effect = Instantiate(effect, Spawner);
        _effect.SetActive(true);
        return _effect;
    }

    private void DeleteObject(GameObject effect) // ����Ʈ ���� �Լ�
    {
        effect?.SetActive(false);
        Destroy(effect);
    }
}
