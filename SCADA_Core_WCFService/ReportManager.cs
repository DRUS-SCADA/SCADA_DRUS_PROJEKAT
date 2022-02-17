using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADACore
{
    public class ReportManager : IReportManager
    {
        public void Report1(double time)
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
        }
        public void Report2(string priority)
        {
            List<Alarm> report2 = new List<Alarm>();
            using (var db = new AlarmContext())
            {
                var data = (from alarm in db.Alarms
                            where alarm.PriorityString == priority
                            select alarm).ToList();
                report2 = data.OrderByDescending(x => x.DateTime.Date).ThenByDescending(x => x.DateTime.TimeOfDay).ToList();
            }
        }
        public void Report3(string tag, double time)
        {
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
            }
        }
        public void Report4()
        {
            List<AITag> aITags = new List<AITag>();
            using (var db = new TagContext())
            {
                var data = (from ai in db.AITags
                            select ai).ToList();
                aITags = data.OrderByDescending(x => x.TagName).ThenByDescending(x => x.TimeStamp.Date).ThenByDescending(x => x.TimeStamp.TimeOfDay).ToList();
            }
        }
        public void Report5()
        {
            List<DITag> dITags = new List<DITag>();
            using (var db = new TagContext())
            {
                var data = (from di in db.DITags
                            select di).ToList();
                dITags = data.OrderByDescending(x => x.TagName).ThenByDescending(x => x.TimeStamp.Date).ThenByDescending(x => x.TimeStamp.TimeOfDay).ToList();
            }
        }
        public void Report6(string tagName, string tag)
        {
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
            }
        }

    }
}
