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

        public bool State { get; private set; }

        public LightSwitch(string name)
        {
           identifier = new UniqueIdentifier(this, name);
        }

        public UniqueIdentifier Identifier
        {
            get { return identifier; }
        }

        public void SendPacket()
        {
            //Packet packet = new Packet(this, "state", this.state);
            if (packetSendEvent != null)
            {
                packetSendEvent(this, new PacketSendEventArgs(new Packet(this, "State", this.State)));
            }
        }

        public System.Collections.Generic.List<System.Type> GetDesiredPacketType()
        {
            throw new NotImplementedException();
        }


        internal void FlickSwitch(bool state)
        {
            this.State = state;
        }

        public event PacketSendEvent packetSendEvent;
    }
}
