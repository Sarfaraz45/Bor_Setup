<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Master.master" AutoEventWireup="true" CodeFile="POS_Old.aspx.cs" Inherits="PROCUREMENT_POS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">


<%--<script src="https://spos.tecdiary.com/themes/default/assets/plugins/jQuery/jQuery-2.1.4.min.js"></script>--%>
     <script src="../jquery-1.8.2.js" type="text/javascript"></script>
    <script src="../jquery.min.js" type="text/javascript"></script>
    <%--<link href="../styles.css" rel="stylesheet" type="text/css" />--%>
    <link rel="stylesheet" href="../Style/chosen.css" />
  <%--<script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>--%>
    <style type="text/css">
		a img{border: none;}
		ol li{list-style: decimal outside;}
		div#container{width: 780px;margin: 0 auto;padding: 1em 0;}
		div.side-by-side{width: 100%;margin-bottom: 1em;}
		div.side-by-side > div{float: left;width: 50%;}
		div.side-by-side > div > em{margin-bottom: 10px;display: block;}
		.clearfix:after{content: "\0020";display: block;height: 0;clear: both;overflow: hidden;visibility: hidden;}
		
	</style>
	
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

//        alert("A");
//        $(document).ready(function () {
//            var BranchID = localStorage.getItem("BranchID");
//            var dataToSend = JSON.stringify({ 'BranchID': BranchID });
//           // alert(dataToSend);
//            $.ajax({
//                type: "POST",
//                contentType: "application/json; charset=utf-8",
//                url: "POS.aspx/LoadRegion",
//                dataType: "json",
//                data: dataToSend,
//                success: function (data) {
//                    var jsdata = JSON.parse(data.d);
//                    var ro = "";

//                    if ($('#tablepaging').length != 0) {

//                        $('#tablepaging').remove();
//                    }

//                    ro = "<table id='tablepaging' class='table table-hover' style='cursor: pointer;' ><thead><th style='text-align: left;'>ID</th><th style='text-align: left;'>Product Name</th><th style='text-align: left;'>Product Code</th></thead><tbody>";
//                    $.each(jsdata, function (key, value) {
//                        ro += "<tr><td   class='one'>" + value.ITEMID + "</td>   <td   class='two'>" + value.ITEMName + "</td>    <td   class='three'>" + value.ItemCode + "</td>    <td  style='display:none;' class='four'>" + value.BarCode + "</td>  <td  style='display:none;'  class='five'>" + value.Discription + "</td> <td   style='display:none;' class='six'>" + value.UnitTypeID + "</td><td   style='display:none;' class='seven'>" + value.Category + "</td><td   style='display:none;' class='Cost'>" + value.Cost + "</td> <td   style='display:none;' class='Price'>" + value.Price + "</td> </tr>";

//                    });
//                    ro = ro + "</tbody></table>";

//                    $("#DivRegion").append(ro);



//                },
//                error: function (result) {
//                    alert(result);
//                }


//            });

//        });



        function LoadRegion() {
            var BranchID = localStorage.getItem("BranchID");
            var dataToSend = JSON.stringify({ 'BranchID': BranchID });
//        alert(dataToSend);
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "POS.aspx/LoadRegion",
                dataType: "json",
                data: dataToSend,
                success: function (data) {
                    var jsdata = JSON.parse(data.d);
                    var ro = "";

                    if ($('#tablepaging').length != 0) {

                        $('#tablepaging').remove();
                    }

                    ro = "<table id='tablepaging' class='table table-hover' style='cursor: pointer;' ><thead><th style='text-align: left;'>ID</th><th style='text-align: left;'>Product Name</th><th style='text-align: left;'>Product Code</th></thead><tbody>";
                    $.each(jsdata, function (key, value) {
                        ro += "<tr><td   class='one'>" + value.ITEMID + "</td>   <td   class='two'>" + value.ITEMName + "</td>    <td   class='three'>" + value.ItemCode + "</td>    <td  style='display:none;' class='four'>" + value.BarCode + "</td>  <td  style='display:none;'  class='five'>" + value.Discription + "</td> <td   style='display:none;' class='six'>" + value.UnitTypeID + "</td><td   style='display:none;' class='seven'>" + value.Category + "</td><td   style='display:none;' class='Cost'>" + value.Cost + "</td> <td   style='display:none;' class='Price'>" + value.Price + "</td> </tr>";

                    });
                    ro = ro + "</tbody></table>";

                    $("#DivRegion").append(ro);
                    

                },
                error: function (result) {
                    alert(result);
                }


            });

        };



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
                        //ro += "<div class='col-md-2' id=" + value.ITEMID + " style='text-align: center;font-size: 19px;'><a class='app app-button' style='width:100%; height: 100px;font-weight: bolder;' onclick=AddData(\'" + value.ITEMID + "'\,'Name'," + value.Price + ");><img src='../images/Userimages/" + value.ITEMID + ".jpg' style='    border-radius: 15px;    border: 2px solid #00a65a;width: 105px;    height: 105px;' ></a><br/><label id='lbl" + value.ITEMID + "'>" + value.ITEMName + "</label> </div>";
                        ro += "<div class='col-md-2' id=" + value.ITEMID + " style='text-align: center;font-size: 12px;'><a class='app app-button' style='width:10%; height: 10px;font-weight: bolder;' onclick=AddData(\'" + value.ITEMID + "'\,'Name'," + value.Price + ");><img src='../images/Userimages/" + value.ITEMID + ".jpg' style='    border-radius: 15px;    border: 2px solid #00a65a;width: 15px;    height: 15px;' ></a><br/><label id='lbl" + value.ITEMID + "'>" + value.ITEMName + "</label> </div>";
                        if (parseFloat(cc) == 6) {
                            cc = 0;
                            ro += "<div class='col-md-12' style='height:10px;'></div>";
                        }

                        //  ddl += "<option value=" + value.ITEMID + ">" + value.ITEMName + "</option>"

                       // alert(ro);
                    });
                    //  alert(ddl);
                    //$("#ContentPlaceHolder1_ddlItem").append(ddl);

                    document.getElementById("loadItems").innerHTML = ro;
                    // alert(ro);

                },
                error: function (result) {
                    alert(result);
                }


            });

        };


       
      





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
            var dataToSend = JSON.stringify({ 'ITEMName': $('#txtitemname').val(), 'ItemCode': $('#txtItemCode').val(), 'BarCode': $('#txtbarcode').val(), 'Discription': $('#txtDiscription').val(), 'UserID': UserID, 'UnitTypeID': ddl, 'Category': ddlCategory, 'Brand': ddlBrand, 'BranchID': BranchID, 'PurchasePrice': $('#txtPurchasePrice').val(), 'SalePrice': $('#txtSalePrice').val() });
           
            var results = $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "POS.aspx/InsertRegion",
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

        function DeleteRegion() {
            //  alert($('#ContentPlaceHolder1_hdnid').val());
            var UserID = localStorage.getItem("UserID");
            var BranchID = localStorage.getItem("BranchID");
            var dataToSend = JSON.stringify({ 'ITEMID': $('#ContentPlaceHolder1_hdnid').val(), 'UserID': UserID, 'BranchID': BranchID });
            var results = $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "POS.aspx/DeleteRegion",
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
            $('#txtPurchasePrice').val('0');
            $('#txtSalePrice').val('0');
            $("#btnSave").text("Save");
        }



        $("#tablepaging tbody tr").live('click', function () {

            document.getElementById("txtitemname").value = $(this).closest('tr').children('td.two').text();
            document.getElementById("txtItemCode").value = $(this).closest('tr').children('td.three').text();
            document.getElementById("txtbarcode").value = $(this).closest('tr').children('td.four').text();
            document.getElementById("txtDiscription").value = $(this).closest('tr').children('td.five').text();
            document.getElementById("ContentPlaceHolder1_DropDownList").value = $(this).closest('tr').children('td.six').text();
            document.getElementById("ContentPlaceHolder1_DropDownList1").value = $(this).closest('tr').children('td.seven').text();
            document.getElementById("txtPurchasePrice").value = $(this).closest('tr').children('td.Cost').text();
            document.getElementById("txtSalePrice").value = $(this).closest('tr').children('td.Price').text();


            $("#btnSave").text("Update");


        });

