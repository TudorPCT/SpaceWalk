using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Launcher : MonoBehaviourPunCallbacks
{
    #region

    [Tooltip("The Ui Panel to let the user enter name, connect and play")]
    [SerializeField]
    private GameObject controlPanel;
    [Tooltip("The UI Label to inform the user that the connection is in progress")]
    [SerializeField]
    private GameObject progressLabel;

    #endregion

    void Start()
    {
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
    }


    #region Public Methods

    public void Connect()
    {
        progressLabel.SetActive(true);
        controlPanel.SetActive(false);
        SceneManager.LoadScene(1);
    }

    #endregion

}
