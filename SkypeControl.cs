using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SKYPE4COMLib;
using System.Windows.Forms;
using System.Threading;

namespace DegreeWork_01
{
   public class SkypeControl
    {
       Skype skype = new Skype();
       public bool startSkype()
       {
           try
           {
               if (!skype.Client.IsRunning)
               {
                   skype.Client.Start(true, true);
                   Thread.Sleep(10000);
               }
               skype.Attach(skype.Protocol, true);
               return true;
           }
           catch
           {
               return false;
           }
       }
       bool tryCall(string name, bool isVideo)
       {
           if (!startSkype()) return false;
           Call call = skype.PlaceCall(name);
           do
           {
               if (call.Status == TCallStatus.clsBusy
                  || call.Status == TCallStatus.clsFailed
                  || call.Status == TCallStatus.clsMissed
                  || call.Status == TCallStatus.clsCancelled)
                   return false;
               if (call.Status == TCallStatus.clsRefused)
               {
                   //Пользователь отклонил звонок
                   MessageBox.Show("Пользователь отклонил звонок");
                   return true;
               }
               Thread.Sleep(10);
           } while (call.Status != TCallStatus.clsInProgress);

           if (isVideo == true) call.StartVideoSend();

           return true;
       }
      public void call(string name)
       {
          //Timeout для звонка - 20 сек
           for (int i = 0; i < 20; i++)
           {
               bool isCalling = tryCall(name, false);
               if (isCalling == true) break;
               Thread.Sleep(1000);
           }
       }

       public void videoCall(string name)
      {
          for (int i = 0; i < 20; i++)
          {
              bool isCalling = tryCall(name, true);
              if (isCalling == true) break;
              Thread.Sleep(1000);
          }
      }
       public void stopSkype()
       {
           if (skype.Client.IsRunning)
               skype.Client.Shutdown();
       }

    }
}
