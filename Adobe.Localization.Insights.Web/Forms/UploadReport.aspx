<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadReport.aspx.cs" Inherits="Adobe.Localization.Insights.Web.UploadReport" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>WSR Utility</title>
  <script type ="text/javascript">
      var validFilesTypes = [ "xls","xlsx","xlsm"];
      function ValidateFile() {
          var file = document.getElementById("<%#FileUpload1.ClientID%>");
          var label = document.getElementById("<%#Validate.ClientID%>");
          var path = file.value;
          var ext = path.substring(path.lastIndexOf(".") + 1, path.length).toLowerCase();
          var isValidFile = false;
          for (var i = 0; i < validFilesTypes.length; i++) {
              if (ext == validFilesTypes[i]) {
                  isValidFile = true;
                  break;
              }
          }
          if (!isValidFile) {
              label.style.color = "red";
              label.innerHTML = "Invalid File. Please upload a File with" +
         " extension:\n\n" + validFilesTypes.join(", ");
          }
          return isValidFile;
      }
</script>

    <style type="text/css">
     body{  height:100%; width:98%}
    
        #hider
        {
        position:absolute;
        top: 0%;
        left: 0%;
        width:100%;
        height:1300px;
            z-index: 99;
           background-color:Black;
           opacity:0.6;
       
         }
    
        .tab
        {
            background-color:#666666;
            font : "Adobe Clean";
            text-align:center;
            width: 100px;
            color: Black;
            padding-right: 5PX;
            padding-left: 10px;
            margin: 0 10px 0 0;
           background-repeat:repeat-x;
        }
            
        .selectedtab
        {
            font-weight: bold;
            border-bottom-style: solid;
            border-bottom-width: 1px;
            border-bottom-color:#FFFFFF;
            background-color: white;
            background-repeat:repeat-x;
            width:98%;
            height:100%;
            }
           .tabcontents
                {
                   border: solid 5px #DCDCDC;
                   width:98%;
                    height:100%;
                    
                    }
        .tabs
        {
            position:relative;
            top:1px;
            left:10px;
            float:left;
            z-index:2;
            width:98%;
            height:100%;
            }
       
        .footer{ position: relative;
                
                 width:100%;
                 height:60px;
                 
                  }
                  
        .footer1{position:relative; 
                           
                 width:100%;
                 height:60px;
                 margin-bottom:-750px;}
                 
        .GridViewRow td
                    {
                    border: 1px solid #837E7C;
                    }
 
        #popup_box  
                {
	            font-family: "Adobe Clean","Myriad Pro", "My Helvetica Neue", Helvetica, Arial, sans-serif;
	            border: 10px solid #999999;
	          

	            display:none;
	            position: absolute;
	            top: 35%;
                left: 35%;
                width: 30em;
                height: 26em;
	
	            z-index: 100;
	          
	            background: -moz-linear-gradient(top,  rgba(255,255,255,0) 0%, rgba(0,0,0,0.1) 100%);
	            background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,rgba(255,255,255,0)), color-stop(100%,rgba(0,0,0,0.1)));
	            background: -webkit-linear-gradient(top,  rgba(255,255,255,0) 0%,rgba(0,0,0,0.1) 100%);
	            background: -o-linear-gradient(top,  rgba(255,255,255,0) 0%,rgba(0,0,0,0.1) 100%);
	            background: -ms-linear-gradient(top,  rgba(255,255,255,0) 0%,rgba(0,0,0,0.1) 100%);
	            background: linear-gradient(to bottom,  rgba(103, 112, 93, 0.89) 0%,rgba(250, 255, 224, 0.12) 100%);
	            filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#00ffffff', endColorstr='#1a000000',GradientType=0 );
	            min-height: 20px;
	              background-color: #313131;
	              -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.05);
		             -moz-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.05);
			              box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.05);
                }
                
                 #popup_box_success_upload
                 {
                      font-family: "Adobe Clean","Myriad Pro", "My Helvetica Neue", Helvetica, Arial, sans-serif;
	            border: 10px solid #999999;
	          

	            display:none;
	            position: absolute;
	            top: 40%;
                left: 40%;
                width: 15em;
                height: 15em;
	
	            z-index: 100;
	         
	            background: -moz-linear-gradient(top,  rgba(255,255,255,0) 0%, rgba(0,0,0,0.1) 100%);
	            background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,rgba(255,255,255,0)), color-stop(100%,rgba(0,0,0,0.1)));
	            background: -webkit-linear-gradient(top,  rgba(255,255,255,0) 0%,rgba(0,0,0,0.1) 100%);
	            background: -o-linear-gradient(top,  rgba(255,255,255,0) 0%,rgba(0,0,0,0.1) 100%);
	            background: -ms-linear-gradient(top,  rgba(255,255,255,0) 0%,rgba(0,0,0,0.1) 100%);
	            background: linear-gradient(to bottom,  rgba(103, 112, 93, 0.89) 0%,rgba(250, 255, 224, 0.12) 100%);
	            filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#00ffffff', endColorstr='#1a000000',GradientType=0 );
	            min-height: 20px;
	              background-color: #313131;
	              -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.05);
		             -moz-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.05);
			              box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.05);
                     }
 
    </style>
    </head>
   
