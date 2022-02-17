using AlarmDisplay.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AlarmDisplay
{
    public class AlarmCallback:IAlarmDisplayCallback
    {
        MainWindow mw = (MainWindow)Application.Current.MainWindow;
        public void OnAlarmActivate (Alarm alarm, int count)
        {
            for(int i = 0; i < count; i++)
            {
                mw.History.Text += "ALARM" + " " + alarm.TagName + ": " + alarm.Types + " " + alarm.Message + " " + alarm.DateTime + "\n";
            }
            
        }
        public void OnAlarmStop(Alarm alarm)
        {
            mw.History.Text += "REGULAR" + " " + alarm.TagName + ": " + alarm.Types + " " + alarm.Message + " " + alarm.DateTime  +"\n";
        }
        public void ClearAlarms()
        {
            mw.History.Text = String.Empty;
        }
        public void ShutdownAlarm()
        {
            Application.Current.Shutdown();
        }
    }
}
