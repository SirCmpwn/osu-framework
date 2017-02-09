﻿// Copyright (c) 2007-2016 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using System.Linq;
using osu.Framework.Input;
using OpenTK.Input;
using System;

namespace osu.Framework.Graphics.Containers
{
    /// <summary>
    /// An overlay container that eagerly holds keyboard focus.
    /// </summary>
    public abstract class FocusedOverlayContainer : OverlayContainer
    {
        public override bool RequestingFocus => IsPresent && State == Visibility.Visible;

        public override bool IsPresent => base.IsPresent || State == Visibility.Visible;

        protected override bool OnFocus(InputState state) => true;

        protected override void OnFocusLost(InputState state)
        {
            if (state.Keyboard.Keys.Contains(Key.Escape))
                Hide();
            base.OnFocusLost(state);
        }

        protected override void PopIn()
        {
            TriggerFocusContention();
        }
    }
}