using System;
using System.Collections.Generic;
using UnityEngine;

namespace TwoHandThrowing.Core
{
    public class HandDataConfiguration : Configuration
    {
        public List<HandDataSettings> HandDataSettings;
    }

    [Serializable]
    public class HandDataSettings
    {
        public HandDataType HandDataType;
        public RuntimeAnimatorController LeftHandAnimatorController;
        public RuntimeAnimatorController RightHandAnimatorController;
        public Material HandMaterial;
    }

    public enum HandDataType
    {
        Common,
        Goalkeeper
    }
}
