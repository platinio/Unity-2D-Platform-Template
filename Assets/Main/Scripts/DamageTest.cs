using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamaga.DamageSystem;

public class DamageTest : MonoBehaviour
{
    [SerializeField] private DamageableManager dmgManager = null;
    [SerializeField] private int dmg;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("dmg");
            DamageInfo dmgInfo = new DamageInfo();
            dmgInfo.dmg = dmg;

            dmgManager.DoDamage( dmgInfo );

        }
    }

}
