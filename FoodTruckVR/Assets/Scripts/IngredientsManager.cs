using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientsManager : MonoBehaviour
{
    private static IngredientsManager instance;
    public static IngredientsManager Instance { get { return instance; } }
    
    // Food items' names
    public enum GrabbableItemName
    {
        Tentacle,
        FriedTentacle,
        MoonRock,
        FriedMoonRock,
        SolidifiedSpaceJuice,
        FriedSolidifiedSpaceJuice,
        PsychodelicSnail,
        FriedPsychodelicSnail,
        AlienTrunk,
        FriedAlienTrunk,
        SpaceMushroom,
        FriedSpaceMushroom,
        Charcoal,
        Tardigrade,
        FriedTardigrade,
        Bowl,
        FriedBowlOfTardigrades,
        Shaker,
        SpaceGarbage,
        Test1,
        FriedTest1,
        Test2,
        FriedTest2,
        Test3,
        FriedTest3,
        Test4,
        FriedTest4,
        NumOfIngridients
    };

    public enum GrabbableItemType
    {
        Food,
        KitchenTool,
        NumOfTypes
    };

    public enum FlavorType
    {
        Omega,
        Alpha,
        Sigma,
        Theta,
        NumOfTypes
    };

    [System.Serializable]
    public struct mixingResult
    {
        public GrabbableItemName firstIN;
        public GrabbableItemName secondIN;
        public GrabbableItemName resultOUT;
    }

    public mixingResult[] mixingResults;

    public GameObject tentaclePrefab;
    public GameObject moonRockPrefab;
    public GameObject solidifiedSpaceJuicePrefab;
    public GameObject psychodelicSnailPrefab;
    public GameObject alienTrunkPrefab;
    public GameObject spaceMushroomPrefab;
    public GameObject bowlPrefab;
    public GameObject shakerPrefab;
    public GameObject tardigradePrefab;
    public GameObject spaceGarbagePrefab;
    
    public GameObject test1Prefab;
    public GameObject test2Prefab;
    public GameObject test3Prefab;
    public GameObject test4Prefab;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GrabbableItemName GetResultOfMixing(GrabbableItemName inOne, GrabbableItemName inTwo)
    {
        for(int i = 0; i < mixingResults.Length; i++)
        {
            if (inOne == mixingResults[i].firstIN && inTwo == mixingResults[i].secondIN)
            {
                return mixingResults[i].resultOUT;
            }
            if (inOne == mixingResults[i].secondIN && inTwo == mixingResults[i].firstIN)
            {
                return mixingResults[i].resultOUT;
            }
        }

        return GrabbableItemName.SpaceGarbage;
    }

    public GameObject SpawnItem(GrabbableItemName itemName)
    {
        GameObject spawnedItem;

        switch(itemName)
        {
            case GrabbableItemName.Tentacle:
                {
                    spawnedItem = Instantiate(tentaclePrefab);
                    break;
                }
            case GrabbableItemName.MoonRock:
                {
                    spawnedItem = Instantiate(moonRockPrefab);
                    break;
                }
            case GrabbableItemName.SolidifiedSpaceJuice:
                {
                    spawnedItem = Instantiate(solidifiedSpaceJuicePrefab);
                    break;
                }
            case GrabbableItemName.PsychodelicSnail:
                {
                    spawnedItem = Instantiate(psychodelicSnailPrefab);
                    break;
                }
            case GrabbableItemName.AlienTrunk:
                {
                    spawnedItem = Instantiate(alienTrunkPrefab);
                    break;
                }
            case GrabbableItemName.SpaceMushroom:
                {
                    spawnedItem = Instantiate(spaceMushroomPrefab);
                    break;
                }
            case GrabbableItemName.Tardigrade:
                {
                    spawnedItem = Instantiate(tardigradePrefab);
                    break;
                }
            case GrabbableItemName.Bowl:
                {
                    spawnedItem = Instantiate(bowlPrefab);
                    break;
                }
            case GrabbableItemName.Shaker:
                {
                    spawnedItem = Instantiate(shakerPrefab);
                    break;
                }
            case GrabbableItemName.Test1:
                {
                    spawnedItem = Instantiate(test1Prefab);
                    break;
                }
            case GrabbableItemName.Test2:
                {
                    spawnedItem = Instantiate(test2Prefab);
                    break;
                }
            case GrabbableItemName.Test3:
                {
                    spawnedItem = Instantiate(test3Prefab);
                    break;
                }
            case GrabbableItemName.Test4:
                {
                    spawnedItem = Instantiate(test4Prefab);
                    break;
                }
            default:
                {
                    spawnedItem = Instantiate(spaceGarbagePrefab);
                    break;
                }
        }

        return spawnedItem;
    }
}