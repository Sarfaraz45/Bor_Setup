<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Master.master" AutoEventWireup="true" CodeFile="UserType.aspx.cs" Inherits="Setup_UserType" %>

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

        function EnterEvent(e) {
            if (e.keyCode == 13) {
                event.preventDefault();
                document.getElementById("btnSave").click();
                //   $('#btnSave').click();
            }
        }

        function setFocusToTextBox() {
            document.getElementById("txtRegionName").focus();

        }
        $(document).ready(function () {
            var BranchID = localStorage.getItem("BranchID");
            var dataToSend = JSON.stringify({ 'BranchID': BranchID });
            setFocusToTextBox();
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "UserType.aspx/LoadRegion",
                dataType: "json",
                data: dataToSend,
                success: function (data) {
                    var jsdata = JSON.parse(data.d);
                    var ro = "";

                    if ($('#tablepaging').length != 0) {

                        $('#tablepaging').remove();
                    }

                    ro = "<table id='tablepaging' class='table table-hover' style='cursor: pointer;' ><thead><th>ID</th><th>Name</th></thead><tbody>";
                    $.each(jsdata, function (key, value) {
                        //ro += "<tr><td width='10%'><button type='button' class='btn btn-info m-r-5 m-b-5' id='" + value.ID + "' title='" + value.Name + "' onclick=\"GetRegion('" + value.ID + "','" + value.Name + "');\">Edit</button></td><td width='30%'>" + value.ID + "</td><td width='60%'  class='two'>" + value.Name + "</td></tr>";
                        ro += "<tr><td width='35%'  class='one'>" + value.ID + "</td><td width='65%'  class='two'>" + value.Name + "</td></tr>";

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
            var BranchID = localStorage.getItem("BranchID");
            var dataToSend = JSON.stringify({ 'BranchID': BranchID });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "UserType.aspx/LoadRegion",
                dataType: "json",
                data: dataToSend,
                success: function (data) {
                    var jsdata = JSON.parse(data.d);
                    var ro = "";

                    if ($('#tablepaging').length != 0) {

                        $('#tablepaging').remove();
                    }

                    ro = "<table id='tablepaging' class='table table-hover' style='cursor: pointer;' ><thead><th>ID</th><th>Name</th></thead><tbody>";
                    $.each(jsdata, function (key, value) {
                        //ro += "<tr><td width='10%'><button type='button' class='btn btn-info m-r-5 m-b-5' id='" + value.ID + "' title='" + value.Name + "' onclick=\"GetRegion('" + value.ID + "','" + value.Name + "');\">Edit</button></td><td width='30%'>" + value.ID + "</td><td width='60%'  class='two'>" + value.Name + "</td></tr>";
                        ro += "<tr><td width='35%'  class='one'>" + value.ID + "</td><td width='65%'  class='two'>" + value.Name + "</td></tr>";

                    });
                    ro = ro + "</tbody></table>";

                    $("#DivRegion").append(ro);

                    var pager = new Pager('tablepaging', 8);
                    pager.init();
                    pager.showPageNav('pager', 'pageNavPosition');
                    pager.showPage(1);
                    $("#pageNavPosition").show();
                },
                error: function (result) {
                    alert(result);
                }


            });

        };

        $("#tablepaging tbody tr").live('click', function () {
            var selected = $(this).hasClass("highlight");

            /// For Getting Grid Value Start
            /// For Getting Grid Value Start

            document.getElementById("<%=hdntitle.ClientID%>").value = $(this).closest('tr').children('td.two').text();
            document.getElementById("<%=hdnid.ClientID%>").value = $(this).closest('tr').children('td.one').text();
            //  alert(document.getElementById("<%=hdnid.ClientID%>").value + ' ' + document.getElementById("<%=hdntitle.ClientID%>").value);
            document.getElementById("txtRegionName").value = document.getElementById("<%=hdntitle.ClientID%>").value;
            $("#btnSave").text("Update");
            setFocusToTextBox();
            document.getElementById("txtRegionName").style.border = "solid 1px #ccd0d4";
            /// For Getting Grid Value End
            /// For Getting Grid Value End

            $("#tablepaging tr").removeClass("highlight");
            if (!selected)
                $(this).addClass("highlight");
            else if (selected)
                $(this).addClass("highlight");
        });

        function msgrevert() {
            $("#msgbody").text("Are you sure about this?");
            $("#confirm").show();

        }
        function msgrevert_delete() {
            $("#msgbody_delete").text("Are you sure about this?");
            $("#confirm_delete").show();

        }
        function Save() {

            $("#modal-dialog").modal();

        }

        function SaveData() {
            if ($("#btnSave").text() == "Save") {

                InsertRegion();
                $("#search").val('');
                $("#msgbody").text("Record has been inserted successfully.");
                $("#confirm").hide();
            }
            else if ($("#btnSave").text() == "Update") {
                UpdateRegion();
                $("#search").val('');
                $("#msgbody").text("Record has been updated successfully.");
                $("#confirm").hide();

            }


        }
        function DeleteData() {
            var field = document.getElementById("txtRegionName").value;
            if (field == "" || field == null) {
                document.getElementById("txtRegionName").style.border = "solid 1px red";
                return false;
            }
            $("#modal-dialog_delete").modal();
        }

        function InsertRegion() {
            var UserID = localStorage.getItem("UserID");
            var BranchID = localStorage.getItem("BranchID");
            var dataToSend = JSON.stringify({ 'RegionName': $('#txtRegionName').val(), 'UserID': UserID, 'BranchID': BranchID });
            var results = $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "UserType.aspx/InsertRegion",
                dataType: "json",
                data: dataToSend,
                //data: "{'RegionName':'" + $('#txtRegionName').val() + "'}",                
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
            var UserID = localStorage.getItem("UserID");
            var BranchID = localStorage.getItem("BranchID");
            var dataToSend = JSON.stringify({ 'RegionID': $('#ContentPlaceHolder1_hdnid').val(), 'RegionName': $('#txtRegionName').val(), 'UserID': UserID, 'BranchID': BranchID });
            //alert(dataToSend);
            var results = $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "UserType.aspx/UpdateRegion",
                dataType: "json",
                //data: "{'RegionID':'" + $('#ContentPlaceHolder1_hdnid').val() + "','RegionName':'" + $('#txtRegionName').val() + "'}",
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
            var UserID = localStorage.getItem("UserID");
            var BranchID = localStorage.getItem("BranchID");            
            var dataToSend = JSON.stringify({ 'RegionID': $('#ContentPlaceHolder1_hdnid').val(), 'UserID': UserID , 'BranchID': BranchID});
            var results = $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "UserType.aspx/DeleteRegion",
                dataType: "json",
                // data: "{'RegionID':'" + $('#ContentPlaceHolder1_hdnid').val() + "'}",
                data: dataToSend,
                async: false,
                success: function (data) {
                    var obj = data.d;

                    if (obj == 'true') {
                        $("#search").val('');
                        LoadRegion();
                        Clear();
                        document.getElementById("txtRegionName").style.border = "solid 1px #ccd0d4";
                        setFocusToTextBox();
                        $("#msgbody_delete").text("Record has been deleted successfully.");
                        $("#confirm_delete").hide();
                    }
                    else { $("#msgbody_delete").text("Region ID exist in District Table!"); }

                },
                error: function (result) {
                    alert(result);
                }
            });

            return results;

        }

        function Clear() {
            $('#ContentPlaceHolder1_hdnid').val('');
            $('#ctl00_ContentPlaceHolder1_hdntitle').val('');
            $('#txtRegionName').val('');
            $("#btnSave").text("Save");
            $("#tablepaging tr").removeClass("highlight");
            document.getElementById("txtRegionName").style.border = "solid 1px #ccd0d4";
            setFocusToTextBox();
        }

        function Duplicate() {

            var field = document.getElementById("txtRegionName").value;
            if (field == "" || field == null) {
                document.getElementById("txtRegionName").style.border = "solid 1px red";
                return false;
            }
            var BranchID = localStorage.getItem("BranchID");            
            var dataToSend = JSON.stringify({ 'RegionName': $('#txtRegionName').val(), 'BranchID': BranchID });
            var results = $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "UserType.aspx/LoadRegionName",
                dataType: "json",
                data: dataToSend,
                //data: "{'RegionName':'" + $('#txtRegionName').val() + "'}",                
                async: false,
                success: function (data) {
                    var obj = data.d;
                    if (obj == 'true') {

                        Save();

                    }
                    else {

                        $("#modal-dialog_duplicate").modal();

                    }

                },
                error: function (result) {
                    alert(result);
                }
            });

            return results;


        }

    </script>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">



    <div class="modal fade" id="modal-dialog_duplicate">
								<div class="modal-dialog">
									<div class="modal-content">
										<div class="modal-header">
											<button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
											<h4 class="modal-title">Duplicate Record</h4>
										</div>
										<div class="modal-body" id="Div2">
											Record already exist!
										</div>
										<div class="modal-footer">
											<a href="javascript:;" class="btn btn-sm btn-white" data-dismiss="modal">Close</a>
											
										</div>
									</div>
								</div>
							</div> 
