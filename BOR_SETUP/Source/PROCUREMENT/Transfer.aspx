<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Master.master" AutoEventWireup="true" CodeFile="Transfer.aspx.cs" Inherits="ERP_ITM_Item" EnableEventValidation="false" %>

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

    <style type="text/css">
        .highlight{
            background:#cfd1d3;
        }
        .nonhighlight {
            background:white;
        }
        .pg-normal { 
cursor: pointer; 
background: #fff; 
padding: 8px 15px 8px 15px; 
margin: 20px 0;
border-radius: 4px;
display: inline-block;
box-sizing: border-box;
margin-bottom: 0!important;
margin-top: 0!important;
border-color: #e2e7eb;
border: 1px solid #ddd;
font-size: 12px;
font-family: 'Open Sans',"Helvetica Neue",Helvetica,Arial,sans-serif;
color: #707478;
        
}

.pg-selected { 
color: #fff; 
background: #000000; 
padding: 8px 15px 8px 15px; 
border-radius: 4px;
display: inline-block;
box-sizing: border-box;
margin-bottom: 0!important;
margin-top: 0!important;
border-color: #e2e7eb;
border: 1px solid #ddd;
font-size: 12px;
font-family: 'Open Sans',"Helvetica Neue",Helvetica,Arial,sans-serif;
    
}
.pg-normal:hover { 
color: #242a30;
background: #e2e7eb;
border-color: #d8dde1;
}
      



    </style>
      <script type="text/javascript">

          function Pager(tableName, itemsPerPage) {

              this.tableName = tableName;

              this.itemsPerPage = itemsPerPage;

              this.currentPage = 1;

              this.pages = 0;

              this.inited = false;

              this.showRecords = function (from, to) {

                  var rows = document.getElementById(tableName).rows;

                  // i starts from 1 to skip table header row

                  for (var i = 1; i < rows.length; i++) {

                      if (i < from || i > to)

                          rows[i].style.display = 'none';

                      else

                          rows[i].style.display = '';

                  }

              }

              this.showPage = function (pageNumber) {
                  if (!this.inited) {

                      alert("not inited");

                      return;

                  }

                  var oldPageAnchor = document.getElementById('pg' + this.currentPage);

                  oldPageAnchor.className = 'pg-normal';

                  this.currentPage = pageNumber;

                  var newPageAnchor = document.getElementById('pg' + this.currentPage);

                  newPageAnchor.className = 'pg-selected';

                  var from = (pageNumber - 1) * itemsPerPage + 1;

                  var to = from + itemsPerPage - 1;

                  this.showRecords(from, to);

              }

              this.prev = function () {

                  if (this.currentPage > 1)

                      this.showPage(this.currentPage - 1);


              }

              this.next = function () {
                  var rows = document.getElementById(tableName).rows;
                  var records = (rows.length - 1);
                  this.pages = Math.ceil(records / itemsPerPage);


                  if (this.currentPage < this.pages) {

                      this.showPage(this.currentPage + 1);

                  }

              }


              this.init = function () {

                  var rows = document.getElementById(tableName).rows;

                  var records = (rows.length - 1);

                  this.pages = Math.ceil(records / itemsPerPage);

                  this.inited = true;

              }


              this.showPageNav = function (pagerName, positionId) {

                  if (!this.inited) {

                      alert("not inited");

                      return;

                  }

                  var element = document.getElementById(positionId);

                  var pagerHtml = '<span onclick="' + pagerName + '.prev();" class="pg-normal"> ←&nbsp Previous </span> ';

                  for (var page = 1; page <= this.pages; page++)

                      pagerHtml += '<span id="pg' + page + '" class="pg-normal" onclick="' + pagerName + '.showPage(' + page + ');">' + page + '</span> ';

                  pagerHtml += '<span onclick="' + pagerName + '.next();" class="pg-normal"> Next &nbsp→ </span>';

                  element.innerHTML = pagerHtml;

              }

          }