//        $("#loadItems").live('click', function () {
//            alert("a");
//        });



     //   tblCartDetail
        function AddData(a, name, rate) {
            name = document.getElementById("lbl" + a).innerHTML;
            //  var splt = name.split('^');
//            alert(a);
//            alert(rate)
            
            //var str = "Hello world!";
            var res = name.slice(0, 15);
//            alert(res);
//            alert(name);
     
            
                      
            if ($('#tr' + a).length) {
                //var dt = document.getElementById("tr" + a).innerHTML;
                var qt = document.getElementById("qt" + a).value;
                document.getElementById("qt" + a).value = parseFloat(qt) + 1;
                //document.getElementById("qttd" + a).innerHTML = parseFloat(qt) + 1;
                
                var st = a;
                //alert(st);
                //alert(document.getElementById("st" + st).value);
                document.getElementById("st" + st).value = parseFloat(document.getElementById("rt" + st).value) * parseFloat(document.getElementById("qt" + st).value);
                }
            else {
                //alert(document.getElementById("qt" + a).value);
                //alert(document.getElementById("qt" + a).innerHTML);
                var abc = "<tr id='tr" + a + "'><td><button type='button' class='btn bg-purple btn-block btn-xs edit' id='btn" + a + "' data-item='2' style='font-size: 16px;'><span class='sname' id='sp" + a + "'>" + name + "</span></button></td><td class='text-right'><input class='form-control' value=" + rate + " id='rt" + a + "' onkeyup=tot(\'" + a + "'\); /></td><td id='qttd" + a + "' ><input class='form-control' name='qty' value='1' id='qt" + a + "' onkeyup=tot(\'" + a + "'\); /></td><td class='text-right'><input class='form-control' id='st" + a + "' name='amt' value='0' /></td><td class='text-center'><i class='fa fa-trash-o tip pointer posdel' id='dl" + a + "' title='Remove' onclick=Remove(\'" + a + "'\);></i></td></tr>";
                //alert(abc);
                //alert(document.getElementById("tblCartDetail").innerHTML);
                document.getElementById("tblCartDetail").innerHTML = document.getElementById("tblCartDetail").innerHTML + abc;

                var st = a;
                //alert(st);                
                document.getElementById("st" + st).value = parseFloat(document.getElementById("rt" + st).value) * parseFloat(document.getElementById("qt" + st).value);


//                var elms = document.getElementById('tblCartDetail').getElementsByTagName("input");
//                
//        for (var i = 0; i < elms.length; i++) {
//            if (elms[i].id.indexOf('rtITM') != -1) {
//                elm = elms[i];
//                var iD = elm.id;
//                
//                var txtAccCodeID = iD;
//                var txtQtyID = iD.replace('rt', 'qt');
//                
//                var txtQty = document.getElementById(txtQtyID).value;
//                alert(txtQty);
//            }
//        }

                var arr = document.getElementsByName('qty');
                var totvalue = 0;
                //alert(arr.length);
                for (var i = 0; i < arr.length; i++) {
                    //alert(arr[i].text);
                    if (parseInt(arr[i].value))                        
                        totvalue += parseInt(arr[i].value);
                }

                var arr1 = document.getElementsByName('amt');
                var totvalue1 = 0;
                for (var i = 0; i < arr1.length; i++) {
                    if (parseInt(arr1[i].value))
                        totvalue1 += parseInt(arr1[i].value);
                }

                document.getElementById("countqty").innerHTML = totvalue;
                document.getElementById("countprice").innerHTML = totvalue1;
                document.getElementById("countpriceTotal").innerHTML = totvalue1;



                document.getElementById("item_count").innerHTML = totvalue;
                document.getElementById("twt").innerHTML = totvalue1;
                document.getElementById("total_paying").innerHTML = totvalue1;
                document.getElementById("quick-payable").innerHTML = totvalue1;
                document.getElementById("amount").value = totvalue1;
                
                
            
                
            }

            //alert(abc);
        }



        function tot(a) {
            var st = a;
            //st = st.repalce("st", '');
            //alert(st);
            document.getElementById("st" + st).value = parseFloat(document.getElementById("rt" + st).value) * parseFloat(document.getElementById("qt" + st).value);


            var arr = document.getElementsByName('qty');
            var totvalue = 0;
            for (var i = 0; i < arr.length; i++) {
                if (parseInt(arr[i].value))
                    totvalue += parseInt(arr[i].value);
            }

            var arr1 = document.getElementsByName('amt');
            var totvalue1 = 0;
            for (var i = 0; i < arr1.length; i++) {
                if (parseInt(arr1[i].value))
                    totvalue1 += parseInt(arr1[i].value);
            }

            document.getElementById("countqty").innerHTML = totvalue;
            document.getElementById("countprice").innerHTML = totvalue1;
            document.getElementById("countpriceTotal").innerHTML = totvalue1;

            document.getElementById("item_count").innerHTML = totvalue;
            document.getElementById("twt").innerHTML = totvalue1;
            document.getElementById("total_paying").innerHTML = totvalue1;
            document.getElementById("quick-payable").innerHTML = totvalue1;
            document.getElementById("amount").value = totvalue1;
         
        }


        function Remove(a) {
            //alert(a);
            var abc = "tr" + a;
            $('#' + abc).remove();

            var arr = document.getElementsByName('qty');
            var totvalue = 0;
            for (var i = 0; i < arr.length; i++) {
                if (parseInt(arr[i].value))
                    totvalue += parseInt(arr[i].value);
            }

            var arr1 = document.getElementsByName('amt');
            var totvalue1 = 0;
            for (var i = 0; i < arr1.length; i++) {
                if (parseInt(arr1[i].value))
                    totvalue1 += parseInt(arr1[i].value);
            }

            document.getElementById("countqty").innerHTML = totvalue;
            document.getElementById("countprice").innerHTML = totvalue1;
            document.getElementById("countpriceTotal").innerHTML = totvalue1;


            document.getElementById("item_count").innerHTML = totvalue;
            document.getElementById("twt").innerHTML = totvalue1;
            document.getElementById("total_paying").innerHTML = totvalue1;
            document.getElementById("quick-payable").innerHTML = totvalue1;
            document.getElementById("amount").value = totvalue1;

        }


        function SetAmount() {
            document.getElementById("amount").value = document.getElementById("quick-payable").innerHTML;

            var tt = document.getElementById("amount").value;
            var pp = document.getElementById("twt").innerHTML;

            document.getElementById("balance").innerHTML = parseFloat(tt) - parseFloat(pp);
            document.getElementById("total_paying").innerHTML = document.getElementById("amount").value;
            
        }

        function SetAmountNew(a) {
            document.getElementById("amount").value = parseFloat(document.getElementById("amount").value) + a;
            //document.getElementById("btn" + a).innerHTML = a + "<span class='badge'>1</span>";

            document.getElementById("total_paying").innerHTML = document.getElementById("amount").value;

            var tt = document.getElementById("amount").value;
            var pp = document.getElementById("twt").innerHTML;

            document.getElementById("balance").innerHTML = parseFloat(tt) - parseFloat(pp);

        }


        function SetAmountClear() {
            document.getElementById("amount").value = "0";
            document.getElementById("total_paying").innerHTML = "0";

            var tt = document.getElementById("amount").value;
            var pp = document.getElementById("twt").innerHTML;

            document.getElementById("balance").innerHTML = parseFloat(tt) - parseFloat(pp);



        }


        function SetAmountManual() {

            document.getElementById("total_paying").innerHTML = document.getElementById("amount").value;
            var tt = document.getElementById("amount").value;
            var pp = document.getElementById("twt").innerHTML;

            document.getElementById("balance").innerHTML = parseFloat(tt) - parseFloat(pp);
            
        }


        function SetType() {
            var type = document.getElementById("paid_by").value;
            //alert(type);

            if (type == "GC") {
                document.getElementById("DivGift").style.display = "block";
                document.getElementById("DivCash").style.display = "none";
                document.getElementById("DivCC").style.display = "none";
                document.getElementById("DivChq").style.display = "none";

            }
            else if (type == "CS") {
                document.getElementById("DivGift").style.display = "none";
                document.getElementById("DivCash").style.display = "block";
                document.getElementById("DivCC").style.display = "none";
                document.getElementById("DivChq").style.display = "none";
            }
            else if (type == "CC") {
                document.getElementById("DivGift").style.display = "none";
                document.getElementById("DivCash").style.display = "none";
                document.getElementById("DivCC").style.display = "block";
                document.getElementById("DivChq").style.display = "none";
            }
            else if (type == "CQ") {
                document.getElementById("DivGift").style.display = "none";
                document.getElementById("DivCash").style.display = "none";
                document.getElementById("DivCC").style.display = "none";
                document.getElementById("DivChq").style.display = "block";
            }
            else {
                document.getElementById("DivGift").style.display = "none";
                document.getElementById("DivCash").style.display = "block";
                document.getElementById("DivCC").style.display = "none";
                document.getElementById("DivChq").style.display = "none";
            }
        }


        function SaveSale() {
            var str = ""; var cc = 0;
            var elms = document.getElementById('tblCartDetail').getElementsByTagName("input");
        var str = ""; var cc = 0;
        for (var i = 0; i < elms.length; i++) {
            if (elms[i].id.indexOf('rtITM') != -1) {
                elm = elms[i];
                var iD = elm.id;
                var txtAccCodeID = iD;                
                var txtQtyID = iD.replace('rt', 'qt');
                var txtPriceID = iD.replace('rt', 'st');

                var itmid = iD.replace('rt', '');

                var txtQty = document.getElementById(txtQtyID).value;
                var txtRate = document.getElementById(txtAccCodeID).value;
                var txtTotalPrice = document.getElementById(txtPriceID).value;


                if (txtQty != 0) {
                    if (cc == 0) {
                        str = itmid + "^" + txtQty + "^" + txtRate + "^" + txtTotalPrice;
                    }
                    else {
                        str = str + "`" + itmid + "^" + txtQty + "^" + txtRate + "^" + txtTotalPrice;
                    }
                }
                cc = 1;
            }
        }
        var BranchID = localStorage.getItem("BranchID");
        var UserID = localStorage.getItem("UserID");
        var txtTotAmount = document.getElementById("twt").innerHTML;
        var txtGrandTotal = document.getElementById("twt").innerHTML;
        var txtDiscount = "0";
        var txtTaxRate = "0";
        var txtTotTax = "0";
        var txtDiscountPKR = "0";
        var txtCusAmountPKR = document.getElementById("twt").innerHTML;
        var ddlSupplier = document.getElementById("ContentPlaceHolder1_ddlSupplier").value;
        var ddlShop = "0";
        var txtCustomerName = "Walk In Client";
        var txtContactNo = "0";
        var txtAddress = "0";
        var txtBillNo = "0";
//        var Store = document.getElementById("ContentPlaceHolder1_ddlItem").value;
        if (cc > 0) {
            $.ajax({
                type: 'POST',
                url: 'POS.aspx/SaveTransaction',
                //data: {},
                data: '{ "UserID" : "' + UserID + '", "str" : "' + str + '","txtTotAmount" : "' + txtTotAmount + '", "txtDiscount" : "' + txtDiscount + '", "txtTaxRate" : "' + txtTaxRate + '", "txtTotTax" : "' + txtTotTax + '", "txtGrandTotal" : "' + txtGrandTotal + '", "ddlSupplier" : "' + ddlSupplier + '",  "txtDiscountPKR" : "' + txtDiscountPKR + '", "txtCusAmountPKR" : "' + txtCusAmountPKR + '",  "ddlShop" : "' + ddlShop + '", "txtCustomerName" : "' + txtCustomerName + '", "txtContactNo" : "' + txtContactNo + '","txtAddress" : "' + txtAddress + '", "txtBillNo" : "' + txtBillNo + '", "BranchID" : "' + BranchID + '"}',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (msg) {
                    tblRows = msg.d;
                    GenerateSlip();
                    //alert('SO has been Generated Successfully!');
                    //   window.location = 'SO_LIST.aspx';
                    //location.reload();
                }

            });
        }

    }


    function GenerateSlip() {        
            var tblRows = "";
            var UserID = localStorage.getItem("UserID");
            var BranchID = localStorage.getItem("BranchID");
            $.ajax({
                type: 'POST',
                url: 'POS.aspx/LoadSlip',
                data: '{ "UserID" : "' + UserID + '", "BranchID" : "' + BranchID + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (msg) {
                    var s = msg.d;
                    document.getElementById("msgbody").innerHTML = s;
                    document.getElementById("MainWindow").style.display = "none";
                    document.getElementById("header").style.display = "none";
                    document.getElementById("footer").style.display = "none";
                    
                    
                    $("#modal-dialog").modal();
                    //alert(s);


                }

            });

        }

        function CloseWIndow() {
            location.reload();
        }



         
            
        function GetCode() {
            var id = document.getElementById("hold_ref").value;
            var UserID = localStorage.getItem("UserID");
            var BranchID = localStorage.getItem("BranchID");
            //alert(BranchID);
            $.ajax({
                type: "POST",
                url: "POS.aspx/LoadItemData",
                data: '{ "UserID" : "' + UserID + '", "BranchID" : "' + BranchID + '" , "Code" : "' + id + '" }',
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var jsdata = JSON.parse(data.d);
                    var rows = "";
                    $.each(jsdata, function (key, value) {
                       alert(value.ITEMID);
                        //rows += "<option  value=" + value.UnitTypeID + ">" + value.UnitTypeDesc + "</option>";
                        AddData(value.ITEMID, 'Name', value.Price);
                    });

                },
                error: function (data) {
                    alert("error found");
                }
            });
            
        }
    </script>

    

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="MainWindow">
                                <div class="col-md-6">
                <table style="width:100%;" class="layout-table">
                    <tbody><tr>
                        <td style="width: 460px;">

                            <div id="pos" style="margin-top:-159px;">
                                

                                <div class="well well-sm" id="leftdiv">
                                    <div id="lefttop" style="margin-bottom:5px;">
                                        <div class="form-group" style="margin-bottom:5px;">
                                            <div class="input-group">
                                                                                                
