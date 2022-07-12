#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UnitSkinManager))]
public class UnitSkinRandomSet : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        UnitSkinManager generator = (UnitSkinManager)target;
        if (GUILayout.Button("Random Set"))
        {
            generator.RandomSet();
        }
    }
}
#endif