</script>


    <script type="text/javascript">


        $(document).ready(function () {
            var BranchID = localStorage.getItem("BranchID");
            var dataToSend = JSON.stringify({ 'BranchID': BranchID });
            $.ajax({
                type: "POST",
                url: "SO.aspx/LoadRMbValue",
                data: dataToSend,
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var jsdata = JSON.parse(data.d);

                    $.each(jsdata, function (key, value) {
                        //alert(value.QualificationID);
                        document.getElementById("ContentPlaceHolder1_hdnRMBValue").value = value.UnitTypeID;
                        //   document.getElementById("txtRMBValue1").value = value.UnitTypeID;

                    });


                },
                error: function (data) {
                    alert("error found");
                }
            });
        });



        $(document).ready(function () {

            var BranchID = localStorage.getItem("BranchID");
            var BranchName = localStorage.getItem("BranchName");
            document.getElementById("ContentPlaceHolder1_txtBranchFrom").value = BranchName;

        });

        function LoadQty1(a) {
            var skillsSelect = document.getElementById("ContentPlaceHolder1_ddlItemFrom"+a);
            var selectedText = skillsSelect.options[skillsSelect.selectedIndex].text;
            //var abc = document.getElementById("ContentPlaceHolder1_ddlItemFrom1").value;
            //alert(selectedText);
            //var string = "0,1";
            var array = selectedText.split("^");
            //alert(array[2]);
            var qty = array[2];
            //alert(qty);
            qty = qty.replace(" QTY : ", "");
            qty = qty.replace(" ", "");
            document.getElementById("txtQtyAvaliable" + a).value = qty;
            
        }

        function ValidateQty1(a) {
            var avl = document.getElementById("txtQtyAvaliable" + a).value;
            var rq = document.getElementById("ContentPlaceHolder1_txtQty" + a).value;
            if (avl == "")
            { document.getElementById("txtQtyAvaliable" + a).value = "0"; }
            if (rq == "")
            { document.getElementById("ContentPlaceHolder1_txtQty" + a).value = "0"; }
            if (parseFloat(rq) > parseFloat(avl)) {
                document.getElementById("ContentPlaceHolder1_txtQty" + a).value = "0";
                alert("Please reduce Quantity...!");
                
            }
        }
        



        //Use for Dropdown

        $(document).ready(function () {
            // alert("sadasd");
            var BranchID = localStorage.getItem("BranchID");
            var dataToSend = JSON.stringify({ 'BranchID': BranchID });
            $.ajax({
                type: "POST",
                url: "Transfer.aspx/LoadRegionCombo1",
                data: dataToSend,
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var jsdata = JSON.parse(data.d);
                    var rows = "";
                    if ($('#ContentPlaceHolder1_DropDownList').select.length != 0) {

                        $('#ContentPlaceHolder1_DropDownList').empty();
                    }

                    $.each(jsdata, function (key, value) {
                        //alert(value.QualificationID);
                        rows += "<option  value=" + value.UnitTypeID + ">" + value.UnitTypeDesc + "</option>";
                    });

                    $("#ContentPlaceHolder1_DropDownList").append(rows);




                },
                error: function (data) {
                    alert("error found");
                }
            });
        });


        //Use for Dropdown

        $(document).ready(function () {
            // alert("sadasd");
            var BranchID = localStorage.getItem("BranchID");
            var dataToSend = JSON.stringify({ 'BranchID': BranchID });
            $.ajax({
                type: "POST",
                url: "Transfer.aspx/LoadBrand",
                data: dataToSend,
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var jsdata = JSON.parse(data.d);
                    var rows = "";
                    if ($('#ContentPlaceHolder1_DropDownList2').select.length != 0) {

                        $('#ContentPlaceHolder1_DropDownList2').empty();
                    }

                    $.each(jsdata, function (key, value) {
                        //alert(value.QualificationID);
                        rows += "<option  value=" + value.UnitTypeID + ">" + value.UnitTypeDesc + "</option>";
                    });

                    $("#ContentPlaceHolder1_DropDownList2").append(rows);




                },
                error: function (data) {
                    alert("error found");
                }
            });
        });


        $(document).ready(function () {
            // alert("sadasd");
            var BranchID = localStorage.getItem("BranchID");
            var dataToSend = JSON.stringify({ 'BranchID': BranchID });
            $.ajax({
                type: "POST",
                url: "Transfer.aspx/LoadCategory",
                data: dataToSend,
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var jsdata = JSON.parse(data.d);
                    var rows = "";
                    if ($('#ContentPlaceHolder1_DropDownList1').select.length != 0) {

                        $('#ContentPlaceHolder1_DropDownList1').empty();
                    }

                    $.each(jsdata, function (key, value) {
                        //alert(value.QualificationID);
                        rows += "<option  value=" + value.UnitTypeID + ">" + value.UnitTypeDesc + "</option>";
                    });

                    $("#ContentPlaceHolder1_DropDownList1").append(rows);




                },
                error: function (data) {
                    alert("error found");
                }
            });
        });






        function Save() {


            var field = document.getElementById("txtitemname").value;
         //   var field = document.getElementById("txtItemCode").value;
           


            if (field == "" || field == null) {
                document.getElementById("txtitemname").style.border = "solid 1px red";
                //document.getElementById("txtItemCode").style.border = "solid 1px red";
              


                return false;
            }



            $("#modal-dialog").modal();

        }




        function msgrevert() {
            $("#msgbody").text("Are you sure about this?");
            $("#confirm").show();

        }










        function EnterEvent(e) {
            if (e.keyCode == 13) {
                event.preventDefault();
                document.getElementById("btnSave").click();
                $('#btnSave').click();
            }
        }


        $(document).keydown(function (e) {
//            if (e.altKey && e.which == 83) { // altr + s
//                e.preventDefault();
//                document.getElementById("btnSave").click();
//                $('#btnSave').click();
//            }
            //else
             if (e.ctrlKey && e.which == 83) {  // altr + s
                e.preventDefault();
                document.getElementById("confirm").click();
               // $('#confirm').click();
            }
            else if (e.ctrlKey && e.which == 88) {  // altr + x
                e.preventDefault();
                document.getElementById("SaveClose").click();
                //$('#SaveClose').click();
            }

//            else if (e.altKey && e.which == 68) {  // altr + d
//                e.preventDefault();
//                document.getElementById("confirm_delete").click();
//                $('#confirm_delete').click();
//            }

//            else if (e.altKey && e.which == 90) {  // altr + z
//                e.preventDefault();
//                document.getElementById("confirm_delete").click();
//                $('#confirm_delete').click();
//            }
//            else if (e.altKey && e.which == 87) { // altr + w
//                e.preventDefault();
//                document.getElementById("DeleteClose").click();
//                $('#DeleteClose').click();
//            }
        });







        function DeleteData() {

            $("#modal-dialog_delete").modal();
        }


        function SaveData() {

           




            if ($("#btnSave").text() == "Save") {

                InsertRegion();
                $("#msgbody").text("Record has been inserted successfully.");
                $("#confirm").hide();
            }
            else if ($("#btnSave").text() == "Update") {
                $("#search").val('');
                $("#msgbody").text("Record has been updated successfully.");
                $("#confirm").hide();

                UpdateRegion();

            }

            document.getElementById("txtitemname").style.border = "solid 1px #ccc";
            document.getElementById("txtItemCode").style.border = "solid 1px #ccc";
            document.getElementById("txtbarcode").style.border = "solid 1px #ccc";
            document.getElementById("txtDiscription").style.border = "solid 1px #ccc";



        }



        function InsertRegion() {
            var field = document.getElementById("txtitemname").value;
            if (field == "" || field == null) {
                document.getElementById("txtitemname").style.border = "solid 1px red";
                return false;
            }

            //alert("asd");
            var ddl = document.getElementById("ContentPlaceHolder1_DropDownList").value;
            var ddlCategory = document.getElementById("ContentPlaceHolder1_DropDownList1").value;
            var ddlBrand = document.getElementById("ContentPlaceHolder1_DropDownList2").value;
            var UserID = localStorage.getItem("UserID");
            var BranchID = localStorage.getItem("BranchID");
            var dataToSend = JSON.stringify({ 'ITEMName': $('#txtitemname').val(), 'ItemCode': $('#txtItemCode').val(), 'BarCode': $('#txtbarcode').val(), 'Discription': $('#txtDiscription').val(), 'UserID': UserID, 'UnitTypeID': ddl, 'Category': ddlCategory, 'Brand': ddlBrand, 'BranchID': BranchID });
           
            var results = $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "Transfer.aspx/InsertRegion",
                dataType: "json",
                data: dataToSend,
                //data: "{'RegionName':'" + $('#txtunittype').val() + "'}",                
                async: false,
                success: function (data) {
                    var obj = data.d;
                    if (obj == 'true') {
                        LoadRegion();
                        Clear();


                    }

                },
                error: function (result) {
                    alert(result);
                }
            });

            return results;

        }




        function UpdateRegion() {
            var field = document.getElementById("txtitemname").value;
            if (field == "" || field == null) {
                document.getElementById("txtitemname").style.border = "solid 1px red";
                return false;
            }
            // alert("abc");
            var ddl = document.getElementById("ContentPlaceHolder1_DropDownList").value;
            var ddlCategory = document.getElementById("ContentPlaceHolder1_DropDownList1").value;
            var ddlBrand = document.getElementById("ContentPlaceHolder1_DropDownList2").value;
            var BranchID = localStorage.getItem("BranchID");
            var dataToSend = JSON.stringify({ 'ITEMID': $('#ContentPlaceHolder1_hdnid').val(), 'ITEMName': $('#txtitemname').val(), 'ItemCode': $('#txtItemCode').val(), 'BarCode': $('#txtbarcode').val(), 'Discription': $('#txtDiscription').val(), 'UnitTypeID': ddl, 'Category': ddlCategory, 'Brand': ddlBrand, 'BranchID': BranchID });
           //alert(dataToSend);
            var results = $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "Transfer.aspx/UpdateRegion",
                dataType: "json",
                data: dataToSend,
                async: false,
                success: function (data) {
                    var obj = data.d;
                    if (obj == 'true') {
                        LoadRegion();
                        Clear();

                    }

                },
                error: function (result) {
                    alert(result);
                }
            });

            return results;

        }

        function DeleteRegion() {
            //  alert($('#ContentPlaceHolder1_hdnid').val());
            var UserID = localStorage.getItem("UserID");
            var BranchID = localStorage.getItem("BranchID");
            var dataToSend = JSON.stringify({ 'ITEMID': $('#ContentPlaceHolder1_hdnid').val(), 'UserID': UserID, 'BranchID': BranchID });
            var results = $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "Transfer.aspx/DeleteRegion",
                dataType: "json",
                data: dataToSend,
                async: false,
                success: function (data) {
                    var obj = data.d;

                    if (obj == 'true') {

                        LoadRegion();
                        Clear();
                        $("#msgbody_delete").text("Record has been deleted successfully.");
                        $("#confirm_delete").hide();


                    }


                },
                error: function (result) {
                    alert(result);
                }
            });

            return results;

        }












        function Clear() {
            $('#ctl00_ContentPlaceHolder1_hdnid').val('');
            $('#txtitemname').val('');
            $('#txtItemCode').val('');
            $('#txtbarcode').val('');
            $('#txtDiscription').val('');
            $("#btnSave").text("Save");
        }



        $("#tablepaging tbody tr").live('click', function () {
            document.getElementById("<%=hdnid.ClientID%>").value = $(this).closest('tr').children('td.one').text();
            document.getElementById("txtitemname").value = $(this).closest('tr').children('td.two').text();
            document.getElementById("txtItemCode").value = $(this).closest('tr').children('td.three').text();
            document.getElementById("txtbarcode").value = $(this).closest('tr').children('td.four').text();
            document.getElementById("txtDiscription").value = $(this).closest('tr').children('td.five').text();
            document.getElementById("ContentPlaceHolder1_DropDownList").value = $(this).closest('tr').children('td.six').text();
            document.getElementById("ContentPlaceHolder1_DropDownList1").value = $(this).closest('tr').children('td.seven').text();
            document.getElementById("ContentPlaceHolder1_DropDownList2").value = $(this).closest('tr').children('td.eight').text();


            $("#btnSave").text("Update");


        });







    </script>


