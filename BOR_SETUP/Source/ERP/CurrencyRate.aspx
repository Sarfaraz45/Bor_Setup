<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Master.master" AutoEventWireup="true" CodeFile="CurrencyRate.aspx.cs" Inherits="ERP_UnitType" %>

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
            if ($("#btnSave").text() == "S A V E") {                
                InsertRegion();
                $("#msgbody").text("Record has been inserted successfully.");
                $("#confirm").hide();


            }
            document.getElementById("txtunittype").style.border = "solid 1px white";


        }



        function InsertRegion() {

            var UserID = localStorage.getItem("UserID");
            var BranchID = localStorage.getItem("BranchID");
            var dataToSend = JSON.stringify({ 'UnitType': $('#txtunittype').val(), 'UserID': UserID, 'BranchID': BranchID });
            var results = $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "CurrencyRate.aspx/InsertRegion",
                dataType: "json",
                data: dataToSend,
                async: false,
                success: function (data) {
                    var obj = data.d;
                    if (obj == 'true') {
                        window.location = "../Setup/Index.aspx";
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


    </script>


</asp:Content>








<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">



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

<div class="row"  id="dvOrderList">
                <!-- begin col-12 -->
			    <div class="col-md-12" >
			        <!-- begin panel -->
                   <div class="panel widget light-widget panel-bd-top">
                        <div class="panel-heading bordered">
                    <h3 class="panel-title"> <span class="menu-icon"> <i class="icon-pie"></i> </span> Currency Rate Information </h3>
                    <div class="vd_panel-menu">
  <div data-action="minimize" data-original-title="Minimize" data-toggle="tooltip" data-placement="bottom" class=" menu entypo-icon"> <i class="icon-minus3"></i> </div>
  <div data-action="refresh"  data-original-title="Refresh" data-toggle="tooltip" data-placement="bottom" class=" menu entypo-icon smaller-font"> <i class="icon-cycle"></i> </div>
  
  <div data-action="close" data-original-title="Close" data-toggle="tooltip" data-placement="bottom" class=" menu entypo-icon"> <i class="icon-cross"></i> </div>
</div>
<!-- vd_panel-menu --> 
                  </div>
                        <div class=" panel-body-list" style="padding:10px">

                             
                             
    <div class="col-md-12 col-sm-12 col-xs-12">
                          <input type="text" id="txtunittype" required="required" class="form-control col-md-12 col-xs-12" placeholder="Enter Currency Rate" />
                          <div class="col-md-12" style="height:10px;" ></div>
                            <button type="button" class="btn btn-info col-md-12 col-sm-12 col-xs-12" id="btnSave" style="font-weight:bold; font-size:20px;" class="btn btn-info btn-block"  onclick="Save();">S A V E</button>
                            </div>
                    
                          

                            <asp:HiddenField ID="hdnid" runat="server" />

                </div>
                </div>
                </div>
          
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


    $(document).on('keydown', function (e) {
        var kc = e.which || e.keyCode;
        if (e.ctrlKey && String.fromCharCode(kc).toUpperCase() == "P") {
            e.preventDefault();
            SaveData();
        }
    });

</script>




</asp:Content>

