using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyDataBase))]
public class EnemyDBEditor : Editor
{
    private EnemyDataBase database;

    private void Awake()
    {
        database = (EnemyDataBase)target;
    }

    public override void OnInspectorGUI()
    {
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("RemoveAll"))
        {
            database.ClearDatabase();
        }
        if (GUILayout.Button("Remove"))
        {
            database.RemoveCurrentElement();
        }
        if (GUILayout.Button("Add"))
        {
            database.AddElement();
        }
        if (GUILayout.Button("<="))
        {
            database.GetPrev();
        }
        if (GUILayout.Button("=>"))
        {
            database.GetNext();
        }

        GUILayout.EndHorizontal();
        base.OnInspectorGUI();
    }
}