<body style="background-color: #212121; height:100%">
    
    <form id="form1" runat="server" style="background-color:#212121; height: 100%;" enctype="multipart/form-data" >
     <div id="hider" align="center" runat="server"></div>
     <div id="popup_box" align="center" runat="server">
      <br />Please fill up the following details:-
        <br />
        <br />
     <table cellpadding="5" cellspacing="5" border="3" align="center" frame="box">
        <tr><td>
        <asp:Label ID="vendor" runat="server" Text=" Testing Partner" ForeColor="Black" Font-Bold="True"></asp:Label></td><td>
            <asp:DropDownList ID="DropDownList1" runat="server" >
            </asp:DropDownList> </td></tr>
            <tr><td><asp:Label ID="product" runat="server" Text="Product" ForeColor="Black" Font-Bold="True"></asp:Label></td><td>
            <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="true"
                    onselectedindexchanged="DropDownList2_SelectedIndexChanged">
            </asp:DropDownList> </td></tr>
           <tr><td> <asp:Label ID="Label4" runat="server" Text="Version" ForeColor="Black" Font-Bold="True"></asp:Label></td><td>
            <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="true"  onselectedindexchanged="DropDownList3_SelectedIndexChanged">
            </asp:DropDownList> </td></tr>
            <tr><td>
                <asp:Label ID="Label5" runat="server" Text="Testing Type" ForeColor="Black" Font-Bold="True"></asp:Label></td><td>
                    <asp:RadioButton ID="RadioButton1" runat="server" ForeColor="White" 
                        Text="Functional" GroupName="radio"  AutoPostBack="true" OnCheckedChanged="RadioButton1_CheckedChanged" />
                    <asp:RadioButton ID="RadioButton2"
                        runat="server" ForeColor="White" Text="Linguistic" GroupName="radio" AutoPostBack="true" OnCheckedChanged="RadioButton2_CheckedChanged"/></td></tr>
                        <tr><td>
                            <asp:Label ID="Label6" runat="server" Text="Testing Phase" ForeColor="Black" Font-Bold="True"></asp:Label></td><td>
                                <asp:DropDownList ID="DropDownList4" runat="server">
                                </asp:DropDownList> </td> </tr>
                           
                               
        </table>
         <br />
        <br />
        <asp:Button ID="Submit_WSR" runat="server" Text="Submit WSR" align="centre" OnClientClick="javascript:return confirm('The WSR is going to be submitted in the database. Please review before submission as these changes cannot be reverted.') ;"
             onclick="Submit_WSR_Click"/>
        <asp:Button ID="Cancel" runat="server" Text="Cancel" align="centre" 
             onclick="Cancel_Click"/>
            
     </div>
     <div id="popup_box_success_upload" align="center" runat="server">
      <br />
        <br />
     <table cellpadding="5" cellspacing="5" align="center"><tr><td> Report is submitted successfully. You can view the submitted report in "NewApp"</td></tr>
    <tr><td>Thank You!</td></tr></table> <br />
        <br />
      <asp:Button ID="Success" runat="server" Text="OK" align="centre" onclick="Success_Upload"

             /><br />
        <br />
        
     </div>
    
    <asp:ToolkitScriptManager ID="ToolScriptManager1" runat="server"></asp:ToolkitScriptManager>
    
