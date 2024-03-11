using UnityEditor;
using UnityEngine;

namespace TwoHandThrowing.Player
{
#if UNITY_EDITOR
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
#endif
}