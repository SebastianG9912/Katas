using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Scheduler.Exceptions;

namespace Scheduler.Models
{
    public class CaseWorker
    {
        public string Name;
        public List<Meeting> Meetings;

        public CaseWorker()
        {
            Meetings = new List<Meeting>();

            DateTime startOfWork = DateTime.Today.AddHours(8);
            for (int i = 0; i < 6; i++)
            {
                DateTime startOfMeeting = startOfWork.AddHours(i);
                Meeting meeting = new Meeting(startOfMeeting);

                Meetings.Add(meeting);
            }
        }

        public void NewDateAdded(DateTime start)
        {
            Meeting newMeeting = new Meeting(start);

            bool freeTime = true; //Lade också till så att den inte dubbelbokar ändå
            foreach (Meeting meeting in Meetings)
            {
                // TODO kasta MeetingOverlapException om två möten överlappar
                try
                {
                    if (meeting.Overlap(newMeeting))
                    {
                        freeTime = false;
                        throw new MeetingOverlapException(meeting);
                    }
                }
                catch (MeetingOverlapException e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            if(freeTime) //Lade också till så att den inte dubbelbokar ändå
                Meetings.Add(newMeeting);
        }

        public void ChangeMeeting(int index, DateTime newStart)
        {
            Meeting meetingToChange = Meetings[index];
            Meeting attemptMeeting = new Meeting(newStart, meetingToChange.Duration);

            foreach (Meeting meeting in Meetings)
            {
                if (meeting == meetingToChange)
                    continue;

                // TODO kasta MeetingOverlapException om två möten överlappar
                if (meeting.Overlap(attemptMeeting))
                {
                    throw new MeetingOverlapException(meeting);
                }
            }
            meetingToChange.Start = newStart;
        }
    }
}
