<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SurveyResults.aspx.cs" Inherits="WebApplication1.SurveyResults" %>
<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <asp:Button ID="Button1" runat="server" Text="WYLOGUJ" />
    </p>
    <p>
        Wyniki głosowania:
    </p>
    <p>
&nbsp;<asp:Chart ID="Chart1" runat="server">
            <series>
                <asp:Series Name="Series1">
                </asp:Series>
            </series>
            <chartareas>
                <asp:ChartArea Name="ChartArea1">
                </asp:ChartArea>
            </chartareas>
        </asp:Chart>
    </p>
    <p>
        LEGENDA:<asp:ListBox ID="ListBox1" runat="server"></asp:ListBox>
    </p>
</asp:Content>
