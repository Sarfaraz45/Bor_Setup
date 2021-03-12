<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Master.master" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Setup_Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   <script src="../jquery-1.8.2.js"></script>
    <script src="../jquery.min.js"></script>

     <!-- Fav and touch icons -->
    <link rel="apple-touch-icon-precomposed" sizes="144x144" href="../AAUI/img/ico/apple-touch-icon-144-precomposed.html">
    <link rel="apple-touch-icon-precomposed" sizes="114x114" href="../AAUI/img/ico/apple-touch-icon-114-precomposed.png">
    <link rel="apple-touch-icon-precomposed" sizes="72x72" href="../AAUI/img/ico/apple-touch-icon-72-precomposed.png">
    <link rel="apple-touch-icon-precomposed" href="../AAUI/img/ico/apple-touch-icon-57-precomposed.png">
    <link rel="shortcut icon" href="../AAUI/img/ico/favicon.png">
    
    
    <!-- CSS -->
       
    <!-- Bootstrap & FontAwesome & Entypo CSS -->
    <link href="../AAUI/css/bootstrap.min.css" rel="stylesheet" type="text/css">
    <link href="../AAUI/css/font-awesome.min.css" rel="stylesheet" type="text/css">
    <!--[if IE 7]><link type="text/css" rel="stylesheet" href="css/font-awesome-ie7.min.css"><![endif]-->
    <link href="../AAUI/css/font-entypo.css" rel="stylesheet" type="text/css">    

    <!-- Fonts CSS -->
    <link href="../AAUI/css/fonts.css"  rel="stylesheet" type="text/css">
               
    <!-- Plugin CSS -->
    <link href="../AAUI/plugins/jquery-ui/jquery-ui.custom.min.css" rel="stylesheet" type="text/css">    
    <link href="../AAUI/plugins/prettyPhoto-plugin/css/prettyPhoto.css" rel="stylesheet" type="text/css">
    <link href="../AAUI/plugins/isotope/css/isotope.css" rel="stylesheet" type="text/css">
    <link href="../AAUI/plugins/pnotify/css/jquery.pnotify.css" media="screen" rel="stylesheet" type="text/css">    
	<link href="../AAUI/plugins/google-code-prettify/prettify.css" rel="stylesheet" type="text/css"> 
   
         
    <link href="../AAUI/plugins/mCustomScrollbar/jquery.mCustomScrollbar.css" rel="stylesheet" type="text/css">
    <link href="../AAUI/plugins/tagsInput/jquery.tagsinput.css" rel="stylesheet" type="text/css">
    <link href="../AAUI/plugins/bootstrap-switch/bootstrap-switch.css" rel="stylesheet" type="text/css">    
    <link href="../AAUI/plugins/daterangepicker/daterangepicker-bs3.css" rel="stylesheet" type="text/css">    
    <link href="../AAUI/plugins/bootstrap-timepicker/bootstrap-timepicker.min.css" rel="stylesheet" type="text/css">
    <link href="../AAUI/plugins/colorpicker/css/colorpicker.css" rel="stylesheet" type="text/css">            

	<!-- Specific CSS -->
	<link href="../AAUI/plugins/fullcalendar/fullcalendar.css" rel="stylesheet" type="text/css"><link href="../AAUI/plugins/fullcalendar/fullcalendar.print.css" rel="stylesheet" type="text/css"><link href="../AAUI/plugins/introjs/css/introjs.min.css" rel="stylesheet" type="text/css">    
     
    <!-- Theme CSS -->
    <link href="../AAUI/css/theme.min.css" rel="stylesheet" type="text/css">
    <!--[if IE]> <link href="css/ie.css" rel="stylesheet" > <![endif]-->
    <link href="../AAUI/css/chrome.css" rel="stylesheet" type="text/chrome"> <!-- chrome only css -->    


        
    <!-- Responsive CSS -->
        	<link href="../AAUI/css/theme-responsive.min.css" rel="stylesheet" type="text/css"> 

	  
 
 
    <!-- for specific page in style css -->
        
    <!-- for specific page responsive in style css -->
        
    
    <!-- Custom CSS -->
    <link href="../AAUI/custom/custom.css" rel="stylesheet" type="text/css">



    <!-- Head SCRIPTS -->
    <script type="text/javascript" src="../AAUI/js/modernizr.js"></script> 
    <script type="text/javascript" src="../AAUI/js/mobile-detect.min.js"></script> 
    <script type="text/javascript" src="../AAUI/js/mobile-detect-modernizr.js"></script> 
 <script src="../jquery-1.8.2.js" type="text/javascript"></script>
    <script src="../jquery.min.js" type="text/javascript"></script>
    <script src="../Chart.bundle.js" type="text/javascript"></script>
    <script src="../Chart.bundle.min.js" type="text/javascript"></script>
    <script src="../Chart.js" type="text/javascript"></script>
    <script src="../Chart.min.js" type="text/javascript"></script>
    <script src="../JavaScript.js" type="text/javascript"></script>
    <script src="../canvasjs.min.js" type="text/javascript"></script>
    <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
      <script type="text/javascript" src="js/html5shiv.js"></script>
      <script type="text/javascript" src="js/respond.min.js"></script>     
    <![endif]-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
  window.onload = function () {

    
    var chart = new CanvasJS.Chart("chartContainer_pie",
    {
    animationEnabled: true,
      zoomEnabled: true, 
      title:{
        //text: "Pie Chart" 
      },
      axisY:{
        includeZero: false
      },
      data: data_pie,  // random generator below
      
   });

    chart.render();
  
  }     
  var rows="";
  var rowsID="";  
        $.ajax({
                type: "POST",
                url: "Index.aspx/LoadDetailRegion",
                data: {},
                dataType: "json",
                async: false,
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var jsdata = JSON.parse(data.d);
                    $.each(jsdata, function (key, value) {
                    
                        rows += value.User + ",";
                    
                        rowsID += value.Checked + ",";


                    });


                    rows = rows.slice(0, -1);
                    rowsID = rowsID.slice(0, -1);

                    //                    return rows;

                },
                error: function (data) {
                    alert("error found");
                }
            });

   var limit = 1000;    //increase number of dataPoints by increasing this
   var array = rows.split(',');
   var array1 = rowsID.split(',');
   
    var y = 0;
    
    var data_pie = []; var dataSeries_pie = { type: "pie" };
    var dataPoints = [];
    
    for (var i = 0; i < array.length; i ++) {
         
         dataPoints.push({
         
         label : array[i],
          y: parseInt(array1[i])
             
           });
        }

     
      dataSeries_pie.dataPoints = dataPoints;
     data_pie.push(dataSeries_pie);
  

     
     
            </script>   
