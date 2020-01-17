using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public Text gold_text;
    public Text life_text;

    private int _player_gold = 0;
    private int _player_life = 3;

    public delegate void PlayerOutOfLives();
    public static event PlayerOutOfLives onPlayerOutOfLives;
    
    // Start is called before the first frame update
    void Start()
    {
        gold_text.text = "Gold: 0";
        life_text.text = "Life: 3";
        Player.onPlayerCollect += updateGoldCount;
        Player.onPlayerDeath += updateLifeCount;
    }

    void updateGoldCount()
    {
        _player_gold++;
        gold_text.text = "Gold: " + _player_gold;
    }

    void updateLifeCount()
    {
        _player_life--;
        if(_player_life == 0)
        {
            onPlayerOutOfLives?.Invoke();
        }
        else
        {
            life_text.text = "Life: " + _player_life;
        }
    }
    private void OnDestroy()
    {
        Player.onPlayerCollect -= updateGoldCount;
        Player.onPlayerDeath -= updateLifeCount;
    }
}
