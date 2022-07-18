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
    private GameObject dashEffect;

    [SerializeField]
    private GameObject dashEffect2;

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
        dashEffect.SetActive(_tf);
    }

    public void SetDashEffectScale(Vector3 scale)
    {
        dashEffect.transform.localScale = scale;
    }

    public void MakeDashEffect()
    {
        GameObject obj = Instantiate(dashEffect2, 
            player.transform.position - 3.0f * player.transform.right * player.transform.localScale.x + new Vector3(0,player.transform.position.y * 0.3f,0), 
            Quaternion.Euler(-90,0,0));
        //obj.transform.parent = null;
    }

}
