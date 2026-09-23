using SGED.Domain.Common;
using SGED.Domain.Common.Results;

namespace SGED.Domain.Curriculo.Bimestres;

public class Bimestre : Entity
{
    public int? Numero { get; private set; }
    
    public DateTime? DataInicio { get; private set; }
    public DateTime? DataFim { get; private set; }
    public DateTime? DataLimiteLancamento { get; private set; }
    
    private Bimestre(){}

    private Bimestre(int numero, DateTime dataInicio, DateTime dataFim,  DateTime dataLimiteLancamento)
    {
        Numero = numero;
        DataInicio = dataInicio;
        DataFim = dataFim;
        DataLimiteLancamento = dataLimiteLancamento;
    }

    public static Result<Bimestre> Create(int? numero, DateTime? dataInicio, DateTime? dataFim, DateTime? dataLimiteLancamento)
    {
        if (numero == null)
            return Result<Bimestre>.Failure(new Error(
                "Bimestre.NumeroObrigatorio",
                "O número é obrigatório",
                ErrorType.Validation
            ));
        
        if(dataInicio == null)
            return Result<Bimestre>.Failure(new Error(
                "Bimestre.DataInicio",
                "A data inicial é obrigatório",
                ErrorType.Validation
            ));
        
        if(dataFim == null)
            return Result<Bimestre>.Failure(new Error(
                "Bimestre.DataFim",
                "A data final é obrigatório",
                ErrorType.Validation
            ));
        
        if(dataLimiteLancamento == null)
            return Result<Bimestre>.Failure(new Error(
                "Bimestre.DataLimiteLancamento",
                "A data limite de lançamento é obrigatório",
                ErrorType.Validation
            ));
        
        if(dataInicio > dataFim)
            return Result<Bimestre>.Failure(new Error(
                "Bimestre.DataInicioMaior",
                "A data inicial é maior que a data final",
                ErrorType.Validation
            ));
        
        if(dataInicio > dataLimiteLancamento)
            return Result<Bimestre>.Failure(new Error(
                "Bimestre.DataInicioMaiorDataLimiteLancamento",
                "A data inicial é maior que a data limite de lançamento",
                ErrorType.Validation
            ));

        return Result<Bimestre>.Success(new Bimestre(
            numero.Value,
            dataInicio.Value,
            dataFim.Value,
            dataLimiteLancamento.Value
            ));
    }

    public void UpdateBimestre(int numero, DateTime dataInicio, DateTime dataFim, DateTime dataLimiteLancamento)
    {
        Numero = numero;
        DataInicio = dataInicio;
        DataFim = dataFim;
        DataLimiteLancamento = dataLimiteLancamento;
        MarkAsUpdated();
    }
}