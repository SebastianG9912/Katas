﻿using System;

namespace Scheduler.Models
{
    public class Meeting
    {
        public DateTime Start;
        public TimeSpan Duration;
        public Applicant Applicant;

        public Meeting(DateTime start)
        {
            Start = start;
            Duration = TimeSpan.FromMinutes(30);
            Applicant = null;
        }
        public Meeting(DateTime start, TimeSpan duration)
        {
            Start = start;
            Duration = duration;
            Applicant = null;
        }

        public bool Overlap(Meeting meeting)
        {
            bool endIsBefore = (Start + Duration) < meeting.Start;
            bool startIsAfter = (meeting.Start + meeting.Duration) < Start;

            return !(endIsBefore || startIsAfter);
        }

        public override string ToString()
        {
            string date = Start.ToString("d'/'M'/'yy");

            string timeStart = Start.ToString("H:mm");
            DateTime end = Start + Duration;
            string timeEnd = end.ToString("H:mm");
            
            string info = $"{date}  {timeStart} - {timeEnd}";

            if (Applicant != null)
                info += " with: " + Applicant.Name;

            return info;
        }
    }
}
