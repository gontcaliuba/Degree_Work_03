using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace DegreeWork_01
{
    class RemindControl
    {
        Timer timer = new Timer(1000 * 5);
        RemindRange remindRange = new RemindRange();

        public RemindControl()
        {
            timer.Elapsed += timerEvent;
            timer.Start();
        }

        void timerEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            remindRange = remindRange.updateRemindList();
            if (remindRange == null) return;

            for (int i = 0; i < remindRange.remindList.Count; ++i)
            {
                Remind remind = remindRange.remindList[i];
                DateTime current_time = DateTime.Now;
                DateTime remind_time = remind.getDateTime();
                TimeSpan delta = current_time - remind_time;
                if ((delta.TotalSeconds >= 0) && (delta.TotalSeconds < 60))
                {
                    remindRange.removeMessage(i);
                    System.Windows.Forms.MessageBox.Show(remind.getMessage());
                }
            }
        }
    }
}
