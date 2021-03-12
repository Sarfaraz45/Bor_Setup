<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Master.master" AutoEventWireup="true" CodeFile="PO_ON_WAY_RCV.aspx.cs" Inherits="PROCUREMENT_PO" %>

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

  <asp:HiddenField ID="hdnItems" runat="server" Value='["Karachi","Hyderabad","USA","Arkansas","California","Colorado","Connecticut","Delaware","Florida","Georgia","Hawaii","Idaho","Illinois","Indiana","Iowa","Kansas","Kentucky"]' />
    <asp:HiddenField ID="hdnAmount" runat="server" Value="0" />
    <asp:HiddenField ID="hdnRemVal" runat="server" Value="0" />
    <asp:HiddenField ID="hdnNarration" runat="server" Value="0" />
     <asp:HiddenField ID="hdnDate" runat="server" Value="0" />
     <asp:HiddenField ID="hdnTotDb" runat="server" Value="0" />
     <asp:HiddenField ID="hdnTotCr" runat="server" Value="0" />
     <asp:HiddenField ID="hdnUnits" runat="server" Value="kg^1~gm^2~ton^3~m^4~cm^5~mm^6" />
     <asp:HiddenField ID="hdnQtyTotal" runat="server" Value="0" />
     <asp:HiddenField ID="hdnQtyTotalPaste" runat="server" Value="0" />
     <asp:HiddenField ID="hdnRMBValue" runat="server" Value="0" />
     <div id="dvUnits" style="display:none"></div>
 <!-- #modal-message -->
<!-- #modal-message -->
<!-- #modal-message -->
<div class="modal fade" id="modal-dialog">
								<div class="modal-dialog">
									<div class="modal-content">
										<div class="modal-header"  style="background-color:White; border-bottom: 1px solid #DDDDDD;">
											<button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
											<h4 class="modal-title" style="font-weight: bold;color: red;">Alert !</h4>
										</div>
										<div class="modal-body" id="modalbody" style="font-weight:bold; font-family:Calibri; font-size:medium;">
											
										</div>
										<div class="modal-footer" style="background-color:White; border-top: 1px solid #DDDDDD;">
											<a href="javascript:;" class="btn btn-md btn-warning" data-dismiss="modal">Close</a>
											<%--<a href="javascript:;" class="btn btn-sm btn-success">Action</a>--%>
										</div>
									</div>
								</div>
							</div>

<!-- #modal-message -->
<!-- #modal-message -->
<!-- #modal-message -->

<div class="modal fade" id="HistoryModal">
								<div class="modal-dialog">
									<div class="modal-content">
										<div class="modal-header"  style="background-color:White; border-bottom: 1px solid #DDDDDD;">
											<button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
											<h4 class="modal-title" style="font-weight: bold;color: red;" id ="SPIDHeading"></h4>
										</div>
										<div class="modal-body" id="DivHistory" style=" font-family:Calibri; font-size:medium;">
											
										</div>
										<div class="modal-footer" style="background-color:White; border-top: 1px solid #DDDDDD;">
											<a href="javascript:;" class="btn btn-md btn-warning" data-dismiss="modal">Close</a>
											<%--<a href="javascript:;" class="btn btn-sm btn-success">Action</a>--%>
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
                    <h3 class="panel-title"> <span class="menu-icon"> <i class="icon-pie"></i> </span> Purchase Order ( RECEIVED FROM ON WAY ) </h3>
                    <div class="vd_panel-menu">
  <div data-action="minimize" data-original-title="Minimize" data-toggle="tooltip" data-placement="bottom" class=" menu entypo-icon"> <i class="icon-minus3"></i> </div>
  <div data-action="refresh"  data-original-title="Refresh" data-toggle="tooltip" data-placement="bottom" class=" menu entypo-icon smaller-font"> <i class="icon-cycle"></i> </div>
  
  <div data-action="close" data-original-title="Close" data-toggle="tooltip" data-placement="bottom" class=" menu entypo-icon"> <i class="icon-cross"></i> </div>
