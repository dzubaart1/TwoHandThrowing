﻿using System;
using System.Reflection;
using UnityEngine;

namespace MaterialFactory.Tools
{
    public static class ExtensionMethods
    {
		public static T GetCopyOf<T>(this Component comp, T other) where T : Component
		{
			if(other is null)
            {
				return null;
            }

			Type type = comp.GetType();
			if (type != other.GetType())
			{
				return null;
			}

			BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Default | BindingFlags.DeclaredOnly;
			PropertyInfo[] pinfos = type.GetProperties(flags);
			foreach (var pinfo in pinfos)
			{
				if (pinfo.CanWrite)
				{
					try
					{
						pinfo.SetValue(comp, pinfo.GetValue(other, null), null);
					}
					catch { }
				}
			}

			FieldInfo[] finfos = type.GetFields(flags);
			foreach (var finfo in finfos)
			{
				finfo.SetValue(comp, finfo.GetValue(other));
			}
			return comp as T;
		}
	}
}
