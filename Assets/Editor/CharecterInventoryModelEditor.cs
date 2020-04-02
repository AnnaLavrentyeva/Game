using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;

[CustomEditor(typeof(CharacterInventoryModel))]
public class CharecterInventoryModelEditor : Editor
{
    CharacterInventoryModel isTarget;
    public override void OnInspectorGUI()
    {
        isTarget = (CharacterInventoryModel)target;
        DrawDefaultInspector();
        if (Application.isPlaying == true)
        {
            var itemTypeValues = (ItemType[])Enum.GetValues(typeof(ItemType));

            for (int i = 0; i < itemTypeValues.Length; ++i)
            {
                int itemCount = isTarget.GetItemCount(itemTypeValues[i]);

                if (itemCount > 0)
                {
                    EditorGUILayout.LabelField(itemTypeValues[i].ToString(), itemCount.ToString());

                }
            }
        }
    }

}