</div>
<!-- vd_panel-menu --> 
                  </div>
                        
                        <div class="panel-body">

                             <div class="row" style="display:none;">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblMsg" runat="server" CssClass="parsley-errors-list" Visible="False"></asp:Label>
                                    <asp:TextBox ID="txtid" runat="server" Visible="False" CssClass="form_inputfeild" placeholder="Enter Name"></asp:TextBox>                
                                </div>
                            </div>
                            <div class="row"><div class="col-lg-12" style="height:10px"></div></div>
                            <div class="row">
                                  <div class="col-lg-12">
                                <div class="form-group">
                                    <div class="col-md-10">
                                <asp:DropDownList ID="ddlPendingOnway" runat="server" DataSourceID="SqlDataSource2"  CssClass="chzn-select form-control"
                                            DataTextField="Title" DataValueField="SPID" >
                                </asp:DropDownList>


                                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:Con %>" 
                                            SelectCommand="SELECT * FROM [VW_PURCHASE_ON_WAY_PENDING]  where  BranchID=@BranchID and BranchID1=@BranchID">                                            
                                            <SelectParameters>
                                                <asp:CookieParameter CookieName="BranchID" Name="BranchID"  Type="String" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </div>
                                    <div class="col-md-2">
                                    <input type="button" class="btn btn-block btn-warning" value="GET DATA" onclick="GetOnWayData();" />
                                    </div>
                                </div>
                            </div>
                            </div>
                            <div class="row"><div class="col-lg-12" style="height:10px"></div></div>

                            <div class="row">
                            
                            <div class="col-lg-12">
                                <div class="form-group">
                                <label class="control-label">ON WAY ID :</label>                                    
                                <input type="text" id="txtONWAYID" placeholder="ON WAY TRANSFER ID" class="form-control" disabled="disabled" />
                                     
                                </div>
                            </div>
                           </div>
                            <div class="row"><div class="col-lg-12" style="height:10px"></div></div>
                            <div class="row">
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <label class="col-md-3 control-label">PO Date</label>
                                     <div class="col-md-9">
                                    <div class="input-group">
                                       <input type="text" class="width-100" id="datepicker-autoClose" placeholder="Select Date" />
                                       <span class="input-group-addon" id="datepicker-icon-trigger-PV" data-datepicker="#datepicker-autoClose"><i class="fa fa-calendar"></i></span>
                                       </div>                                                                       
                                    </div>
                                </div>
                            </div>

                            <div class="col-lg-4">
                                <div class="form-group">
                                    <label class="col-md-3 control-label">PO No.</label>
                                    
                                    <div class="col-md-9">
                                        <input type="text" id="txtVoucherNo" class="form-control" disabled="disabled" />
                                    </div>
                                </div>
                            </div>


                            <div class="col-lg-4">
                                <div class="form-group">
                                    <label class="col-md-3 control-label">Supplier</label>
                                    
                                    <div class="col-md-9">
                                <asp:DropDownList ID="ddlSupplier" runat="server" DataSourceID="SqlDataSource1"  CssClass="form-control"  disabled="disabled"
                                            DataTextField="AccountsTitle" DataValueField="AccountsID">
                                </asp:DropDownList>


                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:Con %>" 
                                            SelectCommand="SELECT AccountsID, AccountsTitle + ' ( ' +   convert(nvarchar(20),Round(Balance,2)) + ' ) ' AS AccountsTitle FROM [VW_ACCOUNTS_NAME_WITH_BALANCE]
                                             where BranchID1=@BranchID">                                            
                                             <SelectParameters>
                                                <asp:CookieParameter CookieName="BranchID" Name="BranchID"  Type="String" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>


                                    </div>
                                </div>
                            </div>

                            
                        </div>
                        <div class="row">
                          <div class="col-lg-4">
                                <div class="form-group">
                                    <label class="col-md-3 control-label">Type</label>
                                    
                                    <div class="col-md-9">
                                <select id="ddlPurchaseType" class="form-control" onchange="GetRate();"  disabled="disabled">
                                <option id="PKR">PKR</option>
                                <option id="RMB">RMB</option>
                                </select>

                                    </div>
                                </div>
                            </div>

                            <div class="col-lg-4">
                                <div class="form-group">
                                    <label class="col-md-3 control-label">Rate</label>
                                    
                                    <div class="col-md-9">
                               <input type="text" id="txtCurrencyRate" value="0" class="form-control" onkeydown="SetRate();" onkeyup="SetRate();"  disabled="disabled" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <label class="col-md-3 control-label">Packing Rate</label>
                                    
                                    <div class="col-md-9">
                               <input type="text" id="txtPackingRate" value="0" class="form-control"  disabled="disabled" />
                                    </div>
                                </div>
                            </div>
                             
                        </div>

                        <div class="row">
                          <div class="col-lg-4">
                                <div class="form-group">
                                    <label class="col-md-3 control-label">Packing Account</label>
                                    
                                    <div class="col-md-9">
                                <asp:DropDownList ID="ddlPackingAccount" runat="server" DataSourceID="SqlDataSource1"  CssClass="form-control"
                                            DataTextField="AccountsTitle" DataValueField="AccountsID"  disabled="disabled">
                                </asp:DropDownList>


                                    </div>
                                </div>
                            </div>

                            <div class="col-lg-4">
                                <div class="form-group">
                                    <label class="col-md-3 control-label">Carry Account</label>
                                    
                                    <div class="col-md-9">
                               <asp:DropDownList ID="ddlCarryAccount" runat="server" DataSourceID="SqlDataSource1"  CssClass="chzn-select form-control"
                                            DataTextField="AccountsTitle" DataValueField="AccountsID">
                                </asp:DropDownList>

                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <label class="col-md-3 control-label">Ex. Charges Account</label>
                                    
                                    <div class="col-md-9">
                               <asp:DropDownList ID="ddlExChargesAccount" runat="server" DataSourceID="SqlDataSource1"  CssClass="chzn-select form-control"
                                            DataTextField="AccountsTitle" DataValueField="AccountsID">
                                </asp:DropDownList>

                                    </div>
                                </div>
                            </div>
                             
                        </div>

                          <div class="row">
                           <div class="col-lg-4">
                                <div class="form-group">
                                    <label class="col-md-3 control-label">Container Number</label>
                                    
                                    <div class="col-md-9">

                                    <input type="text" id="txtContainerNo" value="0" class="form-control" disabled="disabled" />                               

                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="form-group">
                                <input type="button" class="btn btn-block btn-danger" value="VIEW HISTORY" onclick="GetOnWayHistory();" />
                                
                                </div>
                            </div>
                         </div>
                        <div class="row">
                                 <div class="col-lg-12">
                                <div class="form-group">
                                    
                                    
                                    <div class="col-md-12">
                               <input type="text" id="txtRemarks" placeholder="Enter Remarks" disabled="disabled" class="form-control" />

                                    </div>
                                </div>
                            </div>
                        </div>
                       
                         <div class="row"><div class="col-lg-12" style="height:10px"></div></div>
                       
                            <div class="row" style="overflow:visible;">
                                <div class="col-lg-12 table-responsive" style="overflow:visible;">
                                <div id="DivMaterial1">
                                    <table id="tblItems" class="table table-striped table-bordered dataTable no-footer" style="overflow:visible;">
                                        


                                    </table>
                                    </div>

                                </div>
                            </div>
                       
                            <div class="col-lg-12" style="display:none;"><i class="fa fa-2x fa-plus-square" title="Press Ctrl + A for new Row" onclick="addRow();SumDbCr();"></i></div>

                            <div class="row" style="height:20px;"></div>
                                <label class="col-md-4 control-label" >Carry Amount PKR</label>
                                <label class="col-md-4 control-label" >Packing Amount PKR</label>
                                <label class="col-md-4 control-label" >Ex. Charges Amount PKR</label>
                                
                                
                                <div class="col-lg-4"><input type="text" id="txtCarryPKR" class="form-control" disabled="disabled" style="width:100%;" value="0" /></div>
                                <div class="col-lg-4"><input type="text" id="txtPackingPKR" class="form-control" disabled="disabled" style="width:100%;"  value="0" /></div>
                                <div class="col-lg-4"><input type="text" id="txtExChrgPKR" class="form-control" disabled="disabled" style="width:100%;"  value="0" /></div>
                                
                            <div class="col-md-12" style="height:20px;"></div>

                            <label class="col-md-4 control-label" >Carry Amount RMB</label>
                                <label class="col-md-4 control-label" >Packing Amount RMB</label>
                                <label class="col-md-4 control-label" >Ex. Charges Amount RMB</label>
                                
                                
                                <div class="col-lg-4"><input type="text" id="txtCarryRMB" class="form-control" disabled="disabled" style="width:100%;"  value="0" /></div>
                                <div class="col-lg-4"><input type="text" id="txtPackingRMB" class="form-control" disabled="disabled" style="width:100%;"  value="0" /></div>
                                <div class="col-lg-4"><input type="text" id="txtExChrgRMB" class="form-control" disabled="disabled" style="width:100%;"  value="0" /></div>
                                
                            <div class="col-md-12" style="height:20px;"></div>

                            <%--<label class="col-md-2 control-label" >Discount Rate</label>--%>
                                <label class="col-md-3 control-label" >Discount Amount PKR</label>
                                <label class="col-md-3 control-label" >Discount Amount RMB</label>
                                <label class="col-md-3 control-label" >Supplier Amount PKR</label>
                                <label class="col-md-3 control-label" >Supplier Amount RMB</label>
                                <%--<div class="col-lg-2"><input type="text" id="txtDiscountRate" value="0" class="form-control"  style="width:100%;" onkeypress="GetDiscountRMB();" onkeydown="GetDiscountRMB();" onkeyup="GetDiscountRMB();" /></div>--%>
                                <div class="col-lg-3"><input type="text" id="txtDiscountPKR" value="0" class="form-control"  style="width:100%;"  disabled="disabled"  onkeypress="GetDiscountRMB();" onkeydown="GetDiscountRMB();" onkeyup="GetDiscountRMB();" /></div>
                                <div class="col-lg-3"><input type="text" id="txtDiscountRMB" value="0" class="form-control" disabled="disabled" style="width:100%;" /></div>
                                <div class="col-lg-3"><input type="text" id="txtSupplierPKR" class="form-control" disabled="disabled" style="width:100%;"  value="0"  /></div>
                                <div class="col-lg-3"><input type="text" id="txtSupplierRMB" class="form-control" disabled="disabled" style="width:100%;"  value="0"  /></div>
                            <div class="col-md-12" style="height:20px;"></div>
                            
                                <label class="col-md-9 control-label" style="text-align:right">Total Amount PKR</label>
                                <div class="col-lg-3"><input type="text" id="txtTotAmount" class="form-control" disabled="disabled" style="width:100%;"  value="0" /></div>
                                <div class="col-md-12" style="height:20px;"></div>
                                <label class="col-md-9 control-label" style="text-align:right">Total Amount RMB</label>
                                <div class="col-lg-3"><input type="text" id="txtTotAmountRMB" class="form-control" disabled="disabled" style="width:100%;"  value="0" /></div>
                            <div class="col-md-12" style="height:20px;"></div>
                            <div class="row" style="display:none;">
                                <label class="col-md-10 control-label" style="text-align:right">Discount</label>
                                <div class="col-lg-2"><input type="text" id="txtDiscount" onkeyup="totPaste();" value="0" class="form-control" style="width:100%;" value="0" /></div>
                            </div>

                            <div class="row" style="display:none;">
                                <label class="col-md-10 control-label" style="text-align:right">Tax Rate</label>
                                <div class="col-lg-2"><input type="text" id="txtTaxRate" value="0" onkeyup="totPaste();" class="form-control" style="width:100%;" value="0" /></div>
                            </div>

                            <div class="row" style="display:none;">
                                <label class="col-md-10 control-label" style="text-align:right">Total Tax</label>
                                <div class="col-lg-2"><input type="text" id="txtTotTax" disabled="disabled" class="form-control" style="width:100%;" value="0" /></div>
                            </div>

                            <div class="row" style="display:none;">
                                <label class="col-md-10 control-label" style="text-align:right">Grand Total</label>
                                <div class="col-lg-2"><input type="text" id="txtGrandTotal" disabled="disabled" class="form-control" style="width:100%;" value="0" /></div>
                            </div>

                            <div class="row" style="display:none;" >
                                <div class="col-lg-12 table-responsive">
                                    <table id="Table1" style="width:100%">
                                        <tr>
                                            <td style="width:5%"></td>
                                            <td style="width:25%"></td>                                            
                                            <td style="width:8%"><span id="spnTotDb" style="font-weight:bold">0</span></td>
                                            <td style="width:8%"><span id="spnTotCr" style="font-weight:bold">0</span></td>
                                            <td style="width:8%"></td>
                                            <td style="width:10%"></td>
                                            <td style="width:25%"></td>
                                            <td style="width:11%"></td>
                                        </tr>
                                        </table>
                                </div>

                                
                            </div>

                          
                             <div class="row"><div class="col-lg-12" >
                            <%-- <input type="button" id="btnClear" class="btn btn-danger btn-sm" value="Clear Form" onclick="ClearForm();" />--%>
                             <input type="button" id="btnSave" style="font-weight:bold; font-size:20px;" class="btn btn-info btn-block" value="P R O C E S S &nbsp&nbsp&nbsp P U R C H A S E  &nbsp&nbsp&nbsp O R D E R" onclick="SaveTransaction();" />
                             </div></div>
                             <%--<div class="col-lg-3" style="text-align:right;"><h2><b id="lblTotalPasteQty">Total Paste Qty : 0</b></h2>--%>
                        </div>
                   </div>
                   </div>
                   </div>

        <script src="../js/jquery-1.9.1.min.js"></script>
		<script src="../js/bootstrap.min.js"></script>
