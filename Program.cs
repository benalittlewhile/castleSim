using Arch.Core;
using Terminal.Gui;
using static Global.Global;

internal class Program
{
    private static void Main(string[] args)
    {
        MakeTestEntities();

        CastleWindow myWindow = new() { };
        Application.Init();
        Application.Invoke(() =>
        {
            Application.AddTimeout(
                TimeSpan.FromSeconds(2),
                () =>
                {
                    world.Create(new Named { name = "after 1" });
                    return false;
                }
            );
        });
        Application.Invoke(() =>
        {
            Application.AddTimeout(
                TimeSpan.FromSeconds(5),
                () =>
                {
                    world.Create(new Named { name = "after 2" });
                    return false;
                }
            );
        });
        AddAnother();
        AddAnother();
        Application.Invoke(() =>
        {
            Application.AddTimeout(
                TimeSpan.FromMilliseconds(100),
                () =>
                {
                    myWindow.myTextTab.Text = GetQueryText();
                    return true;
                }
            );
        });

        Application.Run(myWindow);
        Application.Shutdown();
    }

    private static void MakeTestEntities()
    {
        ReadOnlySpan<string> names = ["Ben", "Zoe", "James", "Nate", "Chris"];

        foreach (string thisName in names)
        {
            world.Create(new Named { name = thisName });
        }
    }

    private int id { get; set; }

    static async void AddAnother()
    {
        Task.Delay(2000)
            .ContinueWith(
                (e) =>
                {
                    world.Create(new Named { name = "another" });
                }
            );
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
