using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    protected Game m_game => Game.instance;
    protected GameLoder m_loader => GameLoder.instance;

    public virtual void LoadScene(string scene) => m_loader.Load(scene);
}
