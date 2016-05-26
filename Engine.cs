using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork_01
{
    public class Engine
    {
        SkypeControl skypeControl = new SkypeControl();
        BrowserControl browserControl = new BrowserControl();
        SystemControl systemControl = new SystemControl();
        RemindRange remindRange = new RemindRange();

        public void commandsHandler(string command)
        {
            if (command == null) return;
            switch(command.ToLower())
            {
                 // Взаимодействия с системой

                case "выключить компьютер":
                    {
                        systemControl.shutdownSystem();
                        break;
                    }
                case "перезагрузить компьютер":
                    {
                        systemControl.reloadSystem();
                        break;
                    }
                case "сменить язык на русский":
                    {
                        systemControl.changeLanguage("ru");
                        break;
                    }
                case "сделать снимок":
                    {
                        systemControl.makeScreenImage();
                        break;
                    }

                    //Взаимодействие с браузером
                case "открыть новости":
                    {
                        browserControl.openNews();
                        break;
                    }
                case "открыть календарь":
                    {
                        browserControl.openCalendar();
                        break;
                    }
                case "открыть посевной календарь":
                    {
                        browserControl.openMoonCalendar();
                        break;
                    }
                case "открыть погоду":
                    {
                        browserControl.openForecast();   
                        break;
                    }

                    //Управление напоминаниями

                case "добавить напоминание":
                    {
                        Form3 remindsForm = new Form3();
                        break;
                    }
                case "удалить напоминание":
                    {
                        Form4 remindListForm = new Form4();
                        break;
                    }

                    //Взаимодействия со скайпом
                case "открыть skype":
                    {
                        skypeControl.startSkype();
                        break;
                    }
                case "закрыть skype":
                    {
                        skypeControl.stopSkype();
                        break;
                    }
                case "позвонить":
                    {
                        skypeControl.startSkype();
                        Form2 formContacts = new Form2(false);
                        formContacts.ShowDialog();
                        break;
                    }
                case "видеозвонок":
                    {
                        skypeControl.startSkype();
                        Form2 formContacts = new Form2(true);
                        formContacts.ShowDialog();
                        break;
                    }
            }
        }

        public void skypeCalls(bool isVideo, string name)
        {
            if (isVideo) skypeControl.videoCall(name);
            else skypeControl.call(name);
        }
        
        public void deleteRemind(Remind remind)
        {
            RemindRange allReminds = new RemindRange();
            allReminds.removeMessage(remind.getId());
        }

        public void addRemind(Remind remind)
        {
            RemindRange allReminds = new RemindRange();
            allReminds.addMessage(remind.getDateTime(), remind.getMessage());
        }
    }
}
