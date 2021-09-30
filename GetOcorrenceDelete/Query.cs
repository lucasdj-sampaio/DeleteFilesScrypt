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
        private static readonly string _conn = "Password=ksu_owner;Persist Security Info=True;User ID=ksu_owner;Initial Catalog=CENIM;Data Source=BRADTSSRVSQL004";


        public IList<ModelOcorrence> GetOcorrence()
        {
            IList<ModelOcorrence> ocorrences = new List<ModelOcorrence>();

            using SqlConnection conn = new(_conn);

            var makedByAutomation = @"SELECT nr_processo FROM T_COE249_PROCESSOS 
                                        WHERE LEN(nr_processo) < 12 
                                        AND cd_processo 
                                            IN (SELECT DISTINCT(cd_processo) from T_COE249_CONTROLE_PROCESSO)";

            SqlCommand command = new(makedByAutomation, conn);
            conn.Open();
            var result = command.ExecuteReader();

            string concat = "";

            while (result.Read())
            {
                concat = concat == "" ? $"'{result[0]}'" : $"{concat}, '{result[0]}'";
            }

            result.Close();

            var digitalDocs = @$"SELECT * FROM KSU_PortalCredimob.dbo.DigitalizacaoDocumentos 
                                    WHERE DocumentoInterno = 1 AND CodDigitalizacao 
	                                    IN(SELECT CodDigitalizacao FROM KSU_PortalCredimob.dbo.Digitalizacao
                                        WHERE CodUsuarioFechamentoLote = 44710 AND 
                                            CodProposta IN (SELECT CodProposta FROM KSU_PortalCredimob.dbo.proposta 
                                                WHERE NumConsulta IN({concat})))";

            SqlCommand SecundCommand = new(digitalDocs, conn);

            var fResult = SecundCommand.ExecuteReader();

            while (fResult.Read())
            {
                if (fResult[2].ToString().Contains("_202")
                    && int.Parse(fResult[5].ToString()) == 44710 && fResult[16].ToString().Trim() != null)
                    ocorrences.Add(new ModelOcorrence()
                    {
                        codDigitalDoc = int.Parse(fResult[0].ToString()),
                        codDigital = int.Parse(fResult[1].ToString()),
                        document = fResult[2].ToString(),
                        codCheckList = fResult[3].ToString(),
                        codGrupo = fResult[4].ToString(),
                        configCodUser = int.Parse(fResult[5].ToString()),
                        dateInclude = fResult[6].ToString(),
                        aliasName = fResult[7].ToString(),
                        migrated = Convert.ToBoolean(fResult[8].ToString()),
                        internalDocument = Convert.ToBoolean(fResult[9].ToString()),
                        codDigitalPai = fResult[10].ToString(),
                        tryCounter = int.Parse(fResult[11].ToString()),
                        pagesSize = fResult[12].ToString(),
                        migratedDate = fResult[13].ToString(),
                        attempt = fResult[14].ToString(),
                        obs = fResult[15].ToString(),
                        robot = fResult[15].ToString()
                    });

                else if (fResult[2].ToString().Contains("_202")
                        && int.Parse(fResult[5].ToString()) == 1)
                    ocorrences.Add(new ModelOcorrence()
                    {
                        codDigitalDoc = int.Parse(fResult[0].ToString()),
                        codDigital = int.Parse(fResult[1].ToString()),
                        document = fResult[2].ToString(),
                        codCheckList = fResult[3].ToString(),
                        codGrupo = fResult[4].ToString(),
                        configCodUser = int.Parse(fResult[5].ToString()),
                        dateInclude = fResult[6].ToString(),
                        aliasName = fResult[7].ToString(),
                        migrated = Convert.ToBoolean(fResult[8].ToString()),
                        internalDocument = Convert.ToBoolean(fResult[9].ToString()),
                        codDigitalPai = fResult[10].ToString(),
                        tryCounter = int.Parse(fResult[11].ToString()),
                        pagesSize = fResult[12].ToString(),
                        migratedDate = fResult[13].ToString(),
                        attempt = fResult[14].ToString(),
                        obs = fResult[15].ToString(),
                        robot = fResult[15].ToString()
                    });
            }

            conn.Close();
            
            return ocorrences;
        }
    }
}