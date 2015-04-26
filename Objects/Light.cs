﻿using System;
using System.Collections.Generic;
using JARVIS.Knowledge;
using JARVIS.Routing;
using JARVIS;
using JARVIS.Evaluation;

namespace ExampleJARVIS.Objects
{
    class Light  : IEvaluatable, IPacketReceiver
    {
        UniqueIdentifier identifier;

        EvaluationPreference[] interests = new EvaluationPreference[] { new TypePreference(typeof(Light)) };

        public bool State { get; private set; }

        byte brightness = 0;

        public Light(string name)
        {
            identifier = new UniqueIdentifier(this, name);
        }

        public UniqueIdentifier Identifier
        {
            get { return identifier; }
        }

        public void ReceivePacket(Packet packet)
        {
            Console.WriteLine("THE LIGHT IS... TURNED ON...");
            this.State = packet.Value;
        }

        public IEnumerable<EvaluationPreference> WhatIBeInterestedIn
        {
            get { return interests; }
        }
    }
}