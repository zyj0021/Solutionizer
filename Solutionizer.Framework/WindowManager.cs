﻿using System.Windows;

namespace Solutionizer.Framework {
    public class WindowManager {
        public void ShowWindow<TViewModel>() {
            var view = (Window)ViewLocator.GetViewForViewModel<TViewModel>();
            view.Show();
        }
    }
}