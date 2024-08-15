using Arch.Core;
using static Global.Global;

class Queries
{
    public static List<string> GetAllNames()
    {
        var query = new QueryDescription().WithAll<Named>();
        List<string> names = [];
        world.Query(
            in query,
            (ref Named name) =>
            {
                names.Add(name.name);
            }
        );
        return names;
    }
}
