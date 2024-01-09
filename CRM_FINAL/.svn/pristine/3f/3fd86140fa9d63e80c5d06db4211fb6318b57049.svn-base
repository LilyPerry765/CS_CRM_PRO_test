function ModalDialog(sURL, vArguments, ModalDialogHeight, ModalDialogWidth) {
    var sFeatures;

    if (window.showModalDialog) {
        if (!navigator.userAgent.toLowerCase().match('chrome')) {
            ModalDialogWidth += "px";
            ModalDialogHeight += "px";
        }
        sFeatures = "status:no;center:yes;dialogWidth:" + ModalDialogWidth + ";dialogHeight:" + ModalDialogHeight + ";resizable:no;";
        //sFeatures = "status:no;center:yes;dialogWidth:" + ModalDialogWidth + ";dialogHeight:" + ModalDialogHeight + ";resizable:no;maximize:no;minimize:no;";
        var returnResult = window.showModalDialog(sURL, vArguments, sFeatures);
        if (returnResult) {
            return returnResult;
        }
    }
    else {
        sFeatures = "status=no,width=" + ModalDialogWidth + ",height=" + ModalDialogHeight + ",menubar=no,scrollbars=no";
        window.margs = vArguments;
        window.open(sURL, "_blank", sFeatures);
    }
}

