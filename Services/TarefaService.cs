using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Exceptions;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Services;

public class TarefaService
{
    private readonly OrganizadorContext organizadorContext;

    public TarefaService(OrganizadorContext organizadorContext)
    {
        this.organizadorContext = organizadorContext;
    }

    public Tarefa ObterPorId(int id)
    {
        return organizadorContext.Tarefas
            .FirstOrDefault(x => x.Id == id) 
            ?? throw new NotFoundException("Tarefa não encontrada");
    }

    public IEnumerable<Tarefa> ObterTodos()
    {
        return organizadorContext.Tarefas
            .ToList();
    }

    public Tarefa ObterPorTitulo(string titulo)
    {
        return organizadorContext.Tarefas
            .FirstOrDefault(x => x.Titulo == titulo)
            ?? throw new NotFoundException("Tarefa não encontrada");
    }

    public IEnumerable<Tarefa> ObterPorData(DateTime data)
    {
        return organizadorContext.Tarefas
            .Where(x => x.Data.Date == data.Date)
            .ToList();
    }

    public IEnumerable<Tarefa> ObterPorStatus(EnumStatusTarefa status)
    {
        return organizadorContext.Tarefas
            .Where(x => x.Status == status)
            .ToList();
    }

    public void Criar(Tarefa tarefa)
    {
        organizadorContext.Tarefas.Add(tarefa);
        organizadorContext.SaveChanges();
    }

    public void Atualizar(int id, Tarefa tarefa)
    {
        var tarefaBanco = organizadorContext.Tarefas.Find(id) ?? throw new NotFoundException("Tarefa não encontrada");
        
        if (tarefa.Data == DateTime.MinValue)
            throw new BadRequestException("A data da tarefa não pode ser vazia");

        tarefaBanco.Data = tarefa.Data;
        tarefaBanco.Descricao = tarefa.Descricao;
        tarefaBanco.Status = tarefa.Status;
        tarefaBanco.Titulo = tarefa.Titulo;

        organizadorContext.SaveChanges();
    }

    public void Deletar(int id)
    {
        var tarefaBanco = organizadorContext.Tarefas.Find(id);

        if (tarefaBanco == null)
            throw new NotFoundException("Tarefa não encontrada");

        organizadorContext.Remove(tarefaBanco);
        organizadorContext.SaveChanges();
    }
}
