﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Adopet.Console
{
    [DocComando($" adopet show <arquivo> comando que exibe no terminal o conteúdo do arquivo importado.")]
    internal class Show
    {
        public void MostrarArquivo(string caminhoDoArquivo)
        {
            LeitorDeArquivos leitor = new LeitorDeArquivos();
            var listaDePets = leitor.RealizaLeitura(caminhoDoArquivo);

            foreach (Pet pet in listaDePets)
            {
                System.Console.WriteLine(pet);
            }
        }
    }
}