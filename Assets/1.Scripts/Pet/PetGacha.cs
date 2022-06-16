using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetGacha : MonoBehaviour
{
    public GameObject Pet = null;
    public GameObject appear = null, glow = null, heart = null, spark = null;
    private Animator anim = null;

    private void Awake()
    {
        anim = Pet.GetComponent<Animator>();

    }

    private void Start()
    {
        StartCoroutine(PetSpawn());
    }

    IEnumerator PetSpawn() // �� �̱��迡�� ��ȯ
    {
        glow.SetActive(true); // glow ����Ʈ ���� ����

        yield return Utill.WaitForSeconds(2f);

        glow.SetActive(false); // glow ����Ʈ ����

        Pet.SetActive(true); // �� ������Ʈ Ȱ��ȭ

        anim.SetInteger("animation", 9); // 9�� jump �ִϸ��̼� ���

        appear.SetActive(true);

        yield return Utill.WaitForSeconds(1f);

        anim.SetInteger("animation", 19); // 19�� IdleA �ִϸ��̼� ��� (������ Idle ���)

        spark.SetActive(true);

        heart.SetActive(true);
    }

}
