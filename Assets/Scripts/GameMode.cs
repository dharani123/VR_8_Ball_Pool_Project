using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    // This class us used to set the Game Mode in the current scene.
    #region Singelton

    public static GameMode Instance;
    private void Awake()
    {
        Instance = this;
    }

    #endregion

    public enum GameType { Single, Multiplayer, Computer };
    public GameType gameType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
