using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame5_Item : MonoBehaviour
{
    public ItemState itemType;
    public int value = 1;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            MiniGame5_SoundManager.Inst.PlayEatItemClip();
            switch (itemType)
            {
                case ItemState.ScoreItem:
                    MiniGame5_GameManager.Inst.Score += value;
                    break;
                case ItemState.HealItem:
                    MiniGame5_GameManager.Inst.Life += 0.1f;
                    break;
                case ItemState.CoinItem:
                    MiniGame5_GameManager.Inst.Coin += value;
                    break;
                case ItemState.BonusItem:
                    MiniGame5_GameManager.Inst.BonusTimeIndex = GetComponent<MiniGame5_SetBonusTimeText>().Index;
                    break;
                case ItemState.MagnetItem:
                    MiniGame5_GameManager.Inst.Player.MagnetModeOn();
                    break;
                case ItemState.BigItem:
                    MiniGame5_GameManager.Inst.Player.BigModeOn();
                    break;
                case ItemState.StageUpItem:
                    MiniGame5_GameManager.Inst.StageLevel = value;
                    break;
            }
            
            Destroy(this.gameObject);
        }
    }

    public void OnMagnet(Vector3 targetPos)
    {
        //if (GetComponent<ItemRotator>() != null) transform.LookAt(targetPos);

        Vector3 dir = 10f * Time.deltaTime * (targetPos - transform.position).normalized;
        transform.Translate(dir);
    }
}
