<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Zaloguj się</h1>
        <p><a href="Login.aspx" class="btn btn-primary btn-lg">Przejdź &raquo;</a></p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Zarejestruj się</h2>
            <p>
                <a class="btn btn-default" href="Register.aspx">Przejdź</a>
            </p>
        </div>
    </div>

</asp:Content>
