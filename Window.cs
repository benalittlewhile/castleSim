using Arch.Core;
using Terminal.Gui;

public class CastleWindow : Window
{
    public Label myTextTab;

    public CastleWindow()
    {
        myTextTab = GetBigTextFileTab();
        Colors.ColorSchemes.Add(
            "mine",
            new(
                // Attribute(foreground, background)
                // normal
                new Terminal.Gui.Attribute(Color.White, Color.Black),
                // focus
                new Terminal.Gui.Attribute(Color.White, Color.Black),
                // hotNormal
                new Terminal.Gui.Attribute(Color.Gray, Color.Black),
                // disabled
                new Terminal.Gui.Attribute(Color.White, Color.Black),
                // hot focus
                new Terminal.Gui.Attribute(Color.Yellow, Color.Black)
            )
        );
        ColorScheme = Colors.ColorSchemes["mine"];
        BorderStyle = LineStyle.None;
        DefaultBorderStyle = LineStyle.None;

        var myTiles = new TileView()
        {
            Height = Dim.Fill(),
            Width = Dim.Fill(),
            LineStyle = LineStyle.None,
        };

        var rightSplit = new TileView()
        {
            Height = Dim.Fill(),
            Width = Dim.Fill(),
            LineStyle = LineStyle.Rounded,
            Orientation = Orientation.Horizontal,
            BorderStyle = LineStyle.None,
        };

        var moreText = new Label()
        {
            Width = Dim.Fill(),
            Height = Dim.Fill(),
            X = Pos.Center(),
            Y = Pos.Center()
        };
        rightSplit.Tiles.ElementAt(0).ContentView.Add(moreText);

        var leftTabs = new TabView() { Width = Dim.Fill(), Height = Dim.Fill(), };

        leftTabs.AddTab(
            new()
            {
                DisplayText = "Tab1",
                View = new Label { Text = "hodor!" }
            },
            false
        );
        leftTabs.AddTab(
            new()
            {
                DisplayText = "Tab2",
                View = new Label { X = Pos.Center(), Y = Pos.Center() }
            },
            false
        );
        leftTabs.AddTab(
            new() { DisplayText = "Interactive Tab", View = GetInteractiveTab() },
            false
        );
        leftTabs.AddTab(new() { DisplayText = "Big Text", View = myTextTab }, false);
        leftTabs.AddTab(new() { DisplayText = "Another", View = ModalButtonTab() }, false);

        myTiles.Tiles.ElementAt(0).ContentView.Add(leftTabs);
        myTiles.Tiles.ElementAt(1).ContentView.Add(rightSplit);

        Add(myTiles);
    }

    private View ModalButtonTab()
    {
        var myButton = new Button() { Text = "click me" };
        myButton.Accept += (e, a) =>
        {
            MessageBox.Query(60, 20, "You've got mail", "Mail text");
        };

        return myButton;
    }

    private View GetInteractiveTab()
    {
        var interactiveTab = new View { Width = Dim.Fill(), Height = Dim.Fill() };
        var lblName = new Label { Text = "Name:" };
        interactiveTab.Add(lblName);

        var tbName = new TextField
        {
            X = Pos.Right(lblName),
            Width = 10,
            CaptionColor = Color.DarkGray,
            Caption = "..."
        };
        interactiveTab.Add(tbName);

        var lblAddr = new Label { Y = 1, Text = "Address:" };
        interactiveTab.Add(lblAddr);

        var tbAddr = new TextField
        {
            X = Pos.Right(lblAddr),
            Y = 1,
            Width = 10,
            CaptionColor = Color.DarkGray,
            Caption = "..."
        };
        interactiveTab.Add(tbAddr);

        return interactiveTab;
    }

    private Label GetBigTextFileTab()
    {
        var text = new Label { Width = Dim.Fill(), Height = Dim.Fill() };
        text.Text = "Starter text";

        return text;
    }

    private static string GetQueryText()
    {
        var sb = new System.Text.StringBuilder();
        var names = Queries.GetAllNames();

        foreach (string name in names)
        {
            sb.Append(name);
            sb.AppendLine();
        }

        return sb.ToString();
    }
}
