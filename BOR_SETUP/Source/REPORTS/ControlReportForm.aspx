<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Master.master" AutoEventWireup="true" CodeFile="ControlReportForm.aspx.cs" Inherits="REPORTS_Ledger_Updated" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

     <script src="../jquery-1.8.2.js"></script>
    <script src="../jquery.min.js"></script>
     <style type="text/css">
		a img{border: none;}
		ol li{list-style: decimal outside;}
		div#container{width: 780px;margin: 0 auto;padding: 1em 0;}
		div.side-by-side{width: 100%;margin-bottom: 1em;}
		div.side-by-side > div{float: left;width: 50%;}
		div.side-by-side > div > em{margin-bottom: 10px;display: block;}
		.clearfix:after{content: "\0020";display: block;height: 0;clear: both;overflow: hidden;visibility: hidden;}
		
	</style>
	<link rel="stylesheet" href="../Style/chosen.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="row">

			    <div class="col-md-12">
			        <!-- begin panel -->
                    <div class="panel widget light-widget panel-bd-top">
                        <div class="panel-heading bordered">
                    <h3 class="panel-title"> <span class="menu-icon"> <i class="icon-pie"></i> </span><b id="MainTitle" runat="server"> Control Account Report </b></h3>
                    <div class="vd_panel-menu">
  <div data-action="minimize" data-original-title="Minimize" data-toggle="tooltip" data-placement="bottom" class=" menu entypo-icon"> <i class="icon-minus3"></i> </div>
  <div data-action="refresh"  data-original-title="Refresh" data-toggle="tooltip" data-placement="bottom" class=" menu entypo-icon smaller-font"> <i class="icon-cycle"></i> </div>
  
  <div data-action="close" data-original-title="Close" data-toggle="tooltip" data-placement="bottom" class=" menu entypo-icon"> <i class="icon-cross"></i> </div>
</div>
<!-- vd_panel-menu --> 
                  </div>
                        <div class=" panel-body-list" style="padding:10px">
                         <div class="col-md-5" >
                      <label class="control-label col-md-4 col-sm-4 col-xs-12" style="margin-top:5px; font-size:medium; font-weight:bold; ">Start Date :<span class="required"></span>
                        </label>
                        <div class="col-md-8 col-sm-8 col-xs-12">
                            <div class="input-group">
                                       <input type="text" class="width-100" id="StartDate" placeholder="Select Date" runat="server" />
                                       <span class="input-group-addon" id="datepicker-icon-trigger-PV" data-datepicker="#datepicker-autoClose"><i class="fa fa-calendar"></i></span>
                                       </div>

</div></div>
 <div class="col-md-5" >
                      <label class="control-label col-md-4 col-sm-4 col-xs-12" style="margin-top:5px; font-size:medium;  font-weight:bold; ">End Date :<span class="required"></span>
                        </label>
                        <div class="col-md-8 col-sm-8 col-xs-12">
                            <div class="input-group">
                                       <input type="text" class="width-100" id="EndDate" runat="server" placeholder="Select Date" />
                                       <span class="input-group-addon" id="Span1" data-datepicker="#datepicker-autoClose"><i class="fa fa-calendar"></i></span>
                                       </div>

</div></div>
<div class="col-md-12" style="height:20px;"></div>
                        <div class="col-md-10"><asp:DropDownList ID="ddlCategory" runat="server" DataSourceID="SqlDataSource2" DataTextField="ControlAccTitle" DataValueField="ControlAccID" CssClass="chzn-select form-control" ></asp:DropDownList></div>

<div class="col-md-2"><asp:Button CssClass="btn btn-info" runat="server" id="btnSave" OnClick="LoadReport" Text="VIEW REPORT" Width="100%" Font-Bold="true" /></div>

                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Con %>" 
                                            SelectCommand="select * from AccountTypeControl  where BranchID=@BranchID"
                                SelectCommandType="Text">
                                 <SelectParameters>
                                                <asp:CookieParameter CookieName="BranchID" Name="BranchID"  Type="String" />
                                            </SelectParameters>
                            </asp:SqlDataSource>

                                           
<div class="col-md-12" style="height:20px;"></div>
<div class="col-md-12"> 





</div>
</div></div></div></div>

 
<script src="../js/jquery-1.9.1.min.js"></script>
		<script src="../js/bootstrap.min.js"></script>
                 <script type="text/javascript" src='../AAUI/plugins/jquery-ui/jquery-ui.custom.min.js'></script>

    <script type="text/javascript">

        document.getElementById("ContentPlaceHolder1_ddlCategory").focus();
        $("#ContentPlaceHolder1_StartDate").datepicker({ dateFormat: 'm/dd/yy' });
        $('[data-datepicker]').click(function (e) {
            var data = $(this).data('datepicker');
            $(data).focus();
        });
        $("#ContentPlaceHolder1_EndDate").datepicker({ dateFormat: 'm/dd/yy' });
        $('[data-datepicker]').click(function (e) {
            var data = $(this).data('datepicker');
            $(data).focus();
        });
    </script>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
		<script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
		<script type="text/javascript">		    $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
</asp:Content>