</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">






    


    <div class="modal fade" id="modal-dialog">
								<div class="modal-dialog">
									<div class="modal-content">
										<div class="modal-header"  style="background-color:White; border-bottom:1px solid #D5D5D5">
											<button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
											<h4 class="modal-title">Confirmation Required</h4>
										</div>
										<div class="modal-body" id="msgbody" style=" border-bottom:1px solid #D5D5D5;">
											Are you sure about this?
										</div>
										<div class="modal-footer"  style="background-color:White;">
											<a href="javascript:;" class="btn btn-sm btn-white" data-dismiss="modal"  id="SaveClose" onclick="msgrevert();">Close (Ctrl + X)</a>
											<a href="javascript:;" class="btn btn-sm btn-success" onclick="SaveData();" id="confirm">Confirm (Ctrl + S)</a>
										</div>
									</div>
								</div>
							</div>



    <div class="modal fade" id="modal-dialog_delete">
								<div class="modal-dialog">
									<div class="modal-content">
										<div class="modal-header" style="background-color:White; border-bottom:1px solid #D5D5D5">
											<button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
											<h4 class="modal-title">Confirmation Required</h4>
										</div>
										<div class="modal-body" id="msgbody_delete" style=" border-bottom:1px solid #D5D5D5;">
											Are you sure about this?
										</div>
										<div class="modal-footer" style="background-color:White;">
											<a href="javascript:;" class="btn btn-sm btn-white" data-dismiss="modal" id="DeleteClose" onclick="msgrevert_delete();">Close</a>
											<a href="javascript:;" class="btn btn-sm btn-danger" onclick="DeleteRegion();" id="confirm_delete">Confirm</a>
										</div>
									</div>
								</div>
							</div>


    <div class="row">
                <!-- begin col-6 -->
			    <div class="col-md-12">
			        <!-- begin panel -->
                    <div class="panel widget light-widget panel-bd-top">
                        <div class="panel-heading bordered">
                    <h3 class="panel-title"> <span class="menu-icon"> <i class="icon-pie"></i> </span> Item Transfer from Branch to Branch</h3>
                    <div class="vd_panel-menu">
  <div data-action="minimize" data-original-title="Minimize" data-toggle="tooltip" data-placement="bottom" class=" menu entypo-icon"> <i class="icon-minus3"></i> </div>
  <div data-action="refresh"  data-original-title="Refresh" data-toggle="tooltip" data-placement="bottom" class=" menu entypo-icon smaller-font"> <i class="icon-cycle"></i> </div>
  
  <div data-action="close" data-original-title="Close" data-toggle="tooltip" data-placement="bottom" class=" menu entypo-icon"> <i class="icon-cross"></i> </div>
