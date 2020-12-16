using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class TestScript: MonoBehaviour
    {
        public DrinkObject drinkObject;


        private void Start()
        {
            StartCoroutine(Populate());
        }

        IEnumerator Populate()
        {
            if (CLocalDatabase.Instance.didFetchDrink == false)
            {
                yield return new WaitForSeconds(1);
                StartCoroutine(Populate());
            }
            else
            {

                int id = 1;
                Model.Product drink = CLocalDatabase.GetProductInfo(id);

                drinkObject.Setup(drink);
            }
        }

    }
}