﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            string caseCommand = command.ToLower();
            switch (caseCommand)
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
                case "сменить язык на английский":
                    {
                        systemControl.changeLanguage("en");
                        break;
                    }
                case "сменить язык на румынский":
                    {
                        systemControl.changeLanguage("ro");
                        break;
                    }
                case "сделать снимок":
                    {
                        systemControl.makeScreenImage();
                        systemControl.openFolderIm();
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
                        RemindForm remindsForm = new RemindForm();
                        remindsForm.ShowDialog();
                        break;
                    }
                case "удалить напоминание":
                    {
                        ReminListForm remindListForm = new ReminListForm();
                        remindListForm.ShowDialog();
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
                        ContactsForm formContacts = new ContactsForm(false);
                        formContacts.ShowDialog();
                        break;
                    }
                case "видеозвонок":
                    {
                        skypeControl.startSkype();
                        ContactsForm formContacts = new ContactsForm(true);
                        formContacts.ShowDialog();
                        break;
                    }
                default:
                    {
                        var request = command;
                        Process.Start("opera.exe", "https://www.google.ru/?gws_rd=ssl#newwindow=1&q=" + Uri.EscapeUriString(request));
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
