using System.Runtime.CompilerServices;

namespace cliqx.cupom.api.Validadores
{
    public class CupomValidador
    {
        public static void ValidaCupom(Cupom cupom)
        {
            if(cupom.TipoDesconto == TipoDesconto.VALOR)
            {
                if (cupom.ValorDesconto <= 0)
                    throw new Exception("O valor do cupom não foi informado!");            
            }

            if (cupom.TipoDesconto == TipoDesconto.PERCENTUAL)
            {
                if (cupom.PercentualDesconto <= 0)
                    throw new Exception("O percentual do cupom não foi informado!");
            }
        }

        public static void VerificaDataValidadeCupom(Cupom cupom)
        {
            if (cupom.DataValidade != DateTime.MinValue)
            {
                if(cupom.DataValidade.Date < DateTime.Now)
                    throw new Exception($"O cupom já passou do tempo de validade, data de validade: {cupom.DataValidade.ToString("dd/mm/YYYY")}");

                //if (cupom.DataValidade.Date == DateTime.Now.Date)
                //    throw new Exception($"O cupom está passando da validade hoje, data de validade: {cupom.DataValidade.ToString("dd/mm/YYYY")}");
            }
        }
    }
}
