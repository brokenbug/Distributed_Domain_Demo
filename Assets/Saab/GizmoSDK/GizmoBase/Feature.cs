//******************************************************************************
// File			: Feature.cs
// Module		: GizmoBase C#
// Description	: C# Bridge to gzFeature class
// Author		: Anders Mod�n		
// Product		: GizmoBase 2.10.4
//		
// Copyright � 2003- Saab Training Systems AB, Sweden
//			
// NOTE:	GizmoBase is a platform abstraction utility layer for C++. It contains 
//			design patterns and C++ solutions for the advanced programmer.
//
//
// Revision History...							
//									
// Who	Date	Description						
//									
// AMO	180301	Created file 	
//
//******************************************************************************

using System.Runtime.InteropServices;
using System;

namespace GizmoSDK
{
    namespace GizmoBase
    {
       
        public class Feature
        {
            static public void SetApplicationMask(UInt32 mask)
            {
                Feature_setApplicationMask(mask);
            }

            static public void SetRoleMask(UInt32 mask)
            {
                Feature_setRoleMask(mask);
            }

            static public bool HasAllowedFeature(string featureName)
            {
                return Feature_hasAllowedFeature(featureName);
            }

            static public Int32 GetFeatureExpirationDays(string featureName)
            {
                return Feature_getFeatureExpirationDays(featureName);
            }

            static public string GetFeatureSetIdentification()
            {
                return Marshal.PtrToStringUni(Feature_getFeatureSetIdentification());
            }

            static public bool ReadFeatures(string url , string elementName)
            {
                return Feature_readFeatures(url, elementName);
            }

            #region // --------------------- Native calls -----------------------
            [DllImport(GizmoSDK.GizmoBase.Platform.BRIDGE, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            private static extern void Feature_setApplicationMask(UInt32 mask);
            [DllImport(GizmoSDK.GizmoBase.Platform.BRIDGE, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            private static extern void Feature_setRoleMask(UInt32 mask);
            [DllImport(GizmoSDK.GizmoBase.Platform.BRIDGE, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            private static extern bool Feature_hasAllowedFeature(string featureName);
            [DllImport(GizmoSDK.GizmoBase.Platform.BRIDGE, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            private static extern Int32 Feature_getFeatureExpirationDays(string featureName);
            [DllImport(GizmoSDK.GizmoBase.Platform.BRIDGE, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            private static extern IntPtr Feature_getFeatureSetIdentification();
            [DllImport(GizmoSDK.GizmoBase.Platform.BRIDGE, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            private static extern bool Feature_readFeatures(string url, string elementName);
            #endregion
        }

                     

       
    }
}

