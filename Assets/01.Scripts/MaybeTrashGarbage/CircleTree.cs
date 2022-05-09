using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleTree : MonoBehaviour
{
    public RectTransform installBtn;
    // Start is called before the first frame update
    void Start()
    {
        CameraManager cameraManager = CameraManager.Instance;
        cameraManager.TopView += LookCameraTopView;
        cameraManager.QuarterView += LookCameraQuarterView;

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }


        gameObject.transform.GetChild(1).GetChild(1).GetComponent<Button>().onClick.AddListener(() =>
        {
            TestTurretDataBasee.Instance.Create(
                installBtn.GetComponent<Image>(),
                installBtn.GetComponent<testSkillScript>().count,
                installBtn.GetComponent<testSkillScript>().floor
                );

            if(GameManagerr.Instance.goldAmount >= GameManagerr.Instance.turretPtice)
            {
                installBtn.GetComponent<testSkillScript>().onTurret = true;
                gameObject.transform.GetChild(1).gameObject.SetActive(false);
                gameObject.transform.GetChild(2).gameObject.SetActive(true);
            }

        });
    }
    private void LookCameraTopView()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(0, -90, 0)), Time.unscaledDeltaTime);
    }

    private void LookCameraQuarterView()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(-30, -50, 0)), Time.unscaledDeltaTime);
    }
}
