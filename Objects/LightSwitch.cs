using System;
using JARVIS.Knowledge;
using JARVIS.Routing;

namespace ExampleJARVIS.Objects
{
    class LightSwitch : IEvaluatable, IPacketSender
    {
        string identifier;

        bool state = false;

        public LightSwitch(string identifier)
        {
            this.identifier = identifier;
        }

        public string Identifier
        {
            get { return identifier; }
        }

        public void SendPacket()
        {
            IPacket packet = new Packet(this, "state", this.state);

            throw new NotImplementedException();
        }

        public System.Collections.Generic.List<System.Type> GetDesiredPacketType()
        {
            throw new NotImplementedException();
        }
    }
}
