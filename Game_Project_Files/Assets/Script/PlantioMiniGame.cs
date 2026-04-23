using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlantioMiniGame : MonoBehaviour
{

    [Header("Tempo")]
    public float tempoLimite = 1f;
    private float tempoAtual;

    [Header("UI")]
    public Slider barraTempo;
    public Image iconEscavar;
    public Image iconMuda;
    public Image iconA;
    public Image iconD;
    private int etapaAtual = 0;
    private bool ativo = false;

    private int alternanciasNecessarias = 6;
    private int alternanciasAtuais = 0;
    private bool esperarBotaoA = true;

    public UnityEvent OnSucesso;
    public UnityEvent OnFalha;

    void Start()
    {
        IniciarMiniGame();
    }

    void Update()
    {
        if (!ativo) return;

        tempoAtual -= Time.deltaTime;
        AtualizarBarraTempo();

        if (tempoAtual <= 0)
        {
            Falhar();
            return;
        }

        switch (etapaAtual)
        {
            case 0: EtapaEscavar(); break;
            case 1: EtapaPegarMuda(); break;
            case 2: EtapaAjustarTerra(); break;
        }
    }

    public void IniciarMiniGame()
    {
        ativo = true;
        etapaAtual = 0;
        alternanciasAtuais = 0;
        esperarBotaoA = true;
        tempoAtual = tempoLimite;

        AtualizarUI();
    }

    void EtapaEscavar()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ProximaEtapa();
        }
    }



    void EtapaPegarMuda()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ProximaEtapa();
        }
    }


    void EtapaAjustarTerra()
    {
        if (esperarBotaoA && Input.GetKeyDown(KeyCode.A))
        {
            Alternar();
        }
        else if (!esperarBotaoA && Input.GetKeyDown(KeyCode.D))
        {
            Alternar();
        }
    }


    void Alternar()
    {
        alternanciasAtuais++;
        esperarBotaoA = !esperarBotaoA;
        AtualizarUI();

        if (alternanciasAtuais >= alternanciasNecessarias)
            Sucesso();
    }

    void ProximaEtapa()
    {
        etapaAtual++;
        tempoAtual = tempoLimite;
        AtualizarUI();
    }

    void AtualizarUI()
    {
        iconEscavar.gameObject.SetActive(false);
        iconMuda.gameObject.SetActive(false);
        iconA.gameObject.SetActive(false);
        iconD.gameObject.SetActive(false);

        if (etapaAtual == 0)
            iconEscavar.gameObject.SetActive(true);

        else if (etapaAtual == 1)
            iconMuda.gameObject.SetActive(true);

        else if (etapaAtual == 2)
        {
            if (esperarBotaoA)
                iconA.gameObject.SetActive(true);
            else
                iconD.gameObject.SetActive(true);
        }
    }

    void AtualizarBarraTempo()
    {
        barraTempo.value = tempoAtual / tempoLimite;
    }

    void Sucesso()
    {

        ativo = false;
        OnSucesso?.Invoke();

    }

    void Falhar()
    {
        ativo = false;
        OnFalha?.Invoke();
    }
}
