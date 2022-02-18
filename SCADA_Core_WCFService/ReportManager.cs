using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADACore
{
    public class ReportManager : IReportManager
    {
        public List<Alarm> Report1(double time)
        {
            DateTime time1 = DateTime.Now;
            DateTime time2 = time1.AddMinutes(time);
            List<Alarm> report1 = new List<Alarm>();
            using (var db = new AlarmContext())
            {
                var data = (from alarm in db.Alarms
                            where alarm.DateTime > time2
                            select alarm).ToList();
                report1 = data.OrderByDescending(x => x.Priorities).ThenByDescending(x => x.DateTime.Date).ThenByDescending(x => x.DateTime.TimeOfDay).ToList();
            }
            return report1;
        }
        public List<Alarm> Report2(string priority)
        {
            List<Alarm> report2 = new List<Alarm>();
            using (var db = new AlarmContext())
            {
                var data = (from alarm in db.Alarms
                            where alarm.PriorityString == priority
                            select alarm).ToList();
                report2 = data.OrderByDescending(x => x.DateTime.Date).ThenByDescending(x => x.DateTime.TimeOfDay).ToList();
            }
            return report2;
        }
        public Tag Report3(string tag, double time)
        {
            Tag tag1 = new Tag();
            DateTime time1 = DateTime.Now;
            DateTime time2 = time1.AddMinutes(time);
            if (tag == "Analog input")
            {
                List<AITag> aITags = new List<AITag>();
                
                using (var db = new TagContext())
                {
                    var data = (from ai in db.AITags
                                where ai.TimeStamp > time2
                                select ai).ToList();
                    aITags = data.OrderByDescending(x => x.TimeStamp).ToList();
                }
                tag1.analogInputs = aITags;
            }
            else if (tag == "Digital input")
            {
                List<DITag> dITags = new List<DITag>();

                using (var db = new TagContext())
                {
                    var data = (from di in db.DITags
                                where di.TimeStamp>time2
                                select di).ToList();
                    dITags = data.OrderByDescending(x => x.TimeStamp).ToList();
                }
                tag1.digitalInputs = dITags;
            }
            return tag1;
        }
        public List<AITag> Report4()
        {
            List<AITag> aITags = new List<AITag>();
            using (var db = new TagContext())
            {
                var data = (from ai in db.AITags
                            select ai).ToList();
                aITags = data.OrderByDescending(x => x.TagName).ThenByDescending(x => x.TimeStamp.Date).ThenByDescending(x => x.TimeStamp.TimeOfDay).ToList();
            }
            return aITags;
        }
        public List<DITag> Report5()
        {
            List<DITag> dITags = new List<DITag>();
            using (var db = new TagContext())
            {
                var data = (from di in db.DITags
                            select di).ToList();
                dITags = data.OrderByDescending(x => x.TagName).ThenByDescending(x => x.TimeStamp.Date).ThenByDescending(x => x.TimeStamp.TimeOfDay).ToList();
            }
            return dITags;
        }
        public Tag Report6(string tagName, string tag)
        {
            Tag tag1 = new Tag();
            if(tag == "Analog input")
            {
                List<AITag> aITags = new List<AITag>();
                using (var db = new TagContext())
                {
                    var data = (from ai in db.AITags
                                where ai.TagName == tagName
                                select ai).ToList();
                    aITags = data.OrderByDescending(x => x.Value).ToList();
                }
                tag1.analogInputs = aITags;
            }
            else if(tag == "Digital input")
            {
                List<DITag> dITags = new List<DITag>();
                using (var db = new TagContext())
                {
                    var data = (from di in db.DITags
                                where di.TagName == tagName
                                select di).ToList();
                    dITags = data.OrderByDescending(x => x.Value).ToList();
                }
                tag1.digitalInputs = dITags;
            }
            return tag1;
        }

    }
}