<div class="modal fade" id="modal-dialog">
								<div class="modal-dialog">
									<div class="modal-content">
										<div class="modal-header">
											<button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
											<h4 class="modal-title">Confirmation Required</h4>
										</div>
										<div class="modal-body" id="msgbody">
											Are you sure about this?
										</div>
										<div class="modal-footer">
											<a href="javascript:;" class="btn btn-sm btn-white" data-dismiss="modal" onclick="msgrevert();">Close</a>
											<a href="javascript:;" class="btn btn-sm btn-success" onclick="SaveData();" id="confirm">Confirm</a>
										</div>
									</div>
								</div>
							</div>
                            <div class="modal fade" id="modal-dialog_delete">
								<div class="modal-dialog">
									<div class="modal-content">
										<div class="modal-header">
											<button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
											<h4 class="modal-title">Confirmation Required</h4>
										</div>
										<div class="modal-body" id="msgbody_delete">
											Are you sure about this?
										</div>
										<div class="modal-footer">
											<a href="javascript:;" class="btn btn-sm btn-white" data-dismiss="modal" onclick="msgrevert_delete();">Close</a>
											<a href="javascript:;" class="btn btn-sm btn-danger" onclick="DeleteRegion();" id="confirm_delete">Confirm</a>
										</div>
									</div>
								</div>
							</div>
   




    <div class="col-md-6 col-sm-12 col-xs-12">
                <div class="x_panel">
                  <div class="x_title">


                      


                    <h2>Create User Type <%--<small>different form elements</small>--%></h2>
                    <ul class="nav navbar-right panel_toolbox">
                      <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                      </li>
                      <li class="dropdown">
                        <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-expanded="false"><i class="fa fa-wrench"></i></a>
                        <ul class="dropdown-menu" role="menu">
                          <li><a href="#">Settings 1</a>
                          </li>
                          <li><a href="#">Settings 2</a>
                          </li>
                        </ul>
                      </li>
                      <li><a class="close-link"><i class="fa fa-close"></i></a>
                      </li>
                    </ul>
                    <div class="clearfix"></div>
                  </div>
                  <div class="x_content">
                    <br />
                    <section  data-parsley-validate class="form-horizontal form-label-left" runat="server">
                        
                      <div class="form-group">
                        <label class="control-label col-md-3 col-sm-3 col-xs-12">User Type <span class="required">*</span>
                        </label>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                          <input type="text" id="txtRegionName" required="required" class="form-control col-md-7 col-xs-12" placeholder="Enter User Type">  
                            
        


                        </div>
                      </div>
                    
                     
                      <div class="ln_solid"></div>
                      <div class="form-group">
                        <div class="col-md-6 col-sm-6 col-xs-12 col-md-offset-3">
                         <%-- <button type="submit" class="btn btn-primary">Cancel</button>
                          <button type="submit" class="btn btn-success">Submit</button>--%>


                            <button type="button" class="btn btn-success"  id="btnSave" onclick="Duplicate();">Save</button>


                                        <button type="button" class="btn btn-primary" onclick="Clear();">Cancel</button>
                                        <button type="button" class="btn btn-danger"  id="btDelete" onclick="DeleteData();">Delete</button>


