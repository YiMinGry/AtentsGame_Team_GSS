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

    IEnumerator PetSpawn() // 펫 뽑기기계에서 소환
    {
        glow.SetActive(true); // glow 이펙트 먼저 나옴

        yield return Utill.WaitForSeconds(2f);

        glow.SetActive(false); // glow 이펙트 꺼짐

        Pet.SetActive(true); // 펫 오브젝트 활성화

        anim.SetInteger("animation", 9); // 9번 jump 애니메이션 재생

        appear.SetActive(true);

        yield return Utill.WaitForSeconds(1f);

        anim.SetInteger("animation", 19); // 19번 IdleA 애니메이션 재생 (전투중 Idle 모션)

        spark.SetActive(true);

        heart.SetActive(true);
    }

}
