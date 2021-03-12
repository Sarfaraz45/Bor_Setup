    <%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Master.master" AutoEventWireup="true" CodeFile="POS.aspx.cs" Inherits="ERP_POS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">



     <script src="../jquery-1.8.2.js"></script>
    <script src="../jquery.min.js"></script>


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


        function LoadItem() {
            var BranchID = localStorage.getItem("BranchID");
            var dataToSend = JSON.stringify({ 'BranchID': BranchID });
            
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "POS.aspx/LoadItem",
                dataType: "json",
                data: dataToSend,
                success: function (data) {
                    var jsdata = JSON.parse(data.d);
                    var ro = "";
                    var cc = 0;
                    var ddl = "";
                    //                    if ($('#ContentPlaceHolder1_ddlItem').select.length != 0) {

                    //                        $('#ContentPlaceHolder1_ddlItem').empty();
                    //                    }
                    $.each(jsdata, function (key, value) {
                        //  alert(value.ITEMID);
                        cc = cc + 1;
                        //alert(cc);
                        //ro += "<div class='col-md-2' id=" + value.ITEMID + " style='text-align: center;font-size: 19px;'><a class='app app-button' style='width:100%; height: 100px;font-weight: bolder;' onclick=AddData(\'" + value.ITEMID + "'\,\'" + value.ITEMName + "'\," + value.Price + ");><img src='../images/itemimages/" + value.ITEMID + ".jpg' style='    border-radius: 15px;    border: 2px solid #00a65a;' >" + value.ITEMName + "</a></div>";                        
                        ro += "<div class='col-md-2' id=" + value.ITEMID + " style='text-align: center;font-size: 19px;'><a class='app app-button' style='width:100%; height: 100px;font-weight: bolder;' onclick=AddData(\'" + value.ITEMID + "'\,'Name'," + value.Price + ");><img src='../images/Userimages/" + value.ITEMID + ".jpg' style='    border-radius: 15px;    border: 2px solid #00a65a;width: 105px;    height: 105px;' ></a><br/><label id='lbl" + value.ITEMID + "'>" + value.ITEMName + "</label> </div>";
                        if (parseFloat(cc) == 6) {
                            cc = 0;
                            ro += "<div class='col-md-12' style='height:10px;'></div>";
                            
                        }

                        //  ddl += "<option value=" + value.ITEMID + ">" + value.ITEMName + "</option>"


                    });
                    //alert(ddl);
                    //$("#ContentPlaceHolder1_ddlItem").append(ddl);

                    document.getElementById("loadItems").innerHTML = ro;

                },
                error: function (result) {
                    alert(result);
                }


            });

        };


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
            var dataToSend = JSON.stringify({ 'ITEMID': $('#ContentPlaceHolder1_hdnid').val(), 'ITEMName': $('#txtitemname').val(), 'ItemCode': $('#txtItemCode').val(), 'BarCode': $('#txtbarcode').val(), 'Discription': $('#txtDiscription').val(), 'UnitTypeID': ddl, 'Category': ddlCategory, 'Brand': ddlBrand, 'BranchID': BranchID, 'PurchasePrice': $('#txtPurchasePrice').val(), 'SalePrice': $('#txtSalePrice').val() });
            //alert(dataToSend);
            var results = $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "POS.aspx/UpdateRegion",
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


        $(document).ready(function () {
            var BranchID = localStorage.getItem("BranchID");
            var dataToSend = JSON.stringify({ 'BranchID': BranchID });

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "POS.aspx/LoadRegion",
                //url: "POS.aspx/LoadItem",
                dataType: "json",
                data: dataToSend,
                success: function (data) {
                    var jsdata = JSON.parse(data.d);
                    var ro = "";

                    if ($('#tablepaging').length != 0) {

                        $('#tablepaging').remove();
                    }

                    ro = "<table id='tablepaging' class='table table-hover' style='cursor: pointer;' ><thead><th style='text-align: left;'>ID</th><th style='text-align: left;'>Product Name</th><th style='text-align: left;'>Price</th><th style='text-align: left;'>Discription</th><th style='text-align: left;'>Pr</th></thead><tbody>";
                    $.each(jsdata, function (key, value) {
                        ro += "<tr><td   class='one'>" + value.ITEMID + "</td>   <td   class='two'>" + value.ITEMName + "</td>    <td   class='three'>" + value.CatTitle + "</td>    <td  class='four'>" + value.BarCode + "</td>  <td   class='five'>" + value.CatTitle + "</td> <td   style='display:none;' class='six'>" + value.UnitTypeID + "</td><td   style='display:none;' class='seven'>" + value.Category + "</td><td   style='display:none;' class='Cost'>" + value.Cost + "</td> <td   style='display:none;' class='Price'>" + value.Price + "</td> </tr>";

                    });
                    ro = ro + "</tbody></table>";
                   // alert(ro);
                    $("#DivRegion").append(ro);
                    var pager = new Pager('tablepaging', 8);
                    pager.init();
                    pager.showPageNav('pager', 'pageNavPosition');
                    pager.showPage(1);


                },
                error: function (result) {
                    alert(result);
                }


            });

        });

        $(document).ready(function () {
            //alert("sadasd");
            var BranchID = localStorage.getItem("BranchID");
            var dataToSend = JSON.stringify({ 'BranchID': BranchID });
            // alert(dataToSend);
            $.ajax({
                type: "POST",
                url: "POS.aspx/LoadBrand",
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
//                      alert(rows);
                    });

                    $("#ContentPlaceHolder1_DropDownList2").append(rows);




                },
                error: function (data) {
                    alert("error found");
                }
            });
             //LoadItem();
        });


        function Clear() {
            $('#ctl00_ContentPlaceHolder1_hdnid').val('');
            $('#txtitemname').val('');
            $('#txtItemCode').val('');
            $('#txtbarcode').val('');
            $('#txtDiscription').val('');
            $('#txtPurchasePrice').val('0');
            $('#txtSalePrice').val('0');
            $("#btnSave").text("Save");
        }



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
											<a href="javascript:;" class="btn btn-sm btn-success" onclick="CheckDuplication();" id="confirm">Confirm (Ctrl + S)</a>
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
			    <div class="col-md-5">
			        <!-- begin panel -->
                    <div class="panel widget light-widget panel-bd-top">
                        <div class="panel-heading bordered">
                    <h3 class="panel-title"> <span class="menu-icon"> <i class="icon-pie"></i> </span> Item Information </h3>
                    <div class="vd_panel-menu">
  <div data-action="minimize" data-original-title="Minimize" data-toggle="tooltip" data-placement="bottom" class=" menu entypo-icon"> <i class="icon-minus3"></i> </div>
  <div data-action="refresh"  data-original-title="Refresh" data-toggle="tooltip" data-placement="bottom" class=" menu entypo-icon smaller-font"> <i class="icon-cycle"></i> </div>
  
  <div data-action="close" data-original-title="Close" data-toggle="tooltip" data-placement="bottom" class=" menu entypo-icon"> <i class="icon-cross"></i> </div>
