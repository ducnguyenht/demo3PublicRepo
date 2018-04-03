<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewCart.ascx.cs" Inherits="FergusonMoriyama.DocCart.UserControls.DocCart.ViewCart" %>
<%@ Import Namespace="FergusonMoriyama.DocCart.Interfaces" %>

<asp:Repeater ID="CartRepeater" runat="server" OnItemCommand="CartRepeaterItemCommand">
    <HeaderTemplate>
        <table>
            <thead>
                <th></th>
                <th>
                    <%# IdColumnHeaderText %>
                </th>
                <th>
                    <%# DescriptionColumnHeaderText %>
                </th>
                <th>
                    <%# QuantityColumnHeaderText %>
                </th>
                <th>
                    <%# PriceColumnHeaderText%>
                </th>
            </thead>
        
    </HeaderTemplate>    
    
    <ItemTemplate>
        <tr>
            <td>
                <asp:HiddenField ID="ItemId" runat="server" Value="<%# ((ICartItem) Container.DataItem).NodeId %>"/>
                <asp:Button ID="DeleteButton" CommandName="Delete" runat="server" Text=" <%# DeleteText %>" />
            </td>
            <td>
                <%# ((ICartItem) Container.DataItem).DisplayId %>
            </td>
            <td>
                <%# ((ICartItem) Container.DataItem).Description %>
            </td>
            <td>
                <asp:TextBox ID="Quantity" runat="server" MaxLength="2" Width="20" Text="<%# ((ICartItem) Container.DataItem).Quantity %>"></asp:TextBox>
                
                <asp:Button ID="UpdateQuantity" CommandName="Update" runat="server" Text="<%# UpdateText %>" />
            </td>
            <td>
               
                <%# string.Format(NumberFormat, ((ICartItem)Container.DataItem).Price)%>
            </td>
        </tr>
    </ItemTemplate>
    
    <FooterTemplate>
        <% if(MacroCartService.GetCartItems().Count() == 0) { %>
            <td colspan="5">
                <%# EmptyCartText %>
            </td>
        <% } %>
        <tr>
            <td colspan="4" style="text-align: right;">
                Sub-Total:
            </td>
            <td>
                <%# string.Format(NumberFormat, GetTotal()) %>
            </td>
        </tr>
    </FooterTemplate>

</asp:Repeater>
