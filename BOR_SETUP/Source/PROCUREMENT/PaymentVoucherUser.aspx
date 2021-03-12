<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Master.master" AutoEventWireup="true" CodeFile="PaymentVoucherUser.aspx.cs" Inherits="PROCUREMENT_PO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script src="../jquery-1.8.2.js"></script>
    <script src="../jquery.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

  <asp:HiddenField ID="hdnAccounts" runat="server" Value='["Karachi","Hyderabad","USA","Arkansas","California","Colorado","Connecticut","Delaware","Florida","Georgia","Hawaii","Idaho","Illinois","Indiana","Iowa","Kansas","Kentucky"]' />
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
                    <h3 class="panel-title"> <span class="menu-icon"> <i class="icon-pie"></i> </span> Payment </h3>
                    <div class="vd_panel-menu">
  <div data-action="minimize" data-original-title="Minimize" data-toggle="tooltip" data-placement="bottom" class=" menu entypo-icon"> <i class="icon-minus3"></i> </div>
  <div data-action="refresh"  data-original-title="Refresh" data-toggle="tooltip" data-placement="bottom" class=" menu entypo-icon smaller-font"> <i class="icon-cycle"></i> </div>
  
  <div data-action="close" data-original-title="Close" data-toggle="tooltip" data-placement="bottom" class=" menu entypo-icon"> <i class="icon-cross"></i> </div>
</div>
<!-- vd_panel-menu --> 
                  </div>
                        
                        <div class="panel-body">

                             
                            <div class="row"><div class="col-lg-12" style="height:10px"></div></div>

                            <div class="row">
                            <div class="col-lg-12">
                                <div class="form-group">
                                    <label class="col-md-3 control-label" style="margin-top:5px;">Date :</label>
                
                                               <label class="col-md-3 control-label" style="margin-top:5px;">Type :</label>
                                               <label class="col-md-3 control-label" style="margin-top:5px;">Currency :</label>
                                               <label class="col-md-3 control-label" style="margin-top:5px;">Rate :</label>

                                     
                                </div>
                            </div>

                            <div class="col-lg-12">
                                <div class="form-group">
                             
                                    <div class="col-md-3">
                                    <div class="input-group">
                                       <input type="text" class="width-100" id="datepicker-autoClose" placeholder="Select Date" />
                                       <span class="input-group-addon" id="datepicker-icon-trigger-PV" data-datepicker="#datepicker-autoClose"><i class="fa fa-calendar"></i></span>
                                       </div>                                                                       
                                    </div>
                                  
                                    <div class="col-md-3">
                             <select id="ddlType"  class="form-control" >
                             <option value="PVD">Payment Voucher</option>
                           <%--  <option value="PVC">Payment Receipt</option>--%>
                             </select>

                                    </div>
                                      <div class="col-md-3">
                             <select id="ddlCurrency"  class="form-control" onchange="GetRate();" >
                             <option value="PKR">PKR</option>
                             <option value="RMB">RMB</option>
                             </select>

                                    </div>
                                    <div class="col-md-3">
                                    <input type="text" id="txtCurrencyRate" value="0" class="form-control" onkeydown="SetRate();" onkeyup="SetRate();" />
                                    </div>
                                </div>
                            </div>


                        
                        </div>

                
                            <div class="row">
                                    
                                <div class="col-lg-12 table-responsive">
                                    <table id="tblItems" class="table table-striped table-bordered dataTable no-footer">
                                        <tr>
                                            <th style="width:5%">Sr1.</th>
                                            <th style="width:25%">From Account</th>                                                                                       
                                            <th style="width:30%">Narration</th>                                            
                                            <th style="width:25%">To Account</th>                                                                                   
                                            <th style="width:10%">Amount</th>
                                            <th style="width:5%"></th>
                                        </tr>
                                        <tr>
                                            <td><input type="text" id="txtSr1" value="1" disabled="disabled" class="form-control"  style="width:100%; border:0px none;"/></td>
                                            <td>                                             
                                             <input type="text" class="span6 typeahead form-control" onblur="javascript:fillUNITS(this);" style="border:0px none;" id="txtAccCode1"  data-provide="typeahead" style="width:100%" 
                                             data-items="4" >
                                            </td>                                                               
                                            <td><input type="text" id="txtNarration1" class="form-control" style="width:100%; border:0px none;" onkeyup="tot(this);"/></td>
                              <td>               <input type="text" class="span6 typeahead form-control" onblur="javascript:fillUNITS(this);" style="border:0px none;" id="txt2AccCode1"  data-provide="typeahead" style="width:100%" 
                                             data-items="4" >
                                            </td>                                                               

                                            <td><input type="text" id="txtAmount1" class="form-control" value="0"  style="width:100%; border:0px none;" /></td>
                                            <td><span class="glyphicon glyphicon-remove" aria-hidden="true" style="font-size: 20px;margin-left: 5px; cursor:pointer; color:Red;"  onmouseover='this.style.color="gray"'  onmouseout='this.style.color="Red"' title="Delete Row" id="txtDelete1" onclick="DeleteRow(this);" ></span></td>                                          
                                            
                                                                    
                                        </tr>

                                    </table>
                                </div>
                            
                            <div class="col-lg-12"><i class="fa fa-2x fa-plus-square"  title="Press Ctrl + A for new Row"  onclick="addRow();SumDbCr();"></i></div>


                            <div class="row"><div class="col-lg-12" style="height:20px"></div></div>
                             <div class="col-lg-12" >
                            
                             <input type="button" id="btnSave" style="font-weight:bold; font-size:20px;" class="btn btn-info btn-block" value="P R O C E S S &nbsp&nbsp&nbsp V O U C H E R" onclick="SaveTransaction();" />
                             </div>
                            
                        </div>
                   </div>
                   </div>
                   </div>

        <script src="../js/jquery-1.9.1.min.js"></script>
		<script src="../js/bootstrap.min.js"></script>
