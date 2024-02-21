using UnityEditor;
using UnityEngine;

namespace Player
{
    [CustomEditor(typeof(HandPose))]
    public class EditorHandPose : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            HandPose handPose = target as HandPose;

            if (GUILayout.Button("Create Attach Point"))
            {
                handPose.CreateAttachPoint();
            }
        }
    }
}