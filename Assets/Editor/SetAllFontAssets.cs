using UnityEngine;
using UnityEditor;
using TMPro;

public class SetAllFontAssets : EditorWindow
{
    private TMP_FontAsset newFontAsset;

    [MenuItem("Tools/Set All TMP Font Assets")]
    public static void ShowWindow()
    {
        GetWindow<SetAllFontAssets>("Set All TMP Font Assets");
    }

    private void OnGUI()
    {
        GUILayout.Label("Set TMP Font Asset", EditorStyles.boldLabel);

        newFontAsset = (TMP_FontAsset)EditorGUILayout.ObjectField("Font Asset", newFontAsset, typeof(TMP_FontAsset), false);

        if (GUILayout.Button("Set Font Asset for All TMP Text"))
        {
            if (newFontAsset != null)
            {
                SetFontAssets(newFontAsset);
            }
            else
            {
                Debug.LogWarning("Please assign a TMP Font Asset first!");
            }
        }
    }

    private void SetFontAssets(TMP_FontAsset fontAsset)
    {
        TMP_Text[] tmpTexts = FindObjectsByType<TMP_Text>(UnityEngine.FindObjectsSortMode.None); // Find all TMP_Text components, including inactive ones

        int count = 0;
        foreach (TMP_Text tmpText in tmpTexts)
        {
            if (tmpText.font != fontAsset)
            {
                Undo.RecordObject(tmpText, "Set TMP Font Asset");
                tmpText.font = fontAsset;
                count++;
            }
        }

        Debug.Log($"Font Asset updated for {count} TMP Text objects.");
    }
}
