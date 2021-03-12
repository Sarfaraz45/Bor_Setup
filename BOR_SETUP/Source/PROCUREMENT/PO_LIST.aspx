<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Master.master" AutoEventWireup="true" CodeFile="PO_LIST.aspx.cs" Inherits="PROCUREMENT_PO_LIST" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script src="../jquery-1.8.2.js"></script>
    <script src="../jquery.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


<!-- ================== BEGIN PAGE LEVEL STYLE ================== -->
	<link href="../vendors/datatables.net-bs/css/dataTables.bootstrap.min.css" rel="stylesheet">
    <link href="../vendors/datatables.net-buttons-bs/css/buttons.bootstrap.min.css" rel="stylesheet">
    <link href="../vendors/datatables.net-fixedheader-bs/css/fixedHeader.bootstrap.min.css" rel="stylesheet">
    <link href="../vendors/datatables.net-responsive-bs/css/responsive.bootstrap.min.css" rel="stylesheet">
    <link href="../vendors/datatables.net-scroller-bs/css/scroller.bootstrap.min.css" rel="stylesheet">
	<!-- ================== END PAGE LEVEL STYLE ================== -->
    <asp:HiddenField ID="hdnUID" Value="0" runat="server" />
   <asp:HiddenField ID="hdnItems" runat="server" Value='["Karachi","Hyderabad","USA","Arkansas","California","Colorado","Connecticut","Delaware","Florida","Georgia","Hawaii","Idaho","Illinois","Indiana","Iowa","Kansas","Kentucky"]' />
   
  <div class="row">
                <!-- begin col-6 -->
			    <div class="col-md-12">
			        <!-- begin panel -->
                    <div class="panel widget light-widget panel-bd-top">
                        <div class="panel-heading bordered">
                    <h3 class="panel-title"> <span class="menu-icon"> <i class="icon-pie"></i> </span> Purchase Order LIST</h3>
                    <div class="vd_panel-menu">
  <div data-action="minimize" data-original-title="Minimize" data-toggle="tooltip" data-placement="bottom" class=" menu entypo-icon"> <i class="icon-minus3"></i> </div>
  <div data-action="refresh"  data-original-title="Refresh" data-toggle="tooltip" data-placement="bottom" class=" menu entypo-icon smaller-font"> <i class="icon-cycle"></i> </div>
  
  <div data-action="close" data-original-title="Close" data-toggle="tooltip" data-placement="bottom" class=" menu entypo-icon"> <i class="icon-cross"></i> </div>
</div>
<!-- vd_panel-menu --> 
                  </div>
                        
                        <div class="panel-body">
                <div class="row">
                                <div class="col-md-12" style=" text-align:right">
                                <asp:HyperLink ID="HyperLink2" NavigateUrl="~/PROCUREMENT/PO.aspx" runat="server">Add New Purchase Order</asp:HyperLink>
                                </div>
                                <div class="col-md-12" style="height:20px;"></div>
                                  <div class="col-md-12">
                             <input type="text" class="span6 typeahead form-control" placeholder="SEARCH BY PURCHASE ORDER NUMBER, DATE, AMOUNT AND SUPPLIER"  onblur="javascript:LoadLISTSearch(this);"  style="overflow:visible;" id="txtAccCode1"  data-provide="typeahead" style="width:100%" 
                                             data-items="10" >
                             </div>
                             <div class="col-md-12" style="height:20px;"></div>
                             <div class="col-md-12">
                                                
                                                 <div class="table-responsive" id="dvList" >
                                <table id="data-table" class="table table-striped" >
                                
                                </table>
                            </div>
                             </div>
 
                
                            
                                            </div>
                                            </div></div></div></div>


                      <script src="../js/jquery-1.9.1.min.js"></script>
		<script src="../js/bootstrap.min.js"></script>

<script type="text/javascript">
    LoadLIST();
    LOAD_ITEMS();

    function LOAD_ITEMS() {
        var BranchID = localStorage.getItem("BranchID");        
        var tblRows = "";
        var UserID = "1";
        $.ajax({
            type: 'POST',
            url: 'PO_LIST.aspx/LOAD_ITEMS',
            data: '{ "UserID" : "' + UserID + '","BranchID" : "' + BranchID + '" }',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (msg) {
                var s = msg.d.split('`');
                tblRows = s[0];
                var b = tblRows.replace(/'/g, '"');
                document.getElementById('ContentPlaceHolder1_hdnItems').value = b;
                txtAccCode = document.getElementById('txtAccCode1');
                txtAccCode.dataset.source = document.getElementById('ContentPlaceHolder1_hdnItems').value;
                document.getElementById("txtAccCode1").focus();

            }

        });
    }

        function LoadLIST() {
        var BranchID = localStorage.getItem("BranchID");        
        var tblRows = "";
        var UserID = localStorage.getItem("UserID");
       
        $.ajax({
            type: 'POST',
            url: 'PO_LIST.aspx/LoadLIST',
            data: '{ "UserID" : "' + UserID + '","BranchID" : "' + BranchID + '"  }',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (msg) {
                var s = msg.d;
               // alert(s);
                document.getElementById('dvList').innerHTML = s;

            }

        });
    }

    function LoadLISTSearch(a) {
        var txtAccCodeID = a.id;
       // alert(var);
        var txtAccCode = document.getElementById(txtAccCodeID).value;
        if (txtAccCode == "" || txtAccCode == null) {
            return;
        }
        var res = txtAccCode.substring(13, 30);        
        var PaymentType = res;
        var tblRows = "";
        var UserID = localStorage.getItem("UserID");
        var BranchID = localStorage.getItem("BranchID");        
        $.ajax({
            type: 'POST',
            url: 'PO_LIST.aspx/LoadLISTSearch',
            data: '{ "UserID" : "' + UserID + '","PaymentType" : "' + PaymentType + '" ,"BranchID" : "' + BranchID + '"  }',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (msg) {
                var s = msg.d;                
                document.getElementById('dvList').innerHTML = s;

            }

        });
    }

</script>

</asp:Content>

