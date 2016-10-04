﻿// Copyright (c) 2007-2016 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using System;
using osu.Framework.Desktop.OS.Windows.Native;
using osu.Framework.Input;
using osu.Framework.OS;
using OpenTK.Graphics;

namespace osu.Framework.Desktop.OS.Linux
{
    public class LinuxGameHost : BasicGameHost
    {
        public override GLControl GLControl => window.Form;
        public override bool IsActive => true; // TODO LINUX

        private LinuxGameWindow window;

        internal LinuxGameHost(GraphicsContextFlags flags, string game)
        {
            window = new LinuxGameWindow(flags);

            Storage = new LinuxStorage(game);

            Window = window;
            Window.Activated += OnActivated;
            Window.Deactivated += OnDeactivated;
        }

        private TextInputSource textInputBox;
        public override TextInputSource TextInput => textInputBox ?? (textInputBox = window.CreateTextInput());

        protected override void OnActivated(object sender, EventArgs args)
        {
            Execution.SetThreadExecutionState(Execution.ExecutionState.Continuous | Execution.ExecutionState.SystemRequired | Execution.ExecutionState.DisplayRequired);
            base.OnActivated(sender, args);
        }

        protected override void OnDeactivated(object sender, EventArgs args)
        {
            base.OnDeactivated(sender, args);
        }
    }
}