</div>
<!-- vd_panel-menu --> 
                  </div>
                        <div class=" panel-body-list" style="padding:10px">

                         <div class="col-lg-12">
                                <div class="form-group">
                                    
                                     <div class="col-md-12">
                                    <div class="input-group">
                                       <input type="text" class="width-100" id="StartDate" placeholder="Select Date" runat="server" />
                                       <span class="input-group-addon" id="datepicker-icon-trigger-PV" data-datepicker="#datepicker-autoClose"><i class="fa fa-calendar"></i></span>
                                       </div>                                                                       
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12" style="height:20px;"></div>
                        <div class="col-md-6">                        
                      <label class="control-label col-md-12 col-sm-12 col-xs-12">TRANSFER FROM</label>
                      </div>
                      <div class="col-md-6">                        
                      <label class="control-label col-md-12 col-sm-12 col-xs-12">TRANSFER TO</label>
                      </div>

                           <div class="col-md-6">                        
                        <div class="col-md-12 col-sm-12 col-xs-12">
                             <input type="text" id="txtBranchFrom" runat="server" required="required" class="form-control col-md-12 col-xs-12" placeholder="Transfer From" disabled="disabled">
                        </div>
                      </div>
                        <div class="col-md-6">                        
                        <div class="col-md-12 col-sm-12 col-xs-12">
                             <asp:DropDownList ID="ddlTransferToBranch" runat="server"  CssClass="chzn-select form-control" DataSourceID="SqlDataSource1"  DataTextField="BranchName" DataValueField="BranchID" AutoPostBack="true"  ></asp:DropDownList> 
                             
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Con %>" 
                                            SelectCommand="select '0' AS BranchID,'Select Branch for Transfer' AS BranchName union SELECT   [BranchID], [BranchName] FROM [Branch] where BranchID!=@BranchID Order by BranchID">
                                            <SelectParameters>
                                                <asp:CookieParameter CookieName="BranchID" Name="BranchID"  Type="String" />
                                            </SelectParameters>
                                             </asp:SqlDataSource>   
                        </div>
                      </div>
                      <div class="col-md-12" style="height:10px;"></div>
                        <div class="col-md-6">                        
                      <label class="control-label col-md-12 col-sm-12 col-xs-12">Local Bill # FROM</label>
                      </div>
                      <div class="col-md-6">                        
                      <label class="control-label col-md-12 col-sm-12 col-xs-12">Local Bill # TO</label>
                      </div>

                           <div class="col-md-6">                        
                        <div class="col-md-12 col-sm-12 col-xs-12">
                             <input type="text" id="txtBillFrom" runat="server" required="required" class="form-control col-md-12 col-xs-12" placeholder="Local Bill # From" />
                        </div>
                      </div>
                        <div class="col-md-6">                        
                        <div class="col-md-12 col-sm-12 col-xs-12">
                           <input type="text" id="txtBillTo" runat="server" required="required" class="form-control col-md-12 col-xs-12" placeholder="Local Bill # To" />
                        </div>
                      </div>
                       <div class="col-md-12" style="height:10px;"></div>
                        <div class="col-md-6">                        
                      <label class="control-label col-md-12 col-sm-12 col-xs-12">Remarks FROM</label>
                      </div>
                      <div class="col-md-6">                        
                      <label class="control-label col-md-12 col-sm-12 col-xs-12">Remarks TO</label>
                      </div>

                           <div class="col-md-6">                        
                        <div class="col-md-12 col-sm-12 col-xs-12">
                             <input type="text" id="txtRemarksFrom" runat="server" required="required" class="form-control col-md-12 col-xs-12" placeholder="Local Bill # From" />
                        </div>
                      </div>
                        <div class="col-md-6">                        
                        <div class="col-md-12 col-sm-12 col-xs-12">
                           <input type="text" id="txtRemarksTo" runat="server" required="required" class="form-control col-md-12 col-xs-12" placeholder="Local Bill # To" />
                        </div>
                      </div>
                      <div class="col-md-12" style="height:20px;"></div>
                       <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Con %>" 
                            SelectCommand=" select '0' AS ITEMID,'Select Item' AS ITEMName,'0' AS ItemCode,'0' AS UnitTypeID,'0' AS BranchID,'0' AS BranchID1,'0' AS BranchID2,'0' AS BranchI3 union select * from VW_ITEM_FOR_SALE where BranchID=@BranchID and  BranchID1=@BranchID and  BranchID2=@BranchID and BranchID3=@BranchID">
                                            <SelectParameters>
                                                <asp:CookieParameter CookieName="BranchID" Name="BranchID"  Type="String" />
                                            </SelectParameters>
                                             </asp:SqlDataSource>   
                                               <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:Con %>" 
                            SelectCommand=" SELECT        dbo.ITM_ITEM.ITEMID, dbo.ITM_ITEM.ITEMID + ' ^ ' + dbo.ITM_ITEM.ITEMName + ' ^ ' + dbo.Category.CatTitle + ' ^ ' + dbo.Brand.BrandTitle AS ITEMName, dbo.ITM_ITEM.ItemCode,  dbo.ITM_ITEM.ITEMID,                         dbo.ITM_ITEM.UnitTypeID FROM            dbo.ITM_ITEM INNER JOIN                          dbo.Category ON dbo.ITM_ITEM.CatID = dbo.Category.CatID INNER JOIN                          dbo.Brand ON dbo.ITM_ITEM.BrandID = dbo.Brand.BrandID WHERE        (dbo.ITM_ITEM.ISDELETE = '0') and dbo.ITM_ITEM.BranchID=@BranchID and dbo.Brand.BranchID=@BranchID and dbo.Category.BranchID=@BranchID">
                             
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="ddlTransferToBranch" Name="BranchID"  Type="String" />
                                            </SelectParameters>
                                             </asp:SqlDataSource>   
                        <div class="col-md-6">                        
                        <div class="col-md-12 col-sm-12 col-xs-12">
                             <asp:DropDownList ID="ddlItemFrom1" runat="server"  CssClass="chzn-select form-control"  DataSourceID="SqlDataSource2"  DataTextField="ITEMName" DataValueField="ITEMID" onchange="LoadQty1(1);" ></asp:DropDownList> 
                             
                           
                        </div>
                     
                      </div>
                        
                        <div class="col-md-6">     
                         <div class="col-md-2">                        
                        <input type="number" id="txtQtyAvaliable1" disabled="disabled" value="0" required="required" class="form-control col-md-12 col-xs-12" placeholder="Enter Qty.">
                      </div>                   
                        <div class="col-md-8 col-sm-8 col-xs-8">
                             <asp:DropDownList ID="ddlItemTo1" runat="server"  CssClass="chzn-select form-control"  DataSourceID="SqlDataSource3"  DataTextField="ITEMName" DataValueField="ITEMID"  ></asp:DropDownList>                              
                        </div>
                        <div class="col-md-2">                        
                        <input type="text" id="txtQty1"  value="0" required="required" class="form-control col-md-12 col-xs-12" placeholder="Enter Qty." onkeyup="ValidateQty1(1);" runat="server" />
                      </div>                   
                      </div>
                     
                     <div class="col-md-12" style="height:20px;"></div>
                  
                    <div class="col-md-6">                        
                        <div class="col-md-12 col-sm-12 col-xs-12">
                             <asp:DropDownList ID="ddlItemFrom2" runat="server"  CssClass="chzn-select form-control"  DataSourceID="SqlDataSource2"  DataTextField="ITEMName" DataValueField="ITEMID" onchange="LoadQty1(2);" ></asp:DropDownList> 
                             
                           
                        </div>
                     
                      </div>
                        
                        <div class="col-md-6">     
                         <div class="col-md-2">                        
                        <input type="number" id="txtQtyAvaliable2" disabled="disabled" value="0" required="required" class="form-control col-md-12 col-xs-12" placeholder="Enter Qty.">
                      </div>                   
                        <div class="col-md-8 col-sm-8 col-xs-8">
                             <asp:DropDownList ID="ddlItemTo2" runat="server"  CssClass="chzn-select form-control"  DataSourceID="SqlDataSource3"  DataTextField="ITEMName" DataValueField="ITEMID"  ></asp:DropDownList>                              
                        </div>
                        <div class="col-md-2">                        
                        <input type="text" id="txtQty2"  value="0" required="required" class="form-control col-md-12 col-xs-12" placeholder="Enter Qty." onkeyup="ValidateQty1(2);" runat="server" />
                      </div>                   
                      </div>
                     
                     <div class="col-md-12" style="height:20px;"></div>
                       <div class="col-md-6">                        
                        <div class="col-md-12 col-sm-12 col-xs-12">
                             <asp:DropDownList ID="ddlItemFrom3" runat="server"  CssClass="chzn-select form-control"  DataSourceID="SqlDataSource2"  DataTextField="ITEMName" DataValueField="ITEMID" onchange="LoadQty1(3);" ></asp:DropDownList> 
                             
                           
                        </div>
                     
                      </div>
                        
                        <div class="col-md-6">     
                         <div class="col-md-2">                        
                        <input type="number" id="txtQtyAvaliable3" disabled="disabled" value="0" required="required" class="form-control col-md-12 col-xs-12" placeholder="Enter Qty.">
                      </div>                   
                        <div class="col-md-8 col-sm-8 col-xs-8">
                             <asp:DropDownList ID="ddlItemTo3" runat="server"  CssClass="chzn-select form-control"  DataSourceID="SqlDataSource3"  DataTextField="ITEMName" DataValueField="ITEMID"  ></asp:DropDownList>                              
                        </div>
                        <div class="col-md-2">                        
                        <input type="text" id="txtQty3"  value="0" required="required" class="form-control col-md-12 col-xs-12" placeholder="Enter Qty." onkeyup="ValidateQty1(3);" runat="server" />
                      </div>                   
                      </div>
                     
                     <div class="col-md-12" style="height:20px;"></div>

                       <div class="col-md-6">                        
                        <div class="col-md-12 col-sm-12 col-xs-12">
                             <asp:DropDownList ID="ddlItemFrom4" runat="server"  CssClass="chzn-select form-control"  DataSourceID="SqlDataSource2"  DataTextField="ITEMName" DataValueField="ITEMID" onchange="LoadQty1(4);" ></asp:DropDownList> 
                             
                           
                        </div>
                     
                      </div>
                        
                        <div class="col-md-6">     
                         <div class="col-md-2">                        
                        <input type="number" id="txtQtyAvaliable4" disabled="disabled" value="0" required="required" class="form-control col-md-12 col-xs-12" placeholder="Enter Qty.">
                      </div>                   
                        <div class="col-md-8 col-sm-8 col-xs-8">
                             <asp:DropDownList ID="ddlItemTo4" runat="server"  CssClass="chzn-select form-control"  DataSourceID="SqlDataSource3"  DataTextField="ITEMName" DataValueField="ITEMID"  ></asp:DropDownList>                              
                        </div>
                        <div class="col-md-2">                        
                        <input type="text" id="txtQty4"  value="0" required="required" class="form-control col-md-12 col-xs-12" placeholder="Enter Qty." onkeyup="ValidateQty1(4);" runat="server" />
                      </div>                   
                      </div>
                     
                     <div class="col-md-12" style="height:20px;"></div>

                       <div class="col-md-6">                        
                        <div class="col-md-12 col-sm-12 col-xs-12">
                             <asp:DropDownList ID="ddlItemFrom5" runat="server"  CssClass="chzn-select form-control"  DataSourceID="SqlDataSource2"  DataTextField="ITEMName" DataValueField="ITEMID" onchange="LoadQty1(5);" ></asp:DropDownList> 
                             
                           
                        </div>
                     
                      </div>
                        
                        <div class="col-md-6">     
                         <div class="col-md-2">                        
                        <input type="number" id="txtQtyAvaliable5" disabled="disabled" value="0" required="required" class="form-control col-md-12 col-xs-12" placeholder="Enter Qty.">
                      </div>                   
                        <div class="col-md-8 col-sm-8 col-xs-8">
                             <asp:DropDownList ID="ddlItemTo5" runat="server"  CssClass="chzn-select form-control"  DataSourceID="SqlDataSource3"  DataTextField="ITEMName" DataValueField="ITEMID"  ></asp:DropDownList>                              
                        </div>
                        <div class="col-md-2">                        
                        <input type="text" id="txtQty5"  value="0" required="required" class="form-control col-md-12 col-xs-12" placeholder="Enter Qty." onkeyup="ValidateQty1(5);" runat="server" />
                      </div>                   
                      </div>
                     
                     <div class="col-md-12" style="height:20px;"></div>

                       <div class="col-md-6">                        
                        <div class="col-md-12 col-sm-12 col-xs-12">
                             <asp:DropDownList ID="ddlItemFrom6" runat="server"  CssClass="chzn-select form-control"  DataSourceID="SqlDataSource2"  DataTextField="ITEMName" DataValueField="ITEMID" onchange="LoadQty1(6);" ></asp:DropDownList> 
                             
                           
                        </div>
                     
                      </div>
                        
                        <div class="col-md-6">     
                         <div class="col-md-2">                        
                        <input type="number" id="txtQtyAvaliable6" disabled="disabled" value="0" required="required" class="form-control col-md-12 col-xs-12" placeholder="Enter Qty.">
                      </div>                   
                        <div class="col-md-8 col-sm-8 col-xs-8">
                             <asp:DropDownList ID="ddlItemTo6" runat="server"  CssClass="chzn-select form-control"  DataSourceID="SqlDataSource3"  DataTextField="ITEMName" DataValueField="ITEMID"  ></asp:DropDownList>                              
                        </div>
                        <div class="col-md-2">                        
                        <input type="text" id="txtQty6"  value="0" required="required" class="form-control col-md-12 col-xs-12" placeholder="Enter Qty." onkeyup="ValidateQty1(6);" runat="server" />
                      </div>                   
                      </div>
                     
                     <div class="col-md-12" style="height:20px;"></div>

                       <div class="col-md-6">                        
                        <div class="col-md-12 col-sm-12 col-xs-12">
                             <asp:DropDownList ID="ddlItemFrom7" runat="server"  CssClass="chzn-select form-control"  DataSourceID="SqlDataSource2"  DataTextField="ITEMName" DataValueField="ITEMID" onchange="LoadQty1(7);" ></asp:DropDownList> 
                             
                           
                        </div>
                     
                      </div>
                        
                        <div class="col-md-6">     
                         <div class="col-md-2">                        
                        <input type="number" id="txtQtyAvaliable7" disabled="disabled" value="0" required="required" class="form-control col-md-12 col-xs-12" placeholder="Enter Qty.">
                      </div>                   
                        <div class="col-md-8 col-sm-8 col-xs-8">
                             <asp:DropDownList ID="ddlItemTo7" runat="server"  CssClass="chzn-select form-control"  DataSourceID="SqlDataSource3"  DataTextField="ITEMName" DataValueField="ITEMID"  ></asp:DropDownList>                              
                        </div>
                        <div class="col-md-2">                        
                        <input type="text" id="txtQty7"  value="0" required="required" class="form-control col-md-12 col-xs-12" placeholder="Enter Qty." onkeyup="ValidateQty1(7);" runat="server" />
                      </div>                   
                      </div>
                     
                     <div class="col-md-12" style="height:20px;"></div>

                       <div class="col-md-6">                        
                        <div class="col-md-12 col-sm-12 col-xs-12">
                             <asp:DropDownList ID="ddlItemFrom8" runat="server"  CssClass="chzn-select form-control"  DataSourceID="SqlDataSource2"  DataTextField="ITEMName" DataValueField="ITEMID" onchange="LoadQty1(8);" ></asp:DropDownList> 
                             
                           
                        </div>
                     
                      </div>
                        
                        <div class="col-md-6">     
                         <div class="col-md-2">                        
                        <input type="number" id="txtQtyAvaliable8" disabled="disabled" value="0" required="required" class="form-control col-md-12 col-xs-12" placeholder="Enter Qty.">
                      </div>                   
                        <div class="col-md-8 col-sm-8 col-xs-8">
                             <asp:DropDownList ID="ddlItemTo8" runat="server"  CssClass="chzn-select form-control"  DataSourceID="SqlDataSource3"  DataTextField="ITEMName" DataValueField="ITEMID"  ></asp:DropDownList>                              
                        </div>
                        <div class="col-md-2">                        
                        <input type="text" id="txtQty8"  value="0" required="required" class="form-control col-md-12 col-xs-12" placeholder="Enter Qty." onkeyup="ValidateQty1(8);" runat="server" />
                      </div>                   
                      </div>
                     
                     <div class="col-md-12" style="height:20px;"></div>

                       <div class="col-md-6">                        
                        <div class="col-md-12 col-sm-12 col-xs-12">
                             <asp:DropDownList ID="ddlItemFrom9" runat="server"  CssClass="chzn-select form-control"  DataSourceID="SqlDataSource2"  DataTextField="ITEMName" DataValueField="ITEMID" onchange="LoadQty1(9);" ></asp:DropDownList> 
                             
                           
                        </div>
                     
                      </div>
                        
                        <div class="col-md-6">     
                         <div class="col-md-2">                        
                        <input type="number" id="txtQtyAvaliable9" disabled="disabled" value="0" required="required" class="form-control col-md-12 col-xs-12" placeholder="Enter Qty.">
                      </div>                   
                        <div class="col-md-8 col-sm-8 col-xs-8">
                             <asp:DropDownList ID="ddlItemTo9" runat="server"  CssClass="chzn-select form-control"  DataSourceID="SqlDataSource3"  DataTextField="ITEMName" DataValueField="ITEMID"  ></asp:DropDownList>                              
                        </div>
                        <div class="col-md-2">                        
                        <input type="text" id="txtQty9"  value="0" required="required" class="form-control col-md-12 col-xs-12" placeholder="Enter Qty." onkeyup="ValidateQty1(9);" runat="server" />
                      </div>                   
                      </div>
                     
                     <div class="col-md-12" style="height:20px;"></div>

                       <div class="col-md-6">                        
                        <div class="col-md-12 col-sm-12 col-xs-12">
                             <asp:DropDownList ID="ddlItemFrom10" runat="server"  CssClass="chzn-select form-control"  DataSourceID="SqlDataSource2"  DataTextField="ITEMName" DataValueField="ITEMID" onchange="LoadQty1(10);" ></asp:DropDownList> 
                             
                           
                        </div>
                     
                      </div>
                        
                        <div class="col-md-6">     
                         <div class="col-md-2">                        
                        <input type="number" id="txtQtyAvaliable10" disabled="disabled" value="0" required="required" class="form-control col-md-12 col-xs-12" placeholder="Enter Qty.">
                      </div>                   
                        <div class="col-md-8 col-sm-8 col-xs-8">
                             <asp:DropDownList ID="ddlItemTo10" runat="server"  CssClass="chzn-select form-control"  DataSourceID="SqlDataSource3"  DataTextField="ITEMName" DataValueField="ITEMID"  ></asp:DropDownList>                              
                        </div>
                        <div class="col-md-2">                        
                        <input type="text" id="txtQty10"  value="0" required="required" class="form-control col-md-12 col-xs-12" placeholder="Enter Qty." onkeyup="ValidateQty1(10);" runat="server" />
                      </div>                   
                      </div>
                     
                     <div class="col-md-12" style="height:20px;"></div>
                           <div class="col-md-6">                        
                      <label class="control-label col-md-12 col-sm-12 col-xs-12">TRANSFER TO ACCOUNT</label>
                      </div>
                <div class="col-md-6">                        
                      <label class="control-label col-md-12 col-sm-12 col-xs-12">TRANSFER FROM ACCOUNT </label>
                      </div>
                
                    
                      <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:Con %>" 
                            SelectCommand="SELECT AccountsID, AccountsTitle + ' ( ' +   convert(nvarchar(20),Round(Balance,2)) + ' ) ' AS AccountsTitle FROM [VW_ACCOUNTS_NAME_WITH_BALANCE]
                            where BranchID1=@BranchID">
                            <SelectParameters>
                                                <asp:CookieParameter CookieName="BranchID" Name="BranchID"  Type="String" />
                                            </SelectParameters>
                                             </asp:SqlDataSource>   
                                               <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:Con %>" 
                            SelectCommand="SELECT AccountsID, AccountsTitle + ' ( ' +   convert(nvarchar(20),Round(Balance,2)) + ' ) ' AS AccountsTitle FROM [VW_ACCOUNTS_NAME_WITH_BALANCE]
                            where BranchID1=@BranchID">
                             
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="ddlTransferToBranch" Name="BranchID"  Type="String" />
                                            </SelectParameters>
                                             </asp:SqlDataSource>  
                        <div class="col-md-6">                        
                        <div class="col-md-12 col-sm-12 col-xs-12">
                             <asp:DropDownList ID="ddlTransferFromAccount" runat="server"  CssClass="chzn-select form-control"  DataSourceID="SqlDataSource4"  DataTextField="AccountsTitle" DataValueField="AccountsID" onchange="LoadQty1(10);" ></asp:DropDownList> 
                        </div>
                      </div>
                      <div class="col-md-6">                        
                        <div class="col-md-12 col-sm-12 col-xs-12">
                             <asp:DropDownList ID="ddlTransferToAccount" runat="server"  CssClass="chzn-select form-control"  DataSourceID="SqlDataSource5"  DataTextField="AccountsTitle" DataValueField="AccountsID" onchange="LoadQty1(10);" ></asp:DropDownList> 
                        </div>
                      </div>


                    <div class="col-md-12" style="padding:10px"></div>
                     
                      <div class="ln_solid"></div>
                      <div class="col-md-12">
                        <div class="col-md-12 col-sm-12 col-xs-12" style="text-align:center;">

                        <asp:Button CssClass="btn btn-success" runat="server" id="Button1" OnClick="Transfer" Text="S A V E" Width="100%" Font-Bold="true"   />
                           <%-- <button type="button" class="btn btn-success" id="btnSave" onclick="Save();">Save</button>

                                        <button type="button" class="btn btn-primary" onclick="Clear();">Cancel</button>
                                        <button type="button" class="btn btn-danger" onclick="DeleteData();">Delete</button>--%>

                            <asp:HiddenField ID="hdnid" runat="server" />
                            <asp:HiddenField ID="hdnRMBValue" runat="server" Value="0" />

                        </div>
                      </div>

                    </div></div></div>





                    
                <!-- begin col-6 -->
			    <div class="col-md-7" style="display:none;">
			        <!-- begin panel -->
                    <div class="panel widget light-widget panel-bd-top">
                        <div class="panel-heading bordered">
                    <h3 class="panel-title"> <span class="menu-icon"> <i class="icon-pie"></i> </span> Item List </h3>
                    <div class="vd_panel-menu">
  <div data-action="minimize" data-original-title="Minimize" data-toggle="tooltip" data-placement="bottom" class=" menu entypo-icon"> <i class="icon-minus3"></i> </div>
  <div data-action="refresh"  data-original-title="Refresh" data-toggle="tooltip" data-placement="bottom" class=" menu entypo-icon smaller-font"> <i class="icon-cycle"></i> </div>
  
  <div data-action="close" data-original-title="Close" data-toggle="tooltip" data-placement="bottom" class=" menu entypo-icon"> <i class="icon-cross"></i> </div>
