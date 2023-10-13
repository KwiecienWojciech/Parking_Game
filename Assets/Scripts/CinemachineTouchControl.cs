using UnityEngine;
using Cinemachine;

public class CinemachineTouchControl : MonoBehaviour
{
    [SerializeField] CinemachineFreeLook cineCam;
    [SerializeField] TouchPanelField touchField;
    [SerializeField] float SenstivityX = .2f;
    [SerializeField] float SenstivityY = .2f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        cineCam.m_XAxis.Value += touchField.TouchDist.x * 100 * SenstivityX * Time.deltaTime;
        cineCam.m_XAxis.Value = Mathf.Clamp(cineCam.m_XAxis.Value, -180, 180);

        cineCam.m_YAxis.Value += touchField.TouchDist.y * SenstivityY * Time.deltaTime * -1f;
        cineCam.m_YAxis.Value = Mathf.Clamp(cineCam.m_YAxis.Value, 0.6f, 0.8f);
    }
}