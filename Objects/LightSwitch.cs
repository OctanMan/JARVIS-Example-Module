using System;
using JARVIS.Knowledge;
using JARVIS.Routing;
using JARVIS;
using JARVIS.Evaluation;

namespace ExampleJARVIS.Objects
{
    class LightSwitch : IEvaluatable, IPacketSender
    {
        UniqueIdentifier identifier;

        bool state = false;

        public LightSwitch(string name)
        {
           identifier = new UniqueIdentifier(name);
        }

        public UniqueIdentifier Identifier
        {
            get { return identifier; }
        }

        public void SendPacket()
        {
            Packet packet = new Packet(this, "state", this.state);
            

            //throw new NotImplementedException();
        }

        public System.Collections.Generic.List<System.Type> GetDesiredPacketType()
        {
            throw new NotImplementedException();
        }

        internal void flickSwitch()
        {
            this.state = !this.state;
        }
    }
}
