using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame5_DataManager : MonoBehaviour
{
    public MiniFriendData[] runnerData;
    public MiniFriendData[] petData;

    public GameObject choiceContentUI;
    public RuntimeAnimatorController startAnimCon;
    public RuntimeAnimatorController mainAnimCon;
    public PhysicMaterial coliMat;

    public int runnerLength { get => runnerData.Length; }
    public int petLength { get => petData.Length; }
}
