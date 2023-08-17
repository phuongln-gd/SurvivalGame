using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public Player player;
    public Level currentLevel;
    public List<Bot> bots = new List<Bot>();
    // Start is called before the first frame update
    void Start()
    {
        SpawnNewTurn();
    }

    public void OnInit()
    {
        player.OnInit();
        SpawnNewTurn();
    }


    public Vector2 RandomPoint()
    {
        Vector2 randPoint = Vector2.zero;
        for(int i = 0; i<50; i++)
        {
            randPoint = currentLevel.RandomPoint();

            if (Vector2.Distance(randPoint, player.transform.position) > 4f)
            {
                return randPoint;
            }
        }

        return randPoint;
    }
    private void SpawnNewBot()
    {
        Vector3 spawnPos = RandomPoint();
        Bot bot = SimplePool.Spawn<Bot>(PoolType.Bot,spawnPos,Quaternion.identity);
        bot.OnInit();
        bots.Add(bot);
    }

    private void SpawnNewTurn()
    {
        int rd = Random.Range(20, 31);

        for (int i = 0; i < rd; i++)
        {
            SpawnNewBot();
        }
    }

    public void BotDeath(Bot bot)
    {
        bots.Remove(bot);
        if(bots.Count <= 5)
        {
            SpawnNewTurn();
        }
    }

}