<script type="text/javascript">

    GetRate();
    GetRMB();
    document.getElementById("datepicker-autoClose").focus();
    document.getElementById("datepicker-autoClose").select();

    function SetRate() {
        document.getElementById("ContentPlaceHolder1_hdnRMBValue").value = document.getElementById("txtCurrencyRate").value;
        //alert(document.getElementById("ContentPlaceHolder1_hdnRMBValue").value);
    }


    function GetOnWayHistory() {
        document.getElementById("SPIDHeading").innerHTML = "";
        var BranchID = localStorage.getItem("BranchID");
        var ddl = document.getElementById("ContentPlaceHolder1_ddlPendingOnway").value;
        var dataToSend = JSON.stringify({ 'Product': ddl, 'BranchID': BranchID });
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "PO_ON_WAY_RCV.aspx/LoadOnwayHistory",
            dataType: "json",
            //data: "{}",
            data: dataToSend,
            async: false,
            success: function (data) {
                var jsdata = JSON.parse(data.d);
                var ro = "";
                if ($('#tblHistory').length != 0) {

                    $('#tblHistory').remove();
                }
                ro = "<table id='tblHistory' class='table table-striped table-bordered dataTable no-footer'  style='overflow:visible;'><tr><th>Remarks</th></tr>";
                $.each(jsdata, function (key, value) {
                    ro += "<tr><td><input type='text' value='" + value.Remarks + "' disabled='disabled' class='form-control'></td></tr>";
                    document.getElementById("SPIDHeading").innerHTML = "ON WAY ID : " + ddl;
                });


                ro = ro + "</table>";

                $("#DivHistory").append(ro);
                $("#HistoryModal").modal();
            },
            error: function (result) {
                alert(result);
            }


        });

    }

    function GetOnWayData() {
        var ddl = document.getElementById("ContentPlaceHolder1_ddlPendingOnway").value;
        var BranchID = localStorage.getItem("BranchID");
        var dataToSend = JSON.stringify({ 'Product': ddl, 'BranchID': BranchID });
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "PO_ON_WAY_RCV.aspx/LoadOnway",
            dataType: "json",
            //data: "{}",
            data: dataToSend,
            async: false,
            success: function (data) {
                var jsdata = JSON.parse(data.d);
                var ro = "";
                if ($('#tblItems').length != 0) {

                    $('#tblItems').remove();
                }
                var cc = 0;
                var ccqty = 0;                              
                ro = "<table id='tblItems' class='table table-striped table-bordered dataTable no-footer'  style='overflow:visible;'><tr><th style='width:5%'>Sr1.</th><th style='width:20%'>Item</th><th style='width:7%'>Qty.</th><th style='width:8%'>Unit Price (PKR)</th><th style='width:8%'>Unit Price (RMB)</th><th style='width:7%'>Packing (RMB)</th>                                                                                    <th style='width:6%'>Carry</th>                                            <th style='width:8%'>Extra Charges.</th>                                            <th style='width:6%'>Cost (PKR)</th>   <th style='width:6%'>Cost (RMB)</th>   <th style='width:9%'>Total Price (PKR)</th><th style='width:9%'>Total Price (RMB)</th></tr>";
                
                $.each(jsdata, function (key, value) {
                    cc = cc + 1;
                    ccqty = ccqty + parseFloat(value.Qty);
                    document.getElementById("ContentPlaceHolder1_ddlSupplier").value = value.Supplier;
                    document.getElementById("ddlPurchaseType").value = value.CurrenctyType;
                    document.getElementById("txtCurrencyRate").value = value.Rate;
                    document.getElementById("txtPackingRate").value = value.PackingRate;
                    document.getElementById("ContentPlaceHolder1_ddlPackingAccount").value = value.PackingAccount;
                    document.getElementById("txtPackingPKR").value = value.PackingAmountPKR;
                    document.getElementById("txtPackingRMB").value = value.PackingAmountRMB;
                    document.getElementById("txtDiscountPKR").value = value.DiscountAmountPKR;
                    document.getElementById("txtDiscountRMB").value = value.DiscountAmountRMB;
                    document.getElementById("txtSupplierPKR").value = value.SupplierAmountPKR;
                    document.getElementById("txtSupplierRMB").value = value.SupplierAmountRMB;
                    document.getElementById("txtTotAmount").value = value.TotalAmountPKR;
                    document.getElementById("txtTotAmountRMB").value = value.TotalAmountRMB;
                    document.getElementById("txtRemarks").value = value.Remarks;
                    document.getElementById("txtContainerNo").value = value.Container;
                    ro += "<tr><td><input type='text' id='txtSr" + cc + "' value='" + cc + "' disabled='disabled' class='form-control' style='width:100%; border:0px none;'></td><td><input type='text' class='span6 typeahead form-control' onblur='javascript:fillUNITS(this);'  disabled='disabled' style='border: 0px none; background: rgb(242, 245, 247);' id='txtAccCode" + cc + "' value='" + value.ItemID + "' </td><td><input type='text' id='txtQty" + cc + "' class='form-control' value='" + value.Qty + "' disabled='disabled'  style='width:100%; border:0px none;' onkeyup='tot(this);'></td><td><input type='text' id='txtPrice" + cc + "' class='form-control'  disabled='disabled' value='" + value.UnitPricePKR + "'  style='width:100%; border:0px none;' onkeyup='tot(this);'/></td><td><input type='text' disabled='disabled'  id='txtRMBPrice" + cc + "' class='form-control' value='" + value.UnitPriceRMB + "'    style='width:100%; border:0px none;' onkeyup='tot(this);'/></td><td><input type='text'  disabled='disabled' id='txtRMBPacking" + cc + "' class='form-control' value='" + value.PackingRMB + "'    style='width:100%; border:0px none;' onkeyup='tot(this);'/></td><td><input type='text' id='txtCarry" + cc + "' class='form-control' value='0'    style='width:100%; border:0px none;' onkeyup='tot(this);'/></td><td><input type='text' id='txtExCharge" + cc + "' class='form-control' value='0'    style='width:100%; border:0px none;' onkeyup='tot(this);'/></td><td><input type='text' id='txtCostPKR" + cc + "' class='form-control' value='" + value.CostPKR + "' disabled='disabled'  style='width:100%; border:0px none;' /></td><td><input type='text' id='txtCostRMB" + cc + "' class='form-control' value='" + value.CostRMB + "' disabled='disabled'  style='width:100%; border:0px none;' /></td><td><input type='text' id='txtTotalPrice" + cc + "' class='form-control' value='" + value.TotalPricePKR + "' disabled='disabled'  style='width:100%; border:0px none;' /></td><td><input type='text' id='txtRMBTotalPrice" + cc + "' class='form-control' value='" + value.TOtalPriceRMB + "' disabled='disabled'  style='width:100%; border:0px none;' /></td>";
                    document.getElementById("txtONWAYID").value = document.getElementById("ContentPlaceHolder1_ddlPendingOnway").value;

                    
                  

                });

                
                ro = ro + "</table>";

                $("#DivMaterial1").append(ro);

            },
            error: function (result) {
                alert(result);
            }


        });

    }

    function GetRate() {
        var ddltype = document.getElementById("ddlPurchaseType").value;
        var BranchID = localStorage.getItem("BranchID");
        $.ajax({
            type: "POST",
            url: "PO_ON_WAY_RCV.aspx/LoadType",
            //data: {},
            data: '{ "ddltype" : "' + ddltype + '","BranchID": "' + BranchID + '" }',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                var jsdata = JSON.parse(data.d);
                $.each(jsdata, function (key, value) {
                    //alert(value.QualificationID);
                    //document.getElementById("ContentPlaceHolder1_hdnRMBValue").value = value.UnitTypeID;
                    //document.getElementById("txtRMBValue1").value = value.UnitTypeID;
                    document.getElementById("txtCurrencyRate").value = value.UnitTypeID;                    
                });


            },
            error: function (data) {
                alert("error found");
            }
        });
    }

    function GetRMB() {
        var BranchID = localStorage.getItem("BranchID");
        var dataToSend = JSON.stringify({ 'BranchID': BranchID });
        $.ajax({
            type: "POST",
            url: "PO_ON_WAY_RCV.aspx/LoadRMbValue",
            data: dataToSend,            
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                var jsdata = JSON.parse(data.d);
                $.each(jsdata, function (key, value) {
                    document.getElementById("ContentPlaceHolder1_hdnRMBValue").value = value.UnitTypeID;
                });


            },
            error: function (data) {
                alert("error found");
            }
        });
    }


    function totPaste() {
        var tot = 0;
        var totRMB = 0;
        var elm = {};
        var elms = document.getElementById('tblItems').getElementsByTagName("input");
        for (var i = 0; i < elms.length; i++) {
            if (elms[i].id.indexOf('txtTotalPrice') != -1) {

                elm = elms[i];
                var iD = elm.id;
                var txtTotalPrice = iD;
                var txtTotalPriceRMBID = iD.replace('txtTotalPrice', 'txtRMBTotalPrice');
                var txtTotalPriceRMB = txtTotalPriceRMBID;
                var totp = document.getElementById(txtTotalPrice).value;
                var totpRMB = document.getElementById(txtTotalPriceRMB).value;

                tot = parseFloat(tot) + parseFloat(totp); //500 200-500=300
                totRMB = parseFloat(totRMB) + parseFloat(totpRMB); //500 200-500=300
            }
        }


        document.getElementById("txtTotAmount").value = tot;
        document.getElementById("txtTotAmountRMB").value = totRMB;
        //document.getElementById('lblTotalPasteQty').innerHTML = "Total Paste Qty : " + p;
        var txtDiscount = document.getElementById("txtDiscount").value;
        var txtTaxRate = document.getElementById("txtTaxRate").value;
        if (txtDiscount == "" || txtDiscount == null) { txtDiscount = 0; }
        if (txtTaxRate == "" || txtTaxRate == null) { txtTaxRate = 0; }

        var totAmount = parseFloat(tot) - parseFloat(txtDiscount);
        var totTax = parseFloat(totAmount) * parseFloat(txtTaxRate) / 100;
        document.getElementById("txtTotTax").value = totTax;
        document.getElementById("txtGrandTotal").value = parseFloat(totAmount) + parseFloat(totTax);



    }


    function totCarry() {
        var tot = 0;
        var totRMB = 0;
        var elm = {};
        var elms = document.getElementById('tblItems').getElementsByTagName("input");
        for (var i = 0; i < elms.length; i++) {
            if (elms[i].id.indexOf('txtCarry') != -1) {

                elm = elms[i];
                var iD = elm.id;
                var txtTotalPrice = iD;
                var txtTotQtyID = iD.replace('txtCarry', 'txtQty');
                var totp = document.getElementById(txtTotalPrice).value;
                var txtTotQty = document.getElementById(txtTotQtyID).value;
                if (totp == "" || totp == null) { totp = 0; }
                if (txtTotQty == "" || txtTotQty == null) { txtTotQty = 0; }
                totp = parseFloat(totp) * parseFloat(txtTotQty);
                tot = parseFloat(tot) + parseFloat(totp); //500 200-500=300
              
            }
        }

        totRMB = parseFloat(tot) / parseFloat(document.getElementById("ContentPlaceHolder1_hdnRMBValue").value);

        document.getElementById("txtCarryPKR").value = tot;
        document.getElementById("txtCarryRMB").value = totRMB;


    }
    function totPacking() {
        var tot = 0;
        var totRMB = 0;
        var elm = {};
        var elms = document.getElementById('tblItems').getElementsByTagName("input");
        for (var i = 0; i < elms.length; i++) {
            if (elms[i].id.indexOf('txtRMBPacking') != -1) {

                elm = elms[i];
                var iD = elm.id;
                var txtTotalPrice = iD;
                var txtTotQtyID = iD.replace('txtRMBPacking', 'txtQty');
                var totp = document.getElementById(txtTotalPrice).value;
                var txtTotQty = document.getElementById(txtTotQtyID).value;
                if (totp == "" || totp == null) { totp = 0; }
                if (txtTotQty == "" || txtTotQty == null) { txtTotQty = 0; }
                totp = parseFloat(totp) * parseFloat(txtTotQty);
                totRMB = parseFloat(totRMB) + parseFloat(totp); //500 200-500=300

            }
        }

        tot = parseFloat(totRMB) * parseFloat(document.getElementById("txtPackingRate").value);

        document.getElementById("txtPackingPKR").value = tot;
        document.getElementById("txtPackingRMB").value = totRMB;


    }
    function totExCharge() {
        var tot = 0;
        var totRMB = 0;
        var elm = {};
        var elms = document.getElementById('tblItems').getElementsByTagName("input");
        for (var i = 0; i < elms.length; i++) {
            if (elms[i].id.indexOf('txtExCharge') != -1) {

                elm = elms[i];
                var iD = elm.id;
                var txtTotalPrice = iD;
                var txtTotQtyID = iD.replace('txtExCharge', 'txtQty');
                var txtTotQty = document.getElementById(txtTotQtyID).value;
                var totp = document.getElementById(txtTotalPrice).value;
                if (totp == "" || totp == null) { totp = 0; }
                if (txtTotQty == "" || txtTotQty == null) { txtTotQty = 0; }
                totp = parseFloat(totp) * parseFloat(txtTotQty);
                tot = parseFloat(tot) + parseFloat(totp); //500 200-500=300


            }
        }

        totRMB = parseFloat(tot) / parseFloat(document.getElementById("ContentPlaceHolder1_hdnRMBValue").value);

        document.getElementById("txtExChrgPKR").value = tot;
        document.getElementById("txtExChrgRMB").value = totRMB;


    }

    function totdiscount() {
        document.getElementById("txtSupplierPKR").value = parseFloat(document.getElementById("txtTotAmount").value) - parseFloat(document.getElementById("txtDiscountPKR").value) - parseFloat(document.getElementById("txtCarryPKR").value) - parseFloat(document.getElementById("txtPackingPKR").value) - parseFloat(document.getElementById("txtExChrgPKR").value);
        document.getElementById("txtSupplierRMB").value = parseFloat(document.getElementById("txtTotAmountRMB").value) - parseFloat(document.getElementById("txtDiscountRMB").value) - parseFloat(document.getElementById("txtCarryRMB").value) - parseFloat(document.getElementById("txtPackingRMB").value) - parseFloat(document.getElementById("txtExChrgRMB").value);
    }

    function GetDiscountRMB() {
    //    if (parseFloat(document.getElementById("txtDiscountRate").value) > 0) {
      //      document.getElementById("txtDiscountPKR").value = (parseFloat(document.getElementById("txtSupplierPKR").value) / 100) * parseFloat(document.getElementById("txtDiscountRate").value);
       // }
        document.getElementById("txtDiscountRMB").value = parseFloat(document.getElementById("txtDiscountPKR").value) / parseFloat(document.getElementById("ContentPlaceHolder1_hdnRMBValue").value);
        //tot();
        //totPaste();
        totdiscount();
    }

