using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ItemDatabaseCreator : MonoBehaviour
{
    [MenuItem("Databases/Create database")]
    public static void CreateItemDatabase()
    {
        ItemDatabase newDatabase = ScriptableObject.CreateInstance<ItemDatabase>();
        AssetDatabase.CreateAsset(newDatabase, "Assets/ItemDatabase.asset");
        AssetDatabase.Refresh();
    }
}
