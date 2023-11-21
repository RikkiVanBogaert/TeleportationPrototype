using System.Collections;
using System.Collections.Generic;
using Abertay.Analytics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    private float timePassed;

    void Update()
    {
        timePassed += Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "PlayerBody") return;

        //analytics;
        Dictionary<string, object> data = new Dictionary<string, object>();
        data.Add("timesTeleported", other.GetComponent<Teleportation>().TimesTeleported);
        var movementPlayer = other.GetComponent<Movement>();
        data.Add("deaths", movementPlayer.DeathsToFalling + movementPlayer.DeathsToEnemies);
        data.Add("deathsToFalling", movementPlayer.DeathsToFalling);
        data.Add("deathsToEnemies", movementPlayer.DeathsToEnemies);
        data.Add("timePassed", timePassed);
        data.Add("sceneName", SceneManager.GetActiveScene().name);
        AnalyticsManager.SendCustomEvent("LevelFinished", data);

        //two less since main menu and analytics init scene dont count
        if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            //Reload level 0
            SceneManager.LoadScene(0);
        }
    }
}
