namespace cliqx.cupom.api.Services.Imp
{
    public class CalculoDescontoService : ICalculoDescontoService
    {
        public decimal CalculaValorDesconto(TipoDesconto tipoDesconto, decimal valor, decimal valorDesconto = 0, decimal percentualDesconto = 0)
        {
            decimal valorCalculado = 0;

            if (tipoDesconto == TipoDesconto.PERCENTUAL)
            {
                valorCalculado = valor - (valor * (percentualDesconto / 100));
            }
            else if (tipoDesconto == TipoDesconto.VALOR)
            {
                valorCalculado = valor - valorDesconto;
            }

            if (valorCalculado < 0)
                return (valorCalculado * -1);
            else
                return valorCalculado;
        }
    }
}
