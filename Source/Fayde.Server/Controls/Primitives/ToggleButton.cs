﻿using Fayde.Xaml.Metadata;

namespace Fayde.Controls.Primitives
{
    public class ToggleButton : ButtonBase
    {
        public static readonly PropertyDescription IsCheckedProperty = PropertyDescription.Register("IsChecked", typeof(Core.Boolean), typeof(ToggleButton));
        public static readonly PropertyDescription IsThreeStateProperty = PropertyDescription.Register("IsThreeState", typeof(bool), typeof(ToggleButton));
    }
}