using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TarefasBackEnd.Models;

namespace TarefasBackEnd.Repositories
{
  public interface ITarefaRepository
  {
    List<Tarefa> Read(Guid id);
    void Create(Tarefa tarefa);
    void Delete(Guid id);
    void Update(Guid id, Tarefa tarefa);

    Tarefa ReadOne(Guid id);
  }
  public class TarefaRepository : ITarefaRepository
  {

    private readonly DataContext _context;
    public TarefaRepository(DataContext context)
    {
      _context = context;        
    }

    public void Create(Tarefa tarefa)
    {


      tarefa.Id = Guid.NewGuid();

      _context.Tarefas.Add(tarefa);
      _context.SaveChanges();

    }

    public void Delete(Guid id)
    {
      var tarefa = _context.Tarefas.Find(id);
      _context.Entry(tarefa).State = EntityState.Deleted;
      _context.SaveChanges();
    }

    public List<Tarefa> Read(Guid id)
    {
      return _context.Tarefas.Where(tarefa => tarefa.UsuarioId == id).ToList();
    }

    public Tarefa ReadOne(Guid id)
    {
      return _context.Tarefas.Find(id);
    }

    public void Update(Guid id, Tarefa tarefa)
    {

      var _tarefa = ReadOne(id);
      

      _tarefa.Nome = tarefa.Nome;
      _tarefa.Concluido = tarefa.Concluido;



      _context.Entry(_tarefa).State = EntityState.Modified;
      _context.SaveChanges();
    }
  }
}