<%--<select name="customer_id" id="spos_customer" data-placeholder="Select Customer" required="required" class="form-control select2 select2-hidden-accessible" style="width:100%;position:absolute;" tabindex="-1" aria-hidden="true">
<option value="1">Walk-in Client</option>
</select>--%>
  <asp:DropDownList ID="ddlSupplier" runat="server" DataSourceID="SqlDataSource1"  CssClass="chzn-select form-control"
                                            DataTextField="AccountsTitle" DataValueField="AccountsID">
                                </asp:DropDownList>


                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:Con %>" 
                                            SelectCommand="SELECT AccountsID, AccountsTitle  FROM [VW_ACCOUNTS_NAME_WITH_BALANCE]
                                            where BranchID1=@BranchID order by AccountsTitle">
                                            <SelectParameters>
                                                <asp:CookieParameter CookieName="BranchID" Name="BranchID"  Type="String" />
                                            </SelectParameters>                                            
                                        </asp:SqlDataSource>

<%--<span class="select2 select2-container select2-container--default" dir="ltr" style="width: 100%;"><span class="selection"><span class="select2-selection select2-selection--single" role="combobox" aria-autocomplete="list" aria-haspopup="true" aria-expanded="false" tabindex="0" aria-labelledby="select2-spos_customer-container"><span class="select2-selection__rendered" id="select2-spos_customer-container" title="Walk-in Client">Walk-in Client</span><span class="select2-selection__arrow" role="presentation"><b role="presentation"></b></span></span></span><span class="dropdown-wrapper" aria-hidden="true"></span></span>--%>
                                                <div class="input-group-addon no-print" style="padding: 2px 5px;">
                                                    <a href="../ERP/Accounts.aspx" id="add-customer" class="external" ><i class="fa fa-2x fa-plus-circle" id="addIcon"></i></a>
                                                </div>
                                            </div>
                                            <div style="clear:both;"></div>
                                        </div>
                                                                                <div class="form-group" style="margin-top:80px;">
                                            <input type="text" name="hold_ref" value="" id="hold_ref" class="form-control kb-text" placeholder="Search by Barcode" onchange="GetCode();">
                                        </div>
                                      <%--  <div class="form-group" style="margin-bottom:5px;">
                                            
                                             <asp:DropDownList ID="ddlItem" runat="server"  CssClass="chzn-select form-control"
                                             DataSourceID="SqlDataSource2"  
                                            DataTextField="StoreTitle" DataValueField="StoreID">
                                </asp:DropDownList>
                                 <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:Con %>" 
                                            SelectCommand="select '0' AS 'StoreID', 'Select Store' AS StoreTitle UNION SELECT StoreID, StoreTitle  FROM Store
                                            where BranchID=@BranchID and IsDelete=0 order by StoreID">
                                            <SelectParameters>
                                                <asp:CookieParameter CookieName="BranchID" Name="BranchID"  Type="String" />
                                            </SelectParameters>                                            
                                        </asp:SqlDataSource>
                                        </div>--%>
                                    </div>
                                    <div style="height:350px;overflow:auto;" id="tblCartMaster">
                                         <table class="table table-striped table-condensed table-hover list-table" style="margin:0;">
                                                    <thead>
                                                        <tr class="success">
                                                            <th>Product</th>
                                                            <%--<th style="width: 15%;text-align:center;">Price</th>
                                                            <th style="width: 15%;text-align:center;">Qty</th>
                                                            <th style="width: 20%;text-align:center;">Subtotal</th>
                                                            <th style="width: 20px;" class="satu"><i class="fa fa-trash-o"></i></th>--%>
                                                            <th style="text-align:center;">Price</th>
                                                            <th style="text-align:center;">Qty</th>
                                                            <th style="text-align:center;">Subtotal</th>
                                                            <th   class="satu"><i class="fa fa-trash-o"></i></th>
                                                        </tr>
                                                    </thead>
                                                </table>
                                                                      <table id="tblCartDetail" class="table table-striped table-condensed table-hover list-table" style="margin:0px;" data-height="100">                                              
                                                
                                            </table>
                                    </div>
                                    <div id="printhead" class="print" style="height: 350px; display:none;">
                                      <%--  <h2><strong>Simple POS</strong></h2>
       My Shop Lot, Shopping Mall,<br>
                                                                                              Post Code, City<br>                                        <p>Date: Mon 10 Sep 2018</p>--%>
                                    </div>
                                    <div id="print" class="fixed-table-container" style="display:none;">
                                        <div class="slimScrollDiv" style="position: relative; overflow: hidden; width: auto; height: 250px;"><div id="list-table-div" style="overflow: hidden; width: auto; height: 0px;">
                                            <div class="fixed-table-header">
                                                <table class="table table-striped table-condensed table-hover list-table" style="margin:0;">
                                                    <thead>
                                                        <tr class="success">
                                                            <th>Product</th>
                                                            <th style="width: 15%;text-align:center;">Price</th>
                                                            <th style="width: 15%;text-align:center;">Qty</th>
                                                            <th style="width: 20%;text-align:center;">Subtotal</th>
                                                            <th style="width: 20px;" class="satu"><i class="fa fa-trash-o"></i></th>
                                                        </tr>
                                                    </thead>
                                                </table>
                                            </div>
                                            <table id="posTable" class="table table-striped table-condensed table-hover list-table" style="margin:0px;" data-height="100">
                                                <thead>
                                                    <tr class="success">
                                                        <th>Product</th>
                                                        <th style="width: 15%;text-align:center;">Price</th>
                                                        <th style="width: 15%;text-align:center;">Qty</th>
                                                        <th style="width: 20%;text-align:center;">Subtotal</th>
                                                        <th style="width: 20px;" class="satu"><i class="fa fa-trash-o"></i></th>
                                                    </tr>
                                                </thead>
                                                <tbody><tr id="1536510783572" class="2" data-item-id="2" data-id="2"><td><input name="product_id[]" type="hidden" class="rid" value="2"><input name="item_comment[]" type="hidden" class="ritem_comment" value=""><input name="product_code[]" type="hidden" value="TOY02"><input name="product_name[]" type="hidden" value="Minion Banana"><button type="button" class="btn bg-purple btn-block btn-xs edit" id="1536510783572" data-item="2"><span class="sname" id="name_1536510783572">Minion Banana (TOY02)</span></button></td><td class="text-right"><input class="realuprice" name="real_unit_price[]" type="hidden" value="15.00"><input class="rdiscount" name="product_discount[]" type="hidden" id="discount_1536510783572" value="0"><span class="text-right sprice" id="sprice_1536510783572">15.00</span></td><td><input name="item_was_ordered[]" type="hidden" class="riwo" value="0"><input class="form-control input-qty kb-pad text-center rquantity" name="quantity[]" type="text" value="1" data-id="1536510783572" data-item="2" id="quantity_1536510783572" onclick="this.select();"></td><td class="text-right"><span class="text-right ssubtotal" id="subtotal_1536510783572">15.00</span></td><td class="text-center"><i class="fa fa-trash-o tip pointer posdel" id="1536510783572" title="Remove"></i></td></tr><tr id="1536510783566" class="1" data-item-id="1" data-id="1"><td><input name="product_id[]" type="hidden" class="rid" value="1"><input name="item_comment[]" type="hidden" class="ritem_comment" value=""><input name="product_code[]" type="hidden" value="TOY01"><input name="product_name[]" type="hidden" value="Minion Hi"><button type="button" class="btn bg-purple btn-block btn-xs edit" id="1536510783566" data-item="1"><span class="sname" id="name_1536510783566">Minion Hi (TOY01)</span></button></td><td class="text-right"><input class="realuprice" name="real_unit_price[]" type="hidden" value="15.00"><input class="rdiscount" name="product_discount[]" type="hidden" id="discount_1536510783566" value="0"><span class="text-right sprice" id="sprice_1536510783566">15.00</span></td><td><input name="item_was_ordered[]" type="hidden" class="riwo" value="0"><input class="form-control input-qty kb-pad text-center rquantity" name="quantity[]" type="text" value="2" data-id="1536510783566" data-item="1" id="quantity_1536510783566" onclick="this.select();"></td><td class="text-right"><span class="text-right ssubtotal" id="subtotal_1536510783566">30.00</span></td><td class="text-center"><i class="fa fa-trash-o tip pointer posdel" id="1536510783566" title="Remove"></i></td></tr><tr id="1536510783559" class="0" data-item-id="0" data-id="0"><td><input name="product_id[]" type="hidden" class="rid" value="0"><input name="item_comment[]" type="hidden" class="ritem_comment" value=""><input name="product_code[]" type="hidden" value="1111 1111 1111 1111"><input name="product_name[]" type="hidden" value="Gift Card"><button type="button" class="btn bg-purple btn-block btn-xs edit" id="1536510783559" data-item="0"><span class="sname" id="name_1536510783559">Gift Card (1111 1111 1111 1111)</span></button></td><td class="text-right"><input class="realuprice" name="real_unit_price[]" type="hidden" value="100"><input class="rdiscount" name="product_discount[]" type="hidden" id="discount_1536510783559" value="0"><span class="text-right sprice" id="sprice_1536510783559">100.00</span></td><td><input name="item_was_ordered[]" type="hidden" class="riwo" value="0"><input class="form-control input-qty kb-pad text-center rquantity" name="quantity[]" type="text" value="1" data-id="1536510783559" data-item="0" id="quantity_1536510783559" onclick="this.select();"></td><td class="text-right"><span class="text-right ssubtotal" id="subtotal_1536510783559">100.00</span></td><td class="text-center"><i class="fa fa-trash-o tip pointer posdel" id="1536510783559" title="Remove"></i></td></tr></tbody>
                                            </table>
                                        </div><div class="slimScrollBar" style="background: rgb(0, 0, 0); width: 7px; position: absolute; top: 0px; opacity: 0.4; display: none; border-radius: 7px; z-index: 99; right: 1px; height: 160px;"></div><div class="slimScrollRail" style="width: 7px; height: 100%; position: absolute; top: 0px; display: none; border-radius: 7px; background: rgb(51, 51, 51); opacity: 0.2; z-index: 90; right: 1px;"></div></div>
                                        <div style="clear:both;"></div>
                                        <div id="totaldiv">
                                            <table id="totaltbl" class="table table-condensed totals" style="margin-bottom:10px;">
                                                <tbody>
                                                    <tr class="info">
                                                        <td width="25%">Total Items</td>
                                                        <td class="text-right" style="padding-right:10px;"><span id="count">0 (0.00)</span></td>
                                                        <td width="25%">Total</td>
                                                        <td class="text-right" colspan="2"><span id="total">00.00</span></td>
                                                    </tr>
                                                    <tr class="info" style="display:none;">
                                                        <td width="25%"><a href="#" id="add_discount">Discount</a></td>
                                                        <td class="text-right" style="padding-right:10px;"><span id="ds_con">(0.00) 0.00</span></td>
                                                        <td width="25%"><a href="#" id="add_tax">Order Tax</a></td>
                                                        <td class="text-right"><span id="ts_con">7.25</span></td>
                                                    </tr>
                                                    <tr class="success">
                                                        <td colspan="2" style="font-weight:bold;">
                                                            Total Payable                                                            <a role="button" data-toggle="modal" data-target="#noteModal">
                                                                <i class="fa fa-comment"></i>
                                                            </a>
                                                        </td>
                                                        <td class="text-right" colspan="2" style="font-weight:bold;"><span id="total-payable">152.25</span></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div id="Div2">
                                            <table id="Table2" class="table table-condensed totals" style="margin-bottom:10px;">
                                                <tbody>
                                                    <tr class="info">
                                                        <td width="25%">Total Items</td>
                                                        <td class="text-right" style="padding-right:10px;"><span id="countqty">0 (0.00)</span></td>
                                                        <td width="25%">Total</td>
                                                        <td class="text-right" colspan="2"><span id="countprice">0.00</span></td>
                                                    </tr>
                                                    <tr class="info" style="display:none;">
                                                        <td width="25%"><a href="#" id="a3">Discount</a></td>
                                                        <td class="text-right" style="padding-right:10px;"><span id="Span14">(0.00) 0.00</span></td>
                                                        <td width="25%"><a href="#" id="a4">Order Tax</a></td>
                                                        <td class="text-right"><span id="Span15">7.25</span></td>
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
                                    
                                        <div class="row">
                                            <div class="col-xs-4" style="padding: 0;">
                                                <div class="btn-group-vertical btn-block">
                                                    <button type="button" class="btn btn-warning btn-block btn-flat" id="suspend">Hold</button>
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

                                    </div>
                                    <div class="clearfix"></div>
                                    <span id="hidesuspend"></span>
                                    <input type="hidden" name="spos_note" value="" id="spos_note">

                                    <div id="payment-con">
                                        <input type="hidden" name="amount" id="amount_val" value="">
                                        <input type="hidden" name="balance_amount" id="balance_val" value="">
                                        <input type="hidden" name="paid_by" id="paid_by_val" value="cash">
                                        <input type="hidden" name="cc_no" id="cc_no_val" value="">
                                        <input type="hidden" name="paying_gift_card_no" id="paying_gift_card_no_val" value="">
                                        <input type="hidden" name="cc_holder" id="cc_holder_val" value="">
                                        <input type="hidden" name="cheque_no" id="cheque_no_val" value="">
                                        <input type="hidden" name="cc_month" id="cc_month_val" value="">
                                        <input type="hidden" name="cc_year" id="cc_year_val" value="">
                                        <input type="hidden" name="cc_type" id="cc_type_val" value="">
                                        <input type="hidden" name="cc_cvv2" id="cc_cvv2_val" value="">
                                        <input type="hidden" name="balance" id="balance_val" value="">
                                        <input type="hidden" name="payment_note" id="payment_note_val" value="">
                                    </div>
                                    <input type="hidden" name="customer" id="customer" value="3">
                                    <input type="hidden" name="order_tax" id="tax_val" value="5%">
                                    <input type="hidden" name="order_discount" id="discount_val" value="0">
                                    <input type="hidden" name="count" id="total_item" value="">
                                    <input type="hidden" name="did" id="is_delete" value="0">
                                    <input type="hidden" name="eid" id="is_delete" value="0">
                                    <input type="hidden" name="total_items" id="total_items" value="0">
                                    <input type="hidden" name="total_quantity" id="total_quantity" value="0">
                                    <input type="submit" id="submit" value="Submit Sale" style="display: none;">
                                </div>
                                                         </div>

                        </td>
                        <td style="background:white;">
                            <%--<div class="contents" id="right-col" style="height: 350px;    margin-top: -133px;">--%>
                            <div class="contents" id="right-col" style="height: 350px;">
                            <div class="row" id="loadItems" style="margin-top: 13px;">

                            </div>
                              
                            </div>
                        </td>
                    </tr>
                </tbody></table>
                </div>

                <div class="modal" data-easein="flipYIn" id="payModal" tabindex="-1" role="dialog" aria-labelledby="payModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-success">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true"><i class="fa fa-times"></i></button>
                <h4 class="modal-title" id="payModalLabel">
                    Payment                </h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-xs-9">
                        <div class="font16" style="margin-bottom: 10px;">
                            <table class="table table-bordered table-condensed" style="background: white;color: black;font-weight: bolder;">
                                <tbody>
                                    <tr>
                                        <td width="25%" style="border-right-color: #FFF !important;">Total Items</td>
                                        <td width="25%" class="text-right"><span id="item_count">0.00</span></td>
                                        <td width="25%" style="border-right-color: #FFF !important;">Total Payable</td>
                                        <td width="25%" class="text-right"><span id="twt">0.00</span></td>
                                    </tr>
                                    <tr>
                                        <td style="border-right-color: #FFF !important;">Total Paying</td>
                                        <td class="text-right"><span id="total_paying">0.00</span></td>
                                        <td style="border-right-color: #FFF !important;">Balance</td>
                                        <td class="text-right"><span id="balance">0.00</span></td>
                                    </tr>
                                </tbody>
                            </table>
                            <div class="clearfix"></div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="form-group">
                                    <label for="note">Note</label>                                    <textarea name="note" id="note" class="pa form-control kb-text"></textarea>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-6">
                                <div class="form-group">
                                    <label for="amount">Amount</label>                                    <input name="amount" type="text" id="amount" class="pa form-control kb-pad amount" onkeyup="SetAmountManual();">
                                </div>
                            </div>
                            <div class="col-xs-6">
                                <div class="form-group">
                                    <label for="paid_by">Paying by</label>                                   
                                    <select id="paid_by" class="form-control" style="width:100%;" onchange="SetType();" >
                                        <option value="CS">Cash</option>
                                        <option value="CC">Credit Card</option>
                                        <option value="CQ">Cheque</option>
                                        <option value="GC">Gift Card</option>
                                        <option value="ST">Stripe</option>                                        <option value="OT">Other</option>
                                    </select>
                                    <%--<span class="select2 select2-container select2-container--default" dir="ltr" style="width: 100%;">
                                    <span class="selection"><span class="select2-selection select2-selection--single" role="combobox" aria-autocomplete="list" aria-haspopup="true" aria-expanded="false" tabindex="0" aria-labelledby="select2-paid_by-container"><span class="select2-selection__rendered" id="select2-paid_by-container" title="Cash">Cash</span><span class="select2-selection__arrow" role="presentation"><b role="presentation"></b></span>
                                    </span></span><span class="dropdown-wrapper" aria-hidden="true"></span></span>--%>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="form-group gc" style="display: none;" id="DivGift">
                                    <label for="gift_card_no">Gift Card No</label>                                    <input type="text" id="gift_card_no" class="pa form-control kb-pad gift_card_no gift_card_input">

                                    <div id="gc_details"></div>
                                </div>
                                <div class="pcc" style="display:none;" id="DivCC">
                                    <div class="form-group">
                                        <input type="text" id="swipe" class="form-control swipe swipe_input" placeholder="Swipe card here then write security code manually">
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-6">
                                            <div class="form-group">
                                                <input type="text" id="pcc_no" class="form-control kb-pad" placeholder="Credit Card No">
                                            </div>
                                        </div>
                                        <div class="col-xs-6">
                                            <div class="form-group">

                                                <input type="text" id="pcc_holder" class="form-control kb-text" placeholder="Holder Name">
                                            </div>
                                        </div>
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                <select id="pcc_type" class="form-control" placeholder="Card Type" tabindex="-1" aria-hidden="true">
                                                <option value="Visa">Visa</option>
                                                <option value="MasterCard">MasterCard</option>
                                                <option value="Amex">Amex</option>
                                                <option value="Discover">Discover</option>
                                            </select>
                                            <%--<span class="select2 select2-container select2-container--default" dir="ltr" style="width: 100px;"><span class="selection"><span class="select2-selection select2-selection--single" role="combobox" aria-autocomplete="list" aria-haspopup="true" aria-expanded="false" tabindex="0" aria-labelledby="select2-pcc_type-container"><span class="select2-selection__rendered" id="select2-pcc_type-container" title="Visa">Visa</span><span class="select2-selection__arrow" role="presentation"><b role="presentation"></b></span></span></span><span class="dropdown-wrapper" aria-hidden="true"></span></span>--%>
                                        </div>
                                    </div>
                                    <div class="col-xs-3">
                                        <div class="form-group">
                                            <input type="text" id="pcc_month" class="form-control kb-pad" placeholder="Month">
                                        </div>
                                    </div>
                                    <div class="col-xs-3">
                                        <div class="form-group">

                                            <input type="text" id="pcc_year" class="form-control kb-pad" placeholder="Year">
                                        </div>
                                    </div>
                                    <div class="col-xs-3">
                                        <div class="form-group">

                                            <input type="text" id="pcc_cvv2" class="form-control kb-pad" placeholder="CVV2">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="pcheque" style="display:none;" id="DivChq">
                                <div class="form-group"><label for="cheque_no">Cheque No</label>                                    <input type="text" id="cheque_no" class="form-control cheque_no kb-text">
                                </div>
                            </div>
                            <div class="pcash" id="DivCash">
                                <div class="form-group"><label for="payment_note">Payment Note</label>                                    <input type="text" id="payment_note" class="form-control payment_note kb-text">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xs-3 text-center">
                    <!-- <span style="font-size: 1.2em; font-weight: bold;">Quick Cash</span> -->

                    <div class="btn-group btn-group-vertical" style="width:100%;">
                        <button type="button" class="btn btn-info btn-block quick-cash" id="quick-payable" onclick="SetAmount();">0.00
                        </button>
                        <button type="button" class="btn btn-block btn-warning quick-cash" id="btn10" onclick="SetAmountNew(10);">10</button>
                        <button type="button" class="btn btn-block btn-warning quick-cash" id="btn20" onclick="SetAmountNew(20);">20</button>
                        <button type="button" class="btn btn-block btn-warning quick-cash" id="btn50" onclick="SetAmountNew(50);">50</button>
                        <button type="button" class="btn btn-block btn-warning quick-cash" id="btn100" onclick="SetAmountNew(100);">100</button>
                        <button type="button" class="btn btn-block btn-warning quick-cash" id="btn500" onclick="SetAmountNew(500);">500</button>  
                                              <button type="button" class="btn btn-block btn-danger" id="clear-cash-notes" onclick="SetAmountClear();">Clear</button>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal-footer">
            <button type="button" class="btn btn-default pull-left" data-dismiss="modal"> Close </button>
            <button class="btn btn-primary"  onclick="SaveSale();" type="button">Submit</button>
        </div>
    </div>
</div>
</div>
 <div class="col-md-6">
 <h1>Test </h1>

 </div>

</div>

-----
  <div class="modal fade" id="modal-dialog">
								<div class="modal-dialog">
									<div class="modal-content">
									<%--	<div class="modal-header"  style="background-color:White; border-bottom:1px solid #D5D5D5">
											<button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
											<h4 class="modal-title">Sale successfully added</h4>
										</div>--%>
										<div class="modal-body" id="msgbody" style=" border-bottom:1px solid #D5D5D5;">
											
										</div>
										<div class="modal-footer"  style="background-color:White;">
                                        
                                        <a href="javascript:;" onclick="window.print();" class="btn btn-block btn-primary">Print</button>
											<a href="javascript:;"  class="btn btn-block btn-warning" data-dismiss="modal"  id="SaveClose" onclick="CloseWIndow();">Close</a>
											<%--<a href="javascript:;" class="btn btn-sm btn-success" onclick="SaveData();" id="confirm">Confirm (Ctrl + S)</a>--%>
										</div>
									</div>
								</div>
							</div>

    
    
		
	
         
</asp:Content>

