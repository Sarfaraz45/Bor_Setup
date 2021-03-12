<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Master.master" AutoEventWireup="true" CodeFile="Unit.aspx.cs" Inherits="ERP_Unit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">



    <script src="../jquery-1.8.2.js"></script>
    <script src="../jquery.min.js"></script>

    <style type="text/css">
        .highlight {
            background: #cfd1d3;
        }

        .nonhighlight {
            background: white;
        }

        .pg-normal {
            cursor: pointer;
            background: #fff;
            padding: 8px 15px 8px 15px;
            margin: 20px 0;
            border-radius: 4px;
            display: inline-block;
            box-sizing: border-box;
            margin-bottom: 0 !important;
            margin-top: 0 !important;
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
            margin-bottom: 0 !important;
            margin-top: 0 !important;
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
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "Unit.aspx/LoadRegion",
                dataType: "json",
                data: "{}",
                success: function (data) {
                    var jsdata = JSON.parse(data.d);
                    var ro = "";

                    if ($('#tablepaging').length != 0) {

                        $('#tablepaging').remove();
                    }

                    ro = "<table id='tablepaging' class='table table-hover' style='cursor: pointer;' ><thead><th>ID</th><th>Unit Title</th></thead><tbody>";
                    $.each(jsdata, function (key, value) {
                        ro += "<tr><td width='35%'  class='one'>" + value.UnitID + "</td><td width='65%'  class='two'>" + value.UnitTitle + "</td></tr>";

                    });
                    ro = ro + "</tbody></table>";

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



        function LoadRegion() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "Unit.aspx/LoadRegion",
                dataType: "json",
                data: "{}",
                success: function (data) {
                    var jsdata = JSON.parse(data.d);
                    var ro = "";

                    if ($('#tablepaging').length != 0) {

                        $('#tablepaging').remove();
                    }

                    ro = "<table id='tablepaging' class='table table-hover' style='cursor: pointer;' ><thead><th>ID</th><th>Unit Title</th></thead><tbody>";
                    $.each(jsdata, function (key, value) {
                        ro += "<tr><td width='35%'  class='one'>" + value.UnitID + "</td><td width='65%'  class='two'>" + value.UnitTitle + "</td></tr>";

                    });
                    ro = ro + "</tbody></table>";

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

        };





        function Save() {
            var field = document.getElementById("txtunittype").value;
            if (field == "" || field == null) {
                document.getElementById("txtunittype").style.border = "solid 1px red";
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

            document.getElementById("txtunittype").style.border = "solid 1px white";


        }



        function DeleteData() {

            $("#modal-dialog_delete").modal();
        }




        function InsertRegion() {
            //alert("asd");
            var UserID = localStorage.getItem("UserID");
            //alert(UserID);
            // var dataToSend = JSON.stringify({ 'RegionName': $('#C').val(), 'UserID': UserID });
            var dataToSend = JSON.stringify({ 'UnitTitle': $('#txtunittype').val(), 'UserID': UserID });
          //  alert(dataToSend);
            var results = $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "Unit.aspx/InsertRegion",
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
            // alert("abc");
            //var UserID = localStorage.getItem("UserID");
         //   alert($('#ContentPlaceHolder1_hdnid').val());
            var dataToSend = JSON.stringify({ 'UnitID': $('#ctl00_ContentPlaceHolder1_hdnid').val(), 'UnitTitle': $('#txtunittype').val() });
       //   alert(dataToSend)
            var results = $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "Unit.aspx/UpdateRegion",
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
          //  alert(ctl00_ContentPlaceHolder1_hdnid);
            var dataToSend = JSON.stringify({ 'UnitID': $('#ctl00_ContentPlaceHolder1_hdnid').val(), 'UserID': UserID });
          //  alert(dataToSend)
            var results = $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "Unit.aspx/DeleteRegion",
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


        function msgrevert_delete() {
            $("#msgbody_delete").text("Are you sure about this?");
            $("#confirm_delete").show();

        }

        function Clear() {
            $('#ctl00_ContentPlaceHolder1_hdnid').val('');
            $('#txtunittype').val('');
            $("#btnSave").text("Save");
        }



        $("#tablepaging tbody tr").live('click', function () {
            document.getElementById("<%=hdnid.ClientID%>").value = $(this).closest('tr').children('td.one').text();
              document.getElementById("txtunittype").value = $(this).closest('tr').children('td.two').text();
              $("#btnSave").text("Update");


          });





    </script>


</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">








    <div class="modal fade" id="modal-dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="background-color:lightgray">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title">Confirmation Required</h4>
                </div>
                <div class="modal-body" id="msgbody">
                    Are you sure about this?
									
                </div>
                <div class="modal-footer" style="background-color:lightgray">
                    <a href="javascript:;" class="btn btn-sm btn-white" data-dismiss="modal" onclick="msgrevert();">Close</a>
                    <a href="javascript:;" class="btn btn-sm btn-success" onclick="SaveData();" id="confirm">Confirm</a>
                </div>
            </div>
        </div>
    </div>



    <div class="modal fade" id="modal-dialog_delete">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="background-color:lightgray">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title">Confirmation Required</h4>
                </div>
                <div class="modal-body" id="msgbody_delete">
                    Are you sure about this?
									
                </div>
                <div class="modal-footer" style="background-color:lightgray">
                    <a href="javascript:;" class="btn btn-sm btn-white" data-dismiss="modal" onclick="msgrevert_delete();">Close</a>
                    <a href="javascript:;" class="btn btn-sm btn-danger" onclick="DeleteRegion();" id="confirm_delete">Confirm</a>
                </div>
            </div>
        </div>
    </div>




    <div class="row">
        <div class="col-md-6">
            <div class="panel widget">
                <div class="panel-heading vd_bg-grey">
                    <h3 class="panel-title"><span class="menu-icon"><i class="fa fa-bar-chart-o"></i></span>Form Designation </h3>
                </div>
                <div class="panel-body">
                    <form class="form-horizontal" action="#" role="form">

                        <div class="form-group">
                            <label class="control-label col-md-3 col-sm-3 col-xs-12">
                                Unit <span class="required">*</span>
                            </label>
                            <div class="col-md-6 col-sm-6 col-xs-12">
                                <input type="text" id="txtunittype" required="required" class="form-control col-md-7 col-xs-12" placeholder="Enter Unit">
                            </div>
                        </div>
                           



                        <div class="form-group form-actions">
                            <div class="col-sm-5"></div>
                            <div style="margin:10px 0px 0px 80px;" class="col-sm-9">
                                <br />
                                <button type="button" class="btn vd_btn vd_bg-green vd_white" id="btnSave" onclick="Save();">Save</button>


                                <button class="btn btn-default" type="button" onclick="Clear();">Cancel</button>
                                <button class="btn btn-danger" type="button" onclick="DeleteData();">Delete</button>




                            </div>
                        </div>

                   <%--     <asp:TextBox ID="hdnid" runat="server"></asp:TextBox>--%>
                    
                        <asp:HiddenField ID="hdnid" runat="server" />

                    </form>
                </div>
            </div>
            <!-- Panel Widget -->
        </div>
        <div class="col-md-6">
            <div class="panel widget">
                <div class="panel-heading vd_bg-grey">
                    <h3 class="panel-title"><span class="menu-icon"><i class="fa fa-dot-circle-o"></i></span>Table No Padding &amp; Rounded Icon </h3>
                </div>
                <div class="panel-body-list  table-responsive" id="DivRegion">
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
            </div>
            <!-- Panel Widget -->

        </div>
        <!-- col-md-12 -->
    </div>


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

