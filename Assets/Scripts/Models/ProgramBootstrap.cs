using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgramBootstrap : MonoBehaviour
{
    [SerializeField] private ProgramStateOperator _stateOperator;

    private void Awake()
    {
        DBOperator dBOperator = new DBOperator();
        //WindowModelCreator windowModelCreator = new WindowModelCreator();

        _stateOperator.Init(dBOperator);
    }
}
