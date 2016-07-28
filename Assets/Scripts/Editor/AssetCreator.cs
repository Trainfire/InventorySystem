using Editor;
using UnityEditor;
using Models;

public class AssetCreator
{
    [MenuItem("Assets/Create/ItemData")]
    public static void CreateItemData()
    {
        ScriptableObjectUtility.CreateAsset<ItemData>();
    }
}