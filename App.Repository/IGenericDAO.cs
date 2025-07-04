using System;
using System.Collections.Generic;

namespace SGED.Repository
{
    public interface IGenericDAO<T>
    {
        Task<Boolean> Salvar(T obj);
        Task<List<T>> Listar(String busca);
        Task<T> Carregar(int idObject);
        Task<Boolean> Excluir(int idObject);
    }
}
