using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpUI : MonoBehaviour
{
    Text levelUp, levelUpShadow;

    private void Awake()
    {
        levelUpShadow = transform.GetChild(0).GetComponent<Text>();
        levelUp = transform.GetChild(1).GetComponent<Text>();
    }

    private void OnEnable()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(MG2_GameManager.Inst.Player.transform.position);
        transform.position = pos;
        levelUp.color = Color.yellow;
        levelUpShadow.color = Color.black;
    }

    void Update()
    {
        transform.position += transform.up * 30.0f * Time.deltaTime;
        levelUp.color -= Color.white * Time.deltaTime;
        levelUpShadow.color -= Color.white * 1.4f * Time.deltaTime;
        if(levelUpShadow.color.a < 0.01f)
        {
            gameObject.SetActive(false);
        }
    }



}
