using System;
using System.Collections.Generic;
using JARVIS.Modules;
using JARVIS.Knowledge;
using JARVIS.Routing;
using ExampleJARVIS.Objects;
using System.Timers;
using JARVIS.Evaluation;
using System.Threading;
using System.Windows;

namespace ExampleJARVIS
{
    class ExampleModule : JModule
    {
        public ExampleModule()
        {
            name = "J.A.R.V.I.S. Example Module";
        }

        public override void Load()
        {
            Light light = new Light("Kitchen Light");

            LightSwitch lightSwitch = new LightSwitch("Kitchen Light Switch");

            Register(light);
            Register(lightSwitch);
        }

        public override void Unload() { }

        private void Register(dynamic thing)
        {
            if(thing is IPacketReceiver)
            {
                receivers.Add(thing);
            }

            if (thing is IPacketSender)
            {
                senders.Add(thing);
            }

            if (thing is IEvaluatable)
            {
                evaluatables.Add(thing);
            }
        }

        private void OnCreateNewWindow(object sender, RoutedEventArgs e)
        {
            Thread thread = new Thread(() =>
            {
                Window1 w = new Window1();
                w.Show();

                w.Closed += (sender2, e2) =>
                    w.Dispatcher.InvokeShutdown();

                System.Windows.Threading.Dispatcher.Run();
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }
    }
}