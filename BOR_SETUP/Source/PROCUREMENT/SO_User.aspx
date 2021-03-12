<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterUser.master" AutoEventWireup="true" CodeFile="SO_User.aspx.cs" Inherits="PROCUREMENT_PO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script src="../jquery-1.8.2.js"></script>
    <script src="../jquery.min.js"></script>
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
<!-- #STYLE 1 -->
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


<!-- #STYLE 2 -->
<div class="modal modal-message fade" id="modal-message">
	<div class="modal-dialog">
									<div class="modal-content">
										<div class="modal-header">
											<button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
											<h4 class="modal-title">Modal Message Header</h4>
										</div>
										<div class="modal-body">
											<p>Text in a modal</p>
											<p>Do you want to turn on location services so GPS can use your location ?</p>
										</div>
										<div class="modal-footer">
											<a href="javascript:;" class="btn btn-sm btn-white" data-dismiss="modal">Close</a>
											<a href="javascript:;" class="btn btn-sm btn-primary">Save Changes</a>
										</div>
									</div>
								</div>
</div>
<!-- #modal-message -->
<!-- #modal-message -->
<!-- #modal-message -->


  <div class="row">
                <!-- begin col-6 -->
			    <div class="col-md-12">
			        <!-- begin panel -->
                    <div class="panel widget light-widget panel-bd-top">
                        <div class="panel-heading bordered">
                    <h3 class="panel-title"> <span class="menu-icon"> <i class="icon-pie"></i> </span> Sale Order (User) </h3>
                    <div class="vd_panel-menu">
  <div data-action="minimize" data-original-title="Minimize" data-toggle="tooltip" data-placement="bottom" class=" menu entypo-icon"> <i class="icon-minus3"></i> </div>
  <div data-action="refresh"  data-original-title="Refresh" data-toggle="tooltip" data-placement="bottom" class=" menu entypo-icon smaller-font"> <i class="icon-cycle"></i> </div>
  
  <div data-action="close" data-original-title="Close" data-toggle="tooltip" data-placement="bottom" class=" menu entypo-icon"> <i class="icon-cross"></i> </div>
