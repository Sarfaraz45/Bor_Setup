<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Master.master" AutoEventWireup="true" CodeFile="Area.aspx.cs" Inherits="ERP_Area" %>

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


        $(document).ready(function () {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "Area.aspx/LoadRegion",
                dataType: "json",
                data: "{}",
                success: function (data) {
                    var jsdata = JSON.parse(data.d);
                    var ro = "";

                    if ($('#tablepaging').length != 0) {

                        $('#tablepaging').remove();
                    }

                    ro = "<table id='tablepaging' class='table table-hover' style='cursor: pointer;' ><thead><th>ID</th><th>Area Title</th><th> </th></thead><tbody>";
                    $.each(jsdata, function (key, value) {
                        ro += "<tr><td   class='one'>" + value.AreaID + "</td><td  class='two'>" + value.AreaTitle + "</td>   <td  style='display:none;' class='four'>" + value.TalukaID + "</td> </tr>";

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
                url: "Area.aspx/LoadRegion",
                dataType: "json",
                data: "{}",
                success: function (data) {
                    var jsdata = JSON.parse(data.d);
                    var ro = "";

                    if ($('#tablepaging').length != 0) {

                        $('#tablepaging').remove();
                    }

                    ro = "<table id='tablepaging' class='table table-hover' style='cursor: pointer;' ><thead><th>ID</th><th>Taluka Title</th><th> </th></thead><tbody>";
                    $.each(jsdata, function (key, value) {
                        ro += "<tr><td   class='one'>" + value.AreaID + "</td><td  class='two'>" + value.AreaTitle + "</td>   <td  style='display:none;' class='four'>" + value.TalukaID + "</td> </tr>";

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



        function EnterEvent(e) {
            if (e.keyCode == 13) {
                event.preventDefault();
                document.getElementById("btnSave").click();
                $('#btnSave').click();
            }
        }


        function SaveData() {

            
            var field = document.getElementById("txttitle").value;
            //var field = document.getElementById("txtdusplay").value;
           // var field = document.getElementById("txtbasemulti").value;
            //alert(field);
            if (field == "" || field == null) {
                document.getElementById("txttitle").style.border = "solid 1px red";
            //    document.getElementById("txtdusplay").style.border = "solid 1px red";
            //    document.getElementById("txtbasemulti").style.border = "solid 1px red";
                return false;
            }






          //  alert("asd");
            if ($("#btnSave").text() == "Save") {

                InsertRegion();
                $("#msgbody").text("Record has been inserted successfully.");
                $("#confirm").hide();

            }
            else if ($("#btnSave").text() == "Update") {
                UpdateRegion();
                $("#search").val('');
                $("#msgbody").text("Record has been updated successfully.");
                $("#confirm").hide();

            }

            document.getElementById("txttitle").style.border = "solid 1px white";
          //  document.getElementById("txtdusplay").style.border = "solid 1px white";
          //  document.getElementById("txtbasemulti").style.border = "solid 1px white";

        }









        //Use for Dropdown

        $(document).ready(function () {
            // alert("sadasd");
            $.ajax({
                type: "POST",
                url: "Area.aspx/LoadRegionCombo1",
                data: {},
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var jsdata = JSON.parse(data.d);
                    var rows = "";
                    if ($('#ctl00_ContentPlaceHolder1_DropDownList').select.length != 0) {

                        $('#ctl00_ContentPlaceHolder1_DropDownList').empty();
                    }

                    $.each(jsdata, function (key, value) {
                       // alert(value.DistrictTitle);
                        rows += "<option  value=" + value.TalukaID + ">" + value.TalukaTitle + "</option>";
                    });

                    $("#ctl00_ContentPlaceHolder1_DropDownList").append(rows);




                },
                error: function (data) {
                    alert("error found..");
                }
            });
        });



        function InsertRegion() {
            //var base = 0;
            //if (document.getElementById("chkbase").checked == true)
            //{ base = 1; }
            //else
            //{ base = 0; }


            var ddl = document.getElementById("ctl00_ContentPlaceHolder1_DropDownList").value;

            var UserID = localStorage.getItem("UserID");
            var dataToSend = JSON.stringify({ 'AreaTitle': $('#txttitle').val(), 'TalukaID': ddl, 'UserID': UserID });
           // alert(dataToSend);
            var results = $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "Area.aspx/InsertRegion",
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



            //var base = 0;
            //if (document.getElementById("chkbase").checked == true)
            //{ base = 1; }
            //else
            //{ base = 0; }


            var ddl = document.getElementById("ctl00_ContentPlaceHolder1_DropDownList").value;


            //alert($('#ContentPlaceHolder1_hdnid').val());
            var dataToSend = JSON.stringify({ 'AreaID': $('#ctl00_ContentPlaceHolder1_hdnid').val(), 'AreaTitle': $('#txttitle').val(), 'TalukaID': ddl });
       //     alert(dataToSend);
            var results = $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "Area.aspx/UpdateRegion",
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
            var dataToSend = JSON.stringify({ 'AreaID': $('#ctl00_ContentPlaceHolder1_hdnid').val(), 'UserID': UserID });
            var results = $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "Area.aspx/DeleteRegion",
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














        function Clear() {
            $('#ctl00_ContentPlaceHolder1_hdnid').val('');
            $('#txttitle').val('');
            $('#txtdusplay').val('');
            $('#ContentPlaceHolder1_DropDownList').val('');
            $('#base').val('');
            $('#txtbasemulti').val('');
            $("#btnSave").text("Save");
        }



        $("#tablepaging tbody tr").live('click', function () {
            document.getElementById("<%=hdnid.ClientID%>").value = $(this).closest('tr').children('td.one').text();
            document.getElementById("txttitle").value = $(this).closest('tr').children('td.two').text();
            document.getElementById("txtdusplay").value = $(this).closest('tr').children('td.three').text();
            document.getElementById("ctl00_ContentPlaceHolder1_DropDownList").value = $(this).closest('tr').children('td.four').text();
            //document.getElementById("base").value = $(this).closest('tr').children('td.five').text();
            document.getElementById("txtbasemulti").value = $(this).closest('tr').children('td.six').text();



            var base = 0;
            base = $(this).closest('tr').children('td.five').text();
            //alert(base);
            if (base == 1)
            { document.getElementById("chkbase").checked = true; }

            else
            { document.getElementById("chkbase").checked = false }
            $("#btnSave").text("Update");


        });













    </script>




</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


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
                    <h3 class="panel-title"> <span class="menu-icon"> <i class="fa fa-bar-chart-o"></i> </span> Form Designation </h3>
                  </div>
                  <div class="panel-body">
                    <form class="form-horizontal" action="#" role="form">


                        
                      <div class="form-group">
                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Area Title</label>
                        <div class="col-md-9 col-sm-9 col-xs-12">
                          <input type="text" required="required" id="txttitle" class="form-control" placeholder="Area Title">
                        </div>
                      </div>



                         <div  style="display:none" class="form-group">
                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Display Name</label>
                        <div class="col-md-9 col-sm-9 col-xs-12">
                          <input type="text" id="txtdusplay" class="form-control" placeholder="Display Name">
                        </div>
                      </div>



                     
                     

                         <div class="form-group">
                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Select Taluka <span class="required"></span>
                        </label>
                        <div class="col-md-9 col-sm-9 col-xs-12">
                             <asp:DropDownList ID="DropDownList" runat="server" CssClass="form-control"></asp:DropDownList>    
                        </div>
                      </div>
                    















                     
                    
                     
                      

                      
                      <div style="display:none" class="form-group">
                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Switch</label>
                        <div class="col-md-9 col-sm-9 col-xs-12">
                          <div class="">
                            <label>
                              <input type="checkbox" class="js-switch" id="chkbase" Unchecked /> IsBase
                            </label>
                          </div>
                          
                        
                        </div>
                      </div>





                         <div style="display:none" class="form-group">
                        <label class="control-label col-md-3 col-sm-3 col-xs-12"> Base Multiplier</label>
                        <div class="col-md-9 col-sm-9 col-xs-12">
                          <input type="text" id="txtbasemulti" class="form-control" placeholder="Base Multiper Name">
                        </div>
                      </div>








                     
                     
                      <div class="form-group form-actions">
                        <div class="col-sm-4"> </div>
                        <div class="col-sm-9">
                           <button type="button" class="btn vd_btn vd_bg-green vd_white" id="btnSave" onclick="SaveData();">Save</button>


                          <button class="btn btn-default" type="button" onclick="Clear();">Cancel</button>
                          <button class="btn btn-danger" type="button" onclick="DeleteRegion();">Delete</button>



                              


                              





                        </div>
                      </div>
                                                                                <asp:HiddenField ID="hdnid" runat="server" />

                    </form>
                  </div>
                </div>
                <!-- Panel Widget --> 
              </div>
              <div class="col-md-6">
                <div class="panel widget">
                  <div class="panel-heading vd_bg-grey">
                    <h3 class="panel-title"> <span class="menu-icon"> <i class="fa fa-dot-circle-o"></i> </span> Table No Padding &amp; Rounded Icon </h3>
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

