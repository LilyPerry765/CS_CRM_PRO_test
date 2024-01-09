﻿namespace CaseManagement.Common.DashboardUser {

    export function initializePage() {
      
        Messages();
       
    }

    function Messages() {

        var Unreadcounter = 0;
        var UnReadMessages;

        $.ajaxSetup({ async: false });
        var InboxResult = CaseManagement.Messaging.VwMessagesService.List({ "Sort": ["InsertedDate DESC"] }, response => {
            //console.log("succesfully called Inbox");
            // console.log(response.TotalCount);  
            UnReadMessages = new Array(5)
            for (j = 0; j < 5; j++) {
                UnReadMessages[j] = new Array(3);
            }

            for (var InboxCount = 0; InboxCount < response.TotalCount; InboxCount++) {

                if (response.Entities[InboxCount].Seen == null && Unreadcounter != 5) {

                    UnReadMessages[Unreadcounter][0] = response.Entities[InboxCount].Id;

                    UnReadMessages[Unreadcounter][1] = response.Entities[InboxCount].SenderName;

                    UnReadMessages[Unreadcounter][2] = response.Entities[InboxCount].Subject;

                    Unreadcounter++;
                }
            }
            var Messages = "شما تعداد " + PersiaNumber(Unreadcounter.toString()) + " پیام خوانده نشده دارید :  \n\n";

            if (UnReadMessages[0][0] != null) {
                Messages = Messages + UnReadMessages[0][1] + " : " + UnReadMessages[0][2] +"  \n";
                $("#first_message_SenderName").text("فرستنده : " + UnReadMessages[0][1]);
                $("#first_message_subject").text("موضوع : " + UnReadMessages[0][2]);
            } else {
                document.getElementById("first_message_SenderName").classList.remove('label-default');
                document.getElementById("first_message_SenderName").classList.add('label-danger');
                $("#first_message_SenderName").text('پیام جدیدی در لیست پیام های دریافت شده وجود ندارد.');
                $("#first_message_subject").text(' ');
            }

            if (UnReadMessages[1][0] != null) {
                Messages = Messages + UnReadMessages[1][1] + " : " + UnReadMessages[1][2] + "  \n";
                $("#second_message_SenderName").text("فرستنده : " + UnReadMessages[1][1]);
                $("#second_message_subject").text("موضوع : " + UnReadMessages[1][2]);
            } else {
                document.getElementById("second_message_SenderName").classList.remove('label-success');
                $("#second_message_SenderName").text(' ');
                $("#second_message_subject").text(' ');
            }

            if (UnReadMessages[2][0] != null) {
                Messages = Messages + UnReadMessages[2][1] + " : " + UnReadMessages[2][2] + "  \n";
                $("#third_message_SenderName").text("فرستنده : " + UnReadMessages[2][1]);
                $("#third_message_subject").text("موضوع : " + UnReadMessages[2][2]);
            } else {
                document.getElementById("third_message_SenderName").classList.remove('label-default');
                $("#third_message_SenderName").text(' ');
                $("#third_message_subject").text(' ');
            }


            if (UnReadMessages[3][0] != null) {
                Messages = Messages + UnReadMessages[3][1] + " : " + UnReadMessages[3][2] + "  \n";
                $("#fourth_message_SenderName").text("فرستنده : " + UnReadMessages[3][1]);
                $("#fourth_message_subject").text("موضوع : " + UnReadMessages[3][2]);
            } else {
                document.getElementById("fourth_message_SenderName").classList.remove('label-success');
                $("#fourth_message_SenderName").text(' ');
                $("#fourth_message_subject").text(' ');
            }

            if (UnReadMessages[4][0] != null) {
                Messages = Messages + UnReadMessages[4][1] + " : " + UnReadMessages[4][2] + "  \n";
                $("#fifth_message_SenderName").text("فرستنده : " + UnReadMessages[4][1]);
                $("#fifth_message_subject").text("موضوع : " + UnReadMessages[4][2]);
            } else {
                document.getElementById("fifth_message_SenderName").classList.remove('label-default');
                $("#fifth_message_SenderName").text(' ');
                $("#fifth_message_subject").text(' ');
            }
            if (Unreadcounter != 0) {
                Q.information(
                    Messages,
                    () => {
                        Q.notifySuccess("اطلاع رسانی گردید.");
                    });
            }
    }

}
    function PersiaNumber(value) {
        var arabicNumbers = ['۰', '١', '٢', '٣', '٤', '٥', '٦', '٧', '٨', '٩'];

        var chars = value.split('');

        for (var i = 0; i < chars.length; i++) {
            if (/\d/.test(chars[i])) {
                chars[i] = arabicNumbers[chars[i]];
            }
        }
        return chars.join('');
    }