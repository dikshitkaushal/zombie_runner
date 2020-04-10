using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    /// <summary>
    /// this is singelton/
    /// </summary>
    public static GameManager instance = null;
    player_logic playerlogic;
    GameObject[] m_coins;
    List<coin_logic> m_coinlogic=new List<coin_logic>();
    GameObject[] m_enemy;
    List<enemy_logic> m_enemylogic = new List<enemy_logic>();

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        playerlogic = GameObject.FindGameObjectWithTag("Player").GetComponent<player_logic>();
        m_coins = GameObject.FindGameObjectsWithTag("coins");
        for(int i=0;i<m_coins.Length;i++)
        {
            m_coinlogic.Add(m_coins[i].GetComponent<coin_logic>());
        }
        m_enemy = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i <m_enemy.Length; i++)
        {
            m_enemylogic.Add(m_enemy[i].GetComponent<enemy_logic>());
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            save();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            load();
        }
    }
    public void save()
    {
        playerlogic.save();
        for (int index = 0; index < m_coinlogic.Count; index++)
        {
            m_coinlogic[index].save(index);
        }
        for (int index = 0; index < m_enemylogic.Count; index++)
        {
            m_enemylogic[index].save(index);
        }
        PlayerPrefs.Save();
    }
    public void load()
    {
        playerlogic.load();
        for (int index = 0; index < m_coinlogic.Count; index++)
        {
            m_coinlogic[index].load(index);
        }
        for (int index = 0; index < m_enemylogic.Count; index++)
        {
            m_enemylogic[index].load(index);
        }
    }
}
