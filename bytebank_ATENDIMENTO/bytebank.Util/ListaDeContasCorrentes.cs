using bytebank.Modelos.Conta;
using System.Runtime.InteropServices;

namespace bytebank_ATENDIMENTO.bytebank.Util;

public class ListaDeContasCorrentes
{
    private ContaCorrente[] _itens = null;
    private int _proximaPosicao = 0;

    public int Tamanho 
    {
        get
        {
            return _proximaPosicao;
        }
    }

    public ContaCorrente this[int indice]
    {
        get
        {
            return RecuperarContaCorrentePorIndice(indice);
        }
    }

    public ListaDeContasCorrentes(int tamanhoInicial = 5)
    {
        _itens = new ContaCorrente[tamanhoInicial];
    }

    public void Adicionar(ContaCorrente item)
    {
        Console.WriteLine($"Adicionando item na posição {_proximaPosicao}");
        VerificarCapacidade(_proximaPosicao + 1);
        _itens[_proximaPosicao] = item;
        _proximaPosicao++;
    }

    public void Remover(ContaCorrente item)
    {
        for (int i = 0; i < _itens.Length; i++)
        {
            if (_itens[i] == item)
            {
                _itens[i] = null;
                break;
            }
        }

        for (int i = 0; i < _itens.Length; i++)
        {
            if (_itens[i] == null)
            {
                if (i + 1 < _itens.Length)
                {
                    _itens[i] = _itens[i + 1];
                    _itens[i + 1] = null;
                }
            }
        }
    }

    public ContaCorrente ContaComMaiorSaldo()
    {
        ContaCorrente contaComSaldoMaior = null;
        double maiorSaldo = 0.0;

        foreach (ContaCorrente conta in _itens)
        {
            if (conta != null)
            {
                if (conta.Saldo > maiorSaldo)
                {
                    maiorSaldo = conta.Saldo;

                    contaComSaldoMaior = conta;
                }
            }
        }

        return contaComSaldoMaior;
    }

    private void VerificarCapacidade(int tamanhoNecessario)
    {
        if (_itens.Length >= tamanhoNecessario)
        {
            return;
        }
        Console.WriteLine("Aumentando a capacidade da lista!");
        ContaCorrente[] novoArray = new ContaCorrente[tamanhoNecessario];

        for (int i = 0; i < _itens.Length; i++)
        {
            novoArray[i] = _itens[i];
        }

        _itens = novoArray;
    }

    public void ListaDeContaCorrente()
    {
        foreach (var conta in _itens)
        {
            if (conta != null)
            {
                Console.WriteLine(conta.ToString());
            }
        }
    }

    public ContaCorrente RecuperarContaCorrentePorIndice(int indice)
    {
        if (indice < 0 || indice >= _proximaPosicao)
        {
            throw new ArgumentOutOfRangeException(nameof(indice));
        }

        return _itens[indice];
    }
}
