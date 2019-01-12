using ProjetoBanco.Enums;
using System;
using System.Collections.Generic;

namespace ProjetoBanco
{
    internal class Conta
    {
        public TipoConta TipoConta { get; private set; }
        public int Agencia { get; private set; }
        public int Numero { get; private set; }
        public decimal Saldo { get; private set; }
        public Banco Banco { get; private set; }
        public List<Transacao> Transacoes { get; private set; }

        public Conta(TipoConta tipoConta, int agencia, int numero, Banco banco)
        {
            TipoConta = tipoConta;
            Agencia = agencia;
            Numero = numero;
            Banco = banco;
            Transacoes = new List<Transacao>();
        }

        public void Sacar(decimal valor)
        {
            if (valor <= 0)
                throw new Exception("O valor solicitado é invalido.");
            if (valor > Saldo)
                throw new Exception("Saldo insuficiente para realizar o saque.");

            Debitar("Retirada", valor);

            Console.WriteLine("Saque realizado com sucesso.");
        }
        public void Depositar(decimal valor)
        {
            if (valor <= 0)
                throw new Exception("O valor é invalido.");

            Creditar("Deposito", valor);

            Console.WriteLine("Deposito realizado com sucesso.");

        }

        public void Transferir(int agencia, int numeroConta, decimal valor)
        {
            if (valor <= 0)
                throw new Exception("O valor é invalido.");
            if (valor > Saldo)
                throw new Exception("Saldo insuficiente para realizar a transferência.");

            var contaDestino = Banco.ObterConta(agencia, numeroConta);

            contaDestino.Creditar("Transferência", valor);

            Debitar("Transferência", valor);

            Console.WriteLine("Transferência realizada com sucesso.");

        }
        public void TirarExtrato()
        {

            if (Transacoes.Count > 0)
            {

                foreach (var transacao in Transacoes)
                {
                    var cor = transacao.TipoTransacao == TipoTransacao.Debito
                        ? ConsoleColor.Red : ConsoleColor.Green;
                    Console.ForegroundColor = cor;
                    var descricao = transacao.Descricao.PadRight(20, '-')
                        + transacao.Valor.ToString("C");
                    Console.WriteLine(descricao);
                }
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(string.Empty);
                var saldoDescricao = "Saldo".PadRight(20, '-')
                    + Saldo.ToString("C");
                Console.WriteLine(saldoDescricao);

            }
        }
        private void Creditar(string descricao, decimal valor)
        {
            var transacao = new Transacao(descricao, valor, TipoTransacao.Credito);
            Transacoes.Add(transacao);
            Saldo = Saldo + valor;
        }
        private void Debitar(string descricao, decimal valor)
        {
            var transacao = new Transacao(descricao, valor, TipoTransacao.Debito);
            Transacoes.Add(transacao);
            Saldo = Saldo - valor;
        }
    }
}