</div>
<!-- vd_panel-menu --> 
                  </div>
         


                  
                                  
                        <div class=" panel-body-list" style="padding:10px">
                          <div  class="row" style="overflow:visible;">
                                <div class="col-lg-12 table-responsive" style="overflow:visible;">
                                    <table id="tblItems" class="table table-striped table-bordered dataTable no-footer" style="overflow:visible;">
                                        <tr>
                                            <%--<th style="width:5%">Sr</th>--%>
                                            <th style="width:15%">Item</th>                                                                                        
                                            <th style="width:7%">Qty.</th>                                            
                                            <th style="width:8%">Unit Price</th>  
                                             <th style="width:9%">Discount</th>                                                                                 
                                           <%-- <th style="width:8%">Unit Price (RMB)</th>  --%>  
                                           <%-- <th style="width:7%">Packing (RMB)</th>  --%>                                                                                  
                                         <%--   <th style="width:6%">Carry</th>                                            
                                            <th style="width:8%">Extra Charges.</th>                                            
                                            <th style="width:6%">Cost </th> --%>  
                                    <%--        <th style="width:6%">Cost (RMB)</th>   --%>
                                            <th style="width:9%">After Discount</th>
                                            <%--<th style="width:9%">Total Price (RMB)</th>--%>
                                            <%--<th style="width:5%"></th>--%>
                                        </tr>
                                        <tr>
                                            <td><input type="text" id="txtSr1" value="1"  class="form-control"  style="width:100%; border:0px none;"/></td>
                                            <td>                                             
                                             <input type="text" class="span6 typeahead form-control"  value = "2" style="border:0px none; overflow:visible;" id="txtAccCode1"  data-provide="typeahead" style="width:100%" 
                                             data-items="10"/>
                                            </td>                                                            
                                            <td><input type="text" id="txtQty1" class="form-control" value="2" style="width:100%; border:0px none;"/></td>                                            
                                            <td><input type="text" id="txtPrice1" class="form-control" value="1"  style="width:100%; border:0px none;"/></td>
                                         <%--   <td style="display:none;"><input type="text" id="txtRMBPrice1" class="form-control" value="0"    style="width:100%; border:0px none;" onkeyup="tot(this);"/></td>
                                            <td style="display:none;"><input type="text" id="txtRMBPacking1" class="form-control" value="0"    style="width:100%; border:0px none;" onkeyup="tot(this);"/></td>
                                            <td><input type="text" id="txtCarry1" class="form-control" value="0"    style="width:100%; border:0px none;" onkeyup="tot(this);"/></td>
                                            <td><input type="text" id="txtExCharge1" class="form-control" value="0"    style="width:100%; border:0px none;" onkeyup="tot(this);"/></td>
                                            <td><input type="text" id="txtCostPKR1" class="form-control" value="0" disabled="disabled"  style="width:100%; border:0px none;" /></td>--%>
                                            <td style="display:none;"><input type="text" id="txtCostRMB1" class="form-control" value="0"   style="width:100%; border:0px none;" /></td>
                                            <td><input type="text" id="txtTotalPrice1" class="form-control" value=""   style="width:100%; border:0px none;" /></td>
                                            <td style="display:none;"><input type="text" id="txtRMBTotalPrice1" class="form-control" value="0"   style="width:100%; border:0px none;" /></td>
                                            <%-- <td><span class="glyphicon glyphicon-remove" aria-hidden="true" style="font-size: 20px;margin-left: 5px; cursor:pointer; color:Red;"  onmouseover='this.style.color="gray"'  onmouseout='this.style.color="Red"' title="Delete Row" id="txtDelete1" onclick="DeleteRow(this);" ></span></td>                                          --%>
                                        </tr>

                                    </table>
                                </div>
                            </div>
                      <%--  <div class="col-lg-12"><i class="fa fa-2x fa-plus-square" title="Press Ctrl + A for new Row" onclick="addRow();SumDbCr();"></i></div>--%>
                      
                           <div class="col-md-12">

                      </div>
                          <div class="col-md-12" style="padding:10px"></div>
                            <div class="col-md-12">
                  
                      </div>
                          <div class="col-md-12" style="padding:74px"></div>
              
         
                           <div  >         
                               
                     


                                       <div class="row">
                                        <div id="Div2">
                                            <table id="Table2" class="table table-condensed totals" style="margin-bottom:10px;">
                                                <tbody>
                                                    <tr class="info">
                                                        <td width="25%">Total Items</td>
                                                        <td class="text-right" style="padding-right:10px;"><span id="countqty">0</span></td>
                                                        <td width="25%">Total</td>
                                                        <td class="text-right" colspan="2"><span id="countprice">0.00</span></td>
                                                    </tr>
                                                    <tr class="info" style="display:none;">
                                                        <td width="25%"><a href="#" id="a3">Discount</a></td>
                                                        <td class="text-right" style="padding-right:10px;"><span id="Span14">(0.00) 0.00</span></td>
                                                        <td width="25%"><a href="#" id="a4">Order Tax</a></td>
                                                        <td class="text-right"><span id="Span15">7.25</span></td>
                                                    </tr>
                                                          <tr class="danger">
                                                        <td width="25%">CashReceived</td>
                                                        <td class="text-right" style="padding-right:10px;"><span id="Span1">0 (0.00)</span></td>
                                                        <td width="25%">Cash Returned</td>
                                                        <td class="text-right" colspan="2"><span id="Span2">0.00</span></td>
                                                    </tr>
                                                    <tr class="success">
                                                        <td colspan="2" style="font-weight:bold;">
                                                            Total Payable                                                            <a role="button" data-toggle="modal" data-target="#noteModal">
                                                                <i class="fa fa-comment"></i>
                                                            </a>
                                                        </td>
                                                        <td class="text-right" colspan="2" style="font-weight:bold;"><span id="countpriceTotal">152.25</span></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                    <div id="botbuttons" class="col-xs-12 text-center">
                        <div class="col-md-12 col-sm-12 col-xs-12" style="text-align:center;">


                                                       <div class="row">
                                            <div class="col-xs-4" style="padding: 0;">
                                                <div class="btn-group-vertical btn-block">
                                                    <button type="button" onclick="" class="btn btn-warning btn-block btn-flat"  id="suspend">Hold</button>
                                                    <button type="button" class="btn btn-danger btn-block btn-flat" id="reset">Cancel</button>
                                                </div>

                                            </div>
                                            <div class="col-xs-4" style="padding: 0 5px;">
                                                <div class="btn-group-vertical btn-block">
                                                    <button type="button" class="btn bg-purple btn-block btn-flat" id="print_order">Print Order</button>

                                                    <button type="button" class="btn bg-navy btn-block btn-flat" id="print_bill">Print Bill</button>
                                                </div>
                                            </div>
                                            <div class="col-xs-4" style="padding: 0;">
                                                <button type="button" class="btn btn-success btn-block btn-flat" id="payment" style="height:67px;"  data-toggle="modal" data-target="#payModal">Payment</button>
                                            </div>
                                        </div>
                                     <%--   <button type="button" class="btn btn-primary" onclick="Clear();">Discount</button>
                                        <button type="button" class="btn btn-danger" onclick="DeleteData();">Print</button>--%>

                            <asp:HiddenField ID="hdnid" runat="server" />

                        </div>
                      </div>
                      </div>
                    </div></div></div>





                    
                <!-- begin col-6 -->
			    <div class="col-md-7">
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
                        <ul> 
                        <a href=""><img border="0" alt="Fruit" src="../LinkImages\Fruit.Jpg"  width="100" height="80"/></a>
                        <a href=""><img border="0" alt="Pets" src="../LinkImages\pets.Jpg"  width="100" height="80"/></a>
                        <a href=""><img border="0" alt="Vegetables" src="../LinkImages\Vegetables.Jpg"  width="100" height="80"/></a>
                        <a href=""><img border="0" alt="ColdDrinks" src="../LinkImages\ColdDrinks.png"  width="100" height="80"/></a>
                        
                   
                        </ul>
                  <div class="x_content"id="DivRegion" >
                                                                      <input type="text" id="search" placeholder="Type to search" class="search form-control" />

                    <table class="table" id="tablepaging">
                      <thead>
                        <tr>
                             <td></td>
                             <td>ID</td>
                             <td>Item Name</td>
                            <td>Item </td>
                           
                          
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



</asp:Content>

