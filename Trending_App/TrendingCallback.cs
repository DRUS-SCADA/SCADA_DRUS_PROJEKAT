﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Trending_App.ServiceReference1;

namespace Trending_App
{
    public class TrendingCallback:ITrendingCallback
    {
        MainWindow mw = (MainWindow)Application.Current.MainWindow;
        public void OnValueReceived(AnalogInput AI)
        {
            if(MainWindow.analogInputs.Count != 0)
            {
                int x = MainWindow.analogInputs.Count;
                int y = 0;
                foreach (AnalogInput i in MainWindow.analogInputs)
                {
                    if (i.TagName == AI.TagName)
                    {
                        i.AnalogValue = AI.AnalogValue;
                        break;
                    }
                    y++;
                }
                if (x==y)
                {
                    MainWindow.analogInputs.Add(AI);
                }
            }
            else
            {
                MainWindow.analogInputs.Add(AI);
            }
        }
        public void OnValueReceived1(DigitalInput DI)
        {
            if (MainWindow.digitalInputs.Count != 0)
            {
                int x = MainWindow.digitalInputs.Count;
                int y = 0;
                foreach (DigitalInput i in MainWindow.digitalInputs)
                {
                    if (i.TagName == DI.TagName)
                    {
                        i.digitalValue = DI.digitalValue;
                        break;
                    }
                    y++;
                }
                if (x == y)
                {
                    MainWindow.digitalInputs.Add(DI);
                }
            }
            else
            {
                MainWindow.digitalInputs.Add(DI);
            }
        }
    }
}
