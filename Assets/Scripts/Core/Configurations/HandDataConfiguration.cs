using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace TwoHandThrowing.Core
{
    public class HandDataConfiguration : Configuration
    {
        public List<HandDataSettings> HandDataSettings;

        [CanBeNull]
        public HandDataSettings FindHandDataSettings(HandDataType handDataType)
        {
            HandDataSettings res = null;
            foreach (var handDataSetting in HandDataSettings)
            {
                if (handDataSetting.HandDataType == handDataType)
                {
                    res = handDataSetting;
                }
            }

            return res;
        }
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
