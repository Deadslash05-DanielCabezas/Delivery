using System.Text;
using TMPro;
using UnityEngine;

public class LeaderBoardUpdater : MonoBehaviour
{
    public TextMeshPro leaderboardText;
    public Timer timer;

    void Update()
    {
        if (timer == null || leaderboardText == null) return;

        var times = timer.times; // already sorted fastest → slowest
        if (times == null || times.Count == 0)
        {
            leaderboardText.text = "🏁 <b>Leaderboard Times</b>\nNo times recorded yet!";
            return;
        }

        StringBuilder sb = new StringBuilder();
        sb.AppendLine("<size=15><b>🏁 Leaderboard Times</b></size>\n");

        int maxEntries = Mathf.Min(times.Count, 10); // show top 10
        for (int i = 0; i < maxEntries; i++)
        {
            float time = times[i];
            int minutes = Mathf.FloorToInt(time / 60f);
            int seconds = Mathf.FloorToInt(time % 60f);
            int milliseconds = Mathf.FloorToInt((time * 1000f) % 1000f / 10f);

            sb.AppendLine($"<b>{i + 1}.</b>  {minutes:00}:{seconds:00}:{milliseconds:00}");
        }

        leaderboardText.text = sb.ToString();
    }
}
