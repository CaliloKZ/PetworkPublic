using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutoManager : MonoBehaviour
{
    public static TutoManager instance;
    [SerializeField][TextArea]
    private string[] _tutoTexts;
    private Button _bt;

    [SerializeField]
    private Transform[] _arrowPos,
                        _boxPos;
    [SerializeField]
    private GameObject _arrow,
                       _textBox,
                       _blockBTs;

    [SerializeField]
    private TextMeshProUGUI _tutoText;

    //[SerializeField]
    //private Button _camCloseArea,
    //              _camBackBT;

    [SerializeField]
    private GameObject _tutoCatBT, //botões tutorial
                       _tutoMenuBT,
                       _tutoStoreBT,
                       _tutoCloseMenuBT,
                       _tutoCloseStoreBT,
                       _tutoCamCloseBT,
                       _tutoSnackBT;


    private int _tutoIndex = -1;

    public bool tutoDone { get; private set; }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("Another instance of TutoManager has been destroyed");
            Destroy(gameObject);
        }
        _bt = GetComponent<Button>();
    }
    private void Start()
    {
        NextTuto();
    }
    public void NextTuto()
    {
        _tutoIndex++;
        if (_tutoIndex == 18)
        {
            _bt.interactable = false;
            DataStorage.instance.tutoDone = true;
            DataStorage.instance.SaveGame();
            Destroy(gameObject);
            return;
        }
        ChangeBoxPos(_tutoIndex);
        ChangeArrowPos(_tutoIndex);
        _tutoText.text = _tutoTexts[_tutoIndex];
        switch (_tutoIndex)
        {
            case 0: //boas vindas
                _textBox.SetActive(true);
                break;
            case 1: //clique no gato
                UIManager.instance.UIButtonSound();
                _bt.interactable = false;
                _tutoCatBT.SetActive(true);
                _arrow.SetActive(true);
                break;
            case 2: //dar petisco
                _tutoCatBT.SetActive(false);
                _blockBTs.SetActive(true);
                _tutoSnackBT.SetActive(true);
                break;
            case 3:// fechar a tela
                _tutoSnackBT.SetActive(false);
                _tutoCamCloseBT.SetActive(true);
                _arrow.SetActive(false);
                break;
            case 4: //mostrar barras
                _tutoCamCloseBT.SetActive(false);
                _blockBTs.SetActive(false);
                _bt.interactable = true;
                _arrow.SetActive(true);
                break;
            case 5: //barra de amor
                UIManager.instance.UIButtonSound();
                break;
            case 6: //barra de higiene
                UIManager.instance.UIButtonSound();
                break;
            case 7: //barra de comida
                UIManager.instance.UIButtonSound();
                break;
            case 8: //carinha
                UIManager.instance.UIButtonSound();
                break;
            case 9:
                UIManager.instance.UIButtonSound();
                _arrow.SetActive(false);
                break;
            case 10: //abrir menu
                UIManager.instance.UIButtonSound();
                _tutoMenuBT.SetActive(true);
                _bt.interactable = false;
                _arrow.SetActive(true);
                break;
            case 11: //abrir loja
                _tutoMenuBT.SetActive(false);
                _tutoStoreBT.SetActive(true);
                break;
            case 12:
                _tutoStoreBT.SetActive(false);
                _arrow.SetActive(false);
                _bt.interactable = true;
                break;
            case 13: //mostrar pontos
                UIManager.instance.UIButtonSound();
                _arrow.SetActive(true);
                break;
            case 14:
                UIManager.instance.UIButtonSound();
                _arrow.SetActive(false);
                break;
            case 15: //sair da loja
                UIManager.instance.UIButtonSound();
                _bt.interactable = false;
                _tutoCloseStoreBT.SetActive(true);
                _arrow.SetActive(true);
                break;
            case 16: //sair do menu
                _tutoCloseStoreBT.SetActive(false);
                _tutoCloseMenuBT.SetActive(true);
                break;
            case 17: //final
                _bt.interactable = true;
                _tutoCloseMenuBT.SetActive(false);
                _arrow.SetActive(false);
                break;
            default:
                Debug.LogError("Error TutoManager.NextTuto(), var _tutoIndex = " + _tutoIndex);
                break;


        }
    }

    void ChangeArrowPos(int index)
    {
        _arrow.transform.position = _arrowPos[index].position;
        _arrow.transform.rotation = _arrowPos[index].rotation;
        _arrow.transform.localScale = _arrowPos[index].localScale;
    }

    void ChangeBoxPos(int index)
    {
        _textBox.transform.position = _boxPos[index].position;
    }

    public void TutoCatBT(Transform camPos)
    {
        NextTuto();
        UIManager.instance.CatBT(camPos);
    }

    public void TutoMenuBT()
    {
        NextTuto();
        UIManager.instance.OpenCloseMenu(true);
    }

    public void TutoCloseMenuBT()
    {
        NextTuto();
        UIManager.instance.OpenCloseMenu(false);
    }

    public void TutoStoreBT()
    {
        NextTuto();
        UIManager.instance.OpenCloseStore(true);
    }

    public void TutoCloseStoreBT()
    {
        NextTuto();
        UIManager.instance.OpenCloseStore(false);
    }

    public void TutoSnackBT()
    {
        NextTuto();
        GameManager.instance.GiveSnack();
    }

    public void TutoCamCloseBT()
    {
        NextTuto();
        UIManager.instance.CloseZoomCam();
    }

    public void LoadTuto(bool done)
    {
        if (done)
        {
            Destroy(gameObject);
        }
    }
}
