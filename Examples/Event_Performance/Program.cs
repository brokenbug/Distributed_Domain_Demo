﻿//******************************************************************************
// File			: Program.cs
// Module		: Distribution Examples
// Description	: Basic examples for C# distribution
// Author		: Anders Modén		
//		
// Copyright © 2003- Saab Training Systems AB, Sweden
//			
// NOTE:	GizmoBase is a platform abstraction utility layer for C++. It contains 
//			design patterns and C++ solutions for the advanced programmer.
//
//
// Revision History...							
//									
// Who	Date	Description						
//									
// AMO	190923	Created file 	
//
//******************************************************************************

using System;

using GizmoSDK.GizmoBase;
using GizmoSDK.GizmoDistribution;

namespace Event_Performance
{
    
    class Program
    {
        const int COUNT = 1000;

        static void Main(string[] args)
        {
            // Initialize platforms for various used SDKs
            GizmoSDK.GizmoBase.Platform.Initialize();
            GizmoSDK.GizmoDistribution.Platform.Initialize();

            // Create a manager. The manager controls it all
            DistManager manager = DistManager.GetManager(true);

            DistTransportType protocol = DistTransportType.MULTICAST;

            string iface = "127.0.0.1"; // null;

            // Start the manager with settting for transport protocols
            manager.Start(DistRemoteChannel.CreateDefaultSessionChannel(false,protocol, iface), DistRemoteChannel.CreateDefaultServerChannel(false,protocol, iface));

            // Client set up. You are a client that sends and receives information
            DistClient client = new DistClient("PerfClient", manager);

            // We need to tell the client how to initialize
            client.Initialize(0,0,true);

            // Now we can get a session. A kind of a meeting room that is used to exchange various "topics"
            DistSession session = client.GetSession("PerfSession", true, true);

            // Joint that session and subribe all events
            client.JoinSession(session);
            client.SubscribeEvents(session); // Subscribe All Events

            // Create a delegete
            client.OnEvent += Client_OnEvent;

            Console.WriteLine($"Press <RETURN> to start sending");

            // Now loops around some simple program to get strings from console and distribute them as a message app

            while (true)
            {
                string result = Console.ReadLine();

                if (result == "quit")
                    break;

                // Create COUNT test events 

                Timer timer = new Timer();

                DistEvent [] e_arr=new DistEvent[COUNT];

                for (int i = 0; i < COUNT; i++)
                {
                    e_arr[i] = new DistEvent();
                    e_arr[i].SetAttributeValue("Cnt", i);

                    // Set some additional data e.g. two vec3
                    e_arr[i].SetAttributeValue("vec1", new Vec3(1,2,3));
                    e_arr[i].SetAttributeValue("vec2", new Vec3(4, 5, 6));
                }

                Console.WriteLine($"Created {COUNT} events in {timer.GetTime()} seconds -> Frequency: { timer.GetFrequency(COUNT)}");

                // Send COUNT events

                timer =new Timer();

                for(int i=0;i< COUNT; i++)
                    client.SendEvent(e_arr[i], session);

                Console.WriteLine($"Sent {COUNT} events in {timer.GetTime()} seconds -> Frequency: {timer.GetFrequency(COUNT)}");
            }

            while (manager.HasPendingData())
                System.Threading.Thread.Sleep(10);

            client.Uninitialize(true);
       
            // Some kind of graceful shutdown
            manager.Shutdown();


            // GC and platform uninit is managed by the system automatically
        }

        static int counter = 0;

        static Timer recv_timer=null;

        private static void Client_OnEvent(DistClient sender, DistEvent e)
        {
            // Check if message is from us
            if (e.GetSource() == sender.GetClientID().InstanceID)
                return;

            if(counter==0)
            {
                recv_timer = new Timer();
            }

            if (e.GetAttributeValue("Cnt") != counter)
                Console.WriteLine("Error");

            counter++;

            if(counter== COUNT)
            {
                Console.WriteLine($"Received {COUNT} events in {recv_timer.GetTime()} seconds -> Frequency: {recv_timer.GetFrequency(COUNT)} ");
                counter = 0;
            }

            
        }
    }
}
