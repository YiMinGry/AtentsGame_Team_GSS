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

    public MiniFriendData[] Pets = null;
    [SerializeField]
    Text nameText, gradeText, abilityText;

    private GameObject addedPet;
    public GameObject[] preSpawn, spawn, additional, postSpawn, deSpawn; // ����Ʈ ����
    private GameObject preSpawnEffect, spawnEffect, additionalEffect, postSpawnEffect, deSpawnEffect; // ����Ʈ ���� ���� �Ҵ�
    public GameObject canvas = null;
    public GameObject gachaCamera = null;

    private enum GRADE { GRADE_C = 0, GRADE_B = 1, GRADE_A = 2 };

    private int grade = 0, petNum = 0, petNumMax = 17, effectType = 0;

    bool isGachaActive = false; // ��í ���� �߿� �Ǵٸ� ��í ���� ����

    float time = 1.0f;


    private void Start()
    {
        petNumMax = Pets.Length;
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
            petNum = Random.Range(0, petNumMax);
            gachaCamera.SetActive(true);
            StartCoroutine(PetSpawn());
        }
    }

    public void NormalPetSpawnButton()
    {
        SetNormalGrade();
        PetSpawnButton();
    }

    public void RarePetSpawnButton()
    {
        SetRareGrade();
        PetSpawnButton();
    }


    public void PetDespawnButton()
    {
        StartCoroutine(PetDespawn());
    }

    IEnumerator PetSpawn() // �� �̱��迡�� ��ȯ
    {
        nameText.text = Pets[petNum].friendName;
        gradeText.text = grade.ToString();
        abilityText.text = Pets[petNum].buff_MiniGame5;
        preSpawnEffect = MakeObject(preSpawn[3 * effectType + grade], effectSpawner);

        yield return Utill.WaitForSeconds(2f);

        DeleteObject(preSpawnEffect);
        addedPet = MakeObject(Pets[petNum].prefab, petSpawner); // �� ���� �� petPosition�� �ڽ����� ����
        anim = addedPet.GetComponentInChildren<Animator>();
        anim.SetBool("Spawn", true); // 9�� jump �ִϸ��̼� ���
        spawnEffect = MakeObject(spawn[3 * effectType + grade], effectSpawner);

        yield return Utill.WaitForSeconds(1f);

        postSpawnEffect = MakeObject(postSpawn[3 * effectType + grade], effectSpawner);
        additionalEffect = MakeObject(additional[3 * effectType + grade], effectSpawner);
        canvas.SetActive(true);
    }

    IEnumerator PetDespawn() // �� �̱��迡�� ��ȯ
    {
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
        //effect = Instantiate(effect, Spawner); // ���� �� Spawner�� �ڽ����� ����
        //effect.SetActive(true);

        GameObject _effect = Instantiate(effect, Spawner);
        _effect.SetActive(true);
        return _effect;
    }

    private void DeleteObject(GameObject effect) // ����Ʈ ���� �Լ�
    {
        effect.SetActive(false);
        Destroy(effect);
    }
}