<script type="text/javascript">
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0!
    var yyyy = today.getFullYear();

    if (dd < 10) {
        dd = '0' + dd
    }

    if (mm < 10) {
        mm = '0' + mm
    }

    today = mm + '/' + dd + '/' + yyyy;

    document.getElementById("datepicker-autoClose").value = today;
    document.getElementById("datepicker-autoClose").focus();
    document.getElementById("datepicker-autoClose").select();

    $(document).ready(function () {
        var BranchID = localStorage.getItem("BranchID");
        var dataToSend = JSON.stringify({ 'BranchID': BranchID });
        $.ajax({
            type: "POST",
            url: "PaymentVoucherUser.aspx/LoadRMbValue",
            data: dataToSend,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                var jsdata = JSON.parse(data.d);

                $.each(jsdata, function (key, value) {
                    //alert(value.QualificationID);
                    document.getElementById("ContentPlaceHolder1_hdnRMBValue").value = value.UnitTypeID;
                    

                });


            },
            error: function (data) {
                alert("error found");
            }
        });
       // GetVoucherNo();
        
    });

    LOAD_ACCOUNTS();
    LOAD_ACCOUNTS2();
    GetRate();

    function SetRate() {
        document.getElementById("ContentPlaceHolder1_hdnRMBValue").value = document.getElementById("txtCurrencyRate").value;        
    }

    function GetRate() {
        var ddltype = document.getElementById("ddlCurrency").value;
        var BranchID = localStorage.getItem("BranchID");
        $.ajax({
            type: "POST",
            url: "PO.aspx/LoadType",
            //data: {},
            data: '{ "ddltype" : "' + ddltype + '", "BranchID" : "' + BranchID + '" }',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                var jsdata = JSON.parse(data.d);
                $.each(jsdata, function (key, value) {
                    document.getElementById("txtCurrencyRate").value = value.UnitTypeID;
                });


            },
            error: function (data) {
                alert("error found");
            }
        });
    }


    function LOAD_ACCOUNTS() {
        var BranchID = localStorage.getItem("BranchID");
        var tblRows = "";
        var UserID = "1";
        $.ajax({
            type: 'POST',
            url: 'PaymentVoucherUser.aspx/LOAD_ACCOUNTS',
            data: '{ "UserID" : "' + UserID + '" ,"BranchID" : "' + BranchID + '" }',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (msg) {
                var s = msg.d.split('`');
                tblRows = s[0];
                var b = tblRows.replace(/'/g, '"');
                document.getElementById('ContentPlaceHolder1_hdnAccounts').value = b;
                txtAccCode = document.getElementById('txtAccCode1');
                txtAccCode.dataset.source = document.getElementById('ContentPlaceHolder1_hdnAccounts').value;
                
            }

        });
    }

    function LOAD_ACCOUNTS2() {
        var BranchID = localStorage.getItem("BranchID");
        var tblRows = "";
        var UserID = "1";
        $.ajax({
            type: 'POST',
            url: 'PaymentVoucherUser.aspx/LOAD_ACCOUNTS',
            data: '{ "UserID" : "' + UserID + '" ,"BranchID" : "' + BranchID + '"  }',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (msg) {
                var s = msg.d.split('`');
                tblRows = s[0];
                var b = tblRows.replace(/'/g, '"');
                document.getElementById('ContentPlaceHolder1_hdnAccounts').value = b;
                txtAccCode = document.getElementById('txt2AccCode1');
                txtAccCode.dataset.source = document.getElementById('ContentPlaceHolder1_hdnAccounts').value;

            }

        });
    }


    function ValidateRow() {
        var accList = document.getElementById('ContentPlaceHolder1_hdnAccounts').value;
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

        var msg = cc; //  + '|' + ccDbCR + '|' + ccDate; //1+2+3
        return msg;

    }

    function ValidateRow2() {
        var accList = document.getElementById('ContentPlaceHolder1_hdnAccounts').value;
        accList = accList.replace("[", "");
        accList = accList.replace("]", "");
        accList = accList.replace(/"/g, '');

        var accArray = accList.split(',');

        var cc = 0; var ccDbCR = 0; var ccDate = 0;
        var elm = {};
        var elms = document.getElementById('tblItems').getElementsByTagName("input");
        for (var i = 0; i < elms.length; i++) {
            if (elms[i].id.indexOf('txt2AccCode') != -1) {

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

        var msg = cc; //  + '|' + ccDbCR + '|' + ccDate; //1+2+3
        return msg;

    }

    function addRow() {

        /////////////////////// VALIDATION START
        var cc = ValidateRow();
        if (cc == '1') {
            alert("Item Does not exist in Highlighted Rows! ");
            return false;
        }

        var cc2 = ValidateRow2();
        if (cc2 == '1') {
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
        txtAccCode.dataset.source = document.getElementById('ContentPlaceHolder1_hdnAccounts').value;
        cell2.appendChild(txtAccCode);


        document.getElementById("txtAccCode" + newID).focus();
        document.getElementById("txtAccCode" + newID).select();


        var cell4 = row.insertCell(2); //txtNarration
        var txtQty = document.createElement("input");
        txtQty.type = "text";
        txtQty.id = "txtNarration" + newID;
        txtQty.className = 'form-control';
        txtQty.style.width = '100%';
        txtQty.style.border = '0px none';
        cell4.appendChild(txtQty); //SumDbCr();

        var cell22 = row.insertCell(3);
        var txtAccCode2 = document.createElement("input");
        txtAccCode2.type = "text";
        txtAccCode2.id = "txt2AccCode" + newID;
        txtAccCode2.className = 'span6 typeahead form-control'; //data-source
        txtAccCode2.style.width = '100%';
        txtAccCode2.style.border = '0px none';
        txtAccCode2.dataset.provide = "typeahead";
        txtAccCode2.dataset.source = document.getElementById('ContentPlaceHolder1_hdnAccounts').value;
        cell22.appendChild(txtAccCode2);

        
        var cell7 = row.insertCell(4); //txtTotalPrice
        var txtRMBPrice = document.createElement("input");
        txtRMBPrice.type = "text";
        txtRMBPrice.id = "txtAmount" + newID;
        txtRMBPrice.className = 'form-control';
        txtRMBPrice.style.width = '100%';
        txtRMBPrice.style.border = '0px none';
        txtRMBPrice.value = '0';        
        cell7.appendChild(txtRMBPrice);


        var cell100 = row.insertCell(5); //Delete
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

    function DeleteRow(a) {
        var x = document.getElementById("tblItems").rows.length;
        if (x <= 2) {
            document.getElementById("modalbody").innerHTML = "Sorry you can not delete last record. Thanks!"
            $("#modal-dialog").modal();
            return false;
        }
        else {
            $(a).closest('tr').remove();            
        }
    }



    function GetVoucherNo() {
        var ddlValue = document.getElementById("ddlType").value;
        var BranchID = localStorage.getItem("BranchID");        
        $.ajax({
            type: "POST",
            url: "PaymentVoucherUser.aspx/LoadNUMBER",
            data: '{ "frmt" : "' + ddlValue + '" ,"tbl" : "[tbl_transaction]" ,"col" : "TaskID", "BranchID" : "' + BranchID + '"  }',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                document.getElementById("txtVoucherNo").value = data.d;


            },
            error: function (data) {
                alert("error found");
            }
        });
    }

    function SaveTransaction() {
        if (confirm("Do you want to continue?") == true) {

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

        var cc2 = ValidateRow2();
        if (cc2 == '1') {
            alert("Item Does not exist in Highlighted Rows! ");
            return false;
        }


        /////////////////////// VALIDATION END

        var UserID = localStorage.getItem("UserID");
        var txtVDate = document.getElementById('datepicker-autoClose').value;
        //var txtVoucherNo = document.getElementById('txtVoucherNo').value;
        var ddlType = document.getElementById('ddlType').value;
        var ddlCurrency = document.getElementById('ddlCurrency').value;        
        //var ddlAccountFrom = document.getElementById('ContentPlaceHolder1_ddlAccount1').value;
        //var ddlAccountTo = document.getElementById('ContentPlaceHolder1_ddlAccount2').value;
        //var txtAmount = document.getElementById('txtAmount').value;
        //var txtNarration = document.getElementById('txtNarration').value;
        var RMBValue = document.getElementById("ContentPlaceHolder1_hdnRMBValue").value;

        var elm = {}; var tMarks = 0;
        var elms = document.getElementById('tblItems').getElementsByTagName("input");
        var str = ""; var cc = 0;
        for (var i = 0; i < elms.length; i++) {
            if (elms[i].id.indexOf('txtAccCode') != -1) {

                elm = elms[i];
                var iD = elm.id;
                var txtAccCodeID = iD;
                var txt2AccCodeID = iD.replace('txtAccCode', 'txt2AccCode');
                var txtNarrationID = iD.replace('txtAccCode', 'txtNarration');
                var txtAmountID = iD.replace('txtAccCode', 'txtAmount');
                var txtSrNoID = iD.replace('txtAccCode', 'txtSr');

                var txtAccCode = document.getElementById(txtAccCodeID).value;
                var splt = txtAccCode.split('^');
                var accID = splt[0];
                var txtAccID = accID.replace(/\s/g, '');
                var txtAccTitle = splt[1];


                var txt2AccCode = document.getElementById(txt2AccCodeID).value;
                var splt2 = txt2AccCode.split('^');
                var accID2 = splt2[0];
                var txt2AccID = accID2.replace(/\s/g, '');
                var txt2AccTitle = splt2[1];
                


                var txtNarration = document.getElementById(txtNarrationID).value;
                var txtAmount = document.getElementById(txtAmountID).value;
                var txtSrNo = document.getElementById(txtSrNoID).value;


                if (txtAmount != 0) {
                    if (cc == 0) {
                        str = txtAccID + "^" + txtAccTitle + "^" + txt2AccID + "^" + txt2AccTitle + "^" + txtNarration + "^" + txtAmount + "^" + txtSrNo;
                    }
                    else {
                        str = str + "`" + txtAccID + "^" + txtAccTitle + "^" + txt2AccID + "^" + txt2AccTitle + "^" + txtNarration + "^" + txtAmount + "^" + txtSrNo;
                    }
                }

                cc = 1;


            }
            else {

            }
        }
        var BranchID = localStorage.getItem("BranchID");        
            $.ajax({
                type: 'POST',
                url: 'PaymentVoucherUser.aspx/SaveTransaction',
                //data: {},
                //data: '{ "UserID" : "' + UserID + '", "txtVDate" : "' + txtVDate + '", "txtVoucherNo" : "' + txtVoucherNo + '", "ddlType" : "' + ddlType + '", "ddlAccountFrom" : "' + ddlAccountFrom + '", "ddlAccountTo" : "' + ddlAccountTo + '", "txtAmount" : "' + txtAmount + '", "txtNarration" : "' + txtNarration + '", "RMBValue" : "' + RMBValue + '"}',
                data: '{ "UserID" : "' + UserID + '", "txtVDate" : "' + txtVDate + '",  "ddlType" : "' + ddlType + '", "RMBValue" : "' + RMBValue + '", "str" : "' + str + '",  "ddlCurrency" : "' + ddlCurrency + '",  "BranchID" : "' + BranchID + '"}',                
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (msg) {
                    tblRows = msg.d;
                    alert('Voucher has been Generated Successfully!');
                    //window.location = 'VoucherList.aspx';
                    location.reload();
                }

            });
        

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


    $(document).on('keydown', function (e) {
        var kc = e.which || e.keyCode;
        if (e.ctrlKey && String.fromCharCode(kc).toUpperCase() == "P") {
            e.preventDefault();
            SaveTransaction();
        }
    });

    $(document).on('keydown', function (e) {
        var kc = e.which || e.keyCode;

        if (e.ctrlKey && String.fromCharCode(kc).toUpperCase() == "A") {
            e.preventDefault();
            addRow();
        }
    });

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


    </script>

</asp:Content>

