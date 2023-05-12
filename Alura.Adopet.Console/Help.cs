﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Adopet.Console
{
    [DocComando("adopet help < parametro > ous simplemente adopet help  " +
                    "comando que exibe informações de ajuda dos comandos.")]
    internal class Help
    {
        private Dictionary<string, object> comandos;

        public Help()
        {
            comandos = new()
            {
                { "import", new Import()},
                { "list", new List()},
                { "show", new Show()},
            };
        }
        private string RecuperaDocumentacao(object comando)
        {
            Type tipoComando = comando.GetType();
            var doc = tipoComando.GetCustomAttributes<DocComando>().FirstOrDefault();
            if (doc != null) return doc.Documentacao;
            return string.Empty;
        }
        public void Documentacao()
        {
            System.Console.WriteLine("Lista de comandos.");
            System.Console.WriteLine("Adopet (1.0) - Aplicativo de linha de comando (CLI).");
            System.Console.WriteLine("Realiza a importação em lote de um arquivos de pets.");
            System.Console.WriteLine("Comando possíveis: ");
            var doc = this.GetType().GetCustomAttributes<DocComando>().FirstOrDefault();
            System.Console.WriteLine(doc.Documentacao);
            foreach(var cmd in comandos)
            {
                System.Console.WriteLine(RecuperaDocumentacao(cmd.Value));
            }
            System.Console.WriteLine("Execute 'adopet.exe help [comando]' para obter mais informações sobre um comando." + "\n\n\n");
        }

        public void HelpDoComando(string comando)
        {
            var cmd = comandos[comando];
            if (cmd != null)
            {
                System.Console.WriteLine(RecuperaDocumentacao(cmd));
            }
        }
    }
}
