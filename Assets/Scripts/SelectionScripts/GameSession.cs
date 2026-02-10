using System.Collections.Generic;

public static class GameSession
{
    public static Dictionary<TeamSide, List<int>> Teams = new Dictionary<TeamSide, List<int>>()
    {
        { TeamSide.Left, new List<int>() },
        { TeamSide.Right, new List<int>() }
    };

    public static Dictionary<TeamSide, int> TeamToMonitor = new Dictionary<TeamSide, int>()
    {
        { TeamSide.Left, 0 },
        { TeamSide.Right, 1 }
    };
}
