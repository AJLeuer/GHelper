using System;
using GHelper.ViewModel;
using Microsoft.UI.Xaml;

namespace GHelper.View.Button
{
    public interface StandardButton
    {
        event RoutedEventHandler Click;
        GHubRecordViewModel?     GHubRecordViewModel { get; }

        void WireToGHubRecord();
    }
}