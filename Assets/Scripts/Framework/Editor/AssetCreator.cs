using Editor;
using UnityEditor;
using Models;
using Framework.Stylesheet;

public class AssetCreator
{
    [MenuItem("Assets/Create/ItemData")]
    public static void CreateItemData()
    {
        ScriptableObjectUtility.CreateAsset<ItemData>();
    }

    [MenuItem("Assets/Create/Stylesheet/FontData")]
    public static void CreateStylesheetFont()
    {
        ScriptableObjectUtility.CreateAsset<TextData>();
    }

    [MenuItem("Assets/Create/Stylesheet/ColorData")]
    public static void CreateStylesheetColor()
    {
        ScriptableObjectUtility.CreateAsset<ColorData>();
    }

    [MenuItem("Assets/Create/Stylesheet/ButtonData")]
    public static void CreateStylesheetButton()
    {
        ScriptableObjectUtility.CreateAsset<ButtonData>();
    }

    [MenuItem("Assets/Create/Stylesheet/ElementData")]
    public static void CreateStylesheetElement()
    {
        ScriptableObjectUtility.CreateAsset<ElementData>();
    }
}
