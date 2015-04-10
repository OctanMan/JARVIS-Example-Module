using System;
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

        WhatIBeInterestedIn[] interests = new WhatIBeInterestedIn[] { new TypeIBeInterestedIn(typeof(LightSwitch)) };

        public bool State { get; private set; }

        byte brightness = 0;

        public Light(string name)
        {
            identifier = new UniqueIdentifier(name);
        }

        public UniqueIdentifier Identifier
        {
            get { return identifier; }
        }

        public void ReceivePacket(Packet packet)
        {
            Console.WriteLine("I AM A PACKET");
            this.State = packet.Value;
        }

        public IEnumerable<WhatIBeInterestedIn> WhatIBeInterestedIn
        {
            get { return interests; }
        }
    }
}