</div>
<!-- vd_panel-menu --> 
                  </div>
                        <div class=" panel-body-list" style="padding:10px">
                  <div class="x_content"id="DivRegion" >
                                                                      <input type="text" id="search" placeholder="Type to search" class="search form-control" />

                    <table class="table" id="tablepaging">
                      <thead>
                        <tr>
                             <td></td>
                             <td>ID</td>
                             <td>Item Name</td>
                            <td>Item Code</td>
                           
                          
                        </tr>
                      </thead>
                      <tbody>


                           


                        <tr>
                          <th></th>
                          <th></th>
              
                           
                
                        </tr>
                     
                        
                      </tbody>
                    </table>

                  </div>
                                                                                <div id="pageNavPosition"  align="center" ></div> 

                </div>
                                                                                              <a href="javascript:;" class="btn btn-icon btn-circle btn-success btn-scroll-to-top fade" data-click="scroll-top"><i class="fa fa-angle-up"></i></a>

              </div></div></div>



               

      <script type="text/javascript">
          var pager = new Pager('tablepaging', 8);
          pager.init();
          pager.showPageNav('pager', 'pageNavPosition');
          pager.showPage(1);
</script>

<script type="text/javascript">
    $("#search").keyup(function () {
        //split the current value of searchInput
        var data = this.value.split(" ");
        //create a jquery object of the rows
        var jo = $("#tablepaging").find("tr");
        if (this.value == "") {
            jo.show();
            var pager = new Pager('tablepaging', 8);
            pager.init();
            pager.showPageNav('pager', 'pageNavPosition');
            pager.showPage(1);
            $("#pageNavPosition").show();
            return;

        }
        //hide all the rows
        jo.hide();

        //Recusively filter the jquery object to get results.
        jo.filter(function (i, v) {
            var $t = $(this);
            for (var d = 0; d < data.length; ++d) {
                //if ($t.text().toLowerCase().indexOf(data[d]) > -1) {
                if ($t.text().toLowerCase().indexOf(data[d]) > -1) {
                    //if ($t.is(":contains('" + data[d] + "')")) {                    
                    $("#pageNavPosition").hide();
                    return true;

                }
                else if ($t.text().toUpperCase().indexOf(data[d]) > -1) {
                    //if ($t.is(":contains('" + data[d] + "')")) {                    
                    $("#pageNavPosition").hide();
                    return true;

                }
            }

            return false;
        })


        //show the rows that match.
.show();
    }).focus(function () {
        this.value = "";
        $(this).css({
            "color": "black"
        });
        $(this).unbind('focus');
    }).css({
        "color": "#C0C0C0"
    });

