using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetOcorrenceDelete
{
    public class Query
    {
        private static readonly string _conn = "Password=cGQSn9vYV2;Persist Security Info=True;User ID=USER_ROBO_CRIM;Initial Catalog=CENIM;Data Source=BRADTSSRVSQL004";
    

        public IList<string> GetOcorrence() 
        {
            IList<string> ocorrences = new List<string>();

            using SqlConnection conn = new(_conn);

            var makedByAutomation = @"SELECT nr_processo FROM T_COE249_PROCESSOS 
                                        WHERE LEN(nr_processo) < 12 
                                        AND cd_processo 
                                            IN (SELECT DISTINCT(cd_processo) from T_COE249_CONTROLE_PROCESSO)";

            SqlCommand command = new(makedByAutomation, conn);

            var result = command.ExecuteReader();

            while (result.Read())
            {
                using SqlConnection newConn = new(_conn);

                var digitalDocs = @$"SELECT NomeDocumento, ConfigCodUsuario, Robo, CodDigitalizacaoDocumentos
                                    FROM KSU_PortalCredimob.dbo.DigitalizacaoDocumentos 
                                    WHERE DocumentoInterno = 1 AND CodDigitalizacao 
	                                    IN(SELECT CodDigitalizacao FROM  KSU_PortalCredimob.dbo.Digitalizacao
                                        WHERE CodUsuarioFechamentoLote = 44710 AND CodProposta = '{result[0]}')";

                SqlCommand SecundCommand = new(digitalDocs, newConn);

                var fResult = SecundCommand.ExecuteReader();

                while (fResult.Read())
                {
                    if (fResult[0].ToString().Contains("_202")
                        && int.Parse(fResult[1].ToString()) == 44710 && fResult[2].ToString().Trim() != null)
                        ocorrences.Add(fResult[3].ToString());

                    else if (fResult[0].ToString().Contains("_202")
                            && int.Parse(fResult[1].ToString()) == 1 && fResult[2].ToString().Trim() is null)
                        ocorrences.Add(fResult[3].ToString());

                }

                newConn.Dispose();
            }

            conn.Dispose();

            return ocorrences;
        }
    }
}