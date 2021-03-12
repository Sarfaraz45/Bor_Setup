<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
  <meta charset="UTF-8">
  <title>Login</title>
  
    <link href="css/style.css" rel="stylesheet" type="text/css" />
<style type="text/css">
          .btn
          {
              border-radius: 5px;     background: gray;     color: white;     border: transparent;
              cursor: pointer;
               float: left;
                font: bold 15px Helvetica, Arial, sans-serif;
                height: 35px;
                margin: 20px 0 35px 15px;
                position: relative;
                text-shadow: 0 1px 0 rgba(255,255,255,0.5);
                width: 120px;
          }  
          .btn:hover
          {
              border-radius: 5px;     background: darkgray;     color: White;     border: transparent;
          }  
          .form-control {
    display: block;
    width: 100%;
    height: 34px;
    padding: 6px 12px;
    font-size: 14px;
    line-height: 1.42857143;
    color: #555;
    background-color: #fff;
    background-image: none;
    border: 1px solid #ccc;
    border-radius: 4px;
    -webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
    box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
      </style>    
    <script src="js/jquery.min.js"></script>
    <script src="js/jquery.js"></script>
      
    <%--<link href="AAUI/css/bootstrap.min.css" rel="stylesheet" type="text/css" />--%>
    
    <script type="text/javascript">
        function setCookie(cname, cvalue, exdays) {
            var d = new Date();
            d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
            var expires = "expires=" + d.toUTCString();
            document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
        }


        function ProcessLogin() {
            var dataToSend = JSON.stringify({ 'LoginID': $('#username').val(), 'password': $('#password').val(), 'Branch': $('#ddlBranch').val() });
            //alert(dataToSend);
            //alert("a");
            $.ajax({
                type: "POST",
                url: "Login.aspx/loginUser",
                data: dataToSend,
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var jsdata = JSON.parse(data.d);
                    $.each(jsdata, function (key, value) {
                        localStorage.setItem("UserID", value.ID);
                        localStorage.setItem("UserName", value.Name);
                        localStorage.setItem("LoginID", value.Login);
                        localStorage.setItem("Picture", value.Picture);
                        localStorage.setItem("ManagerID", value.Manager);
                        localStorage.setItem("UTID", value.UTID);
                        localStorage.setItem("Designation", value.Designation);
                        localStorage.setItem("BranchID", value.BranchID);
                        localStorage.setItem("UTDesc", value.UTDesc);
                        localStorage.setItem("BranchName", value.BranchName);
                        setCookie("BranchID", value.BranchID, 365);
                        setCookie("UTID", value.UTID, 365);
                        setCookie("UTDesc", value.UTDesc, 365);
                        setCookie("UserID", value.ID, 365);
                        if (value.UTDesc == "Sales")
                        { window.location.replace("PROCUREMENT/SO_User.aspx"); }
                        else {
                            window.location.replace("Setup/Index.aspx");
                        }
                    });

                },
                error: function (data) {
                    alert("UserID or Password Incorrect");
                    alert("error found");
                }
            });
        };

        function EnterEvent(e) {
            if (e.keyCode == 13) {
                event.preventDefault();
                document.getElementById("Button1").click();
                //   $('#btnSave').click();
            }
        }
    </script>
  
</head>
<body style=" background: url('download.jpg') no-repeat center center fixed;
    -webkit-background-size: cover;
    -moz-background-size: cover;
    background-size: cover;
    -o-background-size: cover;
    height:100%; overflow:hidden;">


<div class="container" style="margin:90px auto; margin-top:230px;">
   
	<section id="content">
     
		<form id="form1" runat="server">
       <%-- <form action="">--%>
			<h1>Sign In Please ! 

</h1>
<div style="    margin-bottom: 10px;width: 94%;margin-left: 3%;">

<asp:DropDownList runat="server" DataSourceID="SqlDataSource1"  CssClass="form-control"
        DataTextField="BranchName" DataValueField="BranchID" ID="ddlBranch"></asp:DropDownList>
			<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:Con %>" 
        SelectCommand="SELECT [BranchID], [BranchName] FROM [Branch]">
    </asp:SqlDataSource>
			</div>
            <div style="height:10px;"><//div>
			<div>
<%--                <input type="text" id="uname" runat="server"  placeholder="Enter User ID" onkeypress="return EnterEvent(event)" />--%>
                				<input type="text" placeholder="Username"  id="username"  onkeypress="return EnterEvent(event)"   />


			</div>
			<div>
<%--                <input type="text" id="password" runat="server" TextMode="password" placeholder="Enter Password" onkeypress="return EnterEvent(event)" />--%>
                				<input type="password" placeholder="Password"  id="password"  onkeypress="return EnterEvent(event)"   />


			</div>
			<div>

				
                <button type="button"   class="btn"   id="Button1" onclick="ProcessLogin();">Sign me in</button>
                
				<%--<a href="#">Lost your password?</a>
				<a href="#">Register</a>--%>
			</div>
		</form>
    <!-- form -->
		<%--<div class="button">
			<a href="#">Download source file</a>
		</div>--%><!-- button -->
	</section><!-- content -->
</div><!-- container -->
</body>
  
      <script src="js/index.js"></script>

</html>
