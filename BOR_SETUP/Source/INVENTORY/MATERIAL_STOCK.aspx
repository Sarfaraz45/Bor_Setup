<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Master.master" AutoEventWireup="true" CodeFile="MATERIAL_STOCK.aspx.cs" Inherits="INVENTORY_MATERIAL_STOCK" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script src="../jquery-1.8.2.js"></script>
    <script src="../jquery.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
<div class="row">
                <!-- begin col-12 -->
			    <div class="col-lg-12">
			        <!-- begin panel -->
                   <div class="panel panel-inverse">
                        <div class="panel-heading">
                            <div class="panel-heading-btn">
                                <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-default" data-click="panel-expand"><i class="fa fa-expand"></i></a>
                                <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-success" data-click="panel-reload"><i class="fa fa-repeat"></i></a>
                                <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-warning" data-click="panel-collapse"><i class="fa fa-minus"></i></a>
                                <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-danger" data-click="panel-remove"><i class="fa fa-times"></i></a>
                            </div>
                            <h4 class="panel-title">Voucher List</h4>
                        </div>
                        <div class="panel-body">

                        <div class="row">
                            <div class="col-lg-12">
                                <asp:Label ID="lblMsg" runat="server" CssClass="parsley-error" Visible="false" Text="sdfsfsfsdfsdfsdfsdfsfsdfs"></asp:Label>

                                <%--<a href="#modal-dialog" class="btn btn-sm btn-primary" id="lnkDetails" data-toggle="modal">Demo</a>--%>
                            </div>
                       </div>

                        <div class="row">
                            <div class="col-lg-12">

                            
                                <fieldset>
                                    
                                  
                                    
                                      <div class="row"><div class="col-lg-12" style="height:10px"></div></div>

                            

                                     <div class="row"><div class="col-lg-12" style="height:10px"></div></div>

                            <div class="row"><div class="col-lg-12">
                                 <div class="form-group">
                                    <label class="col-md-2 control-label">Date Filter</label>
                                    <div class="col-md-8">
                    <%--<input type="text" class="form-control" id="datepicker-autoClose" placeholder="Enter Date" />--%>
                    <div class="input-group input-daterange">

    <asp:TextBox ID="txtFrom" runat="server" CssClass="form-control" placeholder="Date Start"></asp:TextBox>
                                           
                                            <asp:CalendarExtender ID="txtFrom_CalendarExtender" 
                            runat="server" Enabled="True" TargetControlID="txtFrom">
                        </asp:CalendarExtender>
                                           
                                            <span class="input-group-addon">to</span>
                                            <asp:TextBox ID="txtTo" runat="server" CssClass="form-control" placeholder="Date End"></asp:TextBox>
                                            
                                        <asp:CalendarExtender ID="txtTo_CalendarExtender" 
                            runat="server" Enabled="True" TargetControlID="txtTo">
                        </asp:CalendarExtender>
                                            
                                        </div>
                                        
                                    </div>
                                    <div class="col-md-2">
        <asp:Button ID="Button2" runat="server" CssClass="btn btn-sm btn-success" Text="Get Report" 
                                            onclick="Button2_Click" />
                                    </div>
                                </div>
                            </div></div>


                            <div class="row"><div class="col-lg-12" style="height:10px"></div></div>

                                    
                                </fieldset>
                            

                            </div>                        
                        </div>
                           
                       

                        

                      

                        </div>
                    </div>
                    <!-- end panel -->
                </div>
                <!-- end col-12 -->
            </div>
</asp:Content>

