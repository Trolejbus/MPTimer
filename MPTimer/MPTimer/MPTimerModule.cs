﻿using Autofac;
using MPTimer.Components;
using MPTimer.Components.Dashboard;

namespace MPTimer
{
    internal class MPTimerModule : Module
    {
        public MPTimerModule()
        {

        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TrayController>().As<TrayController>().As<IStartable>().SingleInstance();
        }
    }
}
