<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Master.master" AutoEventWireup="true" CodeFile="Viewer.aspx.cs" Inherits="REPORTS_Viewer" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script src="../jquery-1.8.2.js"></script>
    <script src="../jquery.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<input type="button" id="btnPrint" value="Print" onclick="Print();" />
    <div id="dvReport">
<CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
            AutoDataBind="True" Height="50px" 
            Width="350px" ToolPanelView="None" PrintMode="ActiveX" />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="">
            </Report>
        </CR:CrystalReportSource>
        </div>
           <script type="text/javascript">

               function Print() {
                   var dvReport = document.getElementById("dvReport");
                   var frame1 = dvReport.getElementsByTagName("iframe")[0];

                   var frameDoc = frame1.contentWindow ? frame1.contentWindow : frame1.contentDocument.document ? frame1.contentDocument.document : frame1.contentDocument;
                   frameDoc.print();

               }
</script>
</asp:Content>

