<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Master.master" AutoEventWireup="true" CodeFile="PurchaseWareHouse_Master.aspx.cs" Inherits="PROCUREMENT_PO" %>

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



  <div class="row">
                <!-- begin col-6 -->
			    <div class="col-md-12">
			        <!-- begin panel -->
                    <div class="panel widget light-widget panel-bd-top">
                        <div class="panel-heading bordered">
                    <h3 class="panel-title"> <span class="menu-icon"> <i class="icon-pie"></i> </span> Transfer Order IN ( MASTER ) </h3>
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
                            <div class="col-lg-6">
                                <div class="form-group">
                                    <label class="col-md-3 control-label">Tranfer Date</label>
                                     <div class="col-md-9">
                                    <div class="input-group">
                                       <input type="text" class="width-100" id="datepicker-autoClose" placeholder="Select Date" />
                                       <span class="input-group-addon" id="datepicker-icon-trigger-PV" data-datepicker="#datepicker-autoClose"><i class="fa fa-calendar"></i></span>
                                       </div>                                                                       
                                    </div>
                                </div>
                            </div>

                            <div class="col-lg-6">
                                <div class="form-group">
                                    <label class="col-md-3 control-label">Transfer No.</label>
                                    
                                    <div class="col-md-9">
                                        <input type="text" id="txtVoucherNo" class="form-control" disabled="disabled" />
                                    </div>
                                </div>
                            </div>


                        </div>
                           <div class="row">
                           <div class="col-lg-6">
                                <div class="form-group"> 
                                    <div class="col-md-12">
                                        <input type="text" id="txtDescription" placeholder="Enter Description / Narration" class="form-control" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-6">
                                <div class="form-group"> 
                                    <div class="col-md-12">
                                        <input type="text" id="txtShopName" placeholder="Enter Party Name" class="form-control" />
                                    </div>
                                </div>
                            </div>


                        </div>
                         <div class="row"><div class="col-lg-12" style="height:10px"></div></div>
                       
                            <div class="row" style="overflow:visible;">
                                <div class="col-lg-12 table-responsive" style="overflow:visible;">
                                    <table id="tblItems" class="table table-striped table-bordered dataTable no-footer" style="overflow:visible;">
                                        <tr>
                                            <th style="width:10%">Sr1.</th>
                                            <th style="width:75%">Item</th>                                                                                        
                                            <th style="width:10%">Qty.</th>                                            
                                            <th style="width:5%"></th>
                                        </tr>
                                        <tr>
                                            <td><input type="text" id="txtSr1" value="1" disabled="disabled" class="form-control"  style="width:100%; border:0px none;"/></td>
                                            <td>                                             
                                             <input type="text" class="span6 typeahead form-control" onblur="javascript:fillUNITS(this);" style="border:0px none; overflow:visible;" id="txtAccCode1"  data-provide="typeahead" style="width:100%" 
                                             data-items="4" >
                                            </td>                                                               
                                            <td><input type="text" id="txtQty1" class="form-control" value="1" style="width:100%; border:0px none;" onkeyup="tot(this);"/></td>                                                                                        
                                             <td><span class="glyphicon glyphicon-remove" aria-hidden="true" style="font-size: 20px;margin-left: 5px; cursor:pointer; color:Red;"  onmouseover='this.style.color="gray"'  onmouseout='this.style.color="Red"' title="Delete Row" id="txtDelete1" onclick="DeleteRow(this);" ></span></td>                                          
                                        </tr>

                                    </table>
                                </div>
                            </div>
                       
                            <div class="col-lg-12"><i class="fa fa-2x fa-plus-square" title="Press Ctrl + A for new Row" onclick="addRow();SumDbCr();"></i></div>
    
                            <div class="col-lg-12" style="height:20px;"></div>

                        
                          
                             <div class="row"><div class="col-lg-12" >
                            
                             <input type="button" id="btnSave" style="font-weight:bold; font-size:20px;" class="btn btn-info btn-block" value="P R O C E S S &nbsp&nbsp&nbsp T R A N S F E R  &nbsp&nbsp&nbsp O R D E R  &nbsp&nbsp&nbsp I N" onclick="SaveTransaction();" />
                             </div></div>
                             
                        </div>
                   </div>
                   </div>
                   </div>

        <script src="../js/jquery-1.9.1.min.js"></script>
		<script src="../js/bootstrap.min.js"></script>
<script type="text/javascript">

    
    document.getElementById("datepicker-autoClose").focus();
    document.getElementById("datepicker-autoClose").select();


    


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



    
        var cell100 = row.insertCell(3); //Delete
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

        var tblRows = "";
        var UserID = "1";
        $.ajax({
            type: 'POST',
            url: 'PurchaseWareHouse_Master.aspx/LOAD_ITEMS',
            data: '{ "UserID" : "' + UserID + '" }',
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
                //tot(a);
                //totPaste();
            }
        }
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
        var txtDescription = document.getElementById('txtDescription').value;
        var txtShopName = document.getElementById('txtShopName').value;
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
                var txtSrNoID = iD.replace('txtAccCode', 'txtSr');
                
                var txtAccCode = document.getElementById(txtAccCodeID).value;
                var splt = txtAccCode.split('^');
                var accID = splt[0];
                var txtAccID = accID.replace(/\s/g, '');

                var txtAccTitle = splt[1];
            
                var txtQty = document.getElementById(txtQtyID).value;
                var txtSrNo = document.getElementById(txtSrNoID).value;


                if (txtQty == "" || txtQty == null) { txtQty = 0; }                

                if (txtQty != 0) {
                    if (cc == 0) {
                        str = txtAccID + "^" + txtAccTitle + "^" + txtQty + "^" + txtSrNo;
                    }
                    else {
                        str = str + "`" + txtAccID + "^" + txtAccTitle + "^" + txtQty + "^" + txtSrNo;
                    }
                }

                cc = 1;


            }
            else {

            }
        }


        if (cc > 0) {
            $.ajax({
                type: 'POST',
                url: 'PurchaseWareHouse_Master.aspx/SaveTransaction',
                //data: {},
                data: '{ "UserID" : "' + UserID + '", "str" : "' + str + '", "txtVDate" : "' + txtVDate + '", "txtVoucherNo" : "' + txtVoucherNo + '", "txtDescription" : "' + txtDescription + '", "txtShopName" : "' + txtShopName + '"}',                                 
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (msg) {
                    tblRows = msg.d;
                    alert('Transfer order IN has been Generated Successfully!');
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

</asp:Content>

