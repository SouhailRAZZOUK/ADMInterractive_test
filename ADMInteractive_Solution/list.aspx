<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="ADMInteractive_Solution.list" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="Content/bootstrap.css"/>
    <link rel="stylesheet" href="Assets/styles/app.css"/>
</head>
<body>
    <form id="form1" runat="server">
   
        <h1>Product List</h1>
        <div class="sortBox row">
            <div class="col-xs-8">
                <select id="sortByBox">
                    <option value="price" selected="selected">
                        Price
                    </option>
                    <option value="popularity">
                        Popularity
                    </option>
                </select>
            </div>
            <div class="col-xs-4">
                <a id="ascSorting">Asc</a>
                <span>/</span>
                <a id="descSorting">Desc</a>
            </div>
        </div>
       <div class="products">
           
            <asp:Repeater ID="Repeater1" runat="server">
                
                <ItemTemplate>

                    <div class="row product" id="<%# Eval("id") %>" data-popularity="<%# Eval("popularity") %>">
                        <div class="col-xs-2">
                            <img src="Assets/<%# Eval("image") %>" />
                        </div>
                        <div class="col-xs-7">
                            <h2><%# Eval("title") %></h2>
                            <p><%# Eval("description") %></p>
                            <a href="Details.aspx?productId=<%# Eval("id") %>" class="btn btn-default btn-xs">More Details</a>
                        </div>
                        <div class="col-xs-3 price-availability">
                            <div class="price">
                                <span>Price:</span>
                            </div>
                            <div class="availability">
                                <span>Availability:</span>
                            </div>
                        </div>
                    </div>

                </ItemTemplate>

            </asp:Repeater>
           
            
        </div>
      
    </form>

    <script src="Scripts/jquery-2.1.4.js"></script>
    <script src="Scripts/bootstrap.js"></script>
    <script src="Assets/js/app.js"></script>

</body>
</html>
