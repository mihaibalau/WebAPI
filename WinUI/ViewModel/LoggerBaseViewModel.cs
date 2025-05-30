﻿// <copyright file="LoggerBaseViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WinUI.ViewModel
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Base view model class that implements the INotifyPropertyChanged interface.
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Event that is raised when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event for the specified property.
        /// </summary>
        /// <param name="property_name">The name of the property that changed. If null or empty, the caller member name is used.</param>
        protected void OnPropertyChanged([CallerMemberName] string property_name = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property_name));
        }
    }
}