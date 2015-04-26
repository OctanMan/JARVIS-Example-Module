using System;
using JARVIS.Knowledge;
using JARVIS.Routing;
using JARVIS;
using JARVIS.Evaluation;
using System.Threading;

namespace ExampleJARVIS.Objects
{
    public class LightSwitch : IEvaluatable, IPacketSender
    {
        private LightSwitchWindow myUI;

        private UniqueIdentifier myIdentifier;

        public event PacketSendEvent packetSendEvent;

        public bool State { get; private set; }

        public LightSwitch(string name)
        {
            myIdentifier = new UniqueIdentifier(this, name);
            CreateNewWindow();
        }

        public UniqueIdentifier Identifier
        {
            get { return myIdentifier; }
        }

        private void SendPacket()
        {
            //Packet packet = new Packet(this, "state", this.state);
            if (packetSendEvent != null)
            {
                packetSendEvent(this, new PacketSendEventArgs(new Packet(this, "State", this.State)));
            }
        }

        internal void FlickSwitch(bool state)
        {
            this.State = state;
            SendPacket();
        }

        private void CreateNewWindow()
        {
            Thread thread = new Thread(() =>
            {
                myUI =  new LightSwitchWindow(this);
                myUI.Show();

                //NOTE: Closing works but shuts down the entire application - including JARVIS' Core/Router!
                // Really need a way of closing just this window's thread (for the lightswitch) and shutdown JARVIS separately 
                myUI.Closed += (sender, e) =>
                    myUI.Dispatcher.InvokeShutdown();

                System.Windows.Threading.Dispatcher.Run();
            });
            thread.Name = "LightSwitch UI Thread";
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }
    }
}
