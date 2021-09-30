using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GetOcorrenceDelete
{
    class Program
    {
        static void Main(string[] args)
        {
            Query reposiroty = new();

            IList<ModelOcorrence> values = reposiroty.GetOcorrence().ToList();

            var newFile = File.Create("Response.csv");

            newFile.Dispose();

            File.AppendAllTextAsync(newFile.Name,
                $"CodDigitalizacaoDocumentos; " +
                $"CodDigitalizacao; " +
                $"NomeDocumento; " +
                $"CodCheckListDetalhe; " +
                $"CodGrupoDigitalizacao; " +
                $"ConfigCodUsuario; " +
                $"DataInclusao; " +
                $"NomeDocumentoDigitalizacao; " +
                $"Migrado; " +
                $"DocumentoInterno; " +
                $"CodDigitalizacaoDocumentosPai; " +
                $"Tentativas; " +
                $"NumeroPaginas; " +
                $"DataMigracao; " +
                $"DocumentoAtendido; " +
                $"Observacao; " +
                $"Robo; \n");

            foreach (var value in values)
                File.AppendAllTextAsync(newFile.Name,
                    $"{value.codDigitalDoc}; " +
                    $"{value.codDigital}; " +
                    $"{value.document}; " +
                    $"{value.codCheckList}; " +
                    $"{value.codGrupo}; " +
                    $"{value.configCodUser}; " +
                    $"{value.dateInclude}; " +
                    $"{value.aliasName}; " +
                    $"{value.migrated}; " +
                    $"{value.internalDocument}; " +
                    $"{value.codDigitalPai}; " +
                    $"{value.tryCounter}; " +
                    $"{value.pagesSize}; " +
                    $"{value.migratedDate}; " +
                    $"{value.attempt}; " +
                    $"{value.obs}; " +
                    $"{value.robot}; \n");

            Console.WriteLine("Processo Concluído");
        }
    }
}