<%--  <div class="row">
             <div class="col-md-12">
             <button> <a href="http://localhost:63179/Source/PROCUREMENT/POS.aspx">POS</a> </button>
               </div>
                 </div>--%>
   <div class="row">
    
                     <div class="col-md-7">
                
<div class="panel vd_map-widget widget">
  <div class="panel-heading vd_bg-yellow">
    <h3 class="panel-title"> <span class="menu-icon"> <i class="glyphicon glyphicon-map-marker"></i> </span> <span class="menu-text">Inventory Summary</span> </h3>
    <div class="vd_panel-menu">
  <div data-action="minimize" data-original-title="Minimize" data-toggle="tooltip" data-placement="bottom" class=" menu entypo-icon"> <i class="icon-minus3"></i> </div>
  <div data-action="refresh"  data-original-title="Refresh" data-toggle="tooltip" data-placement="bottom" class=" menu entypo-icon smaller-font"> <i class="icon-cycle"></i> </div>
  <div data-original-title="Config" data-toggle="tooltip" data-placement="bottom" class=" menu entypo-icon smaller-font">
    <div class="menu-trigger" data-action="click-trigger"> <i class="icon-cog"></i> </div>
    <div data-action="click-target" class="vd_mega-menu-content  width-xs-2  left-xs">
      <div class="child-menu">
        <div class="content-list content-menu">
          <ul class="list-wrapper pd-lr-10">
            <li> <a href="#"> <div class="menu-icon"><i class=" fa fa-user"></i></div> <div class="menu-text">User Menu</div> </a> </li>
            <li> <a href="#"> <div class="menu-icon"><i class=" icon-trophy"></i></div> <div class="menu-text">Panel Menu</div> </a> </li>
            <li> <a href="#"> <div class="menu-icon"><i class=" fa fa-envelope"></i></div> <div class="menu-text">Other Menu</div> </a> </li>
            <li class="line"></li>
            <li> <a href="#"> <div class="menu-icon"><i class=" fa fa-tasks"></i></div> <div class="menu-text"> Tasks</div> </a> </li>
            <li> <a href="#"> <div class="menu-icon"><i class=" icon-lock"></i></div> <div class="menu-text">Privacy</div> </a> </li>
          </ul>
        </div>
      </div>
    </div>
  </div>
  <div data-action="close" data-original-title="Close" data-toggle="tooltip" data-placement="bottom" class=" menu entypo-icon"> <i class="icon-cross"></i> </div>
