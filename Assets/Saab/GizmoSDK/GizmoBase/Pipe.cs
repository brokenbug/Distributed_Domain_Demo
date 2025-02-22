//******************************************************************************
// File			: Serialize.cs
// Module		: GizmoBase C#
// Description	: C# Bridge to gzSerialize.cpp
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
        public enum PipeConnection
        {
            Local,
            Global,
        }

        public class Pipe : SerializeAdapter
        {
            
            public Pipe(IntPtr nativeReference) : base(nativeReference) { }

            public static Pipe Connect(string url, bool create, PipeConnection connection = PipeConnection.Global, uint timeout = 50, string options = null)
            {
                var res = Pipe_connect(url, create, connection, timeout, options);
                if (res == null)
                    throw new Exception("an error occured during pipe connect");

                return new Pipe(res);
            }

            public static bool SetPortStart(UInt16 port)
            {
                return Pipe_setPortStart(port);
            }

            public static bool SetPortRange(UInt16 count)
            {
                return Pipe_setPortRangle(count);
            }

            public bool Connected
            {
                get { return Pipe_connected(GetNativeReference()); }
            }

            #region -------------- Native calls ------------------

            [DllImport(Platform.BRIDGE, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            private static extern IntPtr Pipe_connect(string url, bool create, PipeConnection connection, uint timeout, string options);
            [DllImport(Platform.BRIDGE, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            private static extern bool Pipe_setPortStart(UInt16 port);
            [DllImport(Platform.BRIDGE, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            private static extern bool Pipe_setPortRangle(UInt16 count);
            [DllImport(Platform.BRIDGE, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            private static extern bool Pipe_connected(IntPtr pipe);
            

            #endregion
        }
    }
}
