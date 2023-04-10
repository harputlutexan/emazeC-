using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(testCollider))]
public class PlayerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        testCollider myScript = (testCollider)target;

        if (GUILayout.Button("palm collider olustur"))
        {
            myScript.olustur();
        }
        if(GUILayout.Button("Circle oluştur"))
        {
            myScript.circleOlustur();
        }
        if (GUILayout.Button("palm collider sil"))
        {
            myScript.sil();
        }
        if (GUILayout.Button("Y ekseninde yansımasını al"))
        {
            myScript.yansıma();
        }
    }
}