//    function DiscountRate() {
//        
//        totdiscount();
//        GetDiscountRMB();
//        }

    LOAD_ITEMS();

    function ValidateRow() {
        var accList = document.getElementById('ContentPlaceHolder1_hdnItems').value;
        accList = accList.replace("[", "");
        accList = accList.replace("]", "");
        accList = accList.replace(/"/g, '');

        var accArray = accList.split(',');

        var cc = 0; var ccDbCR = 0; var ccDate = 0;
        var elm = {};
        var elms = document.getElementById('tblItems').getElementsByTagName("input");
        for (var i = 0; i < elms.length; i++) {
            if (elms[i].id.indexOf('txtAccCode') != -1) {

                elm = elms[i];
                var iD = elm.id;
                var txtAccCodeID = iD;
                var txtAccCode = document.getElementById(txtAccCodeID).value;

                

                ////////////////////////////ACCOUNT LIST Checking
                if (accArray.indexOf(txtAccCode) == -1) {

                    document.getElementById(txtAccCodeID).style.background = 'lavender';
                    // txtAccCodeID.style.background = 'red';background:red
                    cc = 1;
                }
                else {
                    document.getElementById(txtAccCodeID).style.background = 'white';
                }


            }
        }

        var msg = cc;//  + '|' + ccDbCR + '|' + ccDate; //1+2+3
        return msg;

    }

    function addRow() {

        /////////////////////// VALIDATION START
        var cc = ValidateRow();
        if (cc == '1') {
            document.getElementById("modalbody").innerHTML = "Item Does not exist in Highlighted Rows!"
            $("#modal-dialog").modal();
            //alert("Item Does not exist in Highlighted Rows! ");
            return false;            
        }
        /////////////////////// VALIDATION END

        //SumDbCr();
        var table = document.getElementById('tblItems');

        var rowCount = table.rows.length;
        var row = table.insertRow(rowCount);
        var newID = parseFloat(rowCount);

        var cell1 = row.insertCell(0);
        var txtSr = document.createElement("input");
        txtSr.type = "text";
        txtSr.id = "txtSr" + newID;
        txtSr.value = newID;
        txtSr.className = 'form-control';
        txtSr.style.width = '100%';
        txtSr.style.border = '0px none';
        txtSr.disabled = true;
        cell1.appendChild(txtSr);

        var cell2 = row.insertCell(1);
        var txtAccCode = document.createElement("input");
        txtAccCode.type = "text";
        txtAccCode.id = "txtAccCode" + newID;
        txtAccCode.className = 'span6 typeahead form-control'; //data-source
        txtAccCode.style.width = '100%';
        txtAccCode.style.border = '0px none';
        txtAccCode.dataset.provide = "typeahead";
        txtAccCode.dataset.source = document.getElementById('ContentPlaceHolder1_hdnItems').value;

        txtAccCode.onblur = function () { // Note this is a function
            fillUNITS(this)
        };        

        cell2.appendChild(txtAccCode);

        document.getElementById("txtAccCode" + newID).focus();
        document.getElementById("txtAccCode" + newID).select();


        
//        var cell3 = row.insertCell(2); //txtDesc
//        var ddlUnit = document.createElement("select");
//        ddlUnit.id = "ddlUnit" + newID;
//        ddlUnit.className = 'form-control';
//        ddlUnit.style.width = '100%';
//        ddlUnit.style.border = '0px none';
//        cell3.appendChild(ddlUnit);







        var cell4 = row.insertCell(2); //txtQty
        var txtQty = document.createElement("input");
        txtQty.type = "text";
        txtQty.id = "txtQty" + newID;
        txtQty.className = 'form-control';
        txtQty.style.width = '100%';
        txtQty.style.border = '0px none';
        txtQty.value = '1';
        txtQty.onkeyup = function () { // Note this is a function
            tot(this)
        };
        cell4.appendChild(txtQty); //SumDbCr();



//        var cell5 = row.insertCell(3); //RMB Value
//        var txtRMBValue = document.createElement("input");
//        txtRMBValue.type = "text";
//        txtRMBValue.id = "txtRMBValue" + newID;
//        txtRMBValue.className = 'form-control';
//        txtRMBValue.style.width = '100%';
//        txtRMBValue.style.border = '0px none';

//        txtRMBValue.value = document.getElementById("ContentPlaceHolder1_hdnRMBValue").value;
//        txtRMBValue.disabled = "disabled";
//        cell5.appendChild(txtRMBValue); //SumDbCr();


        var cell6 = row.insertCell(3); //txtPrice
        var txtPrice = document.createElement("input");
        txtPrice.type = "text";
        txtPrice.id = "txtPrice" + newID;
        txtPrice.className = 'form-control';
        txtPrice.style.width = '100%';
        txtPrice.style.border = '0px none';

        txtPrice.value = '0';
        txtPrice.onkeyup = function () { // Note this is a function
            tot(this)
        };
        cell6.appendChild(txtPrice); //SumDbCr();


        var cell7 = row.insertCell(4); //txtTotalPrice
        var txtRMBPrice = document.createElement("input");
        txtRMBPrice.type = "text";
        txtRMBPrice.id = "txtRMBPrice" + newID;
        txtRMBPrice.className = 'form-control';
        txtRMBPrice.style.width = '100%';
        txtRMBPrice.style.border = '0px none';
        txtRMBPrice.value = '0';
        txtRMBPrice.onkeyup = function () { // Note this is a function
            tot(this)
        };
        //txtRMBPrice.disabled = "disabled";
        cell7.appendChild(txtRMBPrice);

        var cell7Pack = row.insertCell(5); //txtRMBPackingPrice
        var txtRMBPackingPrice = document.createElement("input");
        txtRMBPackingPrice.type = "text";
        txtRMBPackingPrice.id = "txtRMBPacking" + newID;
        txtRMBPackingPrice.className = 'form-control';
        txtRMBPackingPrice.style.width = '100%';
        txtRMBPackingPrice.style.border = '0px none';
        txtRMBPackingPrice.value = '0';
        txtRMBPackingPrice.onkeyup = function () { // Note this is a function
            tot(this)
        };
        //txtRMBPrice.disabled = "disabled";
        cell7Pack.appendChild(txtRMBPackingPrice);



        var cell77 = row.insertCell(6); //txtCarry
        var txtCarry = document.createElement("input");
        txtCarry.type = "text";
        txtCarry.id = "txtCarry" + newID;
        txtCarry.className = 'form-control';
        txtCarry.style.width = '100%';
        txtCarry.style.border = '0px none';
        txtCarry.value = '0';
        txtCarry.onkeyup = function () { // Note this is a function
            tot(this)
        };
        cell77.appendChild(txtCarry);


        var cell87 = row.insertCell(7); //txtExCharge
        var txtExCharge = document.createElement("input");
        txtExCharge.type = "text";
        txtExCharge.id = "txtExCharge" + newID;
        txtExCharge.className = 'form-control';
        txtExCharge.style.width = '100%';
        txtExCharge.style.border = '0px none';
        txtExCharge.value = '0';
        txtExCharge.onkeyup = function () { // Note this is a function
            tot(this)
        };
        cell87.appendChild(txtExCharge);


        var cell8pkrcost = row.insertCell(8); //txtCostPKR1
        var txtCostPKR1 = document.createElement("input");
        txtCostPKR1.type = "text";
        txtCostPKR1.id = "txtCostPKR" + newID;
        txtCostPKR1.className = 'form-control';
        txtCostPKR1.style.width = '100%';
        txtCostPKR1.style.border = '0px none';
        txtCostPKR1.value = '0';
        txtCostPKR1.disabled = "disabled";
        cell8pkrcost.appendChild(txtCostPKR1);


        var cell8rmbcost = row.insertCell(9); //txtCostRMB1
        var txtCostRMB1 = document.createElement("input");
        txtCostRMB1.type = "text";
        txtCostRMB1.id = "txtCostRMB" + newID;
        txtCostRMB1.className = 'form-control';
        txtCostRMB1.style.width = '100%';
        txtCostRMB1.style.border = '0px none';
        txtCostRMB1.value = '0';
        txtCostRMB1.disabled = "disabled";
        cell8rmbcost.appendChild(txtCostRMB1);


        var cell8 = row.insertCell(10); //txtTotalPrice
        var txtTotalPrice = document.createElement("input");
        txtTotalPrice.type = "text";
        txtTotalPrice.id = "txtTotalPrice" + newID;
        txtTotalPrice.className = 'form-control';
        txtTotalPrice.style.width = '100%';
        txtTotalPrice.style.border = '0px none';
        txtTotalPrice.value = '0';
        txtTotalPrice.disabled = "disabled";
        cell8.appendChild(txtTotalPrice);


        var cell9 = row.insertCell(11); //txtTotalPrice
        var txtRMBTotalPrice = document.createElement("input");
        txtRMBTotalPrice.type = "text";
        txtRMBTotalPrice.id = "txtRMBTotalPrice" + newID;
        txtRMBTotalPrice.className = 'form-control';
        txtRMBTotalPrice.style.width = '100%';
        txtRMBTotalPrice.style.border = '0px none';
        txtRMBTotalPrice.value = '0';
        txtRMBTotalPrice.disabled = "disabled";
        cell9.appendChild(txtRMBTotalPrice);


        var cell100 = row.insertCell(12); //Delete
        var txtDelete = document.createElement("span");
        txtDelete.type = "span";
        txtDelete.id = "txtDelete" + newID;
        txtDelete.className = 'glyphicon glyphicon-remove';
        txtDelete.setAttribute("style", "font-size:20px; margin-left:5px;");
        txtDelete.style.cursor = 'pointer';
        txtDelete.style.color = 'red';
        txtDelete.title = 'Delete Row';

        txtDelete.onmouseover = function () {
            txtDelete.setAttribute("style", "this.style.color='gray'")
            txtDelete.setAttribute("style", "font-size:20px; margin-left:5px;");
            txtDelete.style.cursor = 'pointer';
            txtDelete.title = 'Delete Row';
        };
        txtDelete.onmouseout = function () {
            txtDelete.setAttribute("style", "font-size:20px; margin-left:5px;color:red;");
        };

        txtDelete.onclick = function () {
            DeleteRow(txtDelete)
        };


        cell100.appendChild(txtDelete);

        

    }

    function fillUNITS(a){
        var txtAccCodeID = a.id;
        var ddlUnitID = txtAccCodeID.replace('txtAccCode', 'ddlUnit');

        var txtAccCode = document.getElementById(txtAccCodeID).value;
        var splt = txtAccCode.split('^');
        var accID = splt[0];
        var itmID = accID.replace(/\s/g, '');

        var spnID = 'spnUNT' + itmID;


        var ddlUnit = document.getElementById(ddlUnitID);

        var pp = document.getElementById(spnID).innerHTML;
        //alert(pp);
        var pList = pp.split('~');
        for (var i = 0; i < pList.length; i++) {
            var pItems = pList[i].split('^');
            ddlUnit.appendChild(new Option(pItems[1], pItems[0]));
        }

    }

    function LOAD_ITEMS() {
        var BranchID = localStorage.getItem("BranchID");
        var tblRows = "";
        var UserID = "1";
        $.ajax({
            type: 'POST',
            url: 'PO_ON_WAY_RCV.aspx/LOAD_ITEMS',
            data: '{ "UserID" : "' + UserID + '" , "BranchID" : "' + BranchID + '" }',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (msg) {
                var s = msg.d.split('`');
                tblRows = s[0];
                document.getElementById('txtVoucherNo').value = s[1];
                var b = tblRows.replace(/'/g, '"');
                document.getElementById('ContentPlaceHolder1_hdnItems').value = b;
                txtAccCode = document.getElementById('txtAccCode1');
                txtAccCode.dataset.source = document.getElementById('ContentPlaceHolder1_hdnItems').value;
                document.getElementById('ContentPlaceHolder1_hdnDate').value = s[2];
                document.getElementById('datepicker-autoClose').value = s[2];
                document.getElementById('dvUnits').innerHTML = s[3];

            }

        });
    }
    function DeleteRow(a) {
        var x = document.getElementById("tblItems").rows.length;        
        if (x <= 2) {
            document.getElementById("modalbody").innerHTML = "Sorry you can not delete last record. Thanks!"
            $("#modal-dialog").modal();
            return false;
        }
        else {
            var DeleteID = a.id;
            var abcc = DeleteID.substring(9);
            if (abcc == '1') {
                document.getElementById("modalbody").innerHTML = "Sorry you can not delete first row.... Thanks!"
                $("#modal-dialog").modal();
                return false;
            }
            else {
                $(a).closest('tr').remove();
                tot(a);
                //totPaste();
            }
        }
    }

    function tot(a) {
        var QtyID = ''; //txtQty1
        var PriceID = ''; //txtPrice1
        var totPriceID = ''; //txtTotalPrice1
        //var RMBValueID = ;
        var RMBUnitPriceID = '';
        var RMBTotalPriceID = '';
        var CarryID = '';
        var ExChargeID = '';
        var PKRCostID = "";
        var RMBCostID = "";

        var RMBPackingID = "";
        var DeleteID = "";

        var thisID = a.id;
        if (thisID.indexOf('txtQty') != -1) {
            QtyID = thisID;
            PriceID = thisID.replace('txtQty', 'txtPrice');
            totPriceID = thisID.replace('txtQty', 'txtTotalPrice');

            //RMBValueID = thisID.replace('txtQty', 'txtRMBValue');
            RMBUnitPriceID = thisID.replace('txtQty', 'txtRMBPrice');
            RMBTotalPriceID = thisID.replace('txtQty', 'txtRMBTotalPrice');

            CarryID = thisID.replace('txtQty', 'txtCarry');
            ExChargeID = thisID.replace('txtQty', 'txtExCharge');

            PKRCostID = thisID.replace('txtQty', 'txtCostPKR');
            RMBCostID = thisID.replace('txtQty', 'txtCostRMB');

            RMBPackingID = thisID.replace('txtQty', 'txtRMBPacking');
        }
        else if (thisID.indexOf('txtPrice') != -1) {
            if (document.getElementById("ddlPurchaseType").value == "PKR") {
                QtyID = thisID.replace('txtPrice', 'txtQty');
                PriceID = thisID;
                totPriceID = thisID.replace('txtPrice', 'txtTotalPrice');

                //RMBValueID = thisID.replace('txtPrice', 'txtRMBValue');
                RMBUnitPriceID = thisID.replace('txtPrice', 'txtRMBPrice');
                RMBTotalPriceID = thisID.replace('txtPrice', 'txtRMBTotalPrice');

                CarryID = thisID.replace('txtPrice', 'txtCarry');
                ExChargeID = thisID.replace('txtPrice', 'txtExCharge');

                PKRCostID = thisID.replace('txtPrice', 'txtCostPKR');
                RMBCostID = thisID.replace('txtPrice', 'txtCostRMB');

                RMBPackingID = thisID.replace('txtPrice', 'txtRMBPacking');
            }
        }
        else if (thisID.indexOf('txtTotalPrice') != -1) {
            QtyID = thisID.replace('txtTotalPrice', 'txtQty');
            PriceID = thisID.replace('txtTotalPrice', 'txtPrice');
            totPriceID = thisID;

            //RMBValueID = thisID.replace('txtTotalPrice', 'txtRMBValue');
            RMBUnitPriceID = thisID.replace('txtTotalPrice', 'txtRMBPrice');
            RMBTotalPriceID = thisID.replace('txtTotalPrice', 'txtRMBTotalPrice');

            CarryID = thisID.replace('txtTotalPrice', 'txtCarry');
            ExChargeID = thisID.replace('txtTotalPrice', 'txtExCharge');

            PKRCostID = thisID.replace('txtTotalPrice', 'txtCostPKR');
            RMBCostID = thisID.replace('txtTotalPrice', 'txtCostRMB');

            RMBPackingID = thisID.replace('txtTotalPrice', 'txtRMBPacking');
        }
        else if (thisID.indexOf('txtRMBPrice') != -1) {
            if (document.getElementById("ddlPurchaseType").value == "RMB") {

                QtyID = thisID.replace('txtRMBPrice', 'txtQty');
                PriceID = thisID.replace('txtRMBPrice', 'txtPrice');
                RMBUnitPriceID = thisID;

                //  RMBValueID = thisID.replace('txtRMBPrice', 'txtRMBValue');
                RMBTotalPriceID = thisID.replace('txtRMBPrice', 'txtRMBTotalPrice');
                totPriceID = thisID.replace('txtRMBPrice', 'txtTotalPrice');

                CarryID = thisID.replace('txtRMBPrice', 'txtCarry');
                ExChargeID = thisID.replace('txtRMBPrice', 'txtExCharge');

                PKRCostID = thisID.replace('txtRMBPrice', 'txtCostPKR');
                RMBCostID = thisID.replace('txtRMBPrice', 'txtCostRMB');

                RMBPackingID = thisID.replace('txtRMBPrice', 'txtRMBPacking');
            }
        }
        else if (thisID.indexOf('txtCarry') != -1) {
            QtyID = thisID.replace('txtCarry', 'txtQty');
            CarryID = thisID;
            totPriceID = thisID.replace('txtCarry', 'txtTotalPrice');

            //RMBValueID = thisID.replace('txtCarry', 'txtRMBValue');
            RMBUnitPriceID = thisID.replace('txtCarry', 'txtRMBPrice');
            RMBTotalPriceID = thisID.replace('txtCarry', 'txtRMBTotalPrice');

            PriceID = thisID.replace('txtCarry', 'txtPrice');
            ExChargeID = thisID.replace('txtCarry', 'txtExCharge');

            PKRCostID = thisID.replace('txtCarry', 'txtCostPKR');
            RMBCostID = thisID.replace('txtCarry', 'txtCostRMB');

            RMBPackingID = thisID.replace('txtCarry', 'txtRMBPacking');
        }
        else if (thisID.indexOf('txtExCharge') != -1) {
            QtyID = thisID.replace('txtExCharge', 'txtQty');
            ExChargeID = thisID;
            totPriceID = thisID.replace('txtExCharge', 'txtTotalPrice');

            //RMBValueID = thisID.replace('txtExCharge', 'txtRMBValue');
            RMBUnitPriceID = thisID.replace('txtExCharge', 'txtRMBPrice');
            RMBTotalPriceID = thisID.replace('txtExCharge', 'txtRMBTotalPrice');

            CarryID = thisID.replace('txtExCharge', 'txtCarry');
            PriceID = thisID.replace('txtExCharge', 'txtPrice');

            PKRCostID = thisID.replace('txtExCharge', 'txtCostPKR');
            RMBCostID = thisID.replace('txtExCharge', 'txtCostRMB');

            RMBPackingID = thisID.replace('txtExCharge', 'txtRMBPacking');
        }

        else if (thisID.indexOf('txtRMBPacking') != -1) {
            QtyID = thisID.replace('txtRMBPacking', 'txtQty');
            RMBPackingID = thisID;
            totPriceID = thisID.replace('txtRMBPacking', 'txtTotalPrice');

            //RMBValueID = thisID.replace('txtExCharge', 'txtRMBValue');
            RMBUnitPriceID = thisID.replace('txtRMBPacking', 'txtRMBPrice');
            RMBTotalPriceID = thisID.replace('txtRMBPacking', 'txtRMBTotalPrice');

            CarryID = thisID.replace('txtRMBPacking', 'txtCarry');
            PriceID = thisID.replace('txtRMBPacking', 'txtPrice');

            PKRCostID = thisID.replace('txtRMBPacking', 'txtCostPKR');
            RMBCostID = thisID.replace('txtRMBPacking', 'txtCostRMB');

            ExChargeID = thisID.replace('txtRMBPacking', 'txtExCharge');
        }

        else if (thisID.indexOf('txtDelete') != -1) {
            DeleteID = thisID;
            var abcc = DeleteID.substring(9);
            abcc = parseFloat(abcc) - 1;
            abcc = 'txtDelete' + abcc;
            thisID = abcc;
                QtyID = thisID.replace('txtDelete', 'txtQty');                
                ExChargeID = thisID.replace('txtDelete', 'txtExCharge');
                totPriceID = thisID.replace('txtDelete', 'txtTotalPrice');

                RMBUnitPriceID = thisID.replace('txtDelete', 'txtRMBPrice');
                RMBTotalPriceID = thisID.replace('txtDelete', 'txtRMBTotalPrice');

                CarryID = thisID.replace('txtDelete', 'txtCarry');
                PriceID = thisID.replace('txtDelete', 'txtPrice');

                PKRCostID = thisID.replace('txtDelete', 'txtCostPKR');
                RMBCostID = thisID.replace('txtDelete', 'txtCostRMB');

                RMBPackingID = thisID.replace('txtDelete', 'txtRMBPacking');                
            }
            
        var Qty = document.getElementById(QtyID).value; //txtQty1            
        var Price = document.getElementById(PriceID).value; //txtPrice1
        var totPrice = document.getElementById(totPriceID).value;
        var RMBValue = document.getElementById("ContentPlaceHolder1_hdnRMBValue").value;
        var RMUnitPrice = document.getElementById(RMBUnitPriceID).value;        
        var RMTotalPrice = document.getElementById(RMBTotalPriceID).value;
        var Carry = document.getElementById(CarryID).value;
        var ExCharge = document.getElementById(ExChargeID).value;
        var PkrCost = document.getElementById(PKRCostID).value;
        var RmbCost = document.getElementById(RMBCostID).value;
        var RmbPack = document.getElementById(RMBPackingID).value;        
        
        
        if (Qty == "" || Qty == null) { Qty = 0; }
        if (Price == "" || Price == null) { Price = 0; }
        if (totPrice == "" || totPrice == null) { totPrice = 0; }
        if (RMUnitPrice == "" || RMUnitPrice == null) { RMUnitPrice = 0; }
        if (RMTotalPrice == "" || RMTotalPrice == null) { RMTotalPrice = 0; }
        if (Carry == "" || Carry == null) { Carry = 0; }
        if (ExCharge == "" || ExCharge == null) { ExCharge = 0; }
        if (RmbPack == "" || RmbPack == null) { RmbPack = 0; }
       
        if (document.getElementById("ddlPurchaseType").value == "PKR") {
            document.getElementById(totPriceID).value = parseFloat(Qty) * parseFloat(Price);
            document.getElementById(totPriceID).value = parseFloat(parseFloat(document.getElementById(totPriceID).value) + parseFloat(ExCharge) + parseFloat(Carry));

            var RMPACKPRICE = parseFloat(RmbPack) * parseFloat(document.getElementById("txtPackingRate").value);
           

            var RMBUNITPRICE = parseFloat(Price) / parseFloat(RMBValue);
            RMBUNITPRICE = RMBUNITPRICE.toFixed(2);
            document.getElementById(RMBUnitPriceID).value = RMBUNITPRICE;
            var RMBCarry = parseFloat(Carry) / parseFloat(RMBValue);
            var RMBExtraCharges = parseFloat(ExCharge) / parseFloat(RMBValue);
            var RMBTOT = parseFloat(Qty) * parseFloat(RMBUNITPRICE);
            RMBTOT = RMBTOT + +parseFloat(RMBCarry) + parseFloat(RMBExtraCharges);
            RMBTOT = RMBTOT.toFixed(2);
            document.getElementById(RMBTotalPriceID).value = RMBTOT;


            PkrCost = parseFloat(Price) + parseFloat(Carry) + parseFloat(ExCharge);
            PkrCost = parseFloat(PkrCost) + parseFloat(RMPACKPRICE);
            document.getElementById(PKRCostID).value = PkrCost;

            RmbCost = parseFloat(RMBUNITPRICE) + parseFloat(RMBCarry) + parseFloat(RMBExtraCharges) + parseFloat(RmbPack);
            document.getElementById(RMBCostID).value = RmbCost;

            document.getElementById(totPriceID).value = parseFloat(Qty) * parseFloat(PkrCost);
            document.getElementById(RMBTotalPriceID).value = parseFloat(Qty) * parseFloat(RmbCost);
            

        }
        else {


            var RMPACKPRICEPKR = parseFloat(RmbPack) * parseFloat(document.getElementById("txtPackingRate").value);
            //var RMPACKPRICERMB = parseFloat(RmbPack) * parseFloat(document.getElementById("txtPackingRate").value);          


            document.getElementById(RMBTotalPriceID).value = parseFloat(Qty) * parseFloat(RMUnitPrice);
            var PKRUNITPRICE = parseFloat(RMUnitPrice) * parseFloat(RMBValue);
            ///////////PKRUNITPRICE = PKRUNITPRICE.toFixed(2); Temporary Disable 14 June 2018
            document.getElementById(PriceID).value = PKRUNITPRICE;
            var PKRTOT = parseFloat(Qty) * parseFloat(PKRUNITPRICE);
            PKRTOT = PKRTOT + + parseFloat(Carry) + parseFloat(ExCharge);
            ///////////PKRTOT = PKRTOT.toFixed(2);  Temporary Disable 14 June 2018
            document.getElementById(totPriceID).value = PKRTOT;
            var RMBCarry = parseFloat(Carry) / parseFloat(RMBValue);
            var RMBExtra = parseFloat(ExCharge) / parseFloat(RMBValue);
            var RMBTOT = parseFloat(Qty) * parseFloat(RMUnitPrice);
            RMBTOT = RMBTOT + RMBCarry + RMBExtra;
            RMBTOT = RMBTOT.toFixed(2);
            document.getElementById(RMBTotalPriceID).value = RMBTOT;


            PkrCost = parseFloat(Price) + parseFloat(Carry) + parseFloat(ExCharge);
            PkrCost = parseFloat(PkrCost) + parseFloat(RMPACKPRICEPKR);
            document.getElementById(PKRCostID).value = PkrCost;

            RmbCost = parseFloat(RMUnitPrice) + parseFloat(RMBCarry) + parseFloat(RMBExtra) + parseFloat(RmbPack);
            document.getElementById(RMBCostID).value = RmbCost;

            document.getElementById(totPriceID).value = parseFloat(Qty) * parseFloat(PkrCost);
            document.getElementById(RMBTotalPriceID).value = parseFloat(Qty) * parseFloat(RmbCost);
        }



        totPaste();
        totCarry();
        totPacking();
        totExCharge();
        totSupplier();
        DecimalRound();
        GetDiscountRMB();
    }

    function DecimalRound() {
//////        document.getElementById("txtCarryRMB").value = Math.round(parseFloat(document.getElementById("txtCarryRMB").value) * 100) / 100;
//////        document.getElementById("txtPackingRMB").value = Math.round(parseFloat(document.getElementById("txtPackingRMB").value) * 100) / 100;
//////        document.getElementById("txtExChrgRMB").value = Math.round(parseFloat(document.getElementById("txtExChrgRMB").value) * 100) / 100;
//////        document.getElementById("txtSupplierRMB").value = Math.round(parseFloat(document.getElementById("txtSupplierRMB").value) * 100) / 100;
        //////        document.getElementById("txtTotAmountRMB").value = Math.round(parseFloat(document.getElementById("txtTotAmountRMB").value) * 100) / 100;

        document.getElementById("txtCarryRMB").value = parseFloat(document.getElementById("txtCarryRMB").value) * 100 / 100;
        document.getElementById("txtPackingRMB").value = parseFloat(document.getElementById("txtPackingRMB").value) * 100 / 100;
        document.getElementById("txtExChrgRMB").value = parseFloat(document.getElementById("txtExChrgRMB").value) * 100 / 100;
        document.getElementById("txtSupplierRMB").value = parseFloat(document.getElementById("txtSupplierRMB").value) * 100 / 100;
        document.getElementById("txtTotAmountRMB").value = parseFloat(document.getElementById("txtTotAmountRMB").value) * 100 / 100;
    }

    function totSupplier() {

        var totFinal = 0;
        var totRMBFinal = 0;
        totFinal = parseFloat(document.getElementById("txtTotAmount").value);
        totRMBFinal = parseFloat(document.getElementById("txtTotAmountRMB").value);
        var tottemppkr = parseFloat(document.getElementById("txtCarryPKR").value) + parseFloat(document.getElementById("txtPackingPKR").value) + parseFloat(document.getElementById("txtExChrgPKR").value);
        var tottempRmb = parseFloat(document.getElementById("txtCarryRMB").value) + parseFloat(document.getElementById("txtPackingRMB").value) + parseFloat(document.getElementById("txtExChrgRMB").value);

        document.getElementById("txtSupplierPKR").value = parseFloat(totFinal) - parseFloat(tottemppkr);
        document.getElementById("txtSupplierRMB").value = parseFloat(totRMBFinal) - parseFloat(tottempRmb);
    }

    function SaveTransaction() {
        if (confirm("Do you want to continue?") == true) {
            document.getElementById("btnSave").disabled = true;
        }
        else {
            return;
        }

        /////////////////////// VALIDATION START
        /////////////////////// VALIDATION START
        var cc = ValidateRow();
        if (cc == '1') {
            //alert("Item Does not exist in Highlighted Rows! ");
            document.getElementById("modalbody").innerHTML = "Item Does not exist in Highlighted Rows!"
            $("#modal-dialog").modal();            
            return false;       
        }
        

        /////////////////////// VALIDATION END
        
        var UserID = localStorage.getItem("UserID");
        var txtVDate = document.getElementById('datepicker-autoClose').value;
        var txtVoucherNo = document.getElementById('txtVoucherNo').value;
        var txtTotAmount = document.getElementById('txtTotAmount').value;
        var txtTotAmountRMB = document.getElementById('txtTotAmountRMB').value;
        var txtDiscount = document.getElementById('txtDiscount').value;
        var txtTaxRate = document.getElementById('txtTaxRate').value;
        var txtTotTax = document.getElementById('txtTotTax').value;
        var txtGrandTotal = document.getElementById('txtGrandTotal').value;
        var ddlSupplier = document.getElementById('ContentPlaceHolder1_ddlSupplier').value;
        var RMBValueHDN = document.getElementById("ContentPlaceHolder1_hdnRMBValue").value;
        var elm = {}; var tMarks = 0;
        var elms = document.getElementById('tblItems').getElementsByTagName("input");
        var str = ""; var cc = 0;
        for (var i = 0; i < elms.length; i++) {
            if (elms[i].id.indexOf('txtAccCode') != -1) {

                elm = elms[i];
                var iD = elm.id;
                var txtAccCodeID = iD;
                var ddlUnitID = iD.replace('txtAccCode', 'ddlUnit');
                var txtQtyID = iD.replace('txtAccCode', 'txtQty');
                var txtPriceID = iD.replace('txtAccCode', 'txtPrice');
                var txtTotalPriceID = iD.replace('txtAccCode', 'txtTotalPrice');

                var txtPriceRMBID = iD.replace('txtAccCode', 'txtRMBPrice');
                var txtTotalPriceRMBID = iD.replace('txtAccCode', 'txtRMBTotalPrice');
                var txtRMBValueID = iD.replace('txtAccCode', 'txtRMBValue');
                
                var txtCarryID = iD.replace('txtAccCode', 'txtCarry');
                var txtExChargeID = iD.replace('txtAccCode', 'txtExCharge');
                
                var txtPKRCOSTID = iD.replace('txtAccCode', 'txtCostPKR');
                var txtRMBCOSTID = iD.replace('txtAccCode', 'txtCostRMB');


                var txtSrNoID = iD.replace('txtAccCode', 'txtSr');
                //var txtPackinRateID = document.getElementById("txtPackingRate").value;
                var txtPackinPriceID = iD.replace('txtAccCode', 'txtRMBPacking');
                
                var txtAccCode = document.getElementById(txtAccCodeID).value;
                var splt = txtAccCode.split('^');
                var accID = splt[0];
                var txtAccID = accID.replace(/\s/g, '');

                var txtAccTitle = splt[1];
            //    alert("A");
                //var ddlUnit = document.getElementById(ddlUnitID).value;
                var txtQty = document.getElementById(txtQtyID).value;
                var txtPrice = document.getElementById(txtPriceID).value;
                var txtTotalPrice = document.getElementById(txtTotalPriceID).value;
                //alert("a");
                var txtPriceRMB = document.getElementById(txtPriceRMBID).value;
                var txtTotalPriceRMB = document.getElementById(txtTotalPriceRMBID).value;
                var txtRMBValue = document.getElementById("ContentPlaceHolder1_hdnRMBValue").value;
                
                var txtCarry = document.getElementById(txtCarryID).value;
                var txtExCharge = document.getElementById(txtExChargeID).value;
                
                var txtPKRCOST = document.getElementById(txtPKRCOSTID).value;
                var txtRMBCOST = document.getElementById(txtRMBCOSTID).value;

                var txtPackingRate = document.getElementById("txtPackingRate").value;
                if (txtPackingRate == "" || txtPackingRate == null) { txtPackingRate = 0; }
                var txtPackingPrice = document.getElementById(txtPackinPriceID).value;
                if (txtPackingPrice == "" || txtPackingPrice == null) { txtPackingPrice = 0; }
                var txtPackingPricePKR = parseFloat(txtPackingRate) * parseFloat(txtPackingPrice);
                if (txtPackingPricePKR == "" || txtPackingPricePKR == null) { txtPackingPricePKR = 0; }
                var txtSrNo = document.getElementById(txtSrNoID).value;
              //  alert(txtPackingPricePKR);


                if (txtQty == "" || txtQty == null) { txtQty = 0; }
                if (txtPrice == "" || txtPrice == null) { txtPrice = 0; }
                if (txtCarry == "" || txtCarry == null) { txtCarry = 0; }
                if (txtExCharge == "" || txtExCharge == null) { txtExCharge = 0; }

                if (txtPackingRate == "" || txtPackingRate == null) { txtPackingRate = 0; }
                if (txtPackingPrice == "" || txtPackingPrice == null) { txtPackingPrice = 0; }
                if (txtPackingPricePKR == "" || txtPackingPricePKR == null) { txtPackingPricePKR = 0; }

                if (txtQty != 0) {
                    if (cc == 0) {
                        str = txtAccID + "^" + txtAccTitle + "^" + txtQty + "^" + txtPrice + "^" + txtTotalPrice + "^" + txtPriceRMB + "^" + txtTotalPriceRMB + "^" + txtRMBValue + "^" + txtCarry + "^" + txtExCharge + "^" + txtPKRCOST + "^" + txtRMBCOST + "^" + txtPackingRate + "^" + txtPackingPrice + "^" + txtPackingPricePKR + "^" + txtSrNo;
                    }
                    else {
                        str = str + "`" + txtAccID + "^" + txtAccTitle + "^" + txtQty + "^" + txtPrice + "^" + txtTotalPrice + "^" + txtPriceRMB + "^" + txtTotalPriceRMB + "^" + txtRMBValue + "^" + txtCarry + "^" + txtExCharge + "^" + txtPKRCOST + "^" + txtRMBCOST + "^" + txtPackingRate + "^" + txtPackingPrice + "^" + txtPackingPricePKR + "^" + txtSrNo;
                    }
                }

                cc = 1;


            }
            else {

            }
        }


        var txtCarryPKRNew = document.getElementById('txtCarryPKR').value;
        var txtCarryRMBNew = document.getElementById('txtCarryRMB').value;
        var txtPackingPKRNew = document.getElementById('txtPackingPKR').value;
        var txtPackingRMBNew = document.getElementById('txtPackingRMB').value;
        var txtExChargePKRNew = document.getElementById('txtExChrgPKR').value;
        var txtExChargeRMBNew = document.getElementById('txtExChrgRMB').value;
        var txtSupplierPKRNew = document.getElementById('txtSupplierPKR').value;
        var txtSupplierRMBNew = document.getElementById('txtSupplierRMB').value;

        var DDLCarry = document.getElementById("ContentPlaceHolder1_ddlCarryAccount").value;
        var DDLPacking = document.getElementById("ContentPlaceHolder1_ddlPackingAccount").value;
        var DDLExCharge = document.getElementById("ContentPlaceHolder1_ddlExChargesAccount").value;



        var txtDiscountPKR = document.getElementById("txtDiscountPKR").value;
        var txtDiscountRMB = document.getElementById("txtDiscountRMB").value;

        var txtONWAYID = document.getElementById("txtONWAYID").value;
        var BranchID = localStorage.getItem("BranchID");

        if (cc > 0) {
            $.ajax({
                type: 'POST',
                url: 'PO_ON_WAY_RCV.aspx/SaveTransaction',
                //data: {},
                data: '{ "UserID" : "' + UserID + '", "str" : "' + str + '", "txtVDate" : "' + txtVDate + '", "txtVoucherNo" : "' + txtVoucherNo + '", "txtTotAmount" : "' + txtTotAmount + '", "txtDiscount" : "' + txtDiscount + '", "txtTaxRate" : "' + txtTaxRate + '", "txtTotTax" : "' + txtTotTax + '", "txtGrandTotal" : "' + txtGrandTotal + '", "ddlSupplier" : "' + ddlSupplier + '", "txtTotAmountRMB" : "' + txtTotAmountRMB + '", "RMBValueHDN" : "' + RMBValueHDN + '","txtCarryPKR" : "' + txtCarryPKRNew + '", "txtCarryRMB" : "' + txtCarryRMBNew + '", "txtPackingPKR" : "' + txtPackingPKRNew + '", "txtPackingRMB" : "' + txtPackingRMBNew + '","txtExChargePKR" : "' + txtExChargePKRNew + '", "txtExChargeRMB" : "' + txtExChargeRMBNew + '", "txtSupplierPKR" : "' + txtSupplierPKRNew + '" ,"txtSupplierRMB" : "' + txtSupplierRMBNew + '","DDLCarry" : "' + DDLCarry + '","DDLPacking" : "' + DDLPacking + '","DDLExCharge" : "' + DDLExCharge + '", "txtDiscountPKR" : "' + txtDiscountPKR + '", "txtDiscountRMB" : "' + txtDiscountRMB + '", "txtONWAYID" : "' + txtONWAYID + '", "BranchID" : "' + BranchID + '"}',                                 
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (msg) {
                    tblRows = msg.d;
                    alert('PO has been Generated Successfully!');
                    //window.location = 'PO_LIST.aspx';
                    location.reload();
                }

            });
        }

    }

    function ClearForm() {
        // LOAD_ACCOUNTS();
        // LOAD_PROJECTS();
        var tblItems = document.getElementById('tblItems');
        var rowCount = tblItems.rows.length;
        for (var i = rowCount - 2; i > 0; i--) {
            tblItems.deleteRow(i);
        }
       

    }
    
</script>

  <script src="../js/jquery-1.9.1.min.js"></script>
		<script src="../js/bootstrap.min.js"></script>
                 <script type="text/javascript" src='../AAUI/plugins/jquery-ui/jquery-ui.custom.min.js'></script>

    <script type="text/javascript">

        $("#datepicker-autoClose").datepicker({ dateFormat: 'm/dd/yy' });
        $('[data-datepicker]').click(function (e) {
            var data = $(this).data('datepicker');
            $(data).focus();
        });

        $(document).on('keydown', function (e) {
            var kc = e.which || e.keyCode;

            if (e.ctrlKey && String.fromCharCode(kc).toUpperCase() == "A") {
                e.preventDefault();
                addRow();
            }
        });

        $(document).on('keydown', function (e) {
            var kc = e.which || e.keyCode;
            if (e.ctrlKey && String.fromCharCode(kc).toUpperCase() == "P") {
                e.preventDefault();
                SaveTransaction();
            }
        });
    </script>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
		<script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
		<script type="text/javascript">		    $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
</asp:Content>

