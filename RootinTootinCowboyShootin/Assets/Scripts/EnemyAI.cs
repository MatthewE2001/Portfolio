using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public bool ableToShoot;
    public float doingDamageModifier;
    public float takingDamageModifier;
    public float allyModifier;
    public float allyRange;
    public float stayCloseMultiplier;
    public float stayCloseRange;
    public float Range;
    public float difficultyRating;
    EnemyFileUpdate updater;
    public GameObject targetIndicator;
    const int checkArea = 5;
    public float[,] areaWeights = new float[checkArea, checkArea];
    public List<KeyValuePair<float, KeyValuePair<int, int>>> areaOutput = new List<KeyValuePair<float, KeyValuePair<int, int>>>(checkArea * checkArea);
    public Vector3 targetLocation;
    Vector3 translationVector = new Vector3(checkArea / 2, 0, checkArea / 2);
    public bool recalculate;
    float range;
    public LayerMask layer;
    // Start is called before the first frame update
    void Start()
    {
        layer = 1 << 9;
        range = GetComponent<WeaponBase>().newRange;
        updater = GameObject.Find("EnemyManager").GetComponent<EnemyFileUpdate>();
    }

    // Update is called once per frame
    void Update()
    {
        doingDamageModifier = updater.doingDamageModifier;
        takingDamageModifier = updater.takingDamageModifier;
        allyModifier = updater.allyModifier;
        allyRange = updater.allyRange;
        stayCloseMultiplier = updater.stayCloseMultiplier;
        stayCloseRange = updater.stayCloseRange;
        difficultyRating = updater.difficultyRating;
        CalculateWeights();
    }

    void CalculateWeights()
    {
        ResetValues();

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Player"))
        {
            CanAttack(enemy);
            CanBeAttacked(enemy);
        }
        foreach (GameObject ally in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            NearAllys(ally);
        }
        StayClose();
        ChooseLocation();
        ableToShoot = false;
    }

    void ResetValues()
    {
        for (int x = 0; x < checkArea; x++)
        {
            for (int y = 0; y < checkArea; y++)
            {
                areaWeights[x, y] = 0;
            }
        }
    }

    void CanBeAttacked(GameObject target)
    {
        WeaponBase targetWeapon = target.GetComponent<WeaponBase>();
        int range = (int)targetWeapon.newRange -(int)targetWeapon.newRange / 3;
        Vector3 localPos = GetLocalPosition(target) + translationVector;
        for (int x = (int)localPos.x - range; x < localPos.x + range; x++)
        {
            for (int y = (int)localPos.z - range; y < localPos.z + range; y++)
            {
                if (x >= 0 && x < checkArea && y >= 0 && y < checkArea)
                {
                    areaWeights[x, y] -= takingDamageModifier;//GetDPS(targetWeapon) * takingDamageModifier;
                }
            }
        }
    }

    void CanAttack(GameObject target)
    {
        WeaponBase targetWeapon = this.GetComponent<WeaponBase>();
        int range = (int)targetWeapon.newRange - (int)targetWeapon.newRange / 3;
        Vector3 localPos = GetLocalPosition(target) + translationVector;
        for (int x = (int)localPos.x - range; x < localPos.x + range; x++)
        {
            for (int y = (int)localPos.z - range; y < localPos.z + range; y++)
            {
                if (x >= 0 && x < checkArea && y >= 0 && y < checkArea)
                {
                    areaWeights[x, y] += doingDamageModifier;//GetDPS(targetWeapon) * doingDamageModifier;
                    ableToShoot = true;
                }
            }
        }
    }

    void NearAllys(GameObject freindly)
    {
        if (freindly != this.gameObject)
        {
            WeaponBase targetWeapon = this.GetComponent<WeaponBase>();
            int range = 5;
            Vector3 localPos = GetLocalPosition(freindly) + translationVector;
            for (int x = (int)localPos.x - range; x < localPos.x + range; x++)
            {
                for (int y = (int)localPos.z - range; y < localPos.z + range; y++)
                {
                    if (x >= 0 && x < checkArea && y >= 0 && y < checkArea)
                    {
                        areaWeights[x, y] *= allyModifier;
                    }
                }
            }
        }
    }

    void StayClose()
    {
        WeaponBase targetWeapon = this.GetComponent<WeaponBase>();
        int range = (int)stayCloseRange;
        Vector3 localPos = GetLocalPosition(this.gameObject) + translationVector;
        for (int x = (int)localPos.x - range; x < localPos.x + range; x++)
        {
            for (int y = (int)localPos.z - range; y < localPos.z + range; y++)
            {
                if (x >= 0 && x < checkArea && y >= 0 && y < checkArea)
                {
                    if (areaWeights[x, y] > 0)
                        areaWeights[x, y] *= stayCloseMultiplier;
                    else
                    {
                        areaWeights[x, y] /= stayCloseMultiplier;
                    }
                }
            }
        }
    }

    void ChooseLocation()
    {
            for (int x = 0; x < checkArea; x++)
            {
                for (int y = 0; y < checkArea; y++)
                {
                        areaOutput.Add(new KeyValuePair<float, KeyValuePair<int, int>>(areaWeights[x, y], new KeyValuePair<int, int>(x, y)));
                }
            }
            areaOutput = areaOutput.OrderBy(x => x.Key).ToList();
            if (areaOutput[areaOutput.Count-1].Key <= 0)
            {
                targetLocation = GameObject.FindGameObjectWithTag("Player").transform.position;
            }
            else
            {
                int bestX;
                int bestY;
                int i = 0;
                do
                {
                    int rand = Random.Range(areaOutput.Count - (areaOutput.Count / (int)(11 * difficultyRating)) - 1, areaOutput.Count - 1);
                    //Debug.Log("count = " + areaOutput.Count + " random is: " + rand);
                    bestX = areaOutput[rand].Value.Key;
                    bestY = areaOutput[rand].Value.Value;
                    targetLocation = new Vector3(bestX + this.transform.position.x, this.transform.position.y, this.transform.position.z + bestY) - translationVector;
                    difficultyRating -= 1;
                    i++;
                } while (Physics.OverlapBox(targetLocation, new Vector3(1f, 1f, 1f), Quaternion.identity, layer).Length > 0);
                difficultyRating += i;
            }
            if (targetIndicator != null)
            {
                targetIndicator.transform.position = new Vector3(targetLocation.x,1.3f, targetLocation.z);
            }
        
        this.GetComponent<NavMeshAgent>().SetDestination(targetLocation);
    }

    Vector3 GetLocalPosition(GameObject gameObject)
    {
        return gameObject.transform.position - this.transform.position;
    }

    float GetDPS(WeaponBase weapon)
    {
        return weapon.bulletDamage * (weapon.fullMagazine / (weapon.fullMagazine * weapon.cooldownInSeconds + weapon.setReloadTime));
    }
}
