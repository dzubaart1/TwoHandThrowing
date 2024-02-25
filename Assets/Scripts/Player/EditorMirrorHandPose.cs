using UnityEditor;
using UnityEngine;

namespace TwoHandThrowing.Player
{
    [CustomEditor(typeof(MirrorHandPose))]
    public class EditorMirrorHandPose : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            MirrorHandPose mirrorHandPose = target as MirrorHandPose;

            if (GUILayout.Button("Save Init Pose"))
            {
                mirrorHandPose.SaveInitPose();
            }

            if (GUILayout.Button("Reset Pose"))
            {
                mirrorHandPose.ResetPose();
            }

            if (GUILayout.Button("Update Pose"))
            {
                mirrorHandPose.UpdatePose();
            }
        }
    }
}
