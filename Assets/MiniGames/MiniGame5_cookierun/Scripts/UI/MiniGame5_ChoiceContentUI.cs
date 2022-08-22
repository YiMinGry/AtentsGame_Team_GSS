using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGame5_ChoiceContentUI : MonoBehaviour
{
    MiniFriendData data;
    public MiniFriendData Data
    {
        get => data;
        set
        {
            data = value;
        }
    }

    ChoiceState state;
    public ChoiceState State { get; set; }
    Text friendName;
    Image sprite;
    Transform NotHave;
    Transform Have;
    Transform NotChecked;
    Transform Checked;
    Button chooseBtn;

    bool isChecked = false;
    public bool IsChecked
    {
        get => isChecked;
        set
        {
            if (isChecked != value)
            {
                isChecked = value;

                if (isChecked)
                {
                    switch (state)
                    {
                        case ChoiceState.RunFriend:
                            MiniGame5_GameManager.Inst.RunnerData = data;
                            break;
                        case ChoiceState.NextRunFriend:
                            MiniGame5_GameManager.Inst.NextRunnerData = data;
                            break;
                        case ChoiceState.BuffFriend:
                            MiniGame5_GameManager.Inst.PetData = data;
                            break;
                    }
                }
                
                NotChecked.gameObject.SetActive(!isChecked);
                Checked.gameObject.SetActive(isChecked);
                //transform.SetAsFirstSibling();
            }
        }
    }

    private void Awake()
    {
        state = ChoiceState.RunFriend;
        friendName = transform.Find("Name").GetComponent<Text>();
        sprite = transform.Find("Sprite").GetComponent<Image>();
        NotHave = transform.Find("NotHave");
        Have = transform.Find("Have");
        NotChecked = Have.Find("ChooseBtn");
        Checked = Have.Find("Checked");

        chooseBtn = NotChecked.GetComponent<Button>();
    }

    public void Init()
    {
        friendName.text = data.friendName;
        sprite.sprite = data.sprite;
        if (data.isHave)
        {
            NotHave.gameObject.SetActive(false);
            Have.gameObject.SetActive(true);

            NotChecked.gameObject.SetActive(true);
            Checked.gameObject.SetActive(false);
        }
        else
        {
            NotHave.gameObject.SetActive(true);
            Have.gameObject.SetActive(false);
        }

        chooseBtn.onClick.AddListener(() => IsChecked = true);
    }

    public void OnCheck()
    {
        isChecked = true;
        NotChecked.gameObject.SetActive(!isChecked);
        Checked.gameObject.SetActive(isChecked);
    }
}
