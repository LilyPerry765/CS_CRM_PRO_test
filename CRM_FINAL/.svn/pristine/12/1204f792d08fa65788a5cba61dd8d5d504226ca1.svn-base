using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Application.Views
{
    public class RequestInboxInquiry
    {
        public RequestInboxInquiry()
        {
            RequestsInbox requestInbox = new RequestsInbox();
            requestInbox.IsInquiryMode = true;
            Folder.Console.Navigate(requestInbox, "درخواست های کاربر");
        }
    }

    public class RequestsInboxArchived
    {
        public RequestsInboxArchived()
        {
            RequestsInbox requestInbox = new RequestsInbox();
            requestInbox.IsArchived = true;
            Folder.Console.Navigate(requestInbox, "درخواست های آرشیو شده");
        }
    }
}
