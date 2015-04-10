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
            var lightIsOn = new Condition(() => turnedOn.IsSatisfiedBy(lightSwitch));
            Rule rule = new Rule("Rule #1", lightIsOn, new WhatIBeInterestedIn[]{new TypeIBeInterestedIn(typeof(LightSwitch))});

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