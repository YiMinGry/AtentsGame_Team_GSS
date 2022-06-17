using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetGacha : MonoBehaviour
{
    public Transform petPosition = null;
    private Animator anim = null;

    public GameObject[] Pet = null;
    public GameObject appear = null, glow = null, heart = null, spark = null;
    public GameObject disappear = null;
    public GameObject canvas = null;
    public GameObject gachaCamera = null;

    private GameObject petObj = null;

    public int petNum = 0; // �� ��ȣ

    public void PetSpawnButton()
    {
        gachaCamera.SetActive(true);
        StartCoroutine(PetSpawn());
    }

    public void PetDespawnButton()
    {
        gachaCamera.SetActive(false);
        StartCoroutine(PetDespawn());
    }

    IEnumerator PetSpawn() // �� �̱��迡�� ��ȯ
    {
        glow.SetActive(true); // glow ����Ʈ ���� ����

        yield return Utill.WaitForSeconds(2f);

        glow.SetActive(false); // glow ����Ʈ ����
        petObj = Instantiate(Pet[petNum], petPosition); // �� ���� �� petPosition�� �ڽ����� ����
        anim = petObj.GetComponent<Animator>();
        anim.SetInteger("animation", 9); // 9�� jump �ִϸ��̼� ���
        appear.SetActive(true);

        yield return Utill.WaitForSeconds(1f);

        anim.SetInteger("animation", 19); // 19�� IdleA �ִϸ��̼� ��� (������ Idle ���)
        spark.SetActive(true);
        heart.SetActive(true);
        canvas.SetActive(true);
    }    

    IEnumerator PetDespawn() // �� �̱��迡�� ��ȯ
    {
        anim.SetInteger("animation", 9);
        canvas.SetActive(false);
        appear.SetActive(false);
        spark.SetActive(false);
        heart.SetActive(false);

        yield return Utill.WaitForSeconds(0.7f);

        disappear.SetActive(true);
        Destroy(petObj);

        yield return Utill.WaitForSeconds(1f);

        disappear.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