<h1 align="center" 
     style="font-size: 50px; font-weight: 800; color: #DB512D; font-family: 'Adobe Clean';  position: relative; top: -14px; left: 0px; width: 98%;">
    <asp:Image ID="Image1" runat="server" Height="101px" ImageAlign="Middle" style="margin-bottom:44px"
        ImageUrl="~/Images/report-icon.png" Width="137px" />
    Weekly Status Report Upload Utility</h1>
    <hr  style="background-color: #DB512D; margin-top: -75px; margin-bottom:-22px; width: 100%;" size="7" align="center" />
    <br />
    <br />
    <div align="center" style="background-color: #212121" >
  
        <table align="center" cellpadding="7" cellspacing="10" >
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" BackColor="#212121"  ForeColor="#DB512D" Font-Names="comic sans"
                        Text="Select your  Weekly Status Report:         " 
                        Font-Size="X-Large"></asp:Label>
                </td>
                <td>
                    <asp:FileUpload ID="FileUpload1" runat="server" Font-Names="Consolas"
                        Width="476px" Height="23px" BorderStyle="Solid" BackColor="#212121" 
                        BorderColor="#212121" Font-Bold="True" ForeColor="#FFCC99" ToolTip="Select only Excel File"  />
                </td>
            </tr>
            <tr><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label7" runat="server" BackColor="#212121"  ForeColor="#DB512D" Font-Names="comic sans"
                        Text="Metrics Sheet Name:         " 
                        Font-Size="X-Large"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="SheetName" runat="server"></asp:TextBox>
            </td>
            </tr>
               <tr align="center">
                <td colspan="2">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Upload_button" runat="server" Text="Upload &amp; Review Data" OnClick="upload" OnClientClick = "return ValidateFile()" 
                        BackColor="#FFCC99" />
                   </td>
                       </tr>
                       <tr><td></td><td><asp:Label ID="Validate" runat="server" Text=""></asp:Label></td>
                       </tr>
                       </table>
                       <table align="left" cellpadding="7" cellspacing="10">
                       <tr><td>&nbsp;<asp:Label ID="Label2" runat="server" Font-Bold="True" 
                               Font-Italic="False" ForeColor="#DB512D" Text="Uploaded WSR File:  "></asp:Label></td><td>
                           <asp:Label ID="label_filename" runat="server" Font-Bold="True" Font-Names="Calibri" 
                                   Font-Size="Medium" ForeColor="#66FFFF"></asp:Label>
                           </td><td></td></tr>
                       <tr><td>
                           <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Italic="False" 
                               ForeColor="#DB512D" Text="Weekly Report submitted for duration:   "></asp:Label></td><td>
                           
                           <asp:Label ID="label_weeks" runat="server" Font-Bold="True" Font-Names="Calibri" 
                                   Font-Size="Medium" ForeColor="Aqua"></asp:Label>&nbsp;&nbsp;<asp:Label ID="Guessed_Year"
                                       runat="server" Text="*In case this year is incorrect, please update in sheet and REUPLOAD" Font-Size="Medium" ForeColor="Silver"></asp:Label>
                           </td></tr>
                          
                           <tr><td> <asp:Button ID="Submit" runat="server" OnClick="Submit_Click" 
                        Text="Save &amp; Submit WSR" BackColor="#FFCC99"/></td></tr>
                  </table>
                  <br />
                  <br />
        <br />
        <br />
        </div>
        
       <br />
       <br />
       <br />
       <br /><br />
       <asp:UpdatePanel ID="UpdatePanel" runat="server">
       <Triggers><asp:AsyncPostBackTrigger ControlID="Menu1" /></Triggers>
       <ContentTemplate>
       <div style="margin-bottom: 1px">
           <asp:Menu ID="Menu1" runat="server" Orientation="Horizontal" StaticMenuItemStyle-CssClass="tab" 
         StaticSelectedStyle-CssClass="selectedtab" CssClass="tabs"  
               OnMenuItemClick="Menu1_MenuItemClick" >
            <Items>
                <asp:MenuItem  Text="Metrics" Value="0" Selected="True"></asp:MenuItem>
                <asp:MenuItem Text="Highlights" Value="1"></asp:MenuItem>
            </Items>
           
               <StaticMenuItemStyle CssClass="tab" />
               <StaticSelectedStyle BackColor="#DCDCDC" BorderColor="Black" 
                   BorderStyle="Dashed" BorderWidth="1px" CssClass="selectedtab" />
           
        </asp:Menu>
        </div>
           
        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
            <asp:View ID="View1" runat="server">
               
                            <asp:GridView ID="GridView1" runat="server" BackColor="#CCCCCC"
                                BorderStyle="Solid" CellPadding="10" CellSpacing="2" Font-Size="Smaller"
                                ForeColor="Black" ShowHeader="false" BorderColor="Gainsboro" 
                                BorderWidth="5px" RowStyle CssClass="GridViewRow">
                                </asp:GridView>
                               
<div class="footer1">
       <hr  style="background-color: #DB512D" size="7" align="center" width"100%"/>
    <p style="color: #F3DDCB; font-size: small; " align="center"> ©2012 Adobe Systems Incorporated. All rights reserved. Localization Team (IQE) Confidential</p></div>
   
               </asp:View>
            <asp:View ID="View2" runat="server">
           <div class="tabcontents">
                <br />
                 <br />
                <asp:GridView ID="GridView2" runat="server" ShowHeader="false" GridLines="None" 
                    Font-Bold="True" ForeColor="White" CellPadding="7"  
                    >
                  
                </asp:GridView>
                  </div>
               <div class="footer">
       <hr  style="background-color: #DB512D" size="7" align="center" width"100%"/>
    <p style="color: #F3DDCB; font-size: small;  position:relative" align="center"> ©2012 Adobe Systems Incorporated. All rights reserved. Localization Team (IQE) Confidential</p></div>
    </div>
            </asp:View>
        </asp:MultiView>
        
        </div>
         </ContentTemplate>
        </asp:UpdatePanel>
    
    
    </form>
    
</body>
 
</html>
