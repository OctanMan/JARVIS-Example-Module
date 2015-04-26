using System;
using System.Collections.Generic;
using JARVIS.Knowledge;
using JARVIS.Routing;
using JARVIS;
using JARVIS.Evaluation;
using System.Threading;
using System.ComponentModel;

namespace ExampleJARVIS.Objects
{
    public class Light  : IEvaluatable, IPacketReceiver
    {
        private LightWindow myUI;

<<<<<<< Updated upstream
        EvaluationPreference[] interests = new EvaluationPreference[] { new TypePreference(typeof(Light)) };
=======
        private UniqueIdentifier identifier;

        private WhatIBeInterestedIn[] interests = new WhatIBeInterestedIn[] { new TypeIBeInterestedIn(typeof(Light)) };
>>>>>>> Stashed changes

        public bool State { get; private set; }

        byte brightness = 0;

        public Light(string name)
        {
            identifier = new UniqueIdentifier(this, name);
            CreateNewWindow();
        }

        public UniqueIdentifier Identifier
        {
            get { return identifier; }
        }

        public void ReceivePacket(Packet packet)
        {
            Console.WriteLine(this.identifier.handle+" recieved a Packet");
            this.State = packet.Value;

            //May want to implement MVC/MVVM properly but this is simple for now
            myUI.Dispatcher.Invoke(() => myUI.UpdateVisual(State));
        }

        public IEnumerable<EvaluationPreference> WhatIBeInterestedIn
        {
            get { return interests; }
        }



        private void CreateNewWindow()
        {
            Thread thread = new Thread(() =>
            {
                myUI = new LightWindow();
                myUI.Show();

                //NOTE: Closing works but shuts down the entire application - including JARVIS' Core/Router!
                // Really need a way of closing just this window's thread (for the lightswitch) and shutdown JARVIS separately 
                myUI.Closed += (sender, e) =>
                    myUI.Dispatcher.InvokeShutdown();

                System.Windows.Threading.Dispatcher.Run();
            });
            thread.Name = "Light UI Thread";
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

    }
}