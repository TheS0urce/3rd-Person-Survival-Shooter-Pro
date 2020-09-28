using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int _maxHealth;
    [SerializeField]
    private int _minHealth;
    [SerializeField]
    private int _currentHealth;


    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //damage (int damageAmount)
        //currenthealth - damageAmount
        //check if dead (if currenthealth < minhealth)
        //destroy
    }

    public void Damage(int damageAmount)
    {
       _currentHealth -= damageAmount;

       if(_currentHealth < _minHealth)
        {
            Destroy(this.gameObject);
        }
    }
}
