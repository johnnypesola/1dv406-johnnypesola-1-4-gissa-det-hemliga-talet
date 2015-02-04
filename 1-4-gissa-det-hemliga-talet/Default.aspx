<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="_1_4_gissa_det_hemliga_talet.Default" ViewStateMode="Disabled" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>1-4-gissa-det-hemliga-talet</title>

    <%: Styles.Render("~/Content/styles") %>
    <%: Scripts.Render("~/Content/javascript") %>

</head>
<body>
    <h1>Gissa det hemliga talet</h1>
    <form id="form1" runat="server" defaultbutton="SubmitButton">
        <asp:ValidationSummary ID="ValidationSummary" runat="server" CssClass="error-message" HeaderText="Följande fel inträffade:" />
        <div>
            <asp:Label ID="UserInputLabel" runat="server" Text="Ange ett tal mellan 1 och 100" /> <asp:TextBox ID="UserInputTextBox" runat="server" /> <asp:Button ID="SubmitButton" runat="server" Text="Skicka gissning" OnClick="SubmitButton_Click" />

            <asp:RequiredFieldValidator ID="UserInputRequired" runat="server" ErrorMessage="Var god ange ett tal, minst 1 och max 100." ControlToValidate="UserInputTextbox" Display="None" />
            <asp:CompareValidator ID="UserInputValidator" runat="server" ErrorMessage="Ett heltal mellan 1 och 100 måste anges." ControlToValidate="UserInputTextBox" Type="Integer" Operator="DataTypeCheck" Display="None" />
            <asp:RangeValidator ID="UserInputRangeValidator" runat="server" ErrorMessage="Talet får vara lägst 1 och högst 100" ControlToValidate="UserInputTextBox" MaximumValue="100" MinimumValue="1" Display="None" Type="Integer" />
        </div>

        <asp:PlaceHolder ID="OutputPlaceHolder" runat="server" Visible="False">
            <asp:Label ID="GuessHistory" runat="server" Text="" CssClass="guess-history"></asp:Label>
            <asp:Label ID="CurrentGuess" runat="server" Text="" CssClass="current-guess"></asp:Label>
            <asp:Label ID="GuessStatus" runat="server" Text="" CssClass="guess-status"></asp:Label>
        </asp:PlaceHolder>

        <br />
        
        <asp:Button ID="ResetButton" runat="server" Text="Slumpa nytt hemligt tal" Visible="False" OnClick="ResetButton_Click" CausesValidation="False" />
    </form>
</body>
</html>