<asp:HiddenField ID="hdnid" runat="server" />
    <asp:HiddenField ID="hdntitle" runat="server" />
                        </div>
                      </div>
                        
                    </section>
                  </div>
                </div>
              </div>

    <%--<div class="col-md-6">
               <div class="panel panel-inverse">
                        <div class="panel-heading">
                            <div class="panel-heading-btn">
                                <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-default" data-click="panel-expand"><i class="fa fa-expand"></i></a>
                                <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-success" data-click="panel-reload"><i class="fa fa-repeat"></i></a>
                                <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-warning" data-click="panel-collapse"><i class="fa fa-minus"></i></a>
                                <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-danger" data-click="panel-remove"><i class="fa fa-times"></i></a>
                            </div>
                            <h4 class="panel-title">UserType List</h4>
                        </div>
                        <div class="panel-body">
                          
                            <div id="DivRegion">
                            <input type="text" id="search" placeholder="Type to search" class="search form-control" />
                       <table id="tablepaging" class="table table-hover" >
                       <thead>
            <tr>
                <th></th>
                <th></th>
                <th></th>
                
            </tr>
        </thead>

        <tbody>
            <tr>
                <td></td>
                <td>ID</td>
                <td>Name</td>
                
            </tr>
            </tbody>
                       </table>
                           
                          
                            </div>
                            <div id="pageNavPosition"  align="center" ></div> 
                          
                        </div>
                    </div>
                    </div>--%>



    <div class="col-md-6 col-sm-6 col-xs-12">
                <div class="x_panel">
                  <div class="x_title">
                    <h2>User Type Detials </h2>
                    <ul class="nav navbar-right panel_toolbox">
                      <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                      </li>
                      <li class="dropdown">
                        <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-expanded="false"><i class="fa fa-wrench"></i></a>
                        <ul class="dropdown-menu" role="menu">
                          <li><a href="#">Settings 1</a>
                          </li>
                          <li><a href="#">Settings 2</a>
                          </li>
                        </ul>
                      </li>
                      <li><a class="close-link"><i class="fa fa-close"></i></a>
                      </li>
                    </ul>
                    <div class="clearfix"></div>
                  </div>
                  <div class="x_content"id="DivRegion" >
                                                                      <input type="text" id="search" placeholder="Type to search" class="search form-control" />

                    <table class="table" id="tablepaging">
                      <thead>
                        <tr>
                             <td></td>
                             <td>ID</td>
                             <td>Name</td>
                          
                        </tr>
                      </thead>
                      <tbody>


                           


                        <tr>
                          <th></th>
                <th></th>
                <th></th>
                
                        </tr>
                     
                        
                      </tbody>
                    </table>

                  </div>
                                                                                <div id="pageNavPosition"  align="center" ></div> 

                </div>
                                                                                 <a href="javascript:;" class="btn btn-icon btn-circle btn-success btn-scroll-to-top fade" data-click="scroll-top"><i class="fa fa-angle-up"></i></a>

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

