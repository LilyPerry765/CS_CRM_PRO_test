<%@ Page Title="ثبت قرارداد" Language="C#" MasterPageFile="~/PopUp.Master" AutoEventWireup="true" CodeBehind="ContractForm.aspx.cs" Inherits="CRM.Website.Viewes.ContractForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentsPlaceHolder" runat="server">
    <div>
        <asp:RadioButton ID="NewradioButton" runat="server" Text="قرارداد جدید" GroupName="NewOrCopy" Checked="true" />
        <asp:RadioButton ID="CopyradioButton" runat="server" Text="تمدید قرارداد" GroupName="NewOrCopy" />
        <dl>
            <dt>عنوان قرارداد</dt>
            <dd>
                <asp:DropDownList ID="DocumentTypeIDDropDownList" runat="server" DataValueField="DocumentRequestTypeID" DataTextField="DocumentName" OnSelectedIndexChanged="DocumentTypeIDDropDownList_SelectedIndexChanged" />
            </dd>

            <dt>
                <asp:Label ID="RoundTypeLabel" runat="server" Text="نوع تلفن رند" Visible="false" />
            </dt>
            <dd>
                <asp:DropDownList ID="RoundTypeDropDownList" runat="server" DataValueField="ID" DataTextField="Name" OnSelectedIndexChanged="RoundTypeDropDownList_SelectedIndexChanged" Visible="false" />
            </dd>

        </dl>

        <dl>
            <dt></dt>
            <dd></dd>

            <dt><asp:Label ID="RoundLabel" runat="server" Text="شماره تلفن رند" Visible="false" /></dt>
            <dd>
                <asp:DropDownList ID="DropDownList1" runat="server" DataValueField="ID" DataTextField="Name" OnSelectedIndexChanged="RoundTypeDropDownList_SelectedIndexChanged" Visible="false" />
            </dd>
        </dl>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FooterPlaceHolder" runat="server">
</asp:Content>
