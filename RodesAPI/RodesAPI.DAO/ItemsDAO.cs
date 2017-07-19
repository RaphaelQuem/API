using RodesAPI.ViewModel;
using System.Data;
using RodesAPI.Infra;

namespace RodesAPI.DAO
{
    public class ItemsDAO
    {
        private IDbConnection connection;
        public ItemsDAO(IDbConnection con)
        {
            connection = con;
        }
        public ItemsVM GetItemVMByItemID(string id)
        {
            try
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    IDataParameter pId = command.CreateParameter();
                    pId.ParameterName = "P_ID";
                    pId.Value = id;
                    pId.DbType = DbType.String;

                    command.CommandText += " SELECT		DISTINCT	C.CBARS CodBarra, ";
                    command.CommandText += " 						A.CPROS Referencia, ";
                    command.CommandText += " 						A.DPRO2S DescReferencia, ";
                    command.CommandText += " 						D.DGRUS Peca,";
                    command.CommandText += " 						CAST(ROUND(Cast(A.PRECODE as Numeric(10,2)),2) as varchar) PrecoVenda,";
                    command.CommandText += " 						A.MOECS DescricaoMoeda,";
                    command.CommandText += " 						E.DESCRICAOS DescricaoColecao,";
                    command.CommandText += " 						CAST(ROUND(Cast(A.PVENS as Numeric(10,2)),2) as varchar) PrecoEspecial,";
                    command.CommandText += " 						C.QTDS Saldo, ";
                    command.CommandText += " 						C.EMPS CodLoja, ";
                    command.CommandText += " 						C.EMPS DescricaoLoja, ";
                    command.CommandText += " 						CAST(ROUND(Cast(A.PCUSS as Numeric(10,3)),2) as varchar) CMV, ";
                    command.CommandText += " 						CAST(ROUND(Cast(A.VALORS as Numeric(10,2)),2) as varchar) ValorPeca, ";
                    command.CommandText += " 						C.CODTAMS MedidaPecas,";
                    command.CommandText += " 						'N' Brinde,";
                    command.CommandText += " 						CASE CONTAS";
                    command.CommandText += " 						    WHEN '' THEN 'Vendido'";
                    command.CommandText += " 						    ELSE 'Disponível'";
                    command.CommandText += " 						END Situacao";
                    command.CommandText += "  FROM	    			SIGCDPRO AS A WITH (NOLOCK)";
                    command.CommandText += " 						INNER  JOIN ";
                    command.CommandText += " 						SIGCDUNI AS B WITH (NOLOCK) ";
                    command.CommandText += " 							ON	A.CUNIS = B.CUNIS ";
                    command.CommandText += " 						INNER JOIN ";
                    command.CommandText += " 						SIGOPETQ AS C WITH (NOLOCK) ";
                    command.CommandText += " 							ON	A.CPROS = C.CPROS ";
                    command.CommandText += " 						INNER JOIN ";
                    command.CommandText += " 						SIGCDGRP AS D WITH (NOLOCK) ";
                    command.CommandText += " 							ON	A.CGRUS = D.CGRUS  ";
                    command.CommandText += " 						INNER JOIN ";
                    command.CommandText += " 						SIGCDPSG AS E WITH (NOLOCK) ";
                    command.CommandText += " 							ON	A.SGRUS = E.CODIGOS  ";
                    command.CommandText += "  WHERE		D.MTPRIMAS  != 2 ";
                    command.CommandText += "  AND		D.SERVICOS  != 1 ";
                    command.CommandText += "  AND       B.ETIQS     = 'S'";
                    command.CommandText += "  AND       C.CBARS     = @P_ID";

                    command.Parameters.Add(pId);


                    return command.ExecuteReader().ToSingleViewModel<ItemsVM>();


                }
            }
            catch
            {
                return null;
            }
        }
    }
}
