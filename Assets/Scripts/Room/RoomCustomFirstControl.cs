using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class RoomCustomFirstControl : RoomControl
{
    public GameObject goldenStalfo;
    public GameObject fire;
    public GameObject key;
    private List<GameObject> _fireList = new List<GameObject>();

    public AudioClip key_collection_clip;

    // Start is called before the first frame update
    public override void InitEnemies()
    {
        StartCoroutine(GenerateStalfo());
        StartCoroutine(GenerateFire());
    }

    IEnumerator GenerateStalfo()
    {
        yield return new WaitForSeconds(2f);
        GameObject suicide = Instantiate(goldenStalfo, new Vector3(42, -5, 0), Quaternion.identity);
        Destroy(suicide.GetComponent<StalfoMove>());
        suicide.GetComponent<Rigidbody>().velocity = Vector3.left * 2f;
        suicide.layer = 19;
        _enemies.Add(suicide);
        while (suicide != null)
        {
            yield return null;
        }

        GameObject tmp = Instantiate(fire, new Vector3(40, -8, 0), Quaternion.identity);
        tmp = Instantiate(goldenStalfo, new Vector3(37, -5, 0), Quaternion.identity);
        _enemies.Add(tmp);
        tmp = Instantiate(goldenStalfo, new Vector3(36, -8, 0), Quaternion.identity);
        _enemies.Add(tmp);
        tmp = Instantiate(goldenStalfo, new Vector3(35, -4, 0), Quaternion.identity);
        _enemies.Add(tmp);
        tmp = Instantiate(goldenStalfo, new Vector3(42, -5, 0), Quaternion.identity);
        _enemies.Add(tmp);
        yield return KeyAfterAllDead();
    }

    IEnumerator KeyAfterAllDead()
    {
        while (true)
        {
            if (CheckAllDead() && _isPlayerInRoom)
            {
                GameObject tmp = Instantiate(key, new Vector3(43, -5, 0), Quaternion.identity);
                AudioSource.PlayClipAtPoint(key_collection_clip, Camera.main.transform.position);
                break;
            }

            yield return null;
        }
    }

    IEnumerator GenerateFire()
    {
        GameObject tmp = Instantiate(fire, new Vector3(40, -7, 0), Quaternion.identity);
        _fireList.Add(tmp);


        Vector3[] positions = {new Vector3(35.1f, -5.09f, 0)};
        foreach (Vector3 pos in positions)
        {
            tmp = Instantiate(fire, pos, Quaternion.identity);
            _fireList.Add(tmp);
            tmp.GetComponent<Fire>().StartSelfDestroy(10f);
        }

        while (true)
        {
            int count = 0;
            foreach (GameObject _fire in _fireList)
            {
                if (_fire != null)
                {
                    count += 1;
                }
            }

            bool shouldSpawnFire = count <= 2;
            foreach (Vector3 pos in positions)
            {
                if (shouldSpawnFire)
                {
                    foreach (GameObject _fire in _fireList)
                    {
                        if (_fire != null && _fire.transform.position.Equals(pos))
                        {
                            shouldSpawnFire = false;
                            break;
                        }
                    }
                }

                if (shouldSpawnFire)
                {
                    tmp = Instantiate(fire, pos, Quaternion.identity);
                    _fireList.Add(tmp);
                    tmp.GetComponent<Fire>().StartSelfDestroy(7f);
                }
            }


            yield return new WaitForSeconds(5);
            yield return null;
        }

        yield return null;
    }
}