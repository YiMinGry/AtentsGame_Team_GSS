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
        StartCoroutine(Gacha());
    }

    IEnumerator Gacha()
    {
        glow.SetActive(true);

        yield return Utill.WaitForSeconds(2f);

        glow.SetActive(false);

        spark.SetActive(true);

        Pet.SetActive(true);

        anim.SetInteger("animation", 9);

        appear.SetActive(true);

        yield return Utill.WaitForSeconds(1f);

        anim.SetInteger("animation", 19);

        heart.SetActive(true);
    }

}
