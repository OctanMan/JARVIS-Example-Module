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

        byte brightness = 0;

        public Light(string name)
        {
            identifier = new UniqueIdentifier("name");
        }

        public UniqueIdentifier Identifier
        {
            get { return identifier; }
        }

        public void ReceivePacket(Packet packet)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<WhatIBeInterestedIn> WhatIBeInterestedIn
        {
            get { return interests; }
        }
    }
}