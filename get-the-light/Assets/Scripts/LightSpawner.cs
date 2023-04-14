using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSpawner : MonoBehaviour
{
    [SerializeField]
    private LightTemplate lightTemplate;

    [SerializeField]
    private GameObject selectedEffect;

    private bool isOnLightButton = false; // 빛 건설 버튼을 눌렀는지 체크
    private GameObject followLightClone = null; // 임시 빛 사용 완료 시 삭제를 위해 저장

    public void ReadyToSpawnLight()
    {
        if (isOnLightButton == true) { CancelSpawnLight(); return; }

        //여기에서 설치 가능한 조건을 확인
        isOnLightButton = true;
        selectedEffect.SetActive(true);
        followLightClone = Instantiate(lightTemplate.PreviewLightPrefab);
    }

    public void SpawnLight(Transform tileTransform)
    {
        if (isOnLightButton == false) { return; }

        Tile tile = tileTransform.GetComponent<Tile>();
        if (tile.IsBuildLight)
        {
            return;
        }

        selectedEffect.SetActive(false);


        isOnLightButton = false;
        tile.IsBuildLight = true;

        Instantiate(lightTemplate.LightPrefab, tileTransform.position, Quaternion.identity);

        Destroy(followLightClone);
    }

    public void CancelSpawnLight()
    {
        isOnLightButton = false;
        selectedEffect.SetActive(false);
        Destroy(followLightClone);
    }

    private void Update()
    {
        if (isOnLightButton && Input.GetKeyDown(KeyCode.Escape))
        {
            CancelSpawnLight();
        }
    }
}
