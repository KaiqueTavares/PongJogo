using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class CanvasController : NetworkBehaviour {

    [SyncVar(hook = "UpdateScoreOne")]
    private int playerOneScore;

    [SyncVar(hook = "UpdateScoreTwo")]
    private int playerTwoScore;

    [SyncVar(hook = "UpdateCountdown")]
    private string countdownTextValue;

    public TextMeshProUGUI scoreOneText;
    public TextMeshProUGUI scoreTwoText;
    public TextMeshProUGUI countdownText;

    private int countdown = 3;

    private Bola theBall;

    private void Start()
    {
        playerOneScore = 0;
        playerTwoScore = 0;
    }

    public void PlayerOneScores()
    {
        playerOneScore++;
        UpdateScoreOne(playerOneScore);
    }

    public void PlayerTwoScores()
    {
        playerTwoScore++;
        UpdateScoreTwo(playerTwoScore);
    }

    public void UpdateScoreOne(int value)
    {
        scoreOneText.text = value.ToString();
    }

    public void UpdateScoreTwo(int value)
    {
        scoreTwoText.text = value.ToString();
    }

    public void UpdateCountdown(string value)
    {
        countdownText.text = value;
    }

    public void Countdown(Bola ball)
    {
        if (theBall == null)
        {
            theBall = ball;
        }

        StartCoroutine("DoCountdown");
    }

    private IEnumerator DoCountdown()
    {
        for (int i = countdown; i > 0; i--)
        {
            countdownTextValue = i.ToString();
            UpdateCountdown(countdownTextValue);
            //countdownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        countdownTextValue = "Go!";
        UpdateCountdown(countdownTextValue);

        yield return new WaitForSeconds(1f);

        countdownTextValue = "";
        UpdateCountdown(countdownTextValue);

        theBall.Respawn();
    }

    /*public int GetPlayerOneScore()
    {
        return playerOneScore;
    }

    public int GetPlayerTwoScore()
    {
        return playerTwoScore;
    }*/
}