</div>
<!-- vd_panel-menu --> 
                  </div>
                 
                              <div class="panel-body">

                             <div class="row"  style="display:none;">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblMsg" runat="server" CssClass="parsley-errors-list" Visible="False"></asp:Label>
                                    <asp:TextBox ID="txtid" runat="server" Visible="False" CssClass="form_inputfeild" placeholder="Enter Name"></asp:TextBox>                
                                </div>
                            </div>
                            <div class="row"><div class="col-lg-12" style="height:10px"></div></div>

                            <div class="row">
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="margin-top:5px;">Invoice Date</label>
                                     <div class="col-md-8">
                                    <div class="input-group">
                                       <input type="text" class="width-100" id="datepicker-autoClose" placeholder="Select Date" />
                                       <span class="input-group-addon" id="datepicker-icon-trigger-PV" data-datepicker="#datepicker-autoClose"><i class="fa fa-calendar"></i></span>
                                       </div>                                                                       
                                    </div>
                                </div>
                            </div>

                            <div class="col-lg-4">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="margin-top:5px;">Invoice No.</label>
                                    
                                    <div class="col-md-8">
                                        <input type="text" id="txtVoucherNo" class="form-control" disabled="disabled" />
                                    </div>
                                </div>
                            </div>


                            <div class="col-lg-4">
                                <div class="form-group">
                                    <label class="col-md-3 control-label" style="margin-top:5px;">Customer</label>
                                    
                                    <div class="col-md-9">
                                <asp:DropDownList ID="ddlSupplier" runat="server" DataSourceID="SqlDataSource1"  CssClass="form-control"
                                            DataTextField="AccountsTitle" DataValueField="AccountsID">
                                </asp:DropDownList>


                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:Con %>" 
                                            SelectCommand="SELECT AccountsID, AccountsTitle FROM [VW_ACCOUNTS_NAME_WITH_BALANCE]
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
                            <div class="col-md-4" style="display:none;">
                                <div class="form-group">
                                    
                                     <div class="col-md-12">
                                  
                                       <select id="ddlShop" class="form-control">
                                       <option value="NIL">Select Shop</option>
                                       <option value="SAAD SHOP">SAAD SHOP</option>
                                       <option value="UBAID SHOP">UBAID SHOP</option>
                                       <option value="OSAMA SHOP">OSAMA SHOP</option>
                                       </select>
                                  
                                    </div>
                                </div>
                            </div>

                            <div class="col-lg-4">
                                <div class="form-group">                                   
                                    
                                    <div class="col-md-12">
                                        <input type="text" id="txtCustomerName" class="form-control" placeholder="Type Customer Name" />
                                    </div>
                                </div>
                            </div>
                             <div class="col-lg-4">
                                <div class="form-group">                                   
                                    
                                    <div class="col-md-12">
                                        <input type="text" id="txtContactNo" class="form-control" placeholder="Type Customer Contact No" />
                                    </div>
                                </div>
                            </div>
                               <div class="col-lg-4">
                                <div class="form-group">                                   
                                    
                                    <div class="col-md-12">
                                        <input type="text" id="txtBillNo" class="form-control" placeholder="Type Local Bill No" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-12" style="height:20px;"></div>
                            <div class="col-lg-12">
                                <div class="form-group">                                   
                                    
                                    <div class="col-md-12">
                                        <input type="text" id="txtAddress" class="form-control" placeholder="Type Customer Address" />
                                    </div>
                                </div>
                            </div>
                         

                          

                            
                        </div>
                         <div class="row"><div class="col-lg-12" style="height:10px"></div></div>

                            <div class="row">
                                <div class="col-lg-12 table-responsive">
                                    <table id="tblItems" class="table table-striped table-bordered dataTable no-footer">
                                        <tr>
                                            <th style="width:5%">Sr1.</th>
                                            <th style="width:35%">Item</th>                                            
                                           <%-- <th style="width:8%">Unit</th>                                            --%>
                                            <th style="width:7%">Qty.</th>
                                            <%--<th style="width:10%">RMB Value</th>     --%>
                                            <th style="width:12%">Unit Price (PKR)</th>                                                                                   
                                            <th style="width:12%">Unit Price (RMB)</th>                                            
                                            <th style="width:12%">Total Price (PKR)</th>
                                            <th style="width:12%">Total Price (RMB)</th>
                                            <th style="width:5%"></th>
                                        </tr>
                                        <tr>
                                            <td><input type="text" id="txtSr1" value="1" disabled="disabled" class="form-control"  style="width:100%; border:0px none;"/></td>
                                            <td>                                             
                                             <input type="text" class="span6 typeahead form-control" onblur="javascript:fillUNITS(this);" style="border:0px none;" id="txtAccCode1"  data-provide="typeahead" style="width:100%" 
                                             data-items="10" >
                                            </td>                   
                                            <%--<td>
                                            <select id="ddlUnit1" class="form-control"  style="width:100%; border:0px none;">
                                                
                                                </select>
                                            </td>--%>
                                            <td><input type="text" id="txtQty1" class="form-control" value="1" style="width:100%; border:0px none;" onkeyup="tot(this);"/></td>
                                            <%--<td><input type="text" id="txtRMBValue1" class="form-control" value="" disabled="disabled"  style="width:100%; border:0px none;" /></td>--%>
                                            <td><input type="text" id="txtPrice1" class="form-control" value="0"  style="width:100%; border:0px none;" onkeyup="tot(this);"/></td>
                                            <td><input type="text" id="txtRMBPrice1" class="form-control" value="0"  disabled="disabled"   style="width:100%; border:0px none;" onkeyup="tot(this);"/></td>
                                            <td><input type="text" id="txtTotalPrice1" class="form-control" value="0" disabled="disabled"  style="width:100%; border:0px none;" /></td>
                                            <td><input type="text" id="txtRMBTotalPrice1" class="form-control" value="0" disabled="disabled"  style="width:100%; border:0px none;" /></td>
                                            <td><span class="glyphicon glyphicon-remove" aria-hidden="true" style="font-size: 20px;margin-left: 5px; cursor:pointer; color:Red;"  onmouseover='this.style.color="gray"'  onmouseout='this.style.color="Red"' title="Delete Row" id="txtDelete1" onclick="DeleteRow(this);" ></span></td>                                          
                                            <td style="display:none;"><input type="text" id="txtValidateQty1" class="form-control" value="0" disabled="disabled"  style="width:100%; border:0px none; display:none;" /></td>
                                                                    
                                        </tr>

                                    </table>

                                    <%--<input type="button" value="" class="fa-plus-square" onclick="addRow();SumDbCr();"/>--%>
                                </div>
                            </div>
                            <div class="col-lg-12"><i class="fa fa-2x fa-plus-square"  title="Press Ctrl + A for new Row"  onclick="addRow();SumDbCr();"></i></div>



                            <div class="row" style="height:20px;"></div>
                                <label class="col-md-3 control-label" >Discount Amount PKR</label>
                                <label class="col-md-3 control-label" >Discount Amount RMB</label>
                                <label class="col-md-3 control-label" >Customer Amount PKR</label>
                                <label class="col-md-3 control-label" >Customer  Amount RMB</label>
                                
                                <div class="col-lg-3"><input type="text" id="txtDiscountPKR" value="0" class="form-control"  style="width:100%;" onkeypress="GetDiscountRMB();" onkeydown="GetDiscountRMB();" onkeyup="GetDiscountRMB();" /></div>
                                <div class="col-lg-3"><input type="text" id="txtDiscountRMB" value="0" class="form-control" disabled="disabled" style="width:100%;" /></div>
                                <div class="col-lg-3"><input type="text" id="txtCustomerPKR" value="0" class="form-control" disabled="disabled" style="width:100%;" /></div>
                                <div class="col-lg-3"><input type="text" id="txtCustomerRMB" value="0" class="form-control" disabled="disabled" style="width:100%;" /></div>
                            <div class="col-md-12" style="height:20px;"></div>

                          
                                <label class="col-md-9 control-label" style="text-align:right">Total Amount PKR</label>
                                <div class="col-lg-3"><input type="text" id="txtTotAmount" class="form-control" disabled="disabled" style="width:100%;" /></div>
                          <div class="col-md-12" style="height:10px;"></div>
                                <label class="col-md-9 control-label" style="text-align:right">Total Amount RMB</label>
                                <div class="col-lg-3"><input type="text" id="txtTotAmountRMB" class="form-control" disabled="disabled" style="width:100%;" /></div>
                          
                            <div class="row" style="display:none;">
                                <label class="col-md-10 control-label" style="text-align:right">Discount</label>
                                <div class="col-lg-2"><input type="text" id="txtDiscount" onkeyup="totPaste();" value="0" class="form-control" style="width:100%;"/></div>
                            </div>

                            <div class="row" style="display:none;">
                                <label class="col-md-10 control-label" style="text-align:right">Tax Rate</label>
                                <div class="col-lg-2"><input type="text" id="txtTaxRate" value="0" onkeyup="totPaste();" class="form-control" style="width:100%;"/></div>
                            </div>

                            <div class="row" style="display:none;">
                                <label class="col-md-10 control-label" style="text-align:right">Total Tax</label>
                                <div class="col-lg-2"><input type="text" id="txtTotTax" disabled="disabled" class="form-control" style="width:100%;"/></div>
                            </div>

                            <div class="row" style="display:none;">
                                <label class="col-md-10 control-label" style="text-align:right">Grand Total</label>
                                <div class="col-lg-2"><input type="text" id="txtGrandTotal" disabled="disabled" class="form-control" style="width:100%;"/></div>
                            </div>
                            <div class="col-md-12" style="height:20px;"></div>
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
                             <input type="button" id="btnSave" style="font-weight:bold; font-size:20px;" class="btn btn-info btn-block" value="P R O C E S S &nbsp&nbsp&nbsp S A L E  &nbsp&nbsp&nbsp O R D E R" onclick="SaveTransaction();" />
                             </div></div>
                             <%--<div class="col-lg-3" style="text-align:right;"><h2><b id="lblTotalPasteQty">Total Paste Qty : 0</b></h2>--%>
                        </div>
                        </div>
                      
                        </div>
                        </div>
                        
        <script src="../js/jquery-1.9.1.min.js"></script>
		<script src="../js/bootstrap.min.js"></script>
