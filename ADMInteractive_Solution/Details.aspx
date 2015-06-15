<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="ADMInteractive_Solution.Details" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="Content/bootstrap.css"/>
    <link rel="stylesheet" href="Assets/styles/app.css"/>
</head>
<body>
    <form id="form1" runat="server">
    <div class="products">
        <h1>Product Details</h1>
        <asp:Repeater ID="Repeater1" runat="server">
            
            <ItemTemplate>
                <div class="product row">
                    <div class="col-xs-12">
                        <div class="row">
                            <div class="col-xs-12">
                                <h2><%# Eval("title") %></h2>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-xs-2">
                                <img src="Assets/<%# Eval("image") %>" />
                            </div>
                            <div class="col-xs-10 border">
                                <p><%# Eval("description") %></p>
                                <div class="pull-right price">
                                    <div class="price">
                                        <span>Price: <%# Eval("price") %></span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    
                        <div class="row">
                            <div class="col-xs-12 border">
                                <h3>Specifications:</h3>
                                <ul>
                                    <asp:Repeater id="Repeater2" runat="server" datasource='<%# Eval("specs") %>'>
                                        <ItemTemplate>
                                            <li><%# Eval("specText") %></li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </div>
                        
                        </div>                    
                    </div>
                </div>

            </ItemTemplate>
            
        </asp:Repeater>
        
    
    </div>
    </form>
</body>
</html>
