﻿using System;

namespace ProjetoBanco
{
    internal class Cliente
    {
        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public Endereco Endereco { get; private set; }

        public Cliente(string nome, string cpf, DateTime dataNascimento, Endereco endereco)
        {
            Nome = nome;
            Cpf = cpf;
            DataNascimento = dataNascimento;
            Endereco = endereco;
        }

        public bool MaiorDeIdade()
        {
            var nascimentoMinimo = DateTime.Now.AddYears(-18);
            var maiorDeIdade = DataNascimento <= nascimentoMinimo;
            return maiorDeIdade;
        }
    }
}
