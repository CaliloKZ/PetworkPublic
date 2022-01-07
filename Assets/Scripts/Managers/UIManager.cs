using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MEC;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField]
    private Camera _zoomCam;

    [SerializeField]
    private Slider _loveSlider, 
                   _healthSlider, 
                   _hungerSlider;

    [SerializeField]
    private TextMeshProUGUI _foodBowlTime,
                            _waterBowlTime,
                            _sandboxTime;

    private GameObject[] _bgElements;

    [SerializeField]
    private GameObject _camCase;
    [SerializeField]
    private GameObject _interactPanel;

    [SerializeField]
    private GameObject _panelMenu,
                       _panelInventory,
                       _panelStore,
                       _panelConfig,
                       _panelCredits,
                       _panelVet,
                       _panelAward, //janela que mostra que recebeu uma foto nova
                       _panelNewCat, //janela que mostra que recebeu um gato novo
                       _neWindow; //janela que diz pontos insuficientes

    [SerializeField]
    private GameObject _bowlsBTArea,
                       _sandBoxBTArea,
                       _catBTArea;

    [SerializeField]
    private GameObject _menuBT;
    [SerializeField]
    private GameObject _closeMenuArea;

    [SerializeField]
    private GameObject _background;

    [SerializeField]
    private TextMeshProUGUI[] _pointsTexts;

    [SerializeField]
    private GameObject _cat,
                       _houseCat, //o gato na casinha
                       _boxCat, //o gato na caixa
                       _sandBoxCat,
                       _foodCat,
                       _catHouse, //a casinha do gato
                       _catBox; //a caixa do gato

    public GameObject _activeCat { get; private set; }

    [SerializeField]
    private GameObject _catBT,
                       _houseCatBT,
                       _boxCatBT,
                       _vetBT;

    [SerializeField]
    private Animator _catAnim,
                     _catBoxAnim,
                     _catHouseAnim;

    [SerializeField]
    private Button[] _playBTs;

    [SerializeField]
    private Button _catPlayAreaBT;
    [SerializeField]
    private TextMeshProUGUI _catPlayText;

    [SerializeField]
    private GameObject[] _detectAreasCat,
                         _detectAreasCatBox,
                         _detectAreasCatHouse;

    private GameObject _selectedDetectArea;

    [SerializeField]
    private Image _catHappiness; //carinha do gato na barra de status
    [SerializeField]
    private Sprite[] _catHappinessSprites; //0 = triste, 1 = feliz

    [SerializeField]
    private Image _awardPhoto,
                  _newCatPhoto;

    private int _selectedPlayObjIndex;

    private bool _isTutoOn = true;

    [SerializeField]
    private Transform _loveSpawnPos,
                      _soapSpawnPos,
                      _foodSpawnPos,
                      _statsArea;

    [SerializeField]
    private GameObject _loveStatusPrefab,
                       _soapStatusPrefab,
                       _foodStatusPrefab;

    [SerializeField]
    private CatPlayControl _catPlayControl;

    [SerializeField]
    private GameObject _bowlsTutoPanel,
                       _sandboxTutoPanel,
                       _catTutoPanel;
    

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("Another instance of UIManager has been destroyed");
            Destroy(gameObject);
        }

        //muda o tamanho do background com base na resolução do dispositivo.
        float height = Camera.main.orthographicSize * 2;
        float width = height * Screen.width / Screen.height;
        _background.transform.localScale = new Vector2(width / 17.77778f, height / 10); //gambiarra master
    }

    private void Start()
    {
        SetActiveCat();
    }

    public void UpdateHunger(int value)
    {
        _hungerSlider.value = value;
    }

    public void UpdateHealth(int value)
    {
        _healthSlider.value = value;
    }

    public void UpdateLove(int value)
    {
        _loveSlider.value = value;
    }

    public void NewPhoto(GameObject photo) //aviso que recebeu uma foto nova
    {
        _awardPhoto.sprite = photo.GetComponent<SpriteRenderer>().sprite;
        _panelAward.SetActive(true);
        SoundManager.instance.PlaySFX("yay");
    }

    public void NewCat(Cats cat)
    {
        _newCatPhoto.sprite = cat.spr;
        _panelNewCat.SetActive(true);
        SoundManager.instance.PlaySFX("yay");
    }

    public void CloseAwardPanel() // fecha o aviso que recebeu uma foto nova
    {
        _panelAward.SetActive(false);
        UIButtonClose();
        GameManager.instance.RollCat();
    }

    public void CloseNewCatPanel()
    {
        _panelNewCat.SetActive(false);
        UIButtonClose();
    }

    public void OpenCloseMenu(bool open)
    {
        if (open)
        {
            _panelMenu.SetActive(true);
            _interactPanel.SetActive(false);
            UIButtonSound();
        }
        else
        {
            _panelMenu.SetActive(false);
            _interactPanel.SetActive(true);
            UIButtonClose();
        }
    }

    public void VetBT(bool activate)
    {
        _vetBT.SetActive(activate);
    }

    public void OpenCloseVetPanel(bool open)
    {
        _panelVet.SetActive(open);
        UIButtonSound();
    }

    public void OpenCloseInventory(bool open)
    {
        if (open)
        {
            _panelInventory.SetActive(true);
            _closeMenuArea.SetActive(false);
            UIButtonSound();
        }
        else
        {
            _panelInventory.SetActive(false);
            _closeMenuArea.SetActive(true);
            GameManager.instance.SaveGame();
            UIButtonClose();
        }
    }

    public void OpenCloseStore(bool open)
    {
        if(open)
        {
            _panelStore.SetActive(true);
            _closeMenuArea.SetActive(false);
            UIButtonSound();
        }
        else
        {
            _panelStore.SetActive(false);
            _closeMenuArea.SetActive(true);
            UIButtonClose();
        }
    }

    public void OpenCloseConfig(bool open)
    {
        if (open)
        {
            _panelConfig.SetActive(true);
            _closeMenuArea.SetActive(false);
            UIButtonSound();
        }
        else
        {
            _panelConfig.SetActive(false);
            _closeMenuArea.SetActive(true);
            GameManager.instance.SaveGame();
            UIButtonClose();
        }
    }

    public void OpenCloseCredits(bool open)
    {
        if (open)
        {
            _panelCredits.SetActive(true);
            _closeMenuArea.SetActive(false);
            UIButtonSound();
        }
        else
        {
            _panelCredits.SetActive(false);
            _closeMenuArea.SetActive(true);
            UIButtonClose();
        }
    }

    public void OpenCloseNotEnoughWindow(bool open) //abrir ou fechar a janela que diz pontos insuficientes
    {
        _neWindow.SetActive(open);
    }

    public void UpdatePointsText(int pts)
    {
        for(int i = 0; i < _pointsTexts.Length; i++)
        {
            _pointsTexts[i].text = pts.ToString();
        }
    }

    public void BowlsBT(Transform camPos)
    {
        OpenZoomCam(camPos, 0);
        if (!GameManager.instance.firstTimeBowls)
        {
            _bowlsTutoPanel.SetActive(true);
            GameManager.instance.firstTimeBowls = true;
            PlayerPrefs.SetInt("BowlsFirst", GameManager.instance.firstTimeBowls ? 1 : 0);
        }
        UIButtonSound();
    }

    public void SandBoxBT(Transform camPos)
    {
        OpenZoomCam(camPos, 1);
        if (!GameManager.instance.firstTimeSandbox)
        {
            _sandboxTutoPanel.SetActive(true);
            GameManager.instance.firstTimeSandbox = true;
            PlayerPrefs.SetInt("SandboxFirst", GameManager.instance.firstTimeSandbox ? 1 : 0);
        }
        UIButtonSound();
    }

    public void CatBT(Transform camPos)
    {
        OpenZoomCam(camPos, 2);
        if (!GameManager.instance.firstTimeCat && TutoManager.instance == null)
        {
            _catTutoPanel.SetActive(true);
            GameManager.instance.firstTimeCat = true;
            PlayerPrefs.SetInt("CatFirst", GameManager.instance.firstTimeCat ? 1 : 0);
        }
        UIButtonSound();
    }

    void OpenZoomCam(Transform camPos, int obj)//0 - bowls, 1 - sandBox, 2 - cat
    {
        _zoomCam.gameObject.SetActive(true);
        _zoomCam.transform.position = camPos.position;
        _camCase.SetActive(true);
        _interactPanel.SetActive(false);
        switch (obj)
        {
            case 0:
                _bowlsBTArea.SetActive(true); 
                break;
            case 1:
                _sandBoxBTArea.SetActive(true);
                break;
            case 2:
                _catBTArea.SetActive(true);
                break;
            default:
                Debug.LogError("Error UIManager.OpenZoomCam(camPos, obj), var obj = " + obj);
                break;
        }
    }

    public void CloseZoomCam()
    {
        _zoomCam.gameObject.SetActive(false);
        _camCase.SetActive(false);
        _interactPanel.SetActive(true);
        _bowlsBTArea.SetActive(false);
        _sandBoxBTArea.SetActive(false);
        _catBTArea.SetActive(false);
        if (_activeCat == _cat)
        {
            for (int i = 0; i < _detectAreasCat.Length; i++)
            {
                _detectAreasCat[i].SetActive(false);
            }
        }
        else if (_activeCat == _boxCat)
        {
            for (int i = 0; i < _detectAreasCatBox.Length; i++)
            {
                _detectAreasCatBox[i].SetActive(false);
            }
        }
        else if (_activeCat == _houseCat)
        {
            for (int i = 0; i < _detectAreasCatHouse.Length; i++)
            {
                _detectAreasCatHouse[i].SetActive(false);
            }
        }
        _catPlayControl.OnClose();
        EnableDisablePlayBTs(true);
        _catPlayAreaBT.gameObject.SetActive(false);
        _catPlayText.gameObject.SetActive(false);
        GameManager.instance.SaveGame();
        UIButtonClose();
    }

    public void ChangeCatPos(int posIndex)//0 - normal, 1 - casinha, 
                                          //2 - caixa, 3 - caixa de areia, 4 - comida
    {
        switch (posIndex)
        {
            case 0:
                _cat.SetActive(true);
                _houseCat.SetActive(false);
                _boxCat.SetActive(false);
                _activeCat = _cat;
                //_sandBoxCat.SetActive(false);
                //_foodCat.SetActive(false);
                _catBT.SetActive(true);
                _houseCatBT.SetActive(false);
                break;
            case 1:
                if(_catHouse.activeInHierarchy)
                {
                    _cat.SetActive(false);
                    _houseCat.SetActive(true);
                    _boxCat.SetActive(false);
                    _activeCat = _houseCat;
                    // _sandBoxCat.SetActive(false);
                    // _foodCat.SetActive(false);
                    _catBT.SetActive(false);
                    _houseCatBT.SetActive(true);
                }
                else
                {
                    Debug.Log("house is not active");
                    break;
                }
                break;
            case 2:
                if (_catBox.activeInHierarchy)
                {
                    _cat.SetActive(false);
                    _houseCat.SetActive(false);
                    _boxCat.SetActive(true);
                    _activeCat = _boxCat;
                    // _sandBoxCat.SetActive(false);
                    // _foodCat.SetActive(false);
                    _catBT.SetActive(false);
                    _houseCatBT.SetActive(true);
                }
                else
                {
                    Debug.Log("box is not active");
                    break;
                }
                break;
            //case 3:
            //    _cat.SetActive(false);
            //    _houseCat.SetActive(false);
            //    _boxCat.SetActive(false);
            //   // _sandBoxCat.SetActive(true);
            //   // _foodCat.SetActive(false);
            //    break;
            //case 4:
            //    _cat.SetActive(false);
            //    _houseCat.SetActive(false);
            //    _boxCat.SetActive(false);
            //   // _sandBoxCat.SetActive(false);
            //   // _foodCat.SetActive(true);
            //    break;
            default:
                Debug.LogError("Error UIManager.ChangeCatPos(posIndex), var posIndex = " + posIndex);
                break;
        }
    }

    public void SetActiveCat()
    {
        if (_cat.activeInHierarchy)
        {
            _activeCat = _cat;
        }
        else if (_houseCat.activeInHierarchy)
        {
            _activeCat = _houseCat;
        }
        else if (_boxCat.activeInHierarchy)
        {
            _activeCat = _boxCat;
        }
    }

    public void GiveSnack()
    {

        if (_activeCat == _cat)
        {
            for (int i = 0; i < _detectAreasCat.Length; i++)
            {
                _detectAreasCat[i].SetActive(false);
            }
        }
        else if (_activeCat == _boxCat)
        {
            for (int i = 0; i < _detectAreasCatBox.Length; i++)
            {
                _detectAreasCatBox[i].SetActive(false);
            }
        }
        else if (_activeCat == _houseCat)
        {
            for (int i = 0; i < _detectAreasCatHouse.Length; i++)
            {
                _detectAreasCatHouse[i].SetActive(false);
            }
        }
        _catPlayAreaBT.gameObject.SetActive(false);
        _catPlayText.gameObject.SetActive(false);
        EnableDisablePlayBTs(true);

        if (_activeCat == null)
        {
            SetActiveCat();
        }

        Animator selectedAnim = _activeCat.GetComponent<Animator>();

        if (selectedAnim != null)
        {
            selectedAnim.SetTrigger("Eat");
        }
        else
        {
            Debug.LogError("selectedAnim not found");
        }
    }

    public void SelectPlayObj(int index)
    {
        _selectedPlayObjIndex = index;
        if(_activeCat == _cat)
        {
            for (int i = 0; i < _detectAreasCat.Length; i++)
            {
                _detectAreasCat[i].SetActive(false);
            }
            _selectedDetectArea = _detectAreasCat[index];
        }
        else if(_activeCat == _boxCat)
        {
            for (int i = 0; i < _detectAreasCatBox.Length; i++)
            {
                _detectAreasCatBox[i].SetActive(false);
            }
            _selectedDetectArea = _detectAreasCatBox[index];
        }
        else if (_activeCat == _houseCat)
        {
            for (int i = 0; i < _detectAreasCatHouse.Length; i++)
            {
                _detectAreasCatHouse[i].SetActive(false);
            }
            _selectedDetectArea = _detectAreasCatHouse[index];
        }

        _selectedDetectArea.SetActive(true);
        _catPlayText.gameObject.SetActive(true);
        EnableDisablePlayBTs(true);
        _playBTs[index + 1].interactable = false;
        _catPlayAreaBT.gameObject.SetActive(true);
    }

    public void EnableDisablePlayBTs(bool enable)
    {
        for(int i = 0; i < _playBTs.Length; i++)
        {
            _playBTs[i].interactable = enable;
        }
    }

    public void SetHappiness(bool happy)
    {
        _catHappiness.sprite = _catHappinessSprites[happy ? 1: 0];
    }

    public IEnumerator<float> SpawnLoveStatus(bool add)
    {
        GameObject loveStatus = Instantiate(_loveStatusPrefab, _loveSpawnPos.position, Quaternion.identity);
        loveStatus.transform.SetParent(_statsArea);
        loveStatus.transform.localScale = Vector3.one;
        if (add)
        {
            loveStatus.GetComponentInChildren<TextMeshProUGUI>().text = "+";
        }
        else
        {
            loveStatus.GetComponentInChildren<TextMeshProUGUI>().text = "-";
        }
        yield return Timing.WaitForSeconds(1f);
        Destroy(loveStatus);
    }

    public IEnumerator<float> SpawnSoapStatus(bool add)
    {
        GameObject soapStatus = Instantiate(_soapStatusPrefab, _soapSpawnPos.position, Quaternion.identity);
        soapStatus.transform.SetParent(_statsArea);
        soapStatus.transform.localScale = Vector3.one;
        if (add)
        {
            soapStatus.GetComponentInChildren<TextMeshProUGUI>().text = "+";
        }
        else
        {
            soapStatus.GetComponentInChildren<TextMeshProUGUI>().text = "-";
        }
        yield return Timing.WaitForSeconds(1f);
        Destroy(soapStatus);
    }

    public IEnumerator<float> SpawnFoodStatus(bool add)
    {
        GameObject foodStatus = Instantiate(_foodStatusPrefab, _foodSpawnPos.position, Quaternion.identity);
        foodStatus.transform.SetParent(_statsArea);
        foodStatus.transform.localScale = Vector3.one;
        if (add)
        {
            foodStatus.GetComponentInChildren<TextMeshProUGUI>().text = "+";
        }
        else
        {
            foodStatus.GetComponentInChildren<TextMeshProUGUI>().text = "-";
        }
        yield return Timing.WaitForSeconds(1f);
        Destroy(foodStatus);
    }

    public void FinishPlay()
    {
        _catPlayAreaBT.gameObject.SetActive(false);
        EnableDisablePlayBTs(true);
        SoundManager.instance.StopSFX("Purr");
        SoundManager.instance.PlaySFX("EndPlay");
        _catPlayText.gameObject.SetActive(false);
        _selectedDetectArea.SetActive(false);
        _activeCat.transform.Find("ClosedEyes").gameObject.SetActive(false);
        GameManager.instance.FinishPlay(_selectedPlayObjIndex);
    }

    public void UIButtonSound()
    {
        SoundManager.instance.PlaySFX("ButtonPress");
    }

    public void UIButtonClose()
    {
        SoundManager.instance.PlaySFX("CloseUI");
    }
}
