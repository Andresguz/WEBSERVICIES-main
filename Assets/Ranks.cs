using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Ranks : MonoBehaviour
{
    public double time = 1;
    public double orbs = 100;
    public double stylePoints = 500;
    public double damage = 81;
    public double itemsUsed = 1;

    public TextMeshProUGUI timeText;
    public TextMeshProUGUI orbsText;
    public TextMeshProUGUI styleText;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI itemText;

    public TextMeshProUGUI timeTextRank;
    public TextMeshProUGUI orbsTextRank;
    public TextMeshProUGUI styleTextRank;
    public TextMeshProUGUI damageTextRank;
    public TextMeshProUGUI itemTextRank;

    public TextMeshProUGUI totalTextRank;

    public Text namePlayer;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetRequest("http://localhost:8242/api/players/1"));
       
    }

    IEnumerator GetRequest(string url)
    {
        using (UnityWebRequest webrequest = UnityWebRequest.Get(url))
        {
            yield return webrequest.SendWebRequest();

            switch (webrequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                case UnityWebRequest.Result.ProtocolError:
                    print("error");
                    break;
                case UnityWebRequest.Result.Success:
                    //print(webrequest.downloadHandler.text);
                    Player player = JsonUtility.FromJson<Player>(webrequest.downloadHandler.text);
                    print(player.nickName);
                   // DateTime NowTime = DateTime.Now;
                    //orbsText.text = player.ranks[1].maxExperience.ToString();
                    //styleText.text = player.ranks[2].maxExperience.ToString();
                    //damageText.text = player.ranks[3].maxExperience.ToString();
                    //itemText.text = player.ranks[4].maxExperience.ToString();

                    //namePlayer.text = player.nickName;
                    break;

            };
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        double minutes = Mathf.FloorToInt((float)(time / 60));
        double seconds = Mathf.FloorToInt((float)(time % 60));
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        string[] ranks = { "C", "B", "A", "S", "SS" };

        const double MIN_TIME = 0;
        const double MAX_TIME = 100;
        double totalRankTime = CalculateMaxRank(time, MIN_TIME, MAX_TIME, ranks, true);
        timeTextRank.text = GetLetter(totalRankTime, ranks);

        const double MIN_ORBS = 0;
        const double MAX_ORBS = 100;
        double totalRankOrbs = CalculateMaxRank(orbs, MIN_ORBS, MAX_ORBS, ranks);
        orbsTextRank.text = GetLetter(totalRankOrbs, ranks);

        const double MIN_STYLE = 0;
        const double MAX_STYLE = 1000;
        double totalRankStyle = CalculateMaxRank(stylePoints, MIN_STYLE, MAX_STYLE, ranks);
        styleTextRank.text = GetLetter(totalRankStyle, ranks);

        const double MIN_DAMAGE = 0;
        const double MAX_DAMAGE = 100;
        double totalRankDamage = CalculateMaxRank(damage, MIN_DAMAGE, MAX_DAMAGE, ranks, true);
        damageTextRank.text = GetLetter(totalRankDamage, ranks);

        const double MIN_ITEMS = 0;
        const double MAX_ITEMS = 10;
        double totalRankItems = CalculateMaxRank(itemsUsed, MIN_ITEMS, MAX_ITEMS, ranks, true);
        itemTextRank.text = GetLetter(totalRankItems, ranks);

        double total = GetTotalRank(totalRankTime, totalRankOrbs, totalRankStyle, totalRankDamage, totalRankItems);
        totalTextRank.text = GetLetter(total, ranks);
    }

    double GetTotalRank(params double[] ranks) {
        double sum = 0;
        for (int i = 0; i < ranks.Length; i++)
        {
            sum += ranks[i];
        }

        return sum / ranks.Length;
    }
    double CalculateMaxRank(double points, double min, double max, string[] ranks, bool inverse = false)
    {
        double interval = (min + max) / ranks.Length;
        double percentage = 100 / ranks.Length;
        if (points >= 0 && points <= interval)
        {
            return inverse ? percentage * 5 : percentage;
        }
        if (points > interval && points <= interval * 2)
        {
            return inverse ? percentage * 4 : percentage * 2;
        }
        if (points > interval * 2 && points <= interval * 3)
        {
            return inverse ? percentage * 3 : percentage * 3;
        }
        if (points > interval * 3 && points <= interval * 4)
        {
            return inverse ? percentage * 2 : percentage * 4;
        }
        if (points > interval * 4 && points <= interval * 5)
        {
            return inverse ? percentage : percentage * 5;
        }
        return 0;
    }

    static string GetLetter(double rank, string[] ranks)
    {
        double percentage = 100 / ranks.Length;
        for (int i = 0; i < ranks.Length; i++)
        {
            if (rank <= percentage * (i + 1))
            {
                return ranks[i];
            }
        }
        return ranks[0];
    }
}
