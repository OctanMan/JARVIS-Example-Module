using System;
using System.Collections.Generic;
using JARVIS.Knowledge;
using JARVIS.Routing;

namespace ExampleJARVIS.Objects
{
    class Light  : IEvaluatable, IPacketReceiver
    {
        string identifier;

        byte brightness = 0;

        public Light(string identifier)
        {
            this.identifier = identifier;
        }

        public string Identifier
        {
            get { return identifier; }
        }

        public void ReceivePacket(IPacket packet)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Type> GetDesiredPacketType()
        {
            return new Type[] { typeof(LightSwitch) };
        }
    }
}