</div>
<!-- vd_panel-menu --> 
  </div>
  <div class="panel-body-list">
    <div id="chartContainer_pie"  style="height: 300px; width: 574px;"></div>
    <div class="vd_info br" >
      <h5 class="text-right font-semibold"><strong>TOTAL ITEMS</strong></h5>
      <h3 class="text-right  vd_red" id="TotalDocument2" runat="server"><span class="append-icon"><i class="fa fa-map-marker"></i></span>3,546,456</h3>
    </div>
  </div>
</div>
<!-- Panel Widget -->               </div>
              <!--col-md-7 -->
              <div class="col-md-5">
                <div class="row">
                  <div class="col-md-12">
                    <div class="vd_status-widget vd_bg-green widget">
	<div class="vd_panel-menu">
  <div data-action="refresh" data-original-title="Refresh" data-rel="tooltip" class=" menu entypo-icon smaller-font"> <i class="icon-cycle"></i> </div>
</div>
<!-- vd_panel-menu --> 
                                
    <a class="panel-body" href="#">
            <div class="clearfix">
                <span class="menu-icon">
                    <i class="icon-network"></i>
                </span>
                <span class="menu-value" runat="server" id="TotalDocuments">
                    1,256,134
                </span>  
            </div>   
            <div class="menu-text clearfix">
                ITEMS
            </div>                                                               
    </a>        
</div>                    </div>
                  <!--col-md-12 --> 
                </div>
                <!-- .row -->
                <div class="row">
                  <div class="col-xs-6">
                    <div class="vd_status-widget vd_bg-red  widget">
    <div class="vd_panel-menu">
  <div data-action="refresh" data-original-title="Refresh" data-rel="tooltip" class=" menu entypo-icon smaller-font"> <i class="icon-cycle"></i> </div>
</div>
<!-- vd_panel-menu --> 
                                 
    <a class="panel-body" href="#">                                
        <div class="clearfix">
            <span class="menu-icon">
                <i class="icon-bars"></i>
            </span>
            <span class="menu-value" runat="server" id="TotalCategories">
                24
            </span>  
        </div>   
        <div class="menu-text clearfix">
            Categories
        </div>  
     </a>                                                                
</div>                    </div>
                  <!--col-xs-6 -->
                  <div class="col-xs-6">
                    <div class="vd_status-widget vd_bg-blue widget">
    <div class="vd_panel-menu">
  <div data-action="refresh" data-original-title="Refresh" data-rel="tooltip" class=" menu entypo-icon smaller-font"> <i class="icon-cycle"></i> </div>
</div>
<!-- vd_panel-menu --> 
                                  
    <a class="panel-body"  href="#">                                  
        <div class="clearfix">
            <span class="menu-icon">
                <i class="fa fa-tasks"></i>
            </span>
            <span class="menu-value" runat="server" id="TotalFolders">
                14
            </span>  
        </div>   
        <div class="menu-text clearfix">
            Brands
        </div>
     </a>                                                                  
</div>                   </div>
                  <!--col-xs-6 --> 
                </div>
                <!-- .row -->
                <div class="row">
                  <div class="col-xs-6">
                    <div class="vd_status-widget vd_bg-yellow widget">
    <div class="vd_panel-menu">
  <div data-action="refresh" data-original-title="Refresh" data-rel="tooltip" class=" menu entypo-icon smaller-font"> <i class="icon-cycle"></i> </div>
</div>
<!-- vd_panel-menu --> 
                                  
    <a class="panel-body"  href="#">                                
        <div class="clearfix">
            <span class="menu-icon">
                <i class="icon-users"></i>
            </span>
            <span class="menu-value" runat="server" id="TotalUsers">
                250
            </span>  
        </div>   
        <div class="menu-text clearfix">
            Users
        </div>  
     </a>                                                                
</div>                    </div>
                  <!--col-xs-6 -->
                  <div class="col-xs-6">
                    <div class="vd_status-widget vd_bg-grey widget">
    <div class="vd_panel-menu">
  <div data-action="refresh" data-original-title="Refresh" data-rel="tooltip" class=" menu entypo-icon smaller-font"> <i class="icon-cycle"></i> </div>
</div>
<!-- vd_panel-menu --> 
                                   
    <a class="panel-body"  href="#">                                  
        <div class="clearfix">
            <span class="menu-icon">
                <i class="fa fa-comments"></i>
            </span>
            <span class="menu-value" id="TodaysDocument" runat="server">
                3
            </span>  
        </div>   
        <div class="menu-text clearfix">
            Today's Document
        </div>
     </a>                                                                  
</div>                   </div>
                  <!--col-md-xs-6 --> 
                </div>
                <!-- .row --> 
                
              </div>
              <!-- .col-md-5 --> 
          
                </div>

</asp:Content>

