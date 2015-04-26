using JARVIS.Modules;
using JARVIS.Knowledge;
using JARVIS.Routing;
using JARVIS.Evaluation;
using JARVIS.Specification;
using ExampleJARVIS.Objects;
using JARVIS.Evaluation.Preferences;
using System.Collections.Generic;

namespace ExampleJARVIS
{
    /// <summary>
    /// This Example Module is intended to demonstrate the role of an IModule in the JARVIS Framework.
    /// It inherits from JModule which provides much of the 'boilerplate' code required by the interface.
    /// Feel free to do the same for your IModule ^_^
    /// </summary>
    class ExampleModule : JModule
    {
        public ExampleModule()
        {
            name = "J.A.R.V.I.S. Example Module";
        }

        /// <summary>
        /// The Load method is called to initialise an IModule, enabling it to create any object instances required in its domain.
        /// This example creates a LightSwitch (IEvaluatable, IPacketSender), a Light (IEvaluatable, IPacketReceiver) and a simple
        /// IRule that says to turn the Light on when the LightSwitch is flicked to the 'on' state. This is possible because the 
        /// LightSwitch is an IEvaluatable, meaning it's properties ('State' in this case) are able to be evaluated by an IRule.
        /// </summary>
        public override void Load()
        {
            //---First, create the objects in the Kitchen domain---

            //Create a Light
            Light light = new Light("Kitchen Light");

            //Create a LightSwitch
            LightSwitch lightSwitch = new LightSwitch("Kitchen Light Switch");


            //---Now for the fun part - Rules---

            //Using JARVIS' flexible, built-in Rule system, just follow these 4 steps:

            //1. Define a Specification - encapsulate the logic of a LightSwitch in the 'On' state into a Spec<T> object
            Spec<LightSwitch> isTurnedOn = new Spec<LightSwitch>(ls => ls.State == true);

            //2. Create a Condition - say that the logical evaluation, isTurnedOn, should be performed on the lightSwitch instance
            Condition lightSwitchIsOn = Condition.Create(isTurnedOn, lightSwitch);

            //3. Define a Packet to be sent upon the lightSwitchIsOn Condition being satisfied (basically if it evaluates as being 'true')
            Packet satisfactionPacket = new Packet(light, "State", true);

            //4. Compose the Condition and Packet above into a new Rule with a highly verbose name
            Rule JarvisRule = new Rule("Turn the kitchen light on when the kitchen light switch is pressed on", lightSwitchIsOn, satisfactionPacket);


            //Alternitavely, you may wish to create a custom 'hard-coded' IRule yourself:

            //This one will evaluate if the LightSwitch is turned off. See the class below.
            MyCustomRule myRule = new MyCustomRule(lightSwitch, light);


            //---And, finally, register the domain objects and the rule that governs their behavior---

            Register(light);
            Register(lightSwitch);
            Register(JarvisRule);
            Register(myRule);
        }

        public override void Unload() { }

        /// <summary>
        /// A helper method that registers each thing created by this module by adding them to their appropriate collection(s).
        /// Less boilerplate is always nice, don't ya think?
        /// </summary>
        /// <param name="thing"></param>
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

        /// <summary>
        /// Example of a custom IRule implementation
        /// This one checks if a LightSwitch is in the 'Off' state
        /// </summary>
        public class MyCustomRule : IRule
        {
            private LightSwitch mySwitch;
            private Packet mySatisfiedPacket;
            private List<InstancePreference> myPreferences;

            public MyCustomRule(LightSwitch lightSwitch, Light light)
            {
                mySwitch = lightSwitch;
                mySatisfiedPacket = new Packet(light, "State", false);
                myPreferences = new List<InstancePreference>() { new InstancePreference(mySwitch) };
            }

            /// <summary>
            /// Returns true if the LightSwitch is turned off
            /// </summary>
            public bool IsSatisfied
            {
                get
                {
                    if (mySwitch.State == false)
                        return true;
                    else return false;
                }
            }

            public Packet PacketToSendIfSatisfied
            {
                get
                {
                    return mySatisfiedPacket;
                }
            }

            public IEnumerable<EvaluationPreference> WhatIBeInterestedIn
            {
                get
                {
                    return myPreferences;
                }
            }
        }
    }
}