</script>

  <script src="../js/jquery-1.9.1.min.js"></script>
		<script src="../js/bootstrap.min.js"></script>
                 <script type="text/javascript" src='../AAUI/plugins/jquery-ui/jquery-ui.custom.min.js'></script>
  <script type="text/javascript">
      SetDateNew();

      function SetDateNew() {
          var todaydate = new Date();
          var day = todaydate.getDate();
          var month = todaydate.getMonth() + 1;
          var year = todaydate.getFullYear();
          var datestring = month + "/" + day + "/" + year;
          //alert(datestring);
          document.getElementById("ContentPlaceHolder1_StartDate").value = datestring;
          //document.getElementById("ContentPlaceHolder1_EndDate").value = datestring;
          // alert(document.getElementById("ContentPlaceHolder1_txtDate").value);
          //document.getElementById("ContentPlaceHolder1_txtDate").value = document.getElementById("birthday").value;

      }
      $("#ContentPlaceHolder1_StartDate").datepicker({ dateFormat: 'm/dd/yy' });
      $('[data-datepicker]').click(function (e) {
          var data = $(this).data('datepicker');
          $(data).focus();
      });

</script>
<script src="../Scripts/jquery.min.js" type="text/javascript"></script>
		<script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
		<script type="text/javascript">		    $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>


</asp:Content>

