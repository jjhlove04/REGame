using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameMenu))]
[CanEditMultipleObjects]
class CustomInspecter : Editor
{
    public GameMenu selected;

    private void OnEnable()
    {
        // target�� Editor�� �ִ� ������ ������ ������Ʈ�� �޾ƿ�.
        if (AssetDatabase.Contains(target))
        {
            selected = null;
        }
        else
        {
            // target�� Object���̹Ƿ� Enemy�� ����ȯ
            selected = (GameMenu)target;
        }
    }

    SerializedProperty kindProp;
    SerializedProperty stopButtonProp;
    SerializedProperty settingProp;
    SerializedProperty menuObjProp;
    SerializedProperty gameOverProp;
    SerializedProperty sceneNameProp;

    private void Awake()
    {
        kindProp = serializedObject.FindProperty("kind");
        stopButtonProp = serializedObject.FindProperty("stopButton");
        settingProp = serializedObject.FindProperty("settingObj");
        menuObjProp = serializedObject.FindProperty("menuObj");
        gameOverProp = serializedObject.FindProperty("gameOver");
        sceneNameProp = serializedObject.FindProperty("sceneName");
    }

    public override void OnInspectorGUI()
    {
        if (selected == null)
        {
            return;
        }
        EditorGUILayout.PropertyField(kindProp);;
        if (selected.kind == GameMenu.Kind.RESTART)
        {
            EditorGUILayout.PropertyField(sceneNameProp);
        }

        else if(selected.kind == GameMenu.Kind.GAMESTOP)
        {
            EditorGUILayout.PropertyField(menuObjProp);
        }

        else if (selected.kind == GameMenu.Kind.RESUME)
        {
            EditorGUILayout.PropertyField(menuObjProp);
            EditorGUILayout.PropertyField(stopButtonProp);
        }

        else if (selected.kind == GameMenu.Kind.SETTING)
        {
            EditorGUILayout.PropertyField(settingProp);
        }

        else if (selected.kind == GameMenu.Kind.GAMEOVER)
        {
            EditorGUILayout.PropertyField(gameOverProp);
        }

        else if (selected.kind == GameMenu.Kind.SETTINGEXIT)
        {
            EditorGUILayout.PropertyField(settingProp);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
