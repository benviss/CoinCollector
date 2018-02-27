using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour {

    public List<PlatformController> platformPrefabs = new List<PlatformController>();
    public List<PlatformController> challengePrefabs = new List<PlatformController>();
    public float despawnX = -40;
    public float spawnX = 40;
    public float challengePercentage = .2f;

    public List<PlatformController> platforms = new List<PlatformController>();
    private PlatformController LastPlat;

    void Awake() {
        float lastX = -40;
        while (true) {
            PlatformController lastPlatform = addPlatform(lastX);
            lastX += lastPlatform.size;
            if (lastX > 0) {
                return;
            }
        }
    }

    void Update() {
        CheckPlatforms();
    }

    void CheckPlatforms() {
        float LastX = LastPlat.transform.position.x + .5f * LastPlat.size;
        int indexToMove = -1;
        for (int i = 0; i < platforms.Count - 1; i++) {
            if (platforms[i].transform.position.x + .5f * platforms[i].size < despawnX) {
                indexToMove = i;
            }
        }
        if (indexToMove >= 0) {
            MoveAndReroll(indexToMove, LastX);
        }
        else if (LastX < spawnX) {
            AddThing(LastX);
        }
    }

    PlatformController addPlatform(float xOffset) {
        PlatformController newPlat = Instantiate(platformPrefabs[Random.Range(0, platformPrefabs.Count)], transform.position + Vector3.right * xOffset, Quaternion.identity, transform) as PlatformController;
        newPlat.transform.position = new Vector3(newPlat.transform.position.x + newPlat.size * .5f - GameManager.Instance.scrollSpeed * Time.deltaTime, newPlat.transform.position.y, newPlat.transform.position.z);
        platforms.Add(newPlat);
        LastPlat = newPlat;
        return newPlat;
    }

    PlatformController addChallenge(float xOffset) {
        PlatformController newPlat = Instantiate(challengePrefabs[Random.Range(0, challengePrefabs.Count)], transform.position + Vector3.right * xOffset, Quaternion.identity, transform) as PlatformController;
        newPlat.transform.position = new Vector3(newPlat.transform.position.x + newPlat.size * .5f - GameManager.Instance.scrollSpeed * Time.deltaTime, newPlat.transform.position.y, newPlat.transform.position.z);
        platforms.Add(newPlat);
        LastPlat = newPlat;
        return newPlat;
    }

    void MoveAndReroll(int index, float xOffset) {
        Destroy(platforms[index].gameObject);
        platforms.RemoveAt(index);
        AddThing(xOffset);
    }

    void AddThing(float xOffset) {
        float value = Random.Range(0, 1);

        if (value < challengePercentage) {
            addChallenge(xOffset);
        }
        else {
            addPlatform(xOffset);
        }
    }
}
