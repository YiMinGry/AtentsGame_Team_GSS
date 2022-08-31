using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG2_EffectManager : MonoBehaviour
{
    public MG2_GameManager mg2_GameManager;

    Fish_Player player;

    [SerializeField]
    private GameObject levelUpEffect;

    [SerializeField]
    private GameObject dashEffectTrail;

    [SerializeField]
    private GameObject dashEffectElectricity;

    [SerializeField]
    private GameObject hpLossEffect;

    [SerializeField]
    private GameObject eatEffect;

    [SerializeField]
    private GameObject damageEffect;

    private void Awake()
    {
        player = mg2_GameManager.Player;
    }

    public void SetLevelUpEffect(bool _tf)
    {
        levelUpEffect.SetActive(_tf);
    }

    public void SetDashEffect(bool _tf)
    {
        dashEffectTrail.SetActive(_tf);
    }

    public void SetDashEffectScale(Vector3 scale)
    {
        dashEffectTrail.transform.localScale = scale;
    }

    public void MakeDashEffect()
    {
        GameObject obj = Instantiate(dashEffectElectricity, 
            player.transform.position - 3.0f * player.transform.right * player.transform.localScale.x + new Vector3(0,player.transform.position.y * 0.3f,0), 
            Quaternion.Euler(-90,0,0));
        //obj.transform.parent = null;
    }

    public void MakeHPLossEffect(Vector3 _pos)
    {
        GameObject obj = Instantiate(hpLossEffect, _pos, Quaternion.Euler(0,0,0));
    }

    public void MakeDamageEffect(Vector3 _pos)
    {
        GameObject obj = Instantiate(damageEffect, _pos, Quaternion.Euler(0, 0, 0));
    }
    public void MakeEatEffect(Vector3 _pos)
    {
        GameObject obj = Instantiate(eatEffect, _pos, Quaternion.Euler(0, 0, 0));
    }
}
