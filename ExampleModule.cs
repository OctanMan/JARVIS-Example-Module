using System;
using System.Collections.Generic;
using JARVIS.Modules;
using JARVIS.Knowledge;
using JARVIS.Routing;
using ExampleJARVIS.Objects;
using System.Timers;
using JARVIS.Evaluation;
using System.Threading;
using JARVIS.Specification;

namespace ExampleJARVIS
{
    class ExampleModule : JModule
    {

        LightSwitch lightSwitch;
        public ExampleModule()
        {
            name = "J.A.R.V.I.S. Example Module";
        }

        public override void Load()
        {
            Light light = new Light("Kitchen Light");

            lightSwitch = new LightSwitch("Kitchen Light Switch");
            
            Spec<LightSwitch> turnedOn = new Spec<LightSwitch>(l => l.State);
            Condition lightIsOn = Condition.Create(turnedOn, lightSwitch);
            Packet satisfactionPacket = new Packet(light, "State", true);
            Rule rule = new Rule("Turn light on when switch pressed", lightIsOn, satisfactionPacket);

            Register(light);
            Register(lightSwitch);
            Register(rule);
            OnCreateNewWindow(this);
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

            if(thing is IRule)
            {
                rules.Add(thing);
            }
        }

        private void OnCreateNewWindow(object sender)
        {
            Thread thread = new Thread(() =>
            {
                Window1 w = new Window1();
                w.Show();

                //NOTE: Closing works but shuts down the entire application - including JARVIS' Core/Router!
                // Really need a way of closing just this window's thread (for the lightswitch) and shutdown JARVIS separately 
                w.Closed += (sender2, e2) =>
                    w.Dispatcher.InvokeShutdown();
                w.addDemoSwitch(lightSwitch);

                System.Windows.Threading.Dispatcher.Run();
            });
            thread.Name = "WindowsThread";
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }
    }
}