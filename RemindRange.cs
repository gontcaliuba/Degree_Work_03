using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork_01
{
    [Serializable]
    public class RemindRange
    {
        XmlWorker xml = new XmlWorker();
        public List<Remind> remindList = new List<Remind>();

        public RemindRange()
        { 
            RemindRange listFromXml = xml.readXML("Reminds.xml");
            if (listFromXml != null) remindList = listFromXml.remindList;
        }
        
        private void addRemindsInXml()
        {
            string xmlName = "Reminds.xml";
            File.Delete(xmlName);
            xml.writeXML(xmlName, this);
        }
        public void addMessage(DateTime dateTime, string message)
        {
            remindList.Add(new Remind(remindList.Count(), dateTime, message));
            addRemindsInXml();
        }
        public RemindRange updateRemindList()
        {
            RemindRange listFromXml = xml.readXML("Reminds.xml");
            return listFromXml;
        }

        public void removeMessage(int id)
        {
            if (id < 0) return;
            if (id >= remindList.Count) return;
            remindList.RemoveAt(id);
            alignMessages();
            addRemindsInXml();
        }

        public string extractLastMessage()
        {
            return remindList[remindList.Count - 1].getMessage();
        }

        public DateTime extractLastDateAndTime()
        {
            return remindList[remindList.Count - 1].getDateTime();
        }

        public List<Remind> getMessages()
        {
            return remindList;
        }

        public Remind getRemindById(int id)
        {
            if (id < 0) return null;
            if (id > remindList.Count()) return null;
            return remindList[id];
        }
        private void alignMessages()
        {
            for(int i = 0; i < remindList.Count; i++)
                remindList[i].setId(i);
        }
    }
}
