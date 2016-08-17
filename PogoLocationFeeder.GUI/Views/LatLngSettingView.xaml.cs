﻿/*
PogoLocationFeeder gathers pokemon data from various sources and serves it to connected clients
Copyright (C) 2016  PogoLocationFeeder Development Team <admin@pokefeeder.live>

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as
published by the Free Software Foundation, either version 3 of the
License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Input;
using PogoLocationFeeder.Config;
using PogoLocationFeeder.GUI.Properties;
using UserControl = System.Windows.Controls.UserControl;
using PogoLocationFeeder.Helper;
using System.Security.Permissions;

namespace PogoLocationFeeder.GUI.Views
{
    /// <summary>
    /// Interaction logic for LatLngSettingView.xaml
    /// </summary>
    public partial class LatLngSettingView : UserControl
    {
        public LatLngSettingView()
        {
            InitializeComponent();
            Uri uri = new Uri(AppDomain.CurrentDomain.BaseDirectory + "static/map.html");
            webBrowser1.Navigate(uri);
        }

        private void DoublelValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            double result;
            e.Handled = double.TryParse(e.Text, out result);
        }

        private void setupObjectForScripting(object sender, System.Windows.RoutedEventArgs e)
        {            
            webBrowser1.ObjectForScripting = new HtmlInteropInternalClass();
        }
                
        // Object used for communication from JS -> WPF
        [System.Runtime.InteropServices.ComVisibleAttribute(true)]
        public class HtmlInteropInternalClass
        {
            public void setMapInfo(double lat, double lng, double swLat, double swLng, double neLat, double neLng)
            {
                var sw = new GeoCoordinates(swLat, swLng);
                var ne = new GeoCoordinates(neLat, neLng);
                ViewModels.MainWindowViewModel.Instance.LocationBoundsSettingToSave = new LatLngBounds(sw, ne);
            }
        }
    }
}
