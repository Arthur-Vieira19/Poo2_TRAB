using System;

namespace TrabPoo2
{
    public interface Transacao
    {
        bool Executar();

        Guid IdTransacao { get; }

        DateTime DataHora { get; }

        decimal Valor { get; }
    }
}