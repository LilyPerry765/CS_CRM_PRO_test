
namespace CaseManagement.Administration.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Administration.CalendarEvent")]
    [BasedOnRow(typeof(Entities.CalendarEventRow))]
    public class CalendarEventForm
    {
        public String Title { get; set; }
        public Boolean AllDay { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public String Url { get; set; }
        public String ClassName { get; set; }
        public Boolean IsEditable { get; set; }
        public Boolean IsOverlap { get; set; }
        public String Color { get; set; }
        public String BackgroundColor { get; set; }
        public String TextColor { get; set; }
    }
}