<script type="text/javascript">

    document.getElementById("datepicker-autoClose").focus();
    document.getElementById("datepicker-autoClose").select();

    $(document).ready(function () {
        var BranchID = localStorage.getItem("BranchID");
        var dataToSend = JSON.stringify({ 'BranchID': BranchID });
        $.ajax({
            type: "POST",
            url: "SO_User.aspx/LoadRMbValue",
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

                    document.getElementById(txtAccCodeID).style.background = 'red';
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
            alert("Item Does not exist in Highlighted Rows! ");
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


//        //var newList = document.createElement("select");
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
        txtRMBPrice.disabled = "disabled";
        cell7.appendChild(txtRMBPrice);
        


        var cell8 = row.insertCell(5); //txtTotalPrice
        var txtTotalPrice = document.createElement("input");
        txtTotalPrice.type = "text";
        txtTotalPrice.id = "txtTotalPrice" + newID;
        txtTotalPrice.className = 'form-control';
        txtTotalPrice.style.width = '100%';
        txtTotalPrice.style.border = '0px none';
        txtTotalPrice.value = '0';
        txtTotalPrice.disabled = "disabled";
        cell8.appendChild(txtTotalPrice);


        var cell9 = row.insertCell(6); //txtTotalPrice
        var txtRMBTotalPrice = document.createElement("input");
        txtRMBTotalPrice.type = "text";
        txtRMBTotalPrice.id = "txtRMBTotalPrice" + newID;
        txtRMBTotalPrice.className = 'form-control';
        txtRMBTotalPrice.style.width = '100%';
        txtRMBTotalPrice.style.border = '0px none';
        txtRMBTotalPrice.value = '0';
        txtRMBTotalPrice.disabled = "disabled";
        cell9.appendChild(txtRMBTotalPrice);

        var cell100 = row.insertCell(7); //Delete
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





        var cell10 = row.insertCell(8); //txtValidateQty
        var txtValidateQty = document.createElement("input");
        txtValidateQty.type = "text";
        txtValidateQty.id = "txtValidateQty" + newID;
        txtValidateQty.className = 'form-control';
        txtValidateQty.style.width = '100%';
        txtValidateQty.style.border = '0px none';
        txtValidateQty.value = '0';
        txtValidateQty.disabled = "disabled";
        txtValidateQty.style.display = "none";
        cell9.appendChild(txtValidateQty);



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

    function fillUNITS(a) {        
        var txtAccCodeID = a.id;
        //var ddlUnitID = txtAccCodeID.replace('txtAccCode', 'ddlUnit');
        var QtyID = txtAccCodeID.replace('txtAccCode', 'txtQty');
        var QtyValidateID = txtAccCodeID.replace('txtAccCode', 'txtValidateQty');
        
        var txtAccCode = document.getElementById(txtAccCodeID).value;
        var splt = txtAccCode.split('^');
        var accID = splt[3];
        var Qty = splt[1];
        Qty = Qty.replace('QTY : ', '');
        var itmID = accID.replace(/\s/g, '');

        //var spnID = 'spnUNT' + itmID;
        document.getElementById(QtyID).value = Qty;
        document.getElementById(QtyValidateID).value = Qty;
//        //alert(document.getElementById(QtyValidateID).value);
//        var ddlUnit = document.getElementById(ddlUnitID);        

//        var pp = document.getElementById(spnID).innerHTML;
//        
//        var pList = pp.split('~');
//        for (var i = 0; i < pList.length; i++) {
//            var pItems = pList[i].split('^');
//            ddlUnit.appendChild(new Option(pItems[1], pItems[0]));
//        }

    }

    function LOAD_ITEMS() {
        
        var tblRows = "";
        var UserID = "1";
        var BranchID = localStorage.getItem("BranchID");
        
        $.ajax({
            type: 'POST',
            url: 'SO_User.aspx/LOAD_ITEMS',
            data: '{ "UserID" : "' + UserID + '", "BranchID" : "' + BranchID + '"  }',
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

    function tot(a) {
        var QtyID = ''; //txtQty1
        var PriceID = ''; //txtPrice1
        var totPriceID = ''; //txtTotalPrice1
        var RMBValueID = '';
        var RMBUnitPriceID = '';
        var RMBTotalPriceID = '';
        var QtyValidateID = '';
        var DeleteID = '';
        
        var thisID = a.id;
        if (thisID.indexOf('txtQty') != -1) {
            QtyID = thisID;
            PriceID = thisID.replace('txtQty', 'txtPrice');
            totPriceID = thisID.replace('txtQty', 'txtTotalPrice');

            //RMBValueID = thisID.replace('txtQty', 'txtRMBValue');
            RMBUnitPriceID = thisID.replace('txtQty', 'txtRMBPrice');
            RMBTotalPriceID = thisID.replace('txtQty', 'txtRMBTotalPrice');
            QtyValidateID = thisID.replace('txtQty', 'txtValidateQty');
        }
        else if (thisID.indexOf('txtPrice') != -1) {
            QtyID = thisID.replace('txtPrice', 'txtQty');
            PriceID = thisID;
            totPriceID = thisID.replace('txtPrice', 'txtTotalPrice');

           // RMBValueID = thisID.replace('txtPrice', 'txtRMBValue');
            RMBUnitPriceID = thisID.replace('txtPrice', 'txtRMBPrice');
            RMBTotalPriceID = thisID.replace('txtPrice', 'txtRMBTotalPrice');
            QtyValidateID = thisID.replace('txtPrice', 'txtValidateQty');
        }
        else if (thisID.indexOf('txtTotalPrice') != -1) {
            QtyID = thisID.replace('txtTotalPrice', 'txtQty');
            PriceID = thisID.replace('txtTotalPrice', 'txtPrice');
            totPriceID = thisID;

         //   RMBValueID = thisID.replace('txtTotalPrice', 'txtRMBValue');
            RMBUnitPriceID = thisID.replace('txtTotalPrice', 'txtRMBPrice');
            RMBTotalPriceID = thisID.replace('txtTotalPrice', 'txtRMBTotalPrice');
            QtyValidateID = thisID.replace('txtTotalPrice', 'txtValidateQty');
        }

        else if (thisID.indexOf('txtDelete') != -1) {
            DeleteID = thisID;
            var abcc = DeleteID.substring(9);
            abcc = parseFloat(abcc) - 1;
            abcc = 'txtDelete' + abcc;
            thisID = abcc;
            QtyID = thisID.replace('txtDelete', 'txtQty');
            PriceID = thisID.replace('txtDelete', 'txtPrice');
            totPriceID = thisID.replace('txtDelete', 'txtTotalPrice');
            RMBUnitPriceID = thisID.replace('txtDelete', 'txtRMBPrice');
            RMBTotalPriceID = thisID.replace('txtDelete', 'txtRMBTotalPrice');
            QtyValidateID = thisID.replace('txtDelete', 'txtValidateQty');
        }
        
        var Qty = document.getElementById(QtyID).value; //txtQty1
        var Price = document.getElementById(PriceID).value; //txtPrice1
        var totPrice = document.getElementById(totPriceID).value;
        var RMBValue = document.getElementById("ContentPlaceHolder1_hdnRMBValue").value;
        var QtyValidate = document.getElementById(QtyValidateID).value;
        if (parseFloat(Qty) > parseFloat(QtyValidate)) {
            alert("Please reduce quantity... !");
            document.getElementById(QtyID).value = 0;
        }

        document.getElementById(QtyID).onfocusout = function () {
            if (parseFloat(Qty) == parseFloat(QtyValidate)) {
                alert("Quantity Alert !!");
            }
        };
        if (Qty == "" || Qty == null) { Qty = 0; }
        if (Price == "" || Price == null) { Price = 0; }
        if (totPrice == "" || totPrice == null) { totPrice = 0; }
        document.getElementById(totPriceID).value = parseFloat(Qty) * parseFloat(Price);

        var RMBUNITPRICE = parseFloat(Price) / parseFloat(RMBValue);
        RMBUNITPRICE = RMBUNITPRICE.toFixed(2);
        document.getElementById(RMBUnitPriceID).value = RMBUNITPRICE;
        var RMBTOT = parseFloat(Qty) * parseFloat(RMBUNITPRICE);
        RMBTOT = RMBTOT.toFixed(2);
        document.getElementById(RMBTotalPriceID).value = RMBTOT;

        totPaste();
        totdiscount();
        GetDiscountRMB();
    }

    function totdiscount() {
        document.getElementById("txtCustomerPKR").value = parseFloat(document.getElementById("txtTotAmount").value) - parseFloat(document.getElementById("txtDiscountPKR").value);
        document.getElementById("txtCustomerRMB").value = parseFloat(document.getElementById("txtTotAmountRMB").value) - parseFloat(document.getElementById("txtDiscountRMB").value);
    }

    function GetDiscountRMB() {
        document.getElementById("txtDiscountRMB").value = parseFloat(document.getElementById("txtDiscountPKR").value) / parseFloat(document.getElementById("ContentPlaceHolder1_hdnRMBValue").value);
        //tot();
        //totPaste();
        totdiscount();
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
            alert("Item Does not exist in Highlighted Rows! ");
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
        var ddlShop = document.getElementById('ddlShop').value;
        var txtCustomerName = document.getElementById("txtCustomerName").value;
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
                //var txtRMBValueID = iD.replace('txtAccCode', 'txtRMBValue');

                var txtSrNoID = iD.replace('txtAccCode', 'txtSr');

                var txtAccCode = document.getElementById(txtAccCodeID).value;
                var splt = txtAccCode.split('^');
                var accID = splt[3];
                var txtAccID = accID.replace(/\s/g, '');                
                var txtAccTitle = splt[0];
                var PRID = splt[4];
                var PRDID = splt[5];
                var ddlUnit = "UN-000050";
                var txtQty = document.getElementById(txtQtyID).value;
                var txtPrice = document.getElementById(txtPriceID).value;
                var txtTotalPrice = document.getElementById(txtTotalPriceID).value;                
                var txtPriceRMB = document.getElementById(txtPriceRMBID).value;
                var txtTotalPriceRMB = document.getElementById(txtTotalPriceRMBID).value;
                var txtRMBValue = document.getElementById("ContentPlaceHolder1_hdnRMBValue").value;
                var txtSrNo = document.getElementById(txtSrNoID).value;



                if (txtPrice == "" || txtPrice == null || txtPrice == 0) {
                    alert("Check Unit Price....!");
                    return;
                }

                if (txtQty == "" || txtQty == null) { txtQty = 0; }
                if (txtPrice == "" || txtPrice == null) { txtPrice = 0; }

                if (txtQty != 0) {
                    if (cc == 0) {
                        str = txtAccID + "^" + txtAccTitle + "^" + ddlUnit + "^" + txtQty + "^" + txtPrice + "^" + txtTotalPrice + "^" + txtPriceRMB + "^" + txtTotalPriceRMB + "^" + txtRMBValue + "^" + PRID + "^" + PRDID + "^" + txtSrNo;
                    }
                    else {
                        str = str + "`" + txtAccID + "^" + txtAccTitle + "^" + ddlUnit + "^" + txtQty + "^" + txtPrice + "^" + txtTotalPrice + "^" + txtPriceRMB + "^" + txtTotalPriceRMB + "^" + txtRMBValue + "^" + PRID + "^" + PRDID + "^" + txtSrNo;
                    }
                }

                cc = 1;


            }
            else {

            }
        }


        var txtDiscountPKR = document.getElementById("txtDiscountPKR").value;
        var txtDiscountRMB = document.getElementById("txtDiscountRMB").value;
        var txtCusAmountPKR = document.getElementById("txtCustomerPKR").value;
        var txtCusAmountRMB = document.getElementById("txtCustomerRMB").value;


        var txtContactNo = document.getElementById("txtContactNo").value;
        var txtAddress = document.getElementById("txtAddress").value;
        var txtBillNo = document.getElementById("txtBillNo").value;


        var BranchID = localStorage.getItem("BranchID");

        if (cc > 0) {
            $.ajax({
                type: 'POST',
                url: 'SO_User.aspx/SaveTransaction',
                //data: {},
                data: '{ "UserID" : "' + UserID + '", "str" : "' + str + '", "txtVDate" : "' + txtVDate + '", "txtVoucherNo" : "' + txtVoucherNo +
                '", "txtTotAmount" : "' + txtTotAmount + '", "txtDiscount" : "' + txtDiscount + '", "txtTaxRate" : "' + txtTaxRate + '", "txtTotTax" : "' + txtTotTax + '", "txtGrandTotal" : "' + txtGrandTotal + '", "ddlSupplier" : "' + ddlSupplier + '", "txtTotAmountRMB" : "' + txtTotAmountRMB + '", "RMBValueHDN" : "' + RMBValueHDN + '", "txtDiscountPKR" : "' + txtDiscountPKR + '", "txtDiscountRMB" : "' + txtDiscountRMB + '", "txtCusAmountPKR" : "' + txtCusAmountPKR + '", "txtCusAmountRMB" : "' + txtCusAmountRMB + '", "ddlShop" : "' + ddlShop + '", "txtCustomerName" : "' + txtCustomerName + '", "txtContactNo" : "' + txtContactNo + '", "txtAddress" : "' + txtAddress + '", "txtBillNo" : "' + txtBillNo + '", "BranchID" : "' + BranchID + '"}',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (msg) {
                    tblRows = msg.d;
                    alert('SO has been Generated Successfully!');
                    //   window.location = 'SO_LIST.aspx';
                    window.open('../REPORTS/SO_His.aspx?ID=' + tblRows + '&BID=' + BranchID, '_blank');
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

</asp